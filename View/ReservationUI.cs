public class ReservationUI : UI
{

    public override string Header
    {
        get => @"
    _____                                _   _
    |  __ \                              | | (_)
    | |__) |___  ___  ___ _ ____   ____ _| |_ _  ___  _ __  ___
    |  _  // _ \/ __|/ _ \ '__\ \ / / _` | __| |/ _ \| '_ \/ __|
    | | \ \  __/\__ \  __/ |   \ V / (_| | |_| | (_) | | | \__ \
    |_|  \_\___||___/\___|_|    \_/ \__,_|\__|_|\___/|_| |_|___/
    ============================================================
    ";
    }
    // This will initialize the Reservation System (Module)
    public ReservationUI(string[] menuItems) : base(menuItems)
    {
    }

    public override void ShowUI()
    {
        Console.WriteLine(Header);
        for (int i = 0; i < MenuItems.Length; i++)
        {
            Console.WriteLine($"Enter {i + 1} to {MenuItems[i]}");
        }
    }
    public static void initReserve()
    {
        // Initiatlize the RLogic 
        ReservationLogic RLogic = new ReservationLogic();

        // Reservation Menu
        while (true)
        {
            Console.WriteLine("[1] Create Reservation");
            if (AccountsLogic.CurrentAccount.Type == "Customer")
            {
                Console.WriteLine("[2] Go Back");
            }
            if (AccountsLogic.CurrentAccount.Type == "Admin")
            {
                Console.WriteLine("[2] Show all Reservations");
                Console.WriteLine("[3] Find Reservation by reservation ID");
                Console.WriteLine("[4] Find Reservation by table ID");
            }
            // Console.WriteLine($@"[1] Create Reservation
            //     [2] Show all Reservations
            //     [3] Find Reservation by reservation ID
            //     [4] Find Reservation by table ID
            // ");
            string userInp = Console.ReadLine();
            // No error handling : 2444VIK
            if (userInp == "1")
            {
                Console.WriteLine("Please enter a Name:");
                string inp_name = Console.ReadLine();
                string inp_time = "TIME C";
                Console.WriteLine("Please enter amount of People");
                int inp_Pamount = Convert.ToInt32(Console.ReadLine());
                RLogic.CreateReservation(inp_name, inp_time, inp_Pamount);
                Console.WriteLine("Reservation has been made!");
            }
            else if (userInp == "2")
            {
                if (AccountsLogic.CurrentAccount.Type == "Admin")
                {
                    RLogic.GetAllReservations();
                }
                else
                {
                    OpeningUI.Start();
                }
            }
            else if (userInp == "3")
            {
                Console.WriteLine("Please enter a Reservation ID:");
                int IDinp = Convert.ToInt32(Console.ReadLine());
                ReservationModel ReservationMUD = RLogic.getReservationByID(IDinp);
                if (ReservationMUD == null)
                {
                    Console.WriteLine("Cannot be found");
                }
                else
                {
                    Console.WriteLine($"Name: {ReservationMUD.Contact} | Party amount: {ReservationMUD.P_Amount} | TableID: {ReservationMUD.R_TableID} | ReservationID: {ReservationMUD.R_Id}");
                }

            }
            else if (userInp == "4")
            {
                Console.WriteLine("Please enter a Table ID:");
                int IDinp = Convert.ToInt32(Console.ReadLine());
                ReservationModel ReservationMUD = RLogic.getReservationByTableID(IDinp);
                if (ReservationMUD == null)
                {
                    Console.WriteLine("Cannot be found");
                }
                else
                {
                    Console.WriteLine($"Name: {ReservationMUD.Contact} | Party amount: {ReservationMUD.P_Amount} | TableID: {ReservationMUD.R_TableID} | ReservationID: {ReservationMUD.R_Id}");
                }

            }


        }
    }
}