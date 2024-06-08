using AutoMapper;
using PlatformOneAsset.Core.Interfaces;
using PlatformOneAsset.Core.Models.Request;
using PlatformOneAsset.Core.Models.Response;

namespace PlatformOneAsset.Core.Services;

public class AssetService : IAssetService
{
    private readonly IAssetRepository _assetRepository;
    private readonly IMapper _mapper;

    public AssetService(IAssetRepository assetRepository, IMapper mapper)
    {
        _assetRepository = assetRepository;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<AssetResponse>> GetAllAssetsAsync()
    {
        var results = _assetRepository.GetAll();
        return _mapper.Map<IEnumerable<AssetResponse>>(results);
    }

    public async Task<AssetResponse> GetAssetViaSymbolAsync(string symbol)
    {
        var result = _assetRepository.GetBySymbol(symbol);
        return result is null
            ? null
            : _mapper.Map<AssetResponse>(result);
    }

    public async Task<AssetResponse> AddAssetAsync(CreateAssetRequest request)
    {
        throw new NotImplementedException();
    }

    public async Task<AssetResponse> UpdateAssetAsync(string symbol, UpdateAssetRequest request)
    {
        throw new NotImplementedException();
    }
}