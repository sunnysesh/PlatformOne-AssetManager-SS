using FluentAssertions;
using PlatformOneAsset.Core.Interfaces;
using PlatformOneAsset.Core.Models.Entities;
using PlatformOneAsset.Core.Repositories;

namespace PlatformOneAsset.UnitTests.Repositories;

public class AssetRepositoryUnitTests
{
    private AssetRepository _assetRepository;
    
    [SetUp]
    public void Setup()
    {
        _assetRepository = new AssetRepository();
    }

    [Test]
    public void GetAll_WhenAssetsExist_ShouldReturnAllAssets()
    {
        //Arrange
        var expectedAssets = new List<Asset>()
        {
            new Asset()
            {
                Id = Guid.NewGuid(),
                Name = "Microsoft",
                Symbol = "MSFT",
                ISIN = "US5949181045"
            },
            new Asset()
            {
                Id = Guid.NewGuid(),
                Name = "Apple",
                Symbol = "APPL",
                ISIN = "US5949181046"
            }
        };
        _assetRepository.Add(expectedAssets[0]);
        _assetRepository.Add(expectedAssets[1]);

        //Act
        var result = _assetRepository.GetAll();

        //Assert
        result.Should().BeEquivalentTo(expectedAssets);
    }
    
    [Test]
    public async Task GetAll_WhenNoAssetsExist_ShouldReturnEmptyCollection()
    {
        //Arrange & act
        var result = _assetRepository.GetAll();
        
        //Assert
        result.Should().BeEmpty();
    }
    
    [Test]
    public async Task GetByReferenceAsync_WhenAssetExists_ShouldReturnCorrectAsset()
    {
        //Arrange
        string reference = "MSFT";
        var expectedAsset = new Asset()
        {
            Id = Guid.NewGuid(),
            Name = "Microsoft",
            Symbol = reference,
            ISIN = "US5949181045"
        };
        _assetRepository.Add(expectedAsset);
        
        //Act
        var result = _assetRepository.GetBySymbol(reference);

        //Assert
        result.Should().BeEquivalentTo(expectedAsset);
    }
    
    [Test]
    public async Task GetByReferenceAsync_WhenAssetDoesntExists_ShouldReturnNull()
    {
        //Arrange
        var expectedAsset = new Asset()
        {
            Id = Guid.NewGuid(),
            Name = "Microsoft",
            Symbol = "MSFT",
            ISIN = "US5949181045"
        };
        _assetRepository.Add(expectedAsset);
        
        //Act
        var result = _assetRepository.GetBySymbol("APPL");
        
        //Assert
        result.Should().BeNull();
    }
    
    [Test]
    public async Task Add_WhenAssetDoesntExist_ShouldAddToCollection()
    {
        //Arrange
        
        //Act
        
        //Assert
    }
    
    [Test]
    public async Task Add_WhenAssetExists_ShouldThrowException()
    {
        //Arrange
        
        //Act
        
        //Assert
    }

    [Test]
    public async Task UpdateAsync_WhenAssetExists_ShouldUpdateInCollection()
    {
        //Arrange
        
        //Act
        
        //Assert
    }
    
    [Test]
    public async Task UpdateAsync_WhenAssetDoesntExist_ShouldThrowException()
    {
        //Arrange
        
        //Act
        
        //Assert
    }
}