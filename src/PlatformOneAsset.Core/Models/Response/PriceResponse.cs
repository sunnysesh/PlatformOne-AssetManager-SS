namespace PlatformOneAsset.Core.Models.Response;

public class PriceResponse
{
    public Guid Id { get; set; }
    public string Symbol { get; set; }
    public DateTime Date { get; set; }
    public decimal Value { get; set; }
    public string Source { get; set; }
    public DateTime TimeStamp { get; set; }
}