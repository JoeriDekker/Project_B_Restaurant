class Restaurant
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
    public List<Table> Tables { get; set; }

    public Restaurant(string name, string address, string phoneNumber)
    {
        Name = name;
        Address = address;
        PhoneNumber = phoneNumber;
        Tables = new List<Table>();
    }
    
    public string Info() => $"Restaurant: {_name} \nAddress: {_address} \nPhone Number: {_phoneNumber}";
    
    public string ShowLayout()
    {
        string layout = "";
        foreach (Table table in Tables)
        {
            layout += $"Table {table.TableNumber} - {table.Seats} seats - {table.Status} \n";
        }
        return layout;
    }
}