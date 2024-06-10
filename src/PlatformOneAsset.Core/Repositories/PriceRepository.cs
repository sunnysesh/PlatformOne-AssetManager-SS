using PlatformOneAsset.Core.Interfaces;
using PlatformOneAsset.Core.Models.Entities;

namespace PlatformOneAsset.Core.Repositories;

public class PriceRepository : BaseRepository<Price>, IPriceRepository
{
    public IEnumerable<Price> GetPrices(string symbol, DateTime date, string source = null)
    {
        var result = _entities.Values.Where( i => i.Date == date && i.Symbol == symbol)
            .Where(i => source == null || i.Source == source);
        
        return result;
    }

    protected override string GetEntityId(Price entity)
        => entity.Id.ToString();
}