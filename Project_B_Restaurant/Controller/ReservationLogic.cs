using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Globalization;


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

    public ReservationModel CreateReservation(string c_name, int c_party, string TableID, string Time, string Date)
    {
        //Get time of when they made the reservation.
        TimeSpan currentTime = DateTime.Now.TimeOfDay;

        // Reservation code needed for customer/employees ... 
        string ResCode = createReservationCode();

        // We need to create a reservation model

        //ReservationModel.ReservationModel(int R_Id, string R_Code, string Contact, string R_TableID, int P_Amount, List<Dish> PreOrders, string R_Time, string R_Date)
        ReservationModel res = new ReservationModel(_Reservations.Count() + 1, ResCode, c_name, TableID, c_party, new(), Time, Date);

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
        ReservationModel? getRes = _Reservations.Find(x => x.R_TableID == id);

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

            // Occupied to false || on deletions
            tableLogic.RestoreOccupiedTable(Res.R_TableID);


            return true;
        }
        else
        {
            return false;
        }
    }

    // public static bool CheckReservationsFull(List<ReservationModel> reservations, List<TableModel> tables, string tableId, string desiredTime, out string availableTableId, string desiredDate)
    // {
    //     // Initialize availableTableId as null
    //     availableTableId = null;

    //     // Get the reservations for the specified table
    //     List<ReservationModel> tableReservations = reservations.FindAll(r => r.R_TableID == tableId);

    //     // Check if the reserved time slots have reached the maximum limit
    //     int maxReservations = 4; // Maximum reservations allowed per table
    //     if (tableReservations.Count >= maxReservations)
    //     {
    //         return true; // Reservations are full
    //     }

    //     // Check if the desired time slot is already reserved
    //     TableModel table = tables.Find(t => t.T_ID == tableId);

    //     if (table != null && table.ReservedTime.ContainsKey(desiredDate) && table.ReservedTime[desiredDate].Contains(desiredTime))
    //     {
    //         return true; // Desired time slot is already reserved
    //     }

    //     // Check if the desired time slot conflicts with an existing reservation
    //     foreach (ReservationModel reservation in tableReservations)
    //     {
    //         TimeSpan interval = TimeSpan.FromHours(2); // Minimum interval between reservations
    //         DateTime reservationTime;
    //         DateTime desiredDateTime;

    //         if (DateTime.TryParse(reservation.R_Time, out reservationTime) && DateTime.TryParse(desiredTime, out desiredDateTime))
    //         {
    //             if (Math.Abs((reservationTime - desiredDateTime).TotalHours) < interval.TotalHours)
    //             {
    //                 return true; // Desired time slot conflicts with an existing reservation
    //             }
    //         }
    //     }
    public Tuple<DateTime, DateTime> GetTime(DateOnly date)
    {
        string day = date.DayOfWeek.ToString();
        foreach (Dictionary<string, object> openingHour in _openingHours)
        {
            foreach (KeyValuePair<string, object> kvp in openingHour)
            {
                if (kvp.Key == day)
                {
                    return new(DateTime.Parse(kvp.Value.ToString()!.Split('-')[0]), DateTime.Parse(kvp.Value.ToString()!.Split('-')[1]));
                }
            }
        }
        return null;
    }


    public List<DateTime> GetAvailableTimes(DateOnly date)
    {
        List<DateTime> times = new();
        List<List<DateTime>> AllTimes = new();
        (DateTime openingTime, DateTime closingTime) = GetTime(date);

        foreach (TableModel table in tableLogic.Tables)
        {
            times.Clear();
            DateTime time = openingTime;

            while (true)
            {
                times.Add(time);
                time = time.AddMinutes(15);
                Console.WriteLine(time);

                if (time >= closingTime)
                    break;
            }


            foreach (string reservationTime in table.ReservedTime[date.DayOfWeek.ToString()])
            {
                DateTime unavailableTime = DateTime.Parse(reservationTime);
                times.RemoveAll(x => x - unavailableTime < TimeSpan.FromHours(2));
                AllTimes.Append(times);
            }
        }

        return AllTimes.SelectMany(x => x).Distinct().ToList();
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