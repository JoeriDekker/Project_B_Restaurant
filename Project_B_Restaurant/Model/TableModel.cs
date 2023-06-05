using System.Text.Json.Serialization;

public class TableModel{
    
    [JsonPropertyName("T_ID")]
    public string T_ID { get; set; }

    [JsonPropertyName("Occupied")]
    public bool Occupied { get; set; }

    [JsonPropertyName("ReservedTime")]
    public List<string> ReservedTime { get; set; }
        
    [JsonPropertyName("T_Seats")]
    public int T_Seats { get; set; }

    [JsonPropertyName("R_Code")]
    public string R_Code { get; set; }
    public TableModel(string T_id, bool occupied, List<string> reservedTime, int t_Seats, string r_Code)
    {
        this.T_ID = T_id;
        this.Occupied = occupied;
        this.ReservedTime = reservedTime;
        this.T_Seats = t_Seats;
        this.R_Code = r_Code;
    }
}