namespace NorionBankTest;
public class TollCalculator
{

    /**
     * Calculate the total toll fee for one day
     *
     * @param vehicle - the vehicle
     * @param dates   - date and time of all passes on one day
     * @return - the total toll fee for that day
     */

    public int GetTollFees(IVehicle vehicle, DateTime[] dates)
    {
        var intervalStart = dates[0];
        var totalFee = 0;
        foreach (var date in dates)
        {
            var nextFee = GetTollFee(vehicle, date);
            var tempFee = GetTollFee(vehicle, intervalStart);

            long diffInMillies = date.Millisecond - intervalStart.Millisecond;
            var minutes = diffInMillies / 1000 / 60;

            if (minutes <= 60)
            {
                if (totalFee > 0) totalFee -= tempFee;
                if (nextFee >= tempFee) tempFee = nextFee;
                totalFee += tempFee;
            }
            else
            {
                totalFee += nextFee;
            }
        }
        if (totalFee > 60) totalFee = 60;
        return totalFee;
    }

    private bool IsTollFreeVehicle(IVehicle vehicle)
    {
        if (vehicle == null) return false;
        var vehicleType = vehicle.GetVehicleType();
        return Enum.TryParse<TollFreeVehicles>(vehicleType, out _);
    }

    public int GetTollFee(IVehicle vehicle, DateTime date)
    {
        if (IsTollFreeDate(date) || IsTollFreeVehicle(vehicle))
            return 0;

        var time = date.TimeOfDay;

        foreach (var (end, fee) in FeeTable)
        {
            if (time < end)
                return fee;
        }

        return 0;
    }

    private bool IsTollFreeDate(DateTime date)
    {
        if (date.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday)
            return true;

        if (date.Year != 2013)
            return false;

        if (date.Month == 7)
            return true;

        return Holidays.Contains((date.Month, date.Day));
    }

    private enum TollFreeVehicles
    {
        Motorbike = 0,
        Tractor = 1,
        Emergency = 2,
        Diplomat = 3,
        Foreign = 4,
        Military = 5
    }

    private static readonly HashSet<(int month, int day)> Holidays =
    [
        (1, 1),
        (3, 28),
        (3, 29),
        (4, 1),
        (4, 30),
        (5, 1),
        (5, 8),
        (5, 9),
        (6, 5),
        (6, 6),
        (6, 21),
        (11, 1),
        (12, 24),
        (12, 25),
        (12, 26),
        (12, 31)
    ];

    private static readonly HashSet<(TimeSpan End, int Fee)> FeeTable =
    [
        (TimeSpan.FromHours(6.5), 8),
        (TimeSpan.FromHours(7), 13),
        (TimeSpan.FromHours(8), 18),
        (TimeSpan.FromHours(8.5), 13),
        (TimeSpan.FromHours(15), 8),
        (TimeSpan.FromHours(15.5), 13),
        (TimeSpan.FromHours(17), 18),
        (TimeSpan.FromHours(18), 13),
        (TimeSpan.FromHours(18.5), 8)
    ];
}