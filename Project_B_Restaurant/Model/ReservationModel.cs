using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

public class ReservationModel
{
    [JsonPropertyName("R_Id")]
    public int R_Id { get; set; }

    [JsonPropertyName("R_Code")]
    public string R_Code { get; set; }

    [JsonPropertyName("Contact")]
    public string Contact { get; set; }

    [JsonPropertyName("R_TableID")]
    public string R_TableID { get; set; }

    [JsonPropertyName("P_Amount")]
    public int P_Amount { get; set; }

    [JsonPropertyName("PreOrders")]
    public List<Dish> PreOrders { get; set; }

    [JsonPropertyName("R_Time")]
    public string R_Time { get; set; }

    [JsonPropertyName("R_Date")]
    public string R_Date { get; set; }

    public ReservationModel() { } // Add a default 

    public ReservationModel(int R_Id, string R_Code, string Contact, string R_TableID, int P_Amount, List<Dish> PreOrders, string R_Time, string R_Date)
    {
        this.R_Id = R_Id;
        this.R_Code = R_Code;
        this.Contact = Contact;
        this.R_TableID = R_TableID;
        this.P_Amount = P_Amount;
        this.PreOrders = PreOrders;
        this.R_Time = R_Time;
        this.R_Date = R_Date;
    }
}