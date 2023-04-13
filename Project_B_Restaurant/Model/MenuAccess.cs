using System.Text.Json;
static class MenuAccess{

    static string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/menu.json"));

    static public List<Dish> LoadMenu()
    {
        if (!File.Exists(path))
        {
            return new List<Dish>();
        }
        
        string json = File.ReadAllText(path);
        //If json is null or empty ?
        if (string.IsNullOrEmpty(json))
        {
            return new List<Dish>();
        }

        //Null-coalescing operator it can be either a List<Dish> or null.
        List<Dish>? dishes = JsonSerializer.Deserialize<List<Dish>>(json);
        return dishes ?? new List<Dish>();
    }

    public static void SaveMenu(List<Dish> _dishes)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(_dishes, options);
        File.WriteAllText(path, json);
    }
}