using System.Text.Json.Serialization;


public class AccountModel
{
    private string _password;

    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("emailAddress")]
    public string EmailAddress { get; set; }

    [JsonPropertyName("password")]
    public string Password {get; set;}
    // {
    //     get => AccountsLogic.Encrypt(_password);
    //     set => _password = AccountsLogic.Encrypt(value);
    // }

    [JsonPropertyName("fullName")]
    public string FullName { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; }

    public AccountModel(int id, string emailAddress, string password, string fullName, string type)
    {
        Id = id;
        EmailAddress = emailAddress;
        Password = password;
        FullName = fullName;
        Type = type;
    }
}
