using System.Text.Json;

static class ReservationAccess
{
    static string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/Reservations.json"));

    // public static readonly JsonSerializerOptions DefaultOptions = new JsonSerializerOptions
    // {
    //     DefaultIgnoreCondition = default
    // };

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
        foreach (ReservationModel res in reservations){
            Console.WriteLine(res);
        }
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(reservations, options);
        File.WriteAllText(path, json);
    }
}