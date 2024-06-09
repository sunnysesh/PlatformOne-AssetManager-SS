namespace PlatformOneAsset.Core.Models.Entities;

public class Asset
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
    public string Symbol { get; set; }
    public string ISIN { get; set; }
}