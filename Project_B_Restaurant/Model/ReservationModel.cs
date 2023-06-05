using System.Text.Json.Serialization;

public class ReservationModel{
    
    [JsonPropertyName("R_Id")]
    public int R_Id { get; set; }

    [JsonPropertyName("R_Code")]
    public string R_Code { get; set; }

    [JsonPropertyName("Contact")]
    public string Contact { get; set; }

    [JsonPropertyName("R_time")]
    public string R_time { get; set; }

    [JsonPropertyName("R_TableID")]
    public string R_TableID { get; set; }

    [JsonPropertyName("P_Amount")]
    public int P_Amount { get; set; }

    [JsonPropertyName("PreOrders")]
    public List<Dish> PreOrders {get; set; }

    
    [JsonPropertyName("R_DateTime")]
    public string R_DateTime {get; set; }

    public ReservationModel(int R_id, string R_Code, string Contact, string R_time, string R_TableID, int P_Amount, List<Dish> preorders, string R_DateTime)
    {
        this.R_Id = R_id;
        this.R_Code = R_Code;
        this.Contact = Contact;
        this.R_time = R_time;
        this.R_TableID = R_TableID;
        this.P_Amount = P_Amount;
        this.PreOrders = preorders;
        this.R_DateTime = R_DateTime;
    }
}