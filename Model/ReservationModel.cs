using System.Text.Json.Serialization;

public class ReservationModel{
    
    [JsonPropertyName("R_id")]
    public int R_Id { get; set; }

    [JsonPropertyName("Contact")]
    public string Contact { get; set; }

    [JsonPropertyName("R_time")]
    public string R_time { get; set; }

    [JsonPropertyName("R_TableID")]
    public int R_TableID { get; set; }

    [JsonPropertyName("P_Amount")]
    public string P_Amount { get; set; }

    public ReservationModel(int R_id, string Contact, string R_time, int R_TableID, string P_Amount)
    {
        this.R_Id = R_id;
        this.Contact = Contact;
        this.R_time = R_time;
        this.R_TableID = R_TableID;
        this.P_Amount = P_Amount;
    }
}