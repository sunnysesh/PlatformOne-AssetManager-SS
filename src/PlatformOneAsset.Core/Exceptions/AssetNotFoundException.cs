namespace PlatformOneAsset.Core.Exceptions;

public class AssetNotFoundException : Exception
{
    public AssetNotFoundException(string message) : base(message)
    {
        
    }
}