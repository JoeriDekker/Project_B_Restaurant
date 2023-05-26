using System.Text;

class PreOrderView : UI
{
    private PreOrderController preOrderController = new PreOrderController();
    private ReservationLogic ReservationController = new();

    public ReservationModel Reservation { get; set; }

    private double totalPrice;

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
                totalPrice = 0;
                for (int i = 0; i < Reservation.PreOrders.Count; i++)
                {
                    Dish dish = Reservation.PreOrders[i];
                    totalPrice += dish.Price;
                    sb.Append($"{dish.Name}\n{dish.Price}\n");
                }
                sb.Append($"\nYour total price is: {totalPrice} euro\n");
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
    }

    public override void UserChoosesOption(int option)
    {
        switch (UserOptions[option].Name)
        {
            case "Add single Dish":
                Reservation.PreOrders.Add(preOrderController.PreOrderDishes());
                break;
            case "Add course menu":
                Reservation.PreOrders.Add(preOrderController.PreOrderAppetizer());
                Reservation.PreOrders.Add(preOrderController.PreOrderMain());
                Reservation.PreOrders.Add(preOrderController.preOrderDessert());
                break;
            case Constants.UI.GO_BACK:
            case Constants.UI.EXIT:
                ReservationController.Update(Reservation);
                Exit();
                break;
            default:
                Console.WriteLine("Invalid input");
                break; ;
        }
    }

    public ReservationModel EndPreOrder()
    {
        return this.Reservation;
    }
    public static void Change()
    {
        Console.WriteLine($"What info would you like to change?");
    }
}