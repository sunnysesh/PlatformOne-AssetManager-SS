namespace PlatformOneAsset.Core.Models.Entities;

public class Price
{
    public string Symbol { get; set; }
    public DateTime Date { get; set; }
    public decimal Value { get; set; }
    public DateTime TimeStamp { get; set; }

    public void UpdateTimeStamp(DateTime timestamp)
        => TimeStamp = timestamp;
}