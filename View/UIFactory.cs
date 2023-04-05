using System.ComponentModel;

static class UIFactory
{
    // MenuUI constants:
    private const string ShowMenu = "Show Menu";
    private const string SortMenu = "Sort Menu // still in progress";
    private const string FilterMenu = "Filter Menu // still in progress";
    private const string AddDish = "Add a Dish";
    private const string RemoveDish = "Remove a Dish";
    private const string UpdateDish = "Update a Dish";
    private const string Exit = "Exit";


    // MenuUI options:
    private static string[] MenuGuestOptions = { ShowMenu, SortMenu, FilterMenu, Exit };
    private static string[] MenuCustomerOptions = { ShowMenu, SortMenu, FilterMenu, Exit };
    private static string[] MenuAdminOptions = { ShowMenu, SortMenu, FilterMenu, AddDish, RemoveDish, UpdateDish, Exit };


    // OpeningUI constants:
    private const string Login = "Login";
    private const string ContinueAsGuest = "Continue as Guest";


    // OpeningUI options:
    private static string[] OpeningGuestOptions = { Login, ContinueAsGuest, Exit };


    // ReservationUI constants:
    private const string CreateReservation = "Create Reservation";
    private const string ShowAllReservations = "Show all Reservations";
    private const string FindReservationByID = "Find Reservation by reservation ID";
    private const string FindReservationByTableID = "Find Reservation by table ID";


    // ReservationUI options:
    private static string[] ReservationGuestOptions = { CreateReservation, Exit };
    private static string[] ReservationCustomerOptions = { CreateReservation, Exit };
    private static string[] ReservationAdminOptions = { CreateReservation, ShowAllReservations, FindReservationByID, FindReservationByTableID, Exit };

    public static MenuUI CreateMenuUI()
    {
        switch (Restaurant.User?.Type)
        {
            case "Customer":
                return new(MenuCustomerOptions);
            case "Admin":
                return new(MenuAdminOptions);
            default:
                return new(MenuGuestOptions);
        }
    }

    public static OpeningUI CreateOpeningUI()
    {
        switch (Restaurant.User?.Type)
        {
            case "Customer":
                return new(OpeningGuestOptions);
            case "Admin":
                return new(OpeningGuestOptions);
            default:
                return new(OpeningGuestOptions);
        }
    }

    public static ReservationUI CreateReservationUI()
    {
        switch (Restaurant.User?.Type)
        {
            case "Customer":
                return new(ReservationCustomerOptions);
            case "Admin":
                return new(ReservationAdminOptions);
            default:
                return new(ReservationGuestOptions);
        }
    }
}
// enum MenuUIOptions
// {
//     [Description("Show Menu")]
//     ShowMenu,
//     [Description("Sort Menu // still in progress")]
//     SortMenu,
//     [Description("Filter Menu // still in progress")]
//     FilterMenu,
//     [Description("Add a Dish")]
//     AddDish,
//     [Description("Remove a Dish")]
//     RemoveDish,
//     [Description("Update a Dish")]
//     UpdateDish,
//     [Description("Exit")]
//     Exit
// }