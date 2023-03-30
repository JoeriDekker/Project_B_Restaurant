using System.Text.Json;

static class ReservationAccess
{
    static string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/Reservations.json"));


    public static List<ReservationModel> LoadAll()
    {
        string json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<List<ReservationModel>>(json);

    }


    public static void WriteAll(List<ReservationModel> reservations)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(reservations, options);
        File.WriteAllText(path, json);
    }
}