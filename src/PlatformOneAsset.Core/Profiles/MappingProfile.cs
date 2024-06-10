using AutoMapper;
using PlatformOneAsset.Core.Models.Entities;
using PlatformOneAsset.Core.Models.Request;
using PlatformOneAsset.Core.Models.Response;

namespace PlatformOneAsset.Core.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateAssetRequest, Asset>()
            .ForMember(dest => dest.Id, opt => 
                opt.MapFrom(src => Guid.NewGuid()));
        CreateMap<UpdateAssetRequest, Asset>()
            .ForMember(dest => dest.Id, opt =>
                opt.Ignore());
        CreateMap<Asset, AssetResponse>();

        CreateMap<CreatePriceRequest, Price>()
            .ForMember(dest => dest.Id, opt => 
                opt.MapFrom(src => Guid.NewGuid()));;
        CreateMap<UpdatePriceRequest, Price>();
        CreateMap<Price, PriceResponse>();
    }
}