using NorionBankTest;

var calculator = new TollCalculator();
var car = new Car();
var motorbike = new Motorbike();
var tractor = new Tractor();

DateTime[] dates = 
[
    new(2024, 6, 1, 8, 0, 0),
    new(2026, 4, 8, 6, 0, 0),
    new(2026, 5, 15, 9, 52, 44),
    new(2013, 12, 31, 0, 0, 0),
    new(2026, 11, 5, 6, 35, 43),
    new(2026, 11,5, 7,30,0),
    new (2026, 11, 5, 10, 45, 0),
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

Console.WriteLine("Motorbike fees");
foreach (var date in dates)
{
    var fee = calculator.GetTollFee(motorbike, date);
    Console.WriteLine($"Toll fee for {motorbike.GetVehicleType()} at {date}: {fee} SEK");
}

Console.WriteLine("Toll fees combined: ");
var mbFees = calculator.GetTollFees(tractor, dates);
Console.WriteLine(mbFees);

Console.WriteLine("Tractor fees");
foreach (var date in dates)
{
    var tractorFee = calculator.GetTollFee(tractor, date);
    Console.WriteLine($"Toll fee for {tractor.GetVehicleType()} at {date}: {tractorFee} SEK");
}

Console.WriteLine("Toll fees combined: ");
var tractorFees = calculator.GetTollFees(motorbike, dates);
Console.WriteLine(tractorFees);