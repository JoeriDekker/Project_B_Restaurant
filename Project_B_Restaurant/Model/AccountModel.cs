using System.Text.Json.Serialization;


public class AccountModel
{
    private string _password = string.Empty;

    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("emailAddress")]
    public string EmailAddress { get; set; }

    [JsonPropertyName("password")]
    public string Password
    {
        get => AccountsLogic.Decrypt(_password);
        set => _password = value;
    }

    [JsonPropertyName("fullName")]
    public string FullName { get; set; }

    [JsonPropertyName("reservations")]
    public List<string> Reservations { get; set; }

    [JsonPropertyName("type")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public AccountLevel Level { get; set; }

    [JsonConstructor]
    public AccountModel(int id, string emailAddress, string password, string fullName, AccountLevel level)
    {
        Id = id;
        EmailAddress = emailAddress;
        Password = AccountsLogic.Encrypt(password);
        FullName = fullName;
        Level = level;
        Reservations = new List<string>();
    }

    public AccountModel(int id, string emailAddress, string password, string fullName, AccountLevel level, List<string> reservation) : this(id, emailAddress, password, fullName, level)
    {
        Reservations = reservation;
    }

    public string ShowInfo() => $"FullName: {FullName}\nEmail: {EmailAddress}\nLevel: {Level}";

    public override string ToString()
    {
        return $"{FullName} ({Level})";
    }
}