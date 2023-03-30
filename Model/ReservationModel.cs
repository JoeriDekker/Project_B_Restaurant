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

    public ReservationModel(int R_id, string contactInfo, string r_time, int R_tableID, string P_amount)
    {
        this.R_Id = R_id;
        this.Contact = contactInfo;
        this.R_time = r_time;
        this.R_TableID = R_tableID;
        this.P_Amount = P_amount;
    }
}