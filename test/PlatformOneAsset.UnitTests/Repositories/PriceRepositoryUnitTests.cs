using FluentAssertions;
using PlatformOneAsset.Core.Models.Entities;
using PlatformOneAsset.Core.Repositories;

namespace PlatformOneAsset.UnitTests.Repositories;

public class PriceRepositoryUnitTests
{
    private PriceRepository _priceRepository;

    [SetUp]
    public void Setup()
    {
        _priceRepository = new PriceRepository();
    }

    [Test]
    public void GetByDate_WhenPricesExistForDate_ShouldReturnCorrectPrices()
    {
        //Arrange
        string symbol = "MSFT";
        var expectedPrices = new List<Price>()
        {
            new Price()
            {
                Symbol = symbol,
                Date = DateTime.Today,
                Value = 50.0m
            },
            new Price()
            {
                Symbol = symbol,
                Date = DateTime.Today.AddDays(1),
                Value = 30.0m
            },
        };
        _priceRepository.Add(expectedPrices[0]);
        _priceRepository.Add(expectedPrices[1]);

        //Act
        var result = _priceRepository.GetPrices(symbol, DateTime.Today);

        //Assert
        result.Should().HaveCount(1);
        result.FirstOrDefault().Should().BeEquivalentTo(expectedPrices[0]);
    }
    
    [Test]
    public void GetByDate_WhenPricesDontExistForDate_ShouldReturnEmptyCollection()
    {
        //Arrange
        string symbol = "MSFT";
        var expectedPrices = new List<Price>()
        {
            new Price()
            {
                Symbol = symbol,
                Date = DateTime.Today.AddDays(1),
                Value = 30.0m
            },
        };
        _priceRepository.Add(expectedPrices[0]);
        
        //Act
        var result = _priceRepository.GetPrices(symbol, DateTime.Today);
        
        //Assert
        result.Should().BeEmpty();
    }
}