using System.Text.Json.Serialization;


public class AccountModel
{
    private string _password;

    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("emailAddress")]
    public string EmailAddress { get; set; }

    [JsonPropertyName("password")]
    public string Password
    {
        get => AccountsLogic.Decrypt(_password);
        set => _password = AccountsLogic.Encrypt(value);
    }

    [JsonPropertyName("fullName")]
    public string FullName { get; set; }

    [JsonPropertyName("type")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public AccountLevel Level { get; set; }

    [JsonConstructor]
    public AccountModel(int id, string emailAddress, string password, string fullName, AccountLevel level)
    {
        Id = id;
        EmailAddress = emailAddress;
        Password = password;
        FullName = fullName;
        Level = level;
    }
}