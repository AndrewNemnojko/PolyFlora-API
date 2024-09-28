
using AutoMapper;
using PolyFlora.Application.DTOs.Flower;
using PolyFlora.Application.Interfaces.Repositories;
using PolyFlora.Application.Interfaces.Utilites;
using PolyFlora.Application.Services.Utilites;
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

        public async Task<FlowerDetail?> GetFlowerByIdAsync(Guid id, CancellationToken ct, bool cache = false)
        {
            if (cache)
            {
                var cachedModel = await _cacheService
                    .GetAsync<FlowerDetail>($"flower-id-{id}");
                if (cachedModel != null)
                {
                    return cachedModel;
                }
            }
            var dbModel = await _flowerRepository.GetByIdAsync(id, ct);
            var mapModel = _mapper.Map<FlowerDetail>(dbModel);
            if (dbModel != null && cache)
            {
                await _cacheService.SetAsync($"flower-id-{id}", dbModel);
            }
            return mapModel;
        }

        public async Task<FlowerDetail?> GetFlowerByNameAsync(string tname, CancellationToken ct, bool cache = false)
        {
            if (cache)
            {
                var cachedModel = await _cacheService
                    .GetAsync<FlowerDetail>($"flower-tname-{tname}");
                if (cachedModel != null)
                {
                    return cachedModel;
                }
            }
            var dbModel = await _flowerRepository.GetByNameAsync(tname, ct);
            var mapModel = _mapper.Map<FlowerDetail>(dbModel);
            if (dbModel != null && cache)
            {
                await _cacheService.SetAsync($"flower-tname-{tname}", dbModel);
            }
            return mapModel;
        }
        
        public async Task<FlowerDetail?> CreateFlowerAsync(FlowerCreateRequest request)
        {            
            var flower = _mapper.Map<Flower>(request);           
            flower.TName = _transliterationService.ToUrlFriendly(request.Name);

            if(request.ImageFile != null)
            {
                var imageUploadResult = await _imageService
                    .UploadAsync(request.ImageFile);
                if (imageUploadResult.Success)
                {                  
                    flower.Image = imageUploadResult.Image;
                }
            }

            if (request.ParentId != null)
            {
                var parent = await _flowerRepository
                    .GetByIdAsync(request.ParentId.Value, CancellationToken.None);

                if (parent != null)
                    flower.FlowerParent = parent;
            }
            if (request.ChildrensIds != null)
            {
                var childrens = await _flowerRepository
                    .GetFlowersByIdsAsync(request.ChildrensIds);
                flower.FlowerChildrens = childrens.ToList();
            }
            var result = await _flowerRepository.AddAsync(flower);
            var mapflower = _mapper.Map<FlowerDetail>(result);
            return mapflower;
        }

        public async Task<ICollection<T>> GetFlowersAsync<T>(uint page, CancellationToken ct)
        {
            var data = await _flowerRepository.GetAllAsync(ct);
            var mapData = _mapper.Map<ICollection<T>>(data);
            
            return mapData;
        }
    }
}
