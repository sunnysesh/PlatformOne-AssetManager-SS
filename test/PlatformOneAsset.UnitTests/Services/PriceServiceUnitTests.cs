using NSubstitute;
using PlatformOneAsset.Core.Interfaces;
using PlatformOneAsset.Core.Models.Entities;

namespace PlatformOneAsset.UnitTests.Services;

public class PriceServiceUnitTests
{
    private PriceService _priceService;
    private IPriceRepository _mockPriceRepository;
    
    [SetUp]
    public void Setup()
    {
        _mockPriceRepository = Substitute.For<IPriceRepository>();
        _priceService = new PriceService(_mockPriceRepository);
    }

    [Test]
    public async Task GetAssetPricesViaDate_WhenPricesExistForDate_ShouldReturnCorrectPrices()
    {
        //Arrange
        Guid expectedGuid = Guid.NewGuid();
        string symbol = "MSFT";
        var expectedPrices = new List<Price>()
        {
            new Price()
            {
                Id = expectedGuid,
                Symbol = symbol,
                Date = DateTime.Today,
                Value = 20.0m
            },
            new Price()
            {
                Symbol = symbol,
                Date = DateTime.Today.AddDays(-1),
                Value = 20.0m
            }
        };
        
        _mockPriceRepository.GetPrices(Arg.Any<string>(), Arg.Any<DateTime>())
            .Returns(expectedPrices);
        
        //Act
        var result = await _priceService.GetAssetPricesViaDateAsync(symbol, DateTime.Today);

        //Assert
        result.Should().HaveCount(1);
        result.FirstOrDefault().Id.Sould().Be(expectedGuid);
    }
    
    [Test]
    public async Task GetAssetPricesViaDate_WhenDateOutOfRange_ShouldReturnEmpty()
    {
        //Arrange
        _mockPriceRepository.GetPrices(Arg.Any<string>(), Arg.Any<DateTime>())
            .Returns(new List<Price>());
        
        //Act
        var result = await _priceService.GetAssetPricesViaDateAsync("MSFT", DateTime.Today);
        
        //Assert
        result.Should().BeEmpty();
    }
}