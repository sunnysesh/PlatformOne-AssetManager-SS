using AutoMapper;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using PlatformOneAsset.Core.Exceptions;
using PlatformOneAsset.Core.Interfaces;
using PlatformOneAsset.Core.Models.Entities;
using PlatformOneAsset.Core.Models.Request;
using PlatformOneAsset.Core.Profiles;
using PlatformOneAsset.Core.Services;

namespace PlatformOneAsset.UnitTests.Services;

public class PriceServiceUnitTests
{
    private PriceService _priceService;
    private IPriceRepository _mockPriceRepository;
    private IAssetRepository _mockAssetRepository;
    private IMapper _mapper;
    
    [SetUp]
    public void Setup()
    {
        _mockPriceRepository = Substitute.For<IPriceRepository>();
        _mockAssetRepository = Substitute.For<IAssetRepository>();
        var cfg = new MapperConfiguration(i =>
        {
            i.AddProfile<MappingProfile>();
        });
        _mapper = cfg.CreateMapper();
        _priceService = new PriceService(_mockPriceRepository, _mockAssetRepository, _mapper);
    }

    [Test]
    public async Task GetAssetPricesViaDate_WhenPricesExistForDate_ShouldReturnCorrectPrices()
    {
        //Arrange
        string symbol = "MSFT";
        var expectedPrices = new List<Price>()
        {
            new Price()
            {
                Symbol = symbol,
                Date = DateTime.Today,
                Value = 20.0m
            },
            new Price()
            {
                Symbol = symbol,
                Date = DateTime.Today,
                Value = 30.0m
            }
        };
        
        _mockPriceRepository.GetPrices(Arg.Any<string>(), Arg.Any<DateTime>())
            .Returns(expectedPrices);
        
        //Act
        var result = await _priceService.GetAssetPricesViaDateAsync(symbol, DateTime.Today.ToString("yyyy-MM-dd"));

        //Assert
        result.Should().HaveCount(2);
        result.Should().SatisfyRespectively(
            first =>
            {
                first.Id.Should().Be(expectedPrices[0].Id);
                first.Symbol.Should().Be(symbol);
                first.Date.Should().Be(DateTime.Today);
                first.Value.Should().Be(expectedPrices[0].Value);
            },
            second =>
            {
                second.Id.Should().Be(expectedPrices[1].Id);
                second.Symbol.Should().Be(symbol);
                second.Date.Should().Be(DateTime.Today);
                second.Value.Should().Be(expectedPrices[1].Value);
            });
    }
    
    [Test]
    public async Task GetAssetPricesViaDate_WhenNoPricesExist_ShouldReturnEmpty()
    {
        //Arrange
        _mockPriceRepository.GetPrices(Arg.Any<string>(), Arg.Any<DateTime>())
            .Returns(new List<Price>());
        
        //Act
        var result = await _priceService.GetAssetPricesViaDateAsync("MSFT", DateTime.Today.ToString("yyyy-MM-dd"));
        
        //Assert
        result.Should().BeEmpty();
    }

    [Test]
    public async Task GetAssetPricesViaDate_WhenDateIsInvalid_ShouldThrowException()
    {
        //Arrange
        string invalidDate = "invalidDate";
        
        //Act & assert
        _priceService.Invoking(i => i.GetAssetPricesViaDateAsync("MSFT", invalidDate))
            .Should().ThrowAsync<ArgumentException>();
    }

    [Test]
    public async Task AddPriceAsync_WhenAssetExists_ShouldReturnAddedPrice()
    {
        //Arrange
        _mockAssetRepository.AssetExists(Arg.Any<string>())
            .Returns(true);

        var expectedPrice = new Price()
        {
            Symbol = "MSFT",
            Date = DateTime.Today,
            Value = 30.0m,
            Source = "S&P500",
            TimeStamp = DateTime.Today
        };
        _mockPriceRepository.Add(Arg.Any<Price>())
            .Returns(expectedPrice);
        _mockPriceRepository.GetPrices(Arg.Any<string>(), Arg.Any<DateTime>(), Arg.Any<string>())
            .Returns(_ => new List<Price>());

        var priceRequest = new CreatePriceRequest(
            expectedPrice.Symbol, expectedPrice.Date, expectedPrice.Value, expectedPrice.Source
        );
        
        //Act
        var result = await _priceService.AddPriceAsync(priceRequest);

        //Assert
        result.Symbol.Should().Be(priceRequest.Symbol);
        result.Date.Should().Be(priceRequest.Date);
        result.Value.Should().Be(priceRequest.Value);
        result.Source.Should().Be(priceRequest.Source);
        result.TimeStamp.Should().NotBe(DateTime.MinValue);
    }
    
    [Test]
    public async Task AddPriceAsync_WhenAssetDoesntExist_ShouldThrowException()
    {
        //Arrange
        _mockAssetRepository.AssetExists(Arg.Any<string>())
            .Returns(false);
        
        //Act & assert
        _priceService.Invoking(i => i.AddPriceAsync(new CreatePriceRequest(
                "MSFT", DateTime.Today, 30.0m, "S&P500"
            )))
            .Should().ThrowAsync<AssetNotFoundException>();
    }

    [Test]
    public async Task AddPriceAsync_WhenPriceWithSameSourceAlreadyAttachedoAsset_ShouldThrowException()
    {
        //Arrange
        _mockAssetRepository.AssetExists(Arg.Any<string>())
            .Returns(true);
        
        var priceRequest = new CreatePriceRequest(
            "MSFT", DateTime.Today, 20.0m, "LSEG"
        );
        
        _mockPriceRepository.GetPrices(Arg.Any<string>(), Arg.Any<DateTime>(), Arg.Any<string>())
            .Returns(new List<Price>()
            {
                new Price()
                {
                    Symbol = priceRequest.Symbol,
                    Date = priceRequest.Date,
                    Source = priceRequest.Source
                }
            });
        
        //Act & assert
        _priceService.Invoking(i => i.AddPriceAsync(priceRequest))
            .Should().ThrowAsync<SourceAlreadyExistsException>();
    }

    [Test]
    public async Task UpdatePriceAsync_WhenPriceAlreadyExists_ShouldReturnUpdatedPrice()
    {
        //Arrange
        var existingPrice = new Price()
        {
            Symbol = "MSFT",
            Date = DateTime.Today,
            Value = 50.0m
        };
        _mockPriceRepository.Update(Arg.Any<Price>())
            .Returns(existingPrice);

        //Act
        var result = await _priceService.UpdatePriceAsync(new UpdatePriceRequest(
            existingPrice.Id, 
            existingPrice.Symbol, 
            existingPrice.Date,
            existingPrice.Value
        ));

        //Assert
        result.Symbol.Should().Be(existingPrice.Symbol);
        result.Date.Should().Be(existingPrice.Date);
        result.Value.Should().Be(existingPrice.Value);
    }
    
    [Test]
    public async Task UpdatePriceAsync_WhenPriceDoesntExist_ShouldThrowException()
    {
        //Arrange
        var updatedPriceRequest = new UpdatePriceRequest(
            Guid.NewGuid(), 
            "MSFT", 
            DateTime.Today,
            20.0m
        );

        _mockPriceRepository.Update(Arg.Any<Price>())
            .Throws(new EntityNotFoundException("Error: Entity not found"));

        //Act & assert
        _priceService.Invoking(i => i.UpdatePriceAsync(updatedPriceRequest))
            .Should().ThrowAsync<EntityNotFoundException>();
    }
}