
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public class ReservationLogic{

    private List<ReservationModel> _Reservations;

    static public ReservationModel? CurrentReservation { get; private set; }

    //Get all reservations
    public ReservationLogic()
    {
        _Reservations = ReservationAccess.LoadAll();
    }

    public void CreateReservation(string c_name , string c_timeR, int c_party){
        
        // We need to create a reservation model
        ReservationModel res = new ReservationModel(_Reservations.Count() + 1, c_name,c_timeR, 3, c_party);
        
        //Add to daaaaaaaaaa list c:
        _Reservations.Add(res);

        // Save this data to Reservation.js
        ReservationAccess.WriteAll(_Reservations);

    }

    public void GetAllReservations()
    {
                    Console.WriteLine(@$"
  ____                                _   _                 
  |  _ \ ___  ___  ___ _ ____   ____ _| |_(_) ___  _ __  ___ 
  | |_) / _ \/ __|/ _ \ '__\ \ / / _` | __| |/ _ \| '_ \/ __|
  |  _ <  __/\__ \  __/ |   \ V / (_| | |_| | (_) | | | \__ \
  |_| \_\___||___/\___|_|    \_/ \__,_|\__|_|\___/|_| |_|___/
 =============================================================");
    // Get all reservations || Create Table || String formatiing
    Console.WriteLine("{0,-5} {1,-20} {2,-20} {3,-10} {4,-10}", "R_ID", "Contact", "R_time", "R_TableID", "P_Amount");
        foreach (ReservationModel Res in _Reservations)
        {
            Console.WriteLine("{0,-5} {1,-20} {2,-20} {3,-10} {4,-10}", Res.R_Id, Res.Contact, Res.R_time, Res.R_TableID, Res.P_Amount);
        }
    }
}