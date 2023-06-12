using System.Text;

class UpdateReservationUI : UI
{
    private ReservationLogic ReservationLogic = new();

    public override string Header
    {
        get => @"
     _____                                _   _
    |  __ \                              | | (_)
    | |__) |___  ___  ___ _ ____   ____ _| |_ _  ___  _ __ 
    |  _  // _ \/ __|/ _ \ '__\ \ / / _` | __| |/ _ \| '_ \
    | | \ \  __/\__ \  __/ |   \ V / (_| | |_| | (_) | | | \
    |_|  \_\___||___/\___|_|    \_/ \__,_|\__|_|\___/|_| |_|
    ========================================================
    ";
    }

    public override string SubText
    {

        get
        {
            StringBuilder sb = new();
            sb.AppendLine("===========================================");
            sb.AppendLine($"Code: {Reservation.R_Code}");
            sb.AppendLine($"Name: {Reservation.Contact}");
            sb.AppendLine($"Party Size: {Reservation.P_Amount}");
            sb.AppendLine($"Date: {Reservation.R_Date}");

            if (AccountsLogic.CurrentAccount!.Level >= AccountLevel.Employee)
                sb.AppendLine($"Tables:");
                for (int i = 0; i < Reservation.R_TableID.Count; i++)
                    sb.AppendLine($"  - {Reservation.R_TableID[i]}");

            if (Reservation.PreOrders.Count > 0)
                sb.AppendLine($"Selected Pre-Orders: ");
                foreach (var preOrder in Reservation.PreOrders)
                    sb.AppendLine($"  - ID: {preOrder.ID}, Name: {preOrder.Name}");

            sb.AppendLine("===========================================");
            
            return sb.ToString();
        }
    }


    public ReservationModel Reservation { get; }
    public UpdateReservationUI(UI previousUI, ReservationModel reservation) : base(previousUI)
    {
        Reservation = reservation;
    }
    public override void CreateMenuItems()
    {
        MenuItems.Clear();
        MenuItems.Add(new MenuItem("Change Name"));
        MenuItems.Add(new MenuItem("Change Date"));
        MenuItems.Add(new MenuItem("Change Party Size"));
        // MenuItems.Add(new MenuItem("Change Tables", AccountLevel.Admin));
        MenuItems.Add(new MenuItem("Change Pre-Orders"));

    }

    public override void UserChoosesOption(int option)
    {
        switch (UserOptions[option].Name)
        {
            case "Change Name":
                ChangeName();
                break;
            case "Change Pre-Orders":
                ChangePreOrders();
                break;
            case "Changing other Information":
                ShowInfoToDelete();
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

    public void ChangeName()
    {
        Reservation.Contact = GetString("Please enter the correct name");
    }

    public void ShowInfoToDelete()
    {
        Console.WriteLine("It is not possible to update any other details.");
        Console.WriteLine("Please go back to the previous screen and delete your current Reservation.");
        Console.WriteLine("You can then create a new one with the correct details.");
    }
    
    public void ChangePreOrders()
    {
        PreOrderView preOrder = new(this, Reservation);
        preOrder.Start();
    }
}