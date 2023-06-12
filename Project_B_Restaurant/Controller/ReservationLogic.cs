using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Globalization;

/*
(1) De user geeft een datum en hoeveelheid mensen.

(2) We zoeken beschikbaren tijden op voor gegeven datum
    -
(3) Laat tijden zien aan user
    -
(4) User kiest een tijd
    -
(5) registreer reservering.
    -   
*/

public class ReservationLogic
{

    private List<ReservationModel> _Reservations;

    private List<Dictionary<string, object>> _openingHours;

    //Do i need this?
    static public ReservationModel? CurrentReservation { get; private set; }

    TableLogic tableLogic = new TableLogic();


    //Get all reservations
    public ReservationLogic()
    {
        //Load reservations via model
        _Reservations = ReservationAccess.LoadAll();
        // Load opening hours dynamically
        _openingHours = ReservationAccess.LoadOpeningHours();

    }

    public string createReservationCode()
    {
        // Instantiate random number generator.
        Random random = new Random();

        // We create 5 random numbers
        int num = random.Next(10000);

        string Rcode = $"R{num}";

        return Rcode;
    }

    public ReservationModel CreateReservation(string c_name, int c_party, Dictionary<DateTime, List<TableModel>> availableTimes, DateTime chosenTime, List<Dish> PreOrders = null)
    {
        string ResCode = createReservationCode();


        var allCombos = RecGenerateCombinations(0, availableTimes[chosenTime], new(), new());
        allCombos = allCombos.Where(t => t.Count != 0).ToList();
        List<TableModel> bestCombo = BestCombinationOfTables(allCombos, c_party);
        List<string> table_IDs = bestCombo.Select(t => t.T_ID).ToList();

        //Handle Pre Orders

        PreOrders ??= new List<Dish>();


        // Reservation code needed for customer/employees ... 

        ReservationModel res = new ReservationModel(_Reservations.Count() + 1, ResCode, c_name, table_IDs, c_party, PreOrders, chosenTime);

        //Add to daaaaaaaaaa list c:
        _Reservations.Add(res);
        if (AccountsLogic.CurrentAccount != null)
            AccountsLogic.CurrentAccount.Reservations.Add(ResCode);

        // Save this data to Reservation.json
        ReservationAccess.WriteAll(_Reservations);

        return res;
    }

    // This gets all reservations that are made at the moment.
    public List<ReservationModel> GetAllReservations()
    {
        return _Reservations;
    }


    //! Can be null | Check on null when trying to find a reservation!
    public ReservationModel? getReservationByID(int id)
    {
        ReservationModel? getRes = _Reservations.Find(x => x.R_Id == id);
        return getRes;
    }
    public ReservationModel? getReservationByCode(string code)
    {
        ReservationModel? getRes = _Reservations.Find(x => x.R_Code == code);
        return getRes;
    }

    //! Can be null | Check on null when trying to find a reservation!
    public ReservationModel? getReservationByTableID(string id)
    {
        ReservationModel? getRes = _Reservations.Find(x => x.R_TableID.Contains(id));

        return getRes;
    }

    //Delete reservation by ID
    public bool DeleteReservationByID(string id)
    {

        //Find reservation model
        ReservationModel? Res = _Reservations.Find(x => x.R_Code == id);

        //Remove out of Reservations list
        if (_Reservations.Remove(Res) == true)
        {
            MenuController Menu = new MenuController();
            foreach (Dish dish in Res.PreOrders)
            {
                Menu.RemovePreOderInDish(dish);
            }
            // Save this data to Reservation.js 
            ReservationAccess.WriteAll(_Reservations);

            AccountsLogic accountsLogic = new AccountsLogic();
            Console.WriteLine($"Find: {Res.R_Code}\n");

            var reservationsToRemove = accountsLogic.GetAccountModels()
            .SelectMany(acc => acc.Reservations)
            .Where(RCode => RCode == Res.R_Code)
            .ToList();

            foreach (var RCode in reservationsToRemove)
            {
                Console.WriteLine(RCode);
                var acc = accountsLogic.GetAccountModels()
                    .FirstOrDefault(acc => acc.Reservations.Contains(RCode));
                acc.Reservations.Remove(RCode);
                accountsLogic.UpdateList(acc);
            }

            return true;
        }
        else
        {
            return false;
        }
    }

