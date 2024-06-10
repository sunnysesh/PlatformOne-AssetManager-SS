namespace PlatformOneAsset.Core.Interfaces;

public interface IPriceService
{
    Task<IEnumerable<PriceResponse>> GetAssetPricesViaDateAsync(string symbol, DateTime date, string source = null);

    Task<PriceResponse> AddPriceAsync(CreatePriceRequest request);

    Task<PriceResponse> UpdatePriceAsync(UpdatePriceRequest request);
}