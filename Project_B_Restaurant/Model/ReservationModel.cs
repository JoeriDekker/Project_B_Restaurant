using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.Json;
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
    public List<string> R_TableID { get; set; }

    [JsonPropertyName("P_Amount")]
    public int P_Amount { get; set; }

    [JsonPropertyName("PreOrders")]
    public List<Dish> PreOrders { get; set; }

    [JsonPropertyName("R_Date")]
    [JsonConverter(typeof(JsonDateTimeConverter))]
    public DateTime R_Date { get; set; }

    public ReservationModel(int R_Id, string R_Code, string Contact, List<string> R_TableID, int P_Amount, List<Dish> PreOrders, DateTime date)
    {
        this.R_Id = R_Id;
        this.R_Code = R_Code;
        this.Contact = Contact;
        this.R_TableID = R_TableID;
        this.P_Amount = P_Amount;
        this.PreOrders = PreOrders;
        this.R_Date = date;
    }

    public override string ToString()
    {
        return $"Code: {R_Code}, Name: {Contact}, Party Size: {P_Amount}, Date: {R_Date.ToShortDateString()}, Time: {R_Date.ToShortTimeString()}";
    }
}