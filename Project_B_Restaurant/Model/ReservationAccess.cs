using System.Text.Json;

static class ReservationAccess
{
    // Access needed data

    static string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/Reservations.json"));

    static string openingHoursPath = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"DataSources/openingHours.json"));

    // public static readonly JsonSerializerOptions DefaultOptions = new JsonSerializerOptions
    // {
    //     DefaultIgnoreCondition = default
    // };



    // Access without model ( Dynamic ) static string openingHoursPath = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"DataSources/openingHours.json"));

    public static List<Dictionary<string, object>> LoadOpeningHours()
    {
        if (!File.Exists(openingHoursPath))
        {
            return new List<Dictionary<string, object>>();
        }

        string json = File.ReadAllText(openingHoursPath);
        if (string.IsNullOrEmpty(json))
        {
            return new List<Dictionary<string, object>>();
        }

        List<Dictionary<string, object>> reservations = JsonSerializer.Deserialize<List<Dictionary<string, object>>>(json);
        return reservations ?? new List<Dictionary<string, object>>();
    }

    public static List<ReservationModel> LoadAll()
    {
        //If file does not exist in path
        if (!File.Exists(path))
        {
            return new List<ReservationModel>();
        }

        string json = File.ReadAllText(path);
        //If json is null or empty ?
        if (string.IsNullOrEmpty(json))
        {
            return new List<ReservationModel>();
        }

        //Null-coalescing operator it can be either a List<ReservationModel> or null.
        List<ReservationModel>? reservations = JsonSerializer.Deserialize<List<ReservationModel>>(json);
        return reservations ?? new List<ReservationModel>();
    }


    public static void WriteAll(List<ReservationModel> reservations)
    {   
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(reservations, options);
        File.WriteAllText(path, json);
    }

    public static void SaveReservation(List<ReservationModel> reservations)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(reservations, options);
        File.WriteAllText(path, json);
    }
}