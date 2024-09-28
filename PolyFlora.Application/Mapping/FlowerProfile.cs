
using AutoMapper;
using PolyFlora.Application.DTOs.Flower;
using PolyFlora.Core.Models;

namespace PolyFlora.Application.MappingProfiles
{
    public class FlowerProfile : Profile
    {
        public FlowerProfile()
        {            
            CreateMap<FlowerCreateRequest, Flower>();

            CreateMap<Flower, FlowerDetail>().MaxDepth(2)
                .ForMember(x => x.PicturePath, opt => opt.MapFrom(src => src.Image.FileUrl));

            CreateMap<Flower, FlowerSummary>().MaxDepth(2);
        }
    }
}
