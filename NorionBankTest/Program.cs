using NorionBankTest;

var calculator = new TollCalculator();
var car = new Car();

DateTime[] dates = 
[
    new(2024, 6, 1, 8, 0, 0),
    new(2026, 4, 8, 6, 0, 0),
    new(2026, 5, 15, 9, 52, 44),
    new(2013, 12, 31, 0, 0, 0)
];

Console.WriteLine("Car fees");
foreach (var date in dates)
{
    var fee = calculator.GetTollFee(car, date);
    Console.WriteLine($"Toll fee for {car.GetVehicleType()} at {date}: {fee} SEK");
}

Console.WriteLine("Toll fees combined: ");
var fees = calculator.GetTollFees(car, dates);
Console.WriteLine(fees);
