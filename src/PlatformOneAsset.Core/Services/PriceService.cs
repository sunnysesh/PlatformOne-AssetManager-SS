using AutoMapper;
using PlatformOneAsset.Core.Interfaces;
using PlatformOneAsset.Core.Models.Request;
using PlatformOneAsset.Core.Models.Response;

namespace PlatformOneAsset.Core.Services;

public class PriceService : IPriceService
{
    private readonly IPriceRepository _priceRepository;
    private readonly IMapper _mapper;
    
    public PriceService(IPriceRepository priceRepository, IMapper mapper)
    {
        _priceRepository = priceRepository;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<PriceResponse>> GetAssetPricesViaDateAsync(string symbol, string date, string source = null)
    {
        if (!DateTime.TryParse(date, out var parsedDate))
            throw new ArgumentException("Error: Date is invalid");

        var results = _priceRepository.GetPrices(symbol, parsedDate, source);
        return _mapper.Map<IEnumerable<PriceResponse>>(results);
    }

    public Task<PriceResponse> AddPriceAsync(CreatePriceRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<PriceResponse> UpdatePriceAsync(UpdatePriceRequest request)
    {
        throw new NotImplementedException();
    }
}