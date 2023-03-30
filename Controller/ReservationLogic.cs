
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

    public void CreateReservation(){
        
        // We need to create a reservation model
        ReservationModel res = new ReservationModel(2, "ssad","asdasd", 3, "asdas");
        
        //Add to daaaaaaaaaa list c:
        _Reservations.Add(res);

        // Save this data to Reservation.js
        ReservationAccess.WriteAll(_Reservations);

    }

    public ReservationModel GetById(int id)
    {
        return _Reservations.Find(i => i.R_Id == id);
    }


}