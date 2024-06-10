using PlatformOneAsset.Core.Models.Entities;

namespace PlatformOneAsset.Core.Interfaces;

public interface IPriceRepository : IRepository<Price>
{
    IEnumerable<Price> GetPrices(string symbol, DateTime date, string source = null);
}