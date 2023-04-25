using System.Text.Json;

static class TableAccess
{
    static string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/Tables.json"));


    public static List<TableModel> LoadAll()
    {
        //If file does not exist in path
        if (!File.Exists(path))
        {
            return new List<TableModel>();
        }
        
        string json = File.ReadAllText(path);
        //If json is null or empty ?
        if (string.IsNullOrEmpty(json))
        {
            return new List<TableModel>();
        }

        //Null-coalescing operator it can be either a List<ReservationModel> or null.
        List<TableModel>? reservations = JsonSerializer.Deserialize<List<TableModel>>(json);
        return reservations ?? new List<TableModel>();
    }


    public static void WriteAll(List<TableModel> tables)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(tables, options);
        File.WriteAllText(path, json);
    }
}