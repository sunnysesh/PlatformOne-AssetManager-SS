﻿using AutoMapper;
using NSubstitute;
using FluentAssertions;
using NSubstitute.ExceptionExtensions;
using NSubstitute.ReturnsExtensions;
using PlatformOneAsset.Core.Exceptions;
using PlatformOneAsset.Core.Interfaces;
using PlatformOneAsset.Core.Models.Entities;
using PlatformOneAsset.Core.Models.Request;
using PlatformOneAsset.Core.Models.Response;
using PlatformOneAsset.Core.Profiles;
using PlatformOneAsset.Core.Services;

namespace PlatformOneAsset.UnitTests.Services;

public class AssetServiceUnitTests
{
    private AssetService _assetService;
    private IAssetRepository _mockAssetRepository;
    private IMapper _mapper;
    
    [SetUp]
    public void Setup()
    {
        _mockAssetRepository = Substitute.For<IAssetRepository>();

        var cfg = new MapperConfiguration(i =>
        {
            i.AddProfile<MappingProfile>();
        });
        _mapper = cfg.CreateMapper();
        
        _assetService = new AssetService(_mockAssetRepository, _mapper);
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
                Isin = "US5949181045"
            }
        };
        _mockAssetRepository.GetAll()
            .Returns(expectedAssets);
        
        //Act
        var result = await _assetService.GetAllAssetsAsync();

        //Assert
        result.Should().HaveCount(1);
        result.First().Id.Should().Be(expectedGuid);
    }
    
    [Test]
    public async Task GetAllAssets_WhenAssetsDontExist_ShouldReturnEmpty()
    {
        //Arrange
        _mockAssetRepository.GetAll()
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
            Isin = "US5949181045"
        };
        _mockAssetRepository.GetBySymbol(Arg.Any<string>())
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
        _mockAssetRepository.GetBySymbol(Arg.Any<string>())
            .ReturnsNull();
        
        //Act
        var result = await _assetService.GetAssetViaSymbolAsync("MSFT");

        //Assert
        result.Should().BeNull();
    }
    
    [Test]
    public async Task AddAsset_WhenAssetDoesntExist_ShouldAddToRepo()
    {
        //Arrange
        var assetRequest = new CreateAssetRequest("Microsoft", "MSFT", "US5949181045");
        var mockAsset = new Asset()
        {
            Name = "Microsoft",
            Symbol = "MSFT",
            Isin = "US5949181045"
        };
        
        _mockAssetRepository.Add(Arg.Any<Asset>())
            .Returns(mockAsset);
        
        //Act
        var result = await _assetService.AddAssetAsync(assetRequest);

        //Assert
        result.Name.Should().Be(assetRequest.Name);
        result.Symbol.Should().Be(assetRequest.Symbol);
        result.Isin.Should().Be(assetRequest.Isin);
    }
    
    [Test]
    public void AddAsset_WhenAssetAlreadyExists_ShouldThrowException()
    {
        //Arrange
        var assetRequest = new CreateAssetRequest("Microsoft", "MSFT", "US5949181045");
        _mockAssetRepository.Add(Arg.Any<Asset>())
            .Throws(new EntityAlreadyExistsException("Error: Entity already exists."));

        //Act & assert
        _assetService.Invoking(i => i.AddAssetAsync(assetRequest))
            .Should().ThrowAsync<EntityAlreadyExistsException>();
    }
    
    [Test]
    public async Task UpdateAsset_WhenAssetAlreadyExists_ShouldUpdateRepo()
    {
        //Arrange
        string symbol = "MSFT";
        var updatedAssetRequest = new UpdateAssetRequest("Microsoft", "US5949181045");
        var mockAsset = new Asset()
        {
            Name = "Microsoft",
            Symbol = symbol,
            Isin = "US5949181046"
        };
        
        _mockAssetRepository.Update(Arg.Any<Asset>())
            .Returns(mockAsset);

        //Act
        var result = await _assetService.UpdateAssetAsync(symbol, updatedAssetRequest);

        //Assert
        result.Name.Should().Be(updatedAssetRequest.Name);
        result.Symbol.Should().Be(mockAsset.Symbol);
        result.Isin.Should().Be(mockAsset.Isin);
        
    }
    
    [Test]
    public void UpdateAsset_WhenAssetDoesntExists_ShouldThrowException()
    {
        //Arrange
        var updatedAssetRequest = new UpdateAssetRequest("Microsoft",  "US5949181045");
        _mockAssetRepository.Update(Arg.Any<Asset>())
            .Throws(new EntityNotFoundException("Error: Entity not found"));

        //Act & assert
        _assetService.Invoking(i => i.UpdateAssetAsync("MSFT", updatedAssetRequest))
            .Should().ThrowAsync<EntityNotFoundException>();
    }
}