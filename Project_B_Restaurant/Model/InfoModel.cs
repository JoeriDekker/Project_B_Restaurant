using System.Text.Json.Serialization;

public class InfoModel
{

    [JsonPropertyName("Name")]
    public string Name { get; set; }

    [JsonPropertyName("Address")]
    public string Address { get; set; }

    [JsonPropertyName("PostalCode")]
    public string PostalCode { get; set; }

    [JsonPropertyName("City")]
    public string City { get; set; }

    [JsonPropertyName("Email")]
    public string EmailAddress { get; set; }
    
    [JsonPropertyName("Telephone")]
    public string Telephone { get; set; }

    [JsonConstructor]
    public InfoModel(string name, string address, string postalCode, string city, string emailAddress, string telephone)
    {
        Name = name;
        Address = address;
        PostalCode = postalCode;
        City = city;
        EmailAddress = emailAddress;
        Telephone = telephone;
    }

    public override string ToString()
    {
        return $"Name: {Name}\nAddress: {Address}\nPostalCode: {PostalCode}\nCity: {City}\nEmail: {EmailAddress}\nPhone number: {Telephone}";
    }
}