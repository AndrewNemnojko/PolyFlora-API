
using AutoMapper;
using PolyFlora.Application.DTOs.Flower;
using PolyFlora.Core.Models;

namespace PolyFlora.Application.MappingProfiles
{
    public class FlowerProfile : Profile
    {
        public FlowerProfile()
        {            
            CreateMap<FlowerRequest, Flower>();

            CreateMap<Flower, FlowerDetail>().MaxDepth(2)
                .ForMember(x => x.PicturePath, opt => opt.MapFrom(src => src.Image.FileUrl))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src =>
                    src.CultureDetails.First().Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src =>
                    src.CultureDetails.First().Description));

            CreateMap<Flower, FlowerSummary>().MaxDepth(2)
                .ForMember(x => x.PicturePath, 
                    opt => opt.MapFrom(src => src.Image.FileUrl))
                .ForMember(dest => dest.Name, 
                    opt => opt.MapFrom(src => src.CultureDetails.First().Name));
        }
    }
}
