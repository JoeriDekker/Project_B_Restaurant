using System.Text.Json;
static class MenuAccess<T>
{
    static string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/menu.json"));
    static string future_menu_path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/future_menu.json"));

    static public List<T> LoadMenuPreOrder()
    {
        if (!File.Exists(path))
        {
            Console.WriteLine("File does not exist");
            return new List<T>();
        }

        string json = File.ReadAllText(path);
        if (string.IsNullOrEmpty(json))
        {
            Console.WriteLine("File is empty");
            return new List<T>();
        }

        List<T>? items = JsonSerializer.Deserialize<List<T>>(json);
        return items ?? new List<T>();
    }

    static public List<T> LoadMenu()
    {
        if (!File.Exists(path))
        {
            return new List<T>();
        }

        string json = File.ReadAllText(path);
        if (string.IsNullOrEmpty(json))
        {
            return new List<T>();
        }

        List<T>? items = JsonSerializer.Deserialize<List<T>>(json);
        return items ?? new List<T>();
    }

    public static void SaveMenu(List<T> items)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(items, options);
        File.WriteAllText(path, json);
    }

    static public List<T> LoadFutureMenu()
    {
        if (!File.Exists(future_menu_path))
        {
            return new List<T>();
        }

        string json = File.ReadAllText(future_menu_path);
        if (string.IsNullOrEmpty(json))
        {
            return new List<T>();
        }

        List<T>? items = JsonSerializer.Deserialize<List<T>>(json);
        return items ?? new List<T>();
    }

    public static void SaveFutureMenu(List<T> items)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(items, options);
        File.WriteAllText(future_menu_path, json);
    }
}