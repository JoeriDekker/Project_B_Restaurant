public class program
{
    public static void Main()
    {
        // MenuAccess.LoadMenuPreOder();
        OpeningUI opening = new(null);
        opening.Start();

    //     ReservationUI res = new(null!);


    //     DateOnly date = res.GetDate();

    //    Dictionary<DateTime, List<TableModel>> availableTimes = res.ReservationLogic.GetAvailableTimesToReserve(date, partySize: 4);
        
    //     foreach(KeyValuePair<DateTime, List<TableModel>> kvp in availableTimes)
    //     {
    //         string tables = string.Join("-", kvp.Value);
    //         Console.WriteLine(kvp.Key);
    //         Console.WriteLine(tables);
    //     }

        
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
