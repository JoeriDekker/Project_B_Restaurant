class RestaurantInfo
{
    // private infoController infoLogic = new infoController();
//    public static InfoModel? info = infoController.getInfo();
    public static void Show()
    {
        InfoModel? info = infoController.getInfo();
        Console.WriteLine("test");
        //{info.Address}");
        // Console.WriteLine($"The contact information of restaurant {info.Name}: \nAddress: {info.Address} \nPostal Code: {info.PostalCode} \nCity: {info.City} \nEmail adress: {info.EmailAddress} \n T: {info.Telephone}");
        // \n\nOpening hours: \nMonday: {hours.Monday} \nTuesday: {hours.Tuesday} \nWednesday: {hours.Wednesday} \nThursday: {hours.Thursday} \nFriday: {hours.Friday} \nSaturday: {hours.Saturday} \nSunday: {hours.Sunday}");
    }
    public static void Change()
    {
        Console.WriteLine($"What info would you like to change?");
    }
}