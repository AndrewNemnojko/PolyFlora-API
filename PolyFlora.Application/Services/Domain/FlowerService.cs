
using AutoMapper;
using PolyFlora.Application.DTOs.Common;
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
        
        public async Task<FlowerDetail?> CreateFlowerAsync(FlowerRequest request)
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
            var existFlower = await _flowerRepository.GetByIdAsync(id, CancellationToken.None);
            if (existFlower == null)
            {
                return null;
            }

            existFlower.Name = request.Name;
            existFlower.TName = _transliterationService.ToUrlFriendly(request.Name);
            existFlower.Price = request.Price;
            existFlower.Description = request.Description != null ? request.Description : String.Empty;
            existFlower.InStock = request.InStock;

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

        public async Task<PaginatedResult<T>> GetFlowersWithPaginationAsync<T>(int pageNumber, int pageSize, CancellationToken ct)
        {
            var flowers = await _flowerRepository
                .GetFlowersWithPaginationAsync(pageNumber, pageSize, ct);
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
