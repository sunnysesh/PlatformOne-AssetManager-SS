using FluentAssertions;
using PlatformOneAsset.Core.Interfaces;
using PlatformOneAsset.Core.Models.Entities;
using PlatformOneAsset.Core.Repositories;

namespace PlatformOneAsset.UnitTests.Repositories;

public class AssetRepositoryUnitTests
{
    private IAssetRepository _assetRepository;
    
    [SetUp]
    public void Setup()
    {
        _assetRepository = new AssetRepository();
    }

    [Test]
    public async Task GetAllAsync_WhenAssetsExist_ShouldReturnAllAssets()
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
        _assetRepository.AddAsync(expectedAssets[0]);
        _assetRepository.AddAsync(expectedAssets[1]);

        //Act
        var result = await _assetRepository.GetAllAsync();

        //Assert
        result.Should().BeEquivalentTo(expectedAssets);
    }
    
    [Test]
    public async Task GetAllAsync_WhenNoAssetsExist_ShouldReturnEmptyCollection()
    {
        //Arrange & act
        var result = await _assetRepository.GetAllAsync();
        
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
        _assetRepository.AddAsync(expectedAsset);
        
        //Act
        var result = await _assetRepository.GetByReferenceAsync(reference);

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
        _assetRepository.AddAsync(expectedAsset);
        
        //Act
        var result = await _assetRepository.GetByReferenceAsync("APPL");
        
        //Assert
        result.Should().BeNull();
    }
    
    [Test]
    public async Task AddAsync_WhenAssetDoesntExist_ShouldAddToCollection()
    {
        //Arrange
        
        //Act
        
        //Assert
    }
    
    [Test]
    public async Task AddAsync_WhenAssetExists_ShouldThrowException()
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