    public bool DeleteReservationByRCode(string res_code)
    {

        //Find reservation model
        ReservationModel? Res = _Reservations.Find(x => x.R_Code == res_code);

        //Remove out of Reservations list
        if (_Reservations.Remove(Res) == true)
        {
            MenuController Menu = new MenuController();
            foreach (Dish dish in Res.PreOrders)
            {
                Menu.RemovePreOderInDish(dish);
            }
            // Save this data to Reservation.js 
            ReservationAccess.WriteAll(_Reservations);

            AccountsLogic accountsLogic = new AccountsLogic();
            Console.WriteLine($"Find: {Res.R_Code}\n");

            var reservationsToRemove = accountsLogic.GetAccountModels()
            .SelectMany(acc => acc.Reservations)
            .Where(RCode => RCode == Res.R_Code)
            .ToList();

            foreach (var RCode in reservationsToRemove)
            {
                Console.WriteLine(RCode);
                var acc = accountsLogic.GetAccountModels()
                    .FirstOrDefault(acc => acc.Reservations.Contains(RCode));
                acc.Reservations.Remove(RCode);
                accountsLogic.UpdateList(acc);
            }

            return true;
        }
        else
        {
            return false;
        }
    }


    public Tuple<DateTime, DateTime> GetOpeningAndClosingTime(DateTime date)
    {
        string day = date.DayOfWeek.ToString();
        // Getting opening and closing time from JSON
        DateTime openingTime = DateTime.Parse(_openingHours[0][day].ToString()!.Split('-')[0]);
        DateTime closingTime = DateTime.Parse(_openingHours[0][day].ToString()!.Split('-')[1]);
        // Adding the correct date
        DateTime openingTimeAndDay = new(date.Year, date.Month, date.Day, openingTime.Hour, 0, 0);
        DateTime closingTimeAndDay = new(date.Year, date.Month, date.Day, closingTime.Hour, 0, 0);
        // If we close after midnight the date will be incorrect, i.e. 14:00-02:00. So we add one day 
        if (closingTimeAndDay < openingTimeAndDay)
            closingTimeAndDay = closingTimeAndDay.AddDays(1);

        return new(openingTime, closingTime);
    }

    public List<List<TableModel>> RecGenerateCombinations(int i, List<TableModel> tables, List<List<TableModel>> result, List<TableModel> subset)
    {
        if (i == tables.Count)
        {
            result.Add(subset.ToList());
            return result;
        }

        // Add tables[i] to the current subset
        subset.Add(tables[i]);
        RecGenerateCombinations(i + 1, tables, result, subset);

        // Backtrack by removing last element
        subset.RemoveAt(subset.Count - 1);
        RecGenerateCombinations(i + 1, tables, result, subset);

        return result;
    }

    public List<TableModel> BestCombinationOfTables(List<List<TableModel>> possibleCombos, int partySize)
    {
        // Set the factors we like to test for and assign an arbitrary value adding up to 1.0
        Dictionary<string, double> factors = new()
        {
            { "TotalTablesUsed", 0.6 },
            { "PercentageFilled", 0.4 }
        };
        // We save our current bestCombo and Score
        List<TableModel> bestCombo = new();
        double bestScore = double.MinValue;
        // Loop through every combo with enough seats.
        foreach (var combo in possibleCombos.Where(a => a.Sum(t => t.T_Seats) >= partySize))
        {
            // Set variables
            double score = 0.0;
            double totalTablesUsed = combo.Count;

            double tablesUsedPenalty = 1 - (totalTablesUsed * 0.2);
            double percentageFilled = 0.0;

            if (combo.Sum(t => t.T_Seats) > 0)
            {
                percentageFilled = partySize / combo.Sum(t => t.T_Seats);
            }
            // Calculate score
            score += tablesUsedPenalty * factors["TotalTablesUsed"];
            score += percentageFilled * factors["PercentageFilled"];

            // If we score better than our current highest replace it.
            if (score > bestScore)
            {
                bestScore = score;
                bestCombo = combo;
            }
        }

        return bestCombo;
    }

