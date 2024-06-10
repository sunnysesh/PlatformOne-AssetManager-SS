using PlatformOneAsset.Core.Models.Request;
using PlatformOneAsset.Core.Models.Response;

namespace PlatformOneAsset.Core.Interfaces;

public interface IPriceService
{
    Task<IEnumerable<PriceResponse>> GetAssetPricesViaDateAsync(string symbol, string date, string source = null);

    Task<PriceResponse> AddPriceAsync(CreatePriceRequest request);

    Task<PriceResponse> UpdatePriceAsync(UpdatePriceRequest request);
}