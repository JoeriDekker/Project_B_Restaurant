using System.Text.Json;
static class MenuAccess{

    static string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/menu.json"));

    static public List<Dish> LoadMenu()
    {
        using (StreamReader reader = new StreamReader(path))
        {
            string json = reader.ReadToEnd();
            List<Dish> dishes = JsonSerializer.Deserialize<List<Dish>>(json);
            if (string.IsNullOrWhiteSpace(json)){
                return new List<Dish>();
            }
            else{
                return dishes ?? new List<Dish>();
            }
        }
    }

    public static void SaveMenu(List<Dish> _dishes)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(_dishes, options);
        File.WriteAllText(path, json);
    }
}