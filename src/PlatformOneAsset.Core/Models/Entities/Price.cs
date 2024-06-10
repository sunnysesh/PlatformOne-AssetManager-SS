namespace PlatformOneAsset.Core.Models.Entities;

public class Price
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Symbol { get; set; }
    public DateTime Date { get; set; }
    public decimal Value { get; set; }
    public string Source { get; set; }
    public DateTime TimeStamp { get; set; }

    public void UpdateTimeStamp(DateTime timestamp)
        => TimeStamp = timestamp;
}