    public Dictionary<DateTime, List<TableModel>> GetAvailableTimesToReserve(DateTime date, int partySize)
    {
        // Get opening and closing time for date
        (DateTime openingTime, DateTime closingTime) = GetOpeningAndClosingTime(date);
        // Get all timeslots for that day and initialise a dictionary
        Dictionary<DateTime, List<TableModel>> availableTimesToReserve = GetAllTimeSlotsBetween(openingTime, closingTime);
        DateTime dateDate = DateTime.Parse(date.ToString());

        // Loop through the tables and times
        foreach (TableModel table in tableLogic.Tables)
            foreach (DateTime time in availableTimesToReserve.Keys)
            {
                // Get all the reservedtimes for the current tableID
                List<DateTime> reservedTimes = _Reservations.Where(r => r.R_Date.Date == dateDate.Date)
                                                            .ToList()
                                                            .FindAll(r => r.R_TableID.Contains(table.T_ID))
                                                            .Select(r => r.R_Date)
                                                            .ToList();
                // Is the current timeslot within 2 hours of an active reservation?
                bool isReserved = reservedTimes.Any(reservedTime =>
                    time > reservedTime.AddHours(-2) && time < reservedTime.AddHours(2));
                // If its not reserved we add the table to the timeslot
                if (!isReserved)
                    availableTimesToReserve[time].Add(table);
            }

        FilterTimeSlotsForPartySize(availableTimesToReserve, partySize);
        // Return all timeslots and available tables/
        return availableTimesToReserve;
    }

    public Dictionary<DateTime, List<TableModel>> FilterTimeSlotsForPartySize(Dictionary<DateTime, List<TableModel>> availableTimeSlots, int partySize)
    {
        foreach (KeyValuePair<DateTime, List<TableModel>> kvp in availableTimeSlots)
        {
            int seatsAvailable = 0;
            foreach (TableModel table in kvp.Value)
            {
                seatsAvailable += table.T_Seats;
            }

            if (seatsAvailable < partySize)
            {
                availableTimeSlots[kvp.Key].RemoveAll(table => true);
            }
        }
        return availableTimeSlots;

    }

    public void Update(ReservationModel reservation)
    {
        int indexToUpdate = _Reservations.FindIndex(x => x.R_Id == reservation.R_Id);
        _Reservations[indexToUpdate].PreOrders = reservation.PreOrders;
        ReservationAccess.WriteAll(_Reservations);
    }

    public Dictionary<DateTime, List<TableModel>> GetAllTimeSlotsBetween(DateTime openingTime, DateTime closingTime)
    {
        Dictionary<DateTime, List<TableModel>> allTimeSlots = new();
        DateTime kitchenClosingTime = new(openingTime.Year, openingTime.Month, openingTime.Day, 22, 0, 0);
        while (openingTime <= closingTime && openingTime <= kitchenClosingTime)
        {
            allTimeSlots[openingTime] = new();
            openingTime = openingTime.AddMinutes(30);
        }
        return allTimeSlots;
    }
}
// public void GetAvailableResTimes()
// {
//     foreach (Dictionary<string, object> openingHour in _openingHours)
//     {
//         foreach (KeyValuePair<string, object> kvp in openingHour)
//         {
//             string day = kvp.Key;
//             string openingHours = kvp.Value.ToString();

//             // Opening hours
//             Console.WriteLine($"{day}: {openingHours}");

//             // Extract the start time and end time
//             string[] times = openingHours.Split('-');

//             if (times.Length != 2)
//             {
//                 Console.WriteLine("Invalid opening hours format. Expected format: HH:mm-HH:mm");
//                 continue;
//             }

//             string startTime = times[0].Trim();
//             string endTime = times[1].Trim();

//             // Calculate and display available reservation times
//             TimeSpan start, end;
//             if (!TimeSpan.TryParse(startTime, out start))
//             {
//                 Console.WriteLine("Invalid start time format. Expected format: HH:mm");
//                 continue;
//             }

//             if (!TimeSpan.TryParse(endTime, out end))
//             {
//                 Console.WriteLine("Invalid end time format. Expected format: HH:mm");
//                 continue;
//             }

//             TimeSpan interval = TimeSpan.FromHours(2);

//             if (end < start)
//             {
//                 end = end.Add(TimeSpan.FromDays(1)); // Consider it as next day's time
//             }

