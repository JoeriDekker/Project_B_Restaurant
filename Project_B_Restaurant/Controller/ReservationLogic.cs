
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Newtonsoft.Json.Linq;

public class ReservationLogic{

    private List<ReservationModel> _Reservations;

    //Do i need this?
    static public ReservationModel? CurrentReservation { get; private set; }

    //Get all reservations
    public ReservationLogic()
    {
        _Reservations = ReservationAccess.LoadAll();
    }

    public void CreateReservation(string c_name, int c_party){

        //Get time of when they made the reservation.
        TimeSpan currentTime = DateTime.Now.TimeOfDay; 
        
        // We need to create a reservation model
        ReservationModel res = new ReservationModel(_Reservations.Count() + 1, c_name, $"{currentTime.Hours}:{currentTime.Minutes}", 3, c_party);
        
        //Add to daaaaaaaaaa list c:
        _Reservations.Add(res);

        // Save this data to Reservation.js
        ReservationAccess.WriteAll(_Reservations);

    }

    // This gets all reservations that are made at the moment.
    public List<ReservationModel> GetAllReservations()
    {
        return _Reservations;
    }


    //! Can be null | Check on null when trying to find a reservation!
    public ReservationModel? getReservationByID(int id){
         ReservationModel? getRes = _Reservations.Find(x => x.R_Id == id);
         return getRes;
    }

    //! Can be null | Check on null when trying to find a reservation!
    public ReservationModel? getReservationByTableID(int id){
         ReservationModel? getRes = _Reservations.Find(x => x.R_TableID == id);
         
         return getRes;
    }
}