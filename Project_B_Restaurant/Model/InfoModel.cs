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

    [JsonPropertyName("Monday")]
    public string Monday { get; set; }

    [JsonPropertyName("Tuesday")]
    public string Tuesday { get; set; }

    [JsonPropertyName("Wednesday")]
    public string Wednesday { get; set; }

    [JsonPropertyName("Thursday")]
    public string Thursday { get; set; }

    [JsonPropertyName("Friday")]
    public string Friday { get; set; }

    [JsonPropertyName("Saturday")]
    public string Saturday { get; set; }

    [JsonPropertyName("Sunday")]
    public string Sunday { get; set; }

    [JsonConstructor]
    public InfoModel(string name, string address, string postalCode, string city, string emailAddress, string telephone, string monday, string tuesday, string wednesday, string thursday, string friday, string saturday, string sunday)
    {
        Name = name;
        Address = address;
        PostalCode = postalCode;
        City = city;
        EmailAddress = emailAddress;
        Telephone = telephone;
        Monday = monday;
        Tuesday = tuesday;
        Wednesday = wednesday;
        Thursday = thursday;
        Friday = friday;
        Saturday = saturday;
        Sunday = sunday;
    }

    public override string ToString()
    {
        return $"Name: {Name}\nAddress: {Address}\nPostalCode: {PostalCode}\nCity: {City}\nEmail: {EmailAddress}\nPhone number: {Telephone} \n\n======================================\nOpening hours: \nMonday: {Monday} \nTuesday: {Tuesday} \nWednesday: {Wednesday} \nThursday: {Thursday} \nFriday: {Friday} \nSaturday: {Saturday} \nSunday: {Sunday}";
    }
}