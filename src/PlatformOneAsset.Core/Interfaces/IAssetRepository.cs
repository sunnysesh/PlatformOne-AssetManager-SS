using PlatformOneAsset.Core.Models.Entities;

namespace PlatformOneAsset.Core.Interfaces;

public interface IAssetRepository
{
    Task<IEnumerable<Asset>> GetAllAsync();
    Task<Asset> GetByReferenceAsync(string reference);
    Task<Asset> AddAsync(Asset asset);
    Task<Asset> UpdateAsync(Asset asset);
}