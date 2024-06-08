using PlatformOneAsset.Core.Interfaces;
using PlatformOneAsset.Core.Models.Entities;

namespace PlatformOneAsset.Core.Repositories;

public class AssetRepository : BaseRepository<Asset>, IAssetRepository
{
    public AssetRepository(List<Asset> entities) : base(entities)
    {
    }
}