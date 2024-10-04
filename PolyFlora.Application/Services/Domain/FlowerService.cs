
using AutoMapper;
using PolyFlora.Application.DTOs.Common;
using PolyFlora.Application.DTOs.Flower;
using PolyFlora.Application.Interfaces.Repositories;
using PolyFlora.Application.Interfaces.Utilites;
using PolyFlora.Application.Services.Utilites;
using PolyFlora.Core.Enums;
using PolyFlora.Core.Interfaces;
using PolyFlora.Core.Models;

namespace PolyFlora.Application.Services.Domain
{
    public class FlowerService
    {
        private readonly ICacheService _cacheService;
        private readonly IFlowerRepository _flowerRepository;
        private readonly IMapper _mapper;
        private readonly TransliterationService _transliterationService;
        private readonly IImageService _imageService;
        public FlowerService(
            ICacheService cacheService,
            IFlowerRepository flowerRepository,
            IMapper mapper,
            TransliterationService transliterationService,
            IImageService imageService)
        {
            _cacheService = cacheService;
            _flowerRepository = flowerRepository;
            _mapper = mapper;
            _transliterationService = transliterationService;
            _imageService = imageService;
        }

        public async Task<FlowerDetail?> GetFlowerByIdAsync(Guid id, string langCode, CancellationToken ct, bool cache = false)
        {
            if (cache)
            {
                var cachedModel = await _cacheService
                    .GetAsync<FlowerDetail>($"flower-id-{langCode}-{id}");
                if (cachedModel != null)
                {
                    return cachedModel;
                }
            }
            if (!Enum.TryParse(typeof(LanguageCode), langCode.ToUpper(), out var languageCode))
            {
                throw new ArgumentException
                    ($"Invalid language code: {langCode}. " +
                    $"Allowed values are: {string.Join(", ", Enum.GetNames(typeof(LanguageCode)))}");
            }
            var dbModel = await _flowerRepository
                .GetByIdWithCultureAsync(id, (LanguageCode)languageCode, ct);

            var mapModel = _mapper.Map<FlowerDetail>(dbModel);
            if (dbModel != null && cache)
            {
                await _cacheService.SetAsync($"flower-id-{langCode}-{id}", dbModel);
            }
            return mapModel;
        }

        public async Task<FlowerDetail?> GetFlowerByNameAsync(string tname, string langCode, CancellationToken ct, bool cache = false)
        {
            if (cache)
            {
                var cachedModel = await _cacheService
                    .GetAsync<FlowerDetail>($"flower-tname-{langCode}-{tname}");
                if (cachedModel != null)
                {
                    return cachedModel;
                }
            }
            if (!Enum.TryParse(typeof(LanguageCode), langCode.ToUpper(), out var languageCode))
            {
                throw new ArgumentException
                    ($"Invalid language code: {langCode}. " +
                    $"Allowed values are: {string.Join(", ", Enum.GetNames(typeof(LanguageCode)))}");
            }
            var dbModel = await _flowerRepository
                .GetByNameWithCultureAsync(tname, (LanguageCode)languageCode, ct);

            var mapModel = _mapper.Map<FlowerDetail>(dbModel);

            if (dbModel != null && cache)
            {
                await _cacheService.SetAsync($"flower-tname-{langCode}-{tname}", dbModel);
            }
            return mapModel;
        }
        
        public async Task<FlowerDetail?> CreateFlowerAsync(FlowerRequest request)
        {
            var flower = new Flower();

            flower.Price = request.Price;
            flower.InStock = request.InStock;
            flower.TName = _transliterationService
                .ToUrlFriendly(request.CultureDetails.First(x => x.TargCulture).Name);

            foreach (var culture in request.CultureDetails)
            {
                if (!Enum.TryParse(typeof(LanguageCode), culture.LangCode.ToUpper(), out var languageCode))
                {
                    throw new ArgumentException
                        ($"Invalid language code: {culture.LangCode}. " +
                        $"Allowed values are: {string.Join(", ", Enum.GetNames(typeof(LanguageCode)))}");
                }
                
                var newCulture = new FlowerCulture
                {                  
                    LanguageCode = (LanguageCode)languageCode,
                    TargCulture = culture.TargCulture,
                    Name = culture.Name,
                    Description = culture.Description
                };

                flower.CultureDetails.Add(newCulture);               
            }
                                 
            

            if (request.ImageFile != null)
            {
                var imageUploadResult = await _imageService
                    .UploadAsync(request.ImageFile);
                if (imageUploadResult.Success)
                {                  
                    flower.Image = imageUploadResult.Image;
                }
            }

            if (request.ParentId.HasValue)
            {
                var parent = await _flowerRepository
                    .GetByIdAsync(request.ParentId.Value, CancellationToken.None);

                if (parent != null)
                    flower.FlowerParent = parent;
            }

            if (request.ChildrensIds?.Any() == true)
            {
                var childrens = await _flowerRepository
                    .GetFlowersByIdsAsync(request.ChildrensIds);
                flower.FlowerChildrens = childrens.ToList();
            }

            var result = await _flowerRepository.AddAsync(flower);
            var mapflower = _mapper.Map<FlowerDetail>(result);
            return mapflower;
        }

