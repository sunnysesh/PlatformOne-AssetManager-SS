using AutoMapper;
using FluentAssertions;
using NSubstitute;
using PlatformOneAsset.Core.Interfaces;
using PlatformOneAsset.Core.Models.Entities;
using PlatformOneAsset.Core.Profiles;
using PlatformOneAsset.Core.Services;

namespace PlatformOneAsset.UnitTests.Services;

public class PriceServiceUnitTests
{
    private PriceService _priceService;
    private IPriceRepository _mockPriceRepository;
    private IMapper _mapper;
    
    [SetUp]
    public void Setup()
    {
        _mockPriceRepository = Substitute.For<IPriceRepository>();
        var cfg = new MapperConfiguration(i =>
        {
            i.AddProfile<MappingProfile>();
        });
        _mapper = cfg.CreateMapper();
        _priceService = new PriceService(_mockPriceRepository, _mapper);
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
                first.Symbol.Should().Be(symbol);
                first.Date.Should().Be(DateTime.Today);
                first.Value.Should().Be(expectedPrices[0].Value);
            },
            second =>
            {
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
}