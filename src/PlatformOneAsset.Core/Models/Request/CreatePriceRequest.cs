namespace PlatformOneAsset.Core.Models.Request;

public record CreatePriceRequest(string Symbol, DateTime Date, decimal Value, string Source);