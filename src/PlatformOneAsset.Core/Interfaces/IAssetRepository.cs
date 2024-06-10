using PlatformOneAsset.Core.Models.Entities;

namespace PlatformOneAsset.Core.Interfaces;

public interface IAssetRepository : IRepository<Asset>
{
    Asset GetBySymbol(string symbol);
    bool AssetExists(string symbol);
}