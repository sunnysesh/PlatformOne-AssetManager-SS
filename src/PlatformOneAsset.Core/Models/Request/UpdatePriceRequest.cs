namespace PlatformOneAsset.Core.Models.Request;

public record UpdatePriceRequest(Guid Id, string Symbol, DateTime Date, decimal Value, string? Source = null);