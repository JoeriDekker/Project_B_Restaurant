
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
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

    public ReservationModel CreateReservation(string c_name, int c_party, string TableID)
    {
        //Get time of when they made the reservation.
        TimeSpan currentTime = DateTime.Now.TimeOfDay;

        // Reservation code needed for customer/employees ... 
        string ResCode = createReservationCode();

        //Format needed for table logic
        string date = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
        string formatDate = date.Replace(' ', '#');

        // We need to create a reservation model
        ReservationModel res = new ReservationModel(_Reservations.Count() + 1, ResCode, c_name, $"{formatDate}", TableID, c_party, new());

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
            foreach (Dish dish in Res.PreOrders){
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

    public void GetAvailableResTimes(TimeSpan desiredReservationTime)
    {
        foreach (Dictionary<string, object> openingHour in _openingHours)
        {
            foreach (KeyValuePair<string, object> kvp in openingHour)
            {
                string day = kvp.Key;
                string openingHours = kvp.Value.ToString();

                // Opening hours
                Console.WriteLine($"{day}: {openingHours}");

                // Extract the start time and end time
                string[] times = openingHours.Split('-');
                string startTime = times[0].Trim();
                string endTime = times[1].Trim();

                // Calculate and display available reservation times
                TimeSpan start = TimeSpan.Parse(startTime);
                TimeSpan end = TimeSpan.Parse(endTime);
                TimeSpan interval = TimeSpan.FromHours(2);

                if (end < start)
                {
                    end = end.Add(TimeSpan.FromDays(1)); // Consider it as next day's time
                }

                // UI stuff
                Console.WriteLine("Available reservation times:");
                Console.WriteLine("-----------------------------------");
                Console.WriteLine("|   Start Time   |    End Time    |");
                Console.WriteLine("-----------------------------------");

                for (TimeSpan reservationTime = start; reservationTime.Add(interval) <= end; reservationTime = reservationTime.Add(interval))
                {
                    TimeSpan nextReservationTime = reservationTime.Add(interval);
                    bool isAvailable = true;

                    // Check if the reservation time falls within any existing reservations
                    foreach (ReservationModel reservation in _Reservations)
                    {
                        string[] dayTime = reservation.R_time.Split('#');
                        string ResDay = dayTime[0].Trim();

                        // Split the date into year, month, and day components
                        string[] splitDate = ResDay.Split('-');
                        int year = Convert.ToInt32(splitDate[0]);
                        int month = Convert.ToInt32(splitDate[1]);
                        int dayOfMonth = Convert.ToInt32(splitDate[2]);

                        // Create a DateTime object for the reservation date
                        DateTime reservationDate = new DateTime(year, month, dayOfMonth);

                        string ResTime = dayTime[1].Trim();
                        TimeSpan resTimeParsed = TimeSpan.Parse(ResTime);

                        // Check if the reservation time overlaps with the existing reservation
                        if (reservationDate.DayOfWeek == Enum.Parse<DayOfWeek>(day) &&
                            ((reservationTime >= resTimeParsed && reservationTime < resTimeParsed.Add(interval)) ||
                            (nextReservationTime > resTimeParsed && nextReservationTime <= resTimeParsed.Add(interval))))
                        {
                            isAvailable = false;
                            break;
                        }
                    }

                    if (isAvailable)
                    {
                        Console.WriteLine($"| {reservationTime.ToString(@"hh\:mm")}           |  {nextReservationTime.ToString(@"hh\:mm")}   |");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"| {reservationTime.ToString(@"hh\:mm")}           |  {nextReservationTime.ToString(@"hh\:mm")}   |");
                        Console.ResetColor();
                    }
                }

                Console.WriteLine("-----------------------------------");
                Console.WriteLine();

                // Check if the desired reservation time is available
                bool isDesiredTimeAvailable = IsDesiredTimeAvailable(desiredReservationTime, start, end, interval, day);
                // Probably gonna change this
                Console.WriteLine($"Desired Reservation Time ({desiredReservationTime.ToString(@"hh\:mm")}) Availability: {(isDesiredTimeAvailable ? "Available" : "Not Available")}");
            }
        }
    }

    private bool IsDesiredTimeAvailable(TimeSpan desiredReservationTime, TimeSpan start, TimeSpan end, TimeSpan interval, string day)
    {
        for (TimeSpan reservationTime = start; reservationTime.Add(interval) <= end; reservationTime = reservationTime.Add(interval))
        {
            TimeSpan nextReservationTime = reservationTime.Add(interval);

            if (desiredReservationTime >= reservationTime && desiredReservationTime < nextReservationTime)
            {
                bool isAvailable = true;

                foreach (ReservationModel reservation in _Reservations)
                {
                    string[] dayTime = reservation.R_time.Split('#');
                    string ResDay = dayTime[0].Trim();

                    // Split the date into year, month, and day components
                    string[] splitDate = ResDay.Split('-');
                    int year = Convert.ToInt32(splitDate[0]);
                    int month = Convert.ToInt32(splitDate[1]);
                    int dayOfMonth = Convert.ToInt32(splitDate[2]);

                    // Create a DateTime object for the reservation date
                    DateTime reservationDate = new DateTime(year, month, dayOfMonth);

                    string ResTime = dayTime[1].Trim();
                    TimeSpan resTimeParsed = TimeSpan.Parse(ResTime);

                    // Check if the reservation time overlaps with the existing reservation
                    if (reservationDate.DayOfWeek == Enum.Parse<DayOfWeek>(day) &&
                        (desiredReservationTime >= resTimeParsed && desiredReservationTime < resTimeParsed.Add(interval)))
                    {
                        isAvailable = false;
                        break;
                    }
                }

                return isAvailable;
            }
        }

        return false;
    }

    public void Update(ReservationModel reservation)
    {
        int indexToUpdate = _Reservations.FindIndex(x => x.R_Id == reservation.R_Id);
        _Reservations[indexToUpdate].PreOrders = reservation.PreOrders;
        ReservationAccess.WriteAll(_Reservations);
    }
}