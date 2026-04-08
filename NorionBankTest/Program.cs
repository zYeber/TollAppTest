using NorionBankTest;

var calculator = new TollCalculator();
var vehicle = new Car();
var date = new DateTime(2024, 6, 1, 8, 0, 0);

int fee = calculator.GetTollFee(date, vehicle);
Console.WriteLine($"Toll fee for {vehicle.GetVehicleType()} at {date}: {fee} SEK");