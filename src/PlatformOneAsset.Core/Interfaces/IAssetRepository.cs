using PlatformOneAsset.Core.Models.Entities;

namespace PlatformOneAsset.Core.Interfaces;

public interface IAssetRepository
{
    IEnumerable<Asset> GetAll();
    Asset GetByReference(string reference);
    Asset Add(Asset asset);
    Asset Update(Asset asset);
}