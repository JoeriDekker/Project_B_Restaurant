using System.Text.Json;
static class MenuAccess{

    static string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/menu.json"));

    static public List<Dish> LoadMenu()
    {
        using (StreamReader reader = new StreamReader(path))
        {
            string json = reader.ReadToEnd();
            List<Dish> dishes = JsonSerializer.Deserialize<List<Dish>>(json);
            return dishes ?? new List<Dish>(); // assign the deserialized list to _dishes or an empty list if the deserialized value is null
        }
    }

    static public void SaveMenu(List<Dish> _dishes)
    {
        string json = JsonSerializer.Serialize(_dishes);

        using (StreamWriter writer = new StreamWriter(path))
        {
            writer.Write(json);
        }
    }
}