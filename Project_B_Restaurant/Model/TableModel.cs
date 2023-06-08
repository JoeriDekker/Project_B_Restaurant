using System.Collections.Generic;
using System.Text.Json.Serialization;

public record TableModel
{
    [JsonPropertyName("T_ID")]
    public string T_ID { get; set; }

    [JsonPropertyName("T_Seats")]
    public int T_Seats { get; set; }

    public TableModel(string T_id, bool occupied, int t_Seats, string r_Code)
    {
        this.T_ID = T_id;
        this.T_Seats = t_Seats;
    }
}

