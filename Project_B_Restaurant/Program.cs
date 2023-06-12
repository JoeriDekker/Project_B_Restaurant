public class program
{
    public static void Main()
    {
        // MenuAccess.LoadMenuPreOder();
        OpeningUI opening = new(null);
        opening.Start();

        // ReservationUI res = new(null!);


        // DateOnly date = res.GetDate();
        // int partySize = 17;
        // Dictionary<DateTime, List<TableModel>> availableTimes = res.ReservationLogic.GetAvailableTimesToReserve(date, partySize);
        // var options = res.PrintAllAvailableTimes(availableTimes);
        // DateTime timeslot = res.GetUserSelectedTime(options);

        // var allCombos = res.ReservationLogic.RecGenerateCombinations(0, availableTimes[timeslot], new(), new());
        // Console.WriteLine(allCombos.Count);
        // allCombos.Select(t => t.Count != 0);
        // List<TableModel> bestCombo = res.ReservationLogic.BestCombinationOfTables(allCombos, partySize);
        // bestCombo.ForEach(t => Console.WriteLine(t));
        // Console.WriteLine($"Total Seats used: {bestCombo.Sum(t => t.T_Seats)}, Total tables: {bestCombo.Count}");
        
        // // res.ReservationLogic.CreateReservation("Vik", 2 ,tableIDs, date);
        
        // foreach(KeyValuePair<DateTime, List<TableModel>> kvp in availableTimes)
        // {
        //     string tables = string.Join("-", kvp.Value);
        //     Console.WriteLine(kvp.Key);
        //     Console.WriteLine($"{kvp.Value.Count()} tables total seats");
        // }

        
        // List<ReservationModel> reservations = res.ReservationLogic.GetAllReservations();
        //         Console.WriteLine("{0,-5} {1,-10} {2,-10} {3,-25} {4,-10} {5,-10}", "R_ID", "R_Code", "Contact", "R_time", "R_TableID", "P_Amount");
        //         foreach (ReservationModel Res in reservations)
        //         {
        //             Console.WriteLine(Res.R_Date);
        //             Res.R_Date.AddHours(1);
        //             Console.WriteLine(Res.R_Date);
        //         }

    }
}
