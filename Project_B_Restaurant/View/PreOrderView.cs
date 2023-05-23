using System.Text;

class PreOrderView : UI
{
    private PreOrderController PreOrderController = new PreOrderController();
    private MenuController Menu = new();

    public ReservationModel Reservation { get; set; }

    public int TotalPrice { get => Reservation.PreOrders.ForEach(dish => TotalPrice + dish.Price)}

    public override string Header
    {
        get => @"
     _____             ____          _           
    |  __ \           / __ \        | |          
    | |__) | __ ___  | |  | |_ __ __| | ___ _ __ 
    |  ___/ '__/ _ \ | |  | | '__/ _` |/ _ \ '__|
    | |   | | |  __/ | |__| | | | (_| |  __/ |   
    |_|   |_|  \___|  \____/|_|  \__,_|\___|_|   
    =============================================";
    }

    public override string SubText
    {
        get
        {
            if (Reservation.PreOrders.Count <= 0)
                return $"No Preorders yet";
            else
            {
                StringBuilder sb = new();
                for (int i = 0; i < Reservation.PreOrders.Count; i++)
                {
                    Dish dish = Reservation.PreOrders[i];
                    sb.Append($"{dish.Name}\n{dish.Price}\n");
                }
                sb.Append(TotalPrice);
                return sb.ToString();
            }
        }
    }

    public string ShowPreOrders()
    {
        StringBuilder sb = new();

        for (int i = 0; i < Reservation.PreOrders.Count; i++)
            sb.Append(Reservation.PreOrders[i].ToString());

        return sb.ToString();

    }

    public PreOrderView(UI previousUI, ReservationModel reservation) : base(previousUI)
    {
        Reservation = reservation;
    }
    public override void CreateMenuItems()
    {
        MenuItems.Clear();
        MenuItems.Add(new MenuItem("Add single Dish", AccountLevel.Guest));
        MenuItems.Add(new MenuItem("Add course menu", AccountLevel.Guest));
        MenuItems.Add(new MenuItem("Remove PreOrder", AccountLevel.Guest));
        MenuItems.Add(new MenuItem("Update PreOrder", AccountLevel.Guest));
    }

    public override void UserChoosesOption(int option)
    {
        switch (UserOptions[option].Name)
        {
            case "Add single Dish":
                Reservation.PreOrders.Add(PreOrderController.PreOrderDishes());
                break;
            case "Add course menu":
                Reservation.PreOrders.Add(PreOrderController.PreOrderAppetizer());
                PreOrderController.PreOrderMain();
                PreOrderController.preOrderDessert();
                break;
            case "Remove PreOrder":
                break;
            case "Update PreOrder":
                break;
            case Constants.UI.GO_BACK:
            case Constants.UI.EXIT:
                Exit();
                break;
            default:
                Console.WriteLine("Invalid input");
                break; ;
        }
    }
    public static void Change()
    {
        Console.WriteLine($"What info would you like to change?");
    }
}