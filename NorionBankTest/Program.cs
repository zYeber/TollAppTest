using NorionBankTest;

var calculator = new TollCalculator();
var vehicle = new Motorbike();
var date = new DateTime(2024, 6, 1, 8, 0, 0);
var date2 = new DateTime(2026, 4, 8, 6, 0, 0);

int fee = calculator.GetTollFee(date2, vehicle);
Console.WriteLine($"Toll fee for {vehicle.GetVehicleType()} at {date2}: {fee} SEK");