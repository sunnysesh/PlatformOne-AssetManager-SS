using PlatformOneAsset.Core.Interfaces;
using PlatformOneAsset.Core.Models.Request;
using PlatformOneAsset.Core.Models.Response;

namespace PlatformOneAsset.Core.Services;

public class AssetService : IAssetService
{
    public Task<IEnumerable<AssetResponse>> GetAllAssetsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<AssetResponse> GetAssetViaSymbolAsync(string symbol)
    {
        throw new NotImplementedException();
    }

    public Task<AssetResponse> AddAssetAsync(CreateAssetRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<AssetResponse> UpdateAssetAsync(string symbol, UpdateAssetRequest request)
    {
        throw new NotImplementedException();
    }
}