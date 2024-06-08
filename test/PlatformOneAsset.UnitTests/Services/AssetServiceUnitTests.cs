﻿using NSubstitute;
using FluentAssertions;
using NSubstitute.ReturnsExtensions;
using PlatformOneAsset.Core.Interfaces;
using PlatformOneAsset.Core.Models.Entities;
using PlatformOneAsset.Core.Services;

namespace PlatformOneAsset.UnitTests.Services;

public class AssetServiceUnitTests
{
    private AssetService _assetService;
    private IAssetRepository _mockAssetRepository;
    
    [SetUp]
    public void Setup()
    {
        _mockAssetRepository = Substitute.For<IAssetRepository>();
        _assetService = new AssetService(_mockAssetRepository);
    }

    [Test]
    public async Task GetAllAssets_WhenAssetsExist_ShouldReturnCorrectAssets()
    {
        //Arrange
        Guid expectedGuid = Guid.NewGuid();
        var expectedAssets = new List<Asset>()
        {
            new Asset()
            {
                Id = expectedGuid,
                Name = "Microsoft",
                Symbol = "MSFT",
                ISIN = "US5949181045"
            }
        };
        _mockAssetRepository.GetAllAsync()
            .Returns(expectedAssets);
        
        //Act
        var result = await _assetService.GetAllAssetsAsync();

        //Assert
        result.First().Id.Should().Be(expectedGuid);
    }
    
    [Test]
    public async Task GetAllAssets_WhenAssetsDontExist_ShouldReturnEmpty()
    {
        //Arrange
        _mockAssetRepository.GetAllAsync()
            .Returns(new List<Asset>());
        
        //Act
        var result = await _assetService.GetAllAssetsAsync();

        //Assert
        result.Should().BeEmpty();
    }

    [Test]
    public async Task GetAssetViaSymbol_WhenAssetExists_ShouldReturnCorrectAsset()
    {
        //Arrange
        Guid expectedGuid = Guid.NewGuid();
        string expectedSymbol = "MSFT";
        
        var expectedAsset = new Asset()
        {
            Id = expectedGuid,
            Name = "Microsoft",
            Symbol = "MSFT",
            ISIN = "US5949181045"
        };
        _mockAssetRepository.GetByReferenceAsync(Arg.Any<string>())
            .Returns(expectedAsset);
        
        //Act
        var result = await _assetService.GetAssetViaSymbolAsync(expectedSymbol);

        //Assert
        result.Id.Should().Be(expectedGuid);
        result.Symbol.Should().Be(expectedSymbol);
    }
    
    [Test]
    public async Task GetAssetViaSymbol_WhenAssetDoesntExists_ShouldReturnNull()
    {
        //Arrange
        _mockAssetRepository.GetByReferenceAsync(Arg.Any<string>())
            .ReturnsNull();
        
        //Act
        var result = await _assetService.GetAssetViaSymbolAsync("MSFT");

        //Assert
        result.Should().BeNull();
    }
    
    [Test]
    public void AddAsset_WhenAssetDoesntExist_ShouldAddToRepo()
    {
        //Arrange
        
        //Act
        
        //Assert
    }
    
    [Test]
    public void AddAsset_WhenAssetAlreadyExists_ShouldThrowException()
    {
        //Arrange
        
        //Act
        
        //Assert
    }
    
    [Test]
    public void UpdateAsset_WhenAssetAlreadyExists_ShouldUpdateRepo()
    {
        //Arrange
        
        //Act
        
        //Assert
    }
    
    [Test]
    public void UpdateAsset_WhenAssetDoesntExists_ShouldThrowException()
    {
        //Arrange
        
        //Act
        
        //Assert
    }
}