        public async Task<FlowerDetail?> ChangeFlowerAsync(Guid id, FlowerRequest request)
        {
            var existFlower = await _flowerRepository
                .GetByIdAsync(id, CancellationToken.None);

            if (existFlower == null)
            {
                return null;
            }

            existFlower.TName = _transliterationService
                .ToUrlFriendly(request.CultureDetails.First(x => x.TargCulture).Name);
                       
            existFlower.Price = request.Price;           
            existFlower.InStock = request.InStock;

            foreach (var culture in request.CultureDetails)
            {
                if (!Enum.TryParse(typeof(LanguageCode), culture.LangCode.ToUpper(), out var languageCode))
                {
                    throw new ArgumentException
                        ($"Invalid language code: {culture.LangCode}. " +
                        $"Allowed values are: {string.Join(", ", Enum.GetNames(typeof(LanguageCode)))}");
                }
                var existingCulture = existFlower.CultureDetails
                    .FirstOrDefault(c => c.LanguageCode.ToString() == culture.LangCode);

                if (existingCulture != null)
                {
                    existingCulture.Name = culture.Name;
                    existingCulture.TargCulture = culture.TargCulture;
                    existingCulture.Description = culture.Description;
                }
                else
                {
                    var newCulture = new FlowerCulture
                    {
                        LanguageCode = (LanguageCode)languageCode,
                        TargCulture = culture.TargCulture,
                        Name = culture.Name,
                        Description = culture.Description
                    };
                    existFlower.CultureDetails.Add(newCulture);
                }
            }

            if (request.ImageFile != null)
            {
                var imageUploadResult = await _imageService
                    .UploadAsync(request.ImageFile);
                if (imageUploadResult.Success)
                {
                    await _imageService.DeleteAsync(existFlower.Image.FileName);
                    existFlower.Image = imageUploadResult.Image;
                }
            }

            if (request.ParentId.HasValue)
            {
                var parent = await _flowerRepository
                    .GetByIdAsync(request.ParentId.Value, CancellationToken.None);

                if (parent != null)
                    existFlower.FlowerParent = parent;
            }

            if (request.ChildrensIds?.Any() == true)
            {
                var childrens = await _flowerRepository
                    .GetFlowersByIdsAsync(request.ChildrensIds);
                existFlower.FlowerChildrens = childrens.ToList();
            }

            var result = await _flowerRepository.UpdateAsync(existFlower);
            var mapflower = _mapper.Map<FlowerDetail>(existFlower);
            return mapflower;
        }

        public async Task<IEnumerable<T>> GetAllFlowersAsync<T>(CancellationToken ct)
        {
            var flowers = await _flowerRepository.GetAllAsync(ct);
            var mapData = _mapper.Map<IEnumerable<T>>(flowers);
          
            return mapData;
        }

        public async Task<PaginatedResult<T>> GetFlowersWithPaginationAsync<T>(int pageNumber, int pageSize, string langCode, CancellationToken ct)
        {
            if (!Enum.TryParse(typeof(LanguageCode), langCode.ToUpper(), out var languageCode))
            {
                throw new ArgumentException
                    ($"Invalid language code: {langCode}. " +
                    $"Allowed values are: {string.Join(", ", Enum.GetNames(typeof(LanguageCode)))}");
            }
            var flowers = await _flowerRepository
                .GetFlowersWithPaginationAsync(pageNumber, pageSize, (LanguageCode)languageCode, ct);

            var totalFlowersCount = await _flowerRepository.GetTotalCountAsync(ct);

            var mapFlowers = _mapper.Map<IEnumerable<T>>(flowers);

            return new PaginatedResult<T>
            {
                Items = mapFlowers.ToList(),
                TotalCount = totalFlowersCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            }; 
        }
    }
}
