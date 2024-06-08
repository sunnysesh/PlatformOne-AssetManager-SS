using AutoMapper;
using PlatformOneAsset.Core.Models.Entities;
using PlatformOneAsset.Core.Models.Response;

namespace PlatformOneAsset.Core.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Asset, AssetResponse>();
    }
}