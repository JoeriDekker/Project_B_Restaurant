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

    public ReservationModel CreateReservation(string c_name, int c_party, List<string> TableID, DateTime Date)
    {
        //Get time of when they made the reservation.
        TimeSpan currentTime = DateTime.Now.TimeOfDay;

        // Reservation code needed for customer/employees ... 
        string ResCode = createReservationCode();

        // We need to create a reservation model

        //ReservationModel.ReservationModel(int R_Id, string R_Code, string Contact, string R_TableID, int P_Amount, List<Dish> PreOrders, string R_Time, string R_Date)
        ReservationModel res = new ReservationModel(_Reservations.Count() + 1, ResCode, c_name, TableID, c_party, new(), Date);

        //Add to daaaaaaaaaa list c:
        _Reservations.Add(res);

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

    //! Can be null | Check on null when trying to find a reservation!
    public ReservationModel? getReservationByTableID(string id)
    {
        ReservationModel? getRes = _Reservations.Find(x => x.R_TableID.Contains(id));

        return getRes;
    }

    //Delete reservation by ID
    public bool DeleteReservationByID(int id)
    {

        //Find reservation model
        ReservationModel? Res = _Reservations.Find(x => x.R_Id == id);

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
    public Tuple<DateTime, DateTime> GetOpeningAndClosingTime(DateOnly date)
    {
        string day = date.DayOfWeek.ToString();
        foreach (Dictionary<string, object> openingHour in _openingHours)
        {
            foreach (KeyValuePair<string, object> kvp in openingHour)
            {
                if (kvp.Key == day)
                {
                    DateTime opening = DateTime.Parse(kvp.Value.ToString()!.Split('-')[0]);
                    DateTime closing = DateTime.Parse(kvp.Value.ToString()!.Split('-')[1]);

                    DateTime openingTime = new(date.Year, date.Month, date.Day, opening.Hour, 0, 0);
                    DateTime closingTime = new(date.Year, date.Month, date.Day, closing.Hour, 0, 0);

                    if (closingTime < openingTime)
                        closingTime = closingTime.AddDays(1);

                    return new(openingTime, closingTime);
                }
            }
        }
        return null;
    }


    public Dictionary<DateTime, List<TableModel>> GetAvailableTimesToReserve(DateOnly date, int partySize)
    {
        // Get opening and closing time for date
        (DateTime openingTime, DateTime closingTime) = GetOpeningAndClosingTime(date);
        // Get all timeslots for that day and initialise a dictionary
        Dictionary<DateTime, List<TableModel>> availableTimesToReserve = GetAllTimeSlotsBetween(openingTime, closingTime);

        // Loop through the tables and times
        foreach (TableModel table in tableLogic.Tables)
            foreach (DateTime time in availableTimesToReserve.Keys)
            {
                // Get all the reservedtimes for the current tableID
                List<DateTime> reservedTimes = _Reservations
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
        while (openingTime <= closingTime)
        {
            allTimeSlots[openingTime] = new();
            openingTime = openingTime.AddMinutes(15);
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