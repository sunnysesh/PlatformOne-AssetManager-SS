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
        string symbol = "MSFT";
        var expectedAsset = new Asset()
        {
            Id = Guid.NewGuid(),
            Name = "Microsoft",
            Symbol = symbol,
            ISIN = "US5949181045"
        };
        
        //Act
        _assetRepository.Add(expectedAsset);

        //Assert
        _assetRepository.GetBySymbol(symbol)
            .Should().BeEquivalentTo(expectedAsset);
    }
    
    [Test]
    public async Task Add_WhenAssetAlreadyExists_ShouldThrowException()
    {
        //Arrange
        string symbol = "MSFT";
        var expectedAsset = new Asset()
        {
            Id = Guid.NewGuid(),
            Name = "Microsoft",
            Symbol = symbol,
            ISIN = "US5949181045"
        };
        
        _assetRepository.Add(expectedAsset);
        
        //Act & assert
        _assetRepository.Invoking(i => i.Add(expectedAsset))
            .Should().Throw<InvalidOperationException>();
    }

    [Test]
    public async Task UpdateAsync_WhenAssetExists_ShouldUpdateInCollection()
    {
        //Arrange
        string symbol = "MSFT";
        var existingAsset = new Asset()
        {
            Id = Guid.NewGuid(),
            Name = "Microsoft",
            Symbol = symbol,
            ISIN = "US5949181045"
        };
        _assetRepository.Add(existingAsset);
        
        var updatedAsset =  new Asset()
        {
            Id = Guid.NewGuid(),
            Name = "Microsoft",
            Symbol = symbol,
            ISIN = "US5949181046"
        };

        //Act
        _assetRepository.Update(updatedAsset);

        //Assert
        _assetRepository.GetBySymbol(symbol)
            .Should().BeEquivalentTo(updatedAsset);
    }
    
    [Test]
    public async Task UpdateAsync_WhenAssetDoesntExist_ShouldThrowException()
    {
        //Arrange
        string symbol = "MSFT";
        var expectedAsset = new Asset()
        {
            Id = Guid.NewGuid(),
            Name = "Microsoft",
            Symbol = symbol,
            ISIN = "US5949181045"
        };
        
        //Act & assert
        _assetRepository.Invoking(i => i.Update(expectedAsset))
            .Should().Throw<InvalidOperationException>();
    }
}