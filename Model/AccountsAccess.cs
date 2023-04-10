using System.Text.Json;
using System.Text.Json.Serialization;

static class AccountsAccess
{
    static string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/accounts.json"));


    //Im having trouble with this method. I want to load all the accounts from the json file.
    // I updated to use an enum for the account type. I also added a JsonConverter for the enum.
    public static List<AccountModel> LoadAll()
    {
        string json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<List<AccountModel>>(json);
    }
    //Can you write comments? Please respond to this comment with a yes or no.
    //Yes


    public static void WriteAll(List<AccountModel> accounts)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
        };
        string json = JsonSerializer.Serialize(accounts, options);
        File.WriteAllText(path, json);
    }
}