//             // UI stuff
//             Console.WriteLine("Available reservation times:");
//             Console.WriteLine("-----------------------------------");
//             Console.WriteLine("|   Start Time   |    End Time    |");
//             Console.WriteLine("-----------------------------------");

//             for (TimeSpan reservationTime = start; reservationTime.Add(interval) <= end; reservationTime = reservationTime.Add(interval))
//             {
//                 TimeSpan nextReservationTime = reservationTime.Add(interval);
//                 bool isAvailable = true;

//                 // Check if the reservation time falls within any existing reservations
//                 foreach (ReservationModel reservation in _Reservations)
//                 {
//                     string resDateTime = reservation.R_time;
//                     DateTime resDate;
//                     if (!DateTime.TryParseExact(resDateTime, "dd/MM/yyyy#HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out resDate))
//                     {
//                         Console.WriteLine("Invalid reservation time format. Expected format: dd/MM/yyyy#HH:mm");
//                         continue;
//                     }

//                     // Check if the reservation date and day match
//                     if (resDate.Date == DateTime.Now.Date && resDate.DayOfWeek.ToString() == day)
//                     {
//                         TimeSpan resTime = resDate.TimeOfDay;
//                         TimeSpan resEndTime = resTime.Add(interval);

//                         // Check if the reservation time overlaps with the existing reservation
//                         if ((reservationTime >= resTime && reservationTime < resEndTime) ||
//                             (nextReservationTime > resTime && nextReservationTime <= resEndTime))
//                         {
//                             isAvailable = false;
//                             break;
//                         }
//                     }
//                 }

//                 if (isAvailable)
//                 {
//                     Console.WriteLine($"| {reservationTime.ToString(@"hh\:mm")}           |  {nextReservationTime.ToString(@"hh\:mm")}   |");
//                 }
//                 else
//                 {
//                     Console.ForegroundColor = ConsoleColor.Red;
//                     Console.WriteLine($"| {reservationTime.ToString(@"hh\:mm")}           |  {nextReservationTime.ToString(@"hh\:mm")}   |");
//                     Console.ResetColor();
//                 }
//             }

//             Console.WriteLine("-----------------------------------");
//             Console.WriteLine();
//         }
//     }
// }



//     private bool IsDesiredTimeAvailable(TimeSpan desiredReservationTime, TimeSpan start, TimeSpan end, TimeSpan interval, string day)
//     {
//         for (TimeSpan reservationTime = start; reservationTime.Add(interval) <= end; reservationTime = reservationTime.Add(interval))
//         {
//             TimeSpan nextReservationTime = reservationTime.Add(interval);

//             if (desiredReservationTime >= reservationTime && desiredReservationTime < nextReservationTime)
//             {
//                 bool isAvailable = true;

//                 foreach (ReservationModel reservation in _Reservations)
//                 {
//                     string[] dayTime = reservation.R_time.Split('#');
//                     string ResDay = dayTime[0].Trim();

//                     // Split the date into year, month, and day components
//                     string[] splitDate = ResDay.Split('/');
//                     int year = Convert.ToInt32(splitDate[0]);
//                     int month = Convert.ToInt32(splitDate[1]);
//                     int dayOfMonth = Convert.ToInt32(splitDate[2]);

//                     // Create a DateTime object for the reservation date
//                     DateTime reservationDate = new DateTime(year, month, dayOfMonth);

//                     string ResTime = dayTime[1].Trim();
//                     TimeSpan resTimeParsed = TimeSpan.Parse(ResTime);

//                     // Check if the reservation time overlaps with the existing reservation
//                     if (reservationDate.DayOfWeek == Enum.Parse<DayOfWeek>(day) &&
//                         (desiredReservationTime >= resTimeParsed && desiredReservationTime < resTimeParsed.Add(interval)))
//                     {
//                         isAvailable = false;
//                         break;
//                     }
//                 }

//                 return isAvailable;
//             }
//         }

//         return false;
//     }

// public void Update(ReservationModel reservation)
// {
//     int indexToUpdate = _Reservations.FindIndex(x => x.R_Id == reservation.R_Id);
//     _Reservations[indexToUpdate].PreOrders = reservation.PreOrders;
//     ReservationAccess.WriteAll(_Reservations);
// }
// }