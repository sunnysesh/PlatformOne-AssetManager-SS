using AutoMapper;
using PlatformOneAsset.Core.Exceptions;
using PlatformOneAsset.Core.Interfaces;
using PlatformOneAsset.Core.Models.Entities;
using PlatformOneAsset.Core.Models.Request;
using PlatformOneAsset.Core.Models.Response;

namespace PlatformOneAsset.Core.Services;

public class PriceService : IPriceService
{
    private readonly IPriceRepository _priceRepository;
    private readonly IAssetRepository _assetRepository;
    private readonly IMapper _mapper;
    
    public PriceService(IPriceRepository priceRepository, IAssetRepository assetRepository, IMapper mapper)
    {
        _priceRepository = priceRepository;
        _assetRepository = assetRepository;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<PriceResponse>> GetAssetPricesViaDateAsync(string symbol, string date, string source = null)
    {
        if (!DateTime.TryParse(date, out var parsedDate))
            throw new ArgumentException("Error: Date is invalid");

        var results = _priceRepository.GetPrices(symbol, parsedDate, source);
        return _mapper.Map<IEnumerable<PriceResponse>>(results);
    }

    public async Task<PriceResponse> AddPriceAsync(CreatePriceRequest request)
    {
        if (!_assetRepository.AssetExists(request.Symbol))
            throw new AssetNotFoundException($"Error: Asset not found for {request.Symbol}");

        var price = _mapper.Map<Price>(request);
        price.UpdateTimeStamp(DateTime.UtcNow);

        var result = _priceRepository.Add(price);
        return _mapper.Map<PriceResponse>(result);
    }

    public Task<PriceResponse> UpdatePriceAsync(UpdatePriceRequest request)
    {
        throw new NotImplementedException();
    }
}