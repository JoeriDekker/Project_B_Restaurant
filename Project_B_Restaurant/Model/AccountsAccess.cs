using System.Text.Json;
using System.Text.Json.Serialization;

static class AccountsAccess
{
    static string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/accounts.json"));

    public static List<AccountModel> LoadAll()
    {
        if (!File.Exists(path))
        {
            return new List<AccountModel>();
        }
        
        string json = File.ReadAllText(path);
        //If json is null or empty ?
        if (string.IsNullOrEmpty(json))
        {
            return new List<AccountModel>();
        }

        //Null-coalescing operator it can be either a List<AccountModel> or null.
        List<AccountModel>? accounts = JsonSerializer.Deserialize<List<AccountModel>>(json);
        return accounts ?? new List<AccountModel>();
    }


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