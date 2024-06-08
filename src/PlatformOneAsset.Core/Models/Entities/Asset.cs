namespace PlatformOneAsset.Core.Models.Entities;

public class Asset
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Symbol { get; set; }
    public string ISIN { get; set; }
}