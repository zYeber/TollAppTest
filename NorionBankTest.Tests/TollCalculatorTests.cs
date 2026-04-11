using NSubstitute;
using Xunit;

namespace NorionBankTest.Tests;

public class TollCalculatorTests
{
    private readonly TollCalculator _sut = new();

    private IVehicle CreateVehicle(string type)
    {
        var vehicle = Substitute.For<IVehicle>();
        vehicle.GetVehicleType().Returns(type);
        return vehicle;
    }

    [Fact]
    public void GetTollFees_MultiplePassesWithin60Minutes_ChargesOnlyHighestFee()
    {
        // Arrange
        var vehicle = CreateVehicle("Car");
        var dates = new[]
        {
            new DateTime(2023, 1, 2, 6, 0, 0),
            new DateTime(2023, 1, 2, 6, 30, 0),
            new DateTime(2023, 1, 2, 6, 50, 0),
        };

        // Act
        var result = _sut.GetTollFees(vehicle, dates);

        // Assert
        Xunit.Assert.Equal(13, result);
    }

    [Fact]
    public void GetTolLFees_MultiplePassesOutside60Minutes_ChargesOnly60SEK()
    {
        //Arrange
        var vehicle = CreateVehicle("Car");
        var dates = new[]
        {
            new DateTime(2023, 1, 2, 6, 0, 0),
            new DateTime(2023, 1, 2, 7, 30, 0),
            new DateTime(2023, 1, 2, 8, 30, 0),
            new DateTime(2023, 1, 2, 9, 50, 0),
            new DateTime(2023, 1, 2, 10, 50, 0),
            new DateTime(2023, 1, 2, 11, 50, 0),
            new DateTime(2023, 1, 2, 12, 50, 0),
            new DateTime(2023, 1, 2, 13, 50, 0),
            new DateTime(2023, 1, 2, 14, 50, 0),
            new DateTime(2023, 1, 2, 15, 50, 0),
            new DateTime(2023, 1, 2, 16, 50, 0),
            new DateTime(2023, 1, 2, 17, 50, 0),
        };
        
        //Act
        var result = _sut.GetTollFees(vehicle, dates);
        
        //Assert
        Xunit.Assert.Equal(60, result);
    }
    
    [Xunit.Theory]
    [InlineData("Motorbike")]
    [InlineData("Tractor")]
    [InlineData("Emergency")]
    [InlineData("Diplomat")]
    [InlineData("Foreign")]
    [InlineData("Military")]
    public void GetTollFees_TollFreeVehicle_AlwaysReturnsZero(string vehicleType)
    {
        var vehicle = CreateVehicle(vehicleType);
        var dates = new[] { new DateTime(2023, 1, 2, 8, 0, 0) };

        var result = _sut.GetTollFees(vehicle, dates);

        Xunit.Assert.Equal(0, result);
    }
    
    [Xunit.Theory]
    [InlineData(6, 0,  8)]
    [InlineData(6, 30, 13)]
    [InlineData(7, 30, 18)]
    [InlineData(9, 0,  8)]
    [InlineData(17, 30, 13)]
    [InlineData(19, 0,  0)]
    public void GetTollFee_CorrectFeeForTimeOfDay(int hour, int minute, int expectedFee)
    {
        var vehicle = CreateVehicle("Car");
        var date = new DateTime(2023, 1, 2, hour, minute, 0);

        var result = _sut.GetTollFee(vehicle, date);

        Xunit.Assert.Equal(expectedFee, result);
    }
}