using PlatformOneAsset.Core.Models.Request;
using PlatformOneAsset.Core.Models.Response;

namespace PlatformOneAsset.Core.Interfaces;

public interface IAssetService
{
    Task<IEnumerable<AssetResponse>> GetAllAssetsAsync();

    Task<AssetResponse> GetAssetViaSymbolAsync(string symbol);

    Task<AssetResponse> AddAssetAsync(CreateAssetRequest request);

    Task<AssetResponse> UpdateAssetAsync(string symbol, UpdateAssetRequest request);
}