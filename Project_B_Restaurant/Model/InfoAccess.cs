using System.Text.Json;
using System.Text.Json.Serialization;

static class InfoAccess
{
    static string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/info.json"));

    public static List<InfoModel> LoadAll()
    {
        if (!File.Exists(path))
        {
            return new List<InfoModel>();
        }
        
        string json = File.ReadAllText(path);
        //If json is null or empty ?
        if (string.IsNullOrEmpty(json))
        {
            return new List<InfoModel>();
        }

        //Null-coalescing operator it can be either a List<AccountModel> or null.
        List<InfoModel>? info = JsonSerializer.Deserialize<List<InfoModel>>(json);
        return info ?? new List<InfoModel>();
    }


    public static void WriteAll(List<InfoModel> info)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
        };
        string json = JsonSerializer.Serialize(info, options);
        File.WriteAllText(path, json);
    }
}