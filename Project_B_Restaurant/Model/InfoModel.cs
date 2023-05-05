using System.Text.Json.Serialization;

public class InfoModel
{

    [JsonPropertyName("Address")]
    public int Address { get; set; }

    [JsonPropertyName("PostalCode")]
    public string PostalCode { get; set; }

    [JsonPropertyName("City")]
    public string City { get; set; }

    [JsonPropertyName("Email")]
    public string EmailAddress { get; set; }
    
    [JsonPropertyName("Telephone")]
    public string Telephone { get; set; }

    [JsonConstructor]
    public infoModel(string address, string postalCode, string city, string emailAddress, string telephone)
    {
        Address = address;
        PostalCode = postalCode;
        City = city;
        EmailAddress = emailAddress;
        Telephone = telephone;
    }

}