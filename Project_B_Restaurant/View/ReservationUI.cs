public class ReservationUI : UI
{

    ReservationLogic ReservationLogic = new ReservationLogic();
    TableLogic TableLogic = new TableLogic();
    PreOrderView preOrd; 
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

    public override string SubText
    {
        get => string.Empty;
    }

    // This will initialize the Reservation System (Module)
    public ReservationUI(UI previousUI) : base(previousUI)
    {
    }

    public override void CreateMenuItems()
    {
        MenuItems.Add(new MenuItem("Create Reservation", AccountLevel.Guest));
        MenuItems.Add(new MenuItem("Show all Reservations", AccountLevel.Guest));
        MenuItems.Add(new MenuItem("Find Reservation by Reservation ID", AccountLevel.Employee));
        MenuItems.Add(new MenuItem("Find Reservation by Table ID", AccountLevel.Employee));
        MenuItems.Add(new MenuItem("Delete Reservation by ID", AccountLevel.Employee));
        MenuItems.Add(new MenuItem("Testing", AccountLevel.Guest));

    }

    public override void UserChoosesOption(int choice)
    {
        switch (UserOptions[choice].Name)
        {
            case "Create Reservation":
                string inp_name = GetString("Please enter a Name");

                int inp_Pamount = GetInt("Please enter amount of People");

                Console.WriteLine("Checking for available seats...");

                Console.WriteLine(@$"
    ┌────┐    ┌────┐         ┌────┐
    │ 3A │    │ 1A │         │ 2A │
    │(2) │    │(2) │         │(2) │
    └────┘    └────┘         └────┘

    ┌────┐                   ┌────┐
    │ 6A │                   │ 4A │
    │(2) │                   │(4) │
    └────┘                   └────┘

    ┌────┐                   ┌────┐
    │ 5A │                   │ 7A │
    │(4) │                   │(2) │
    └────┘                   └────┘

    ┌────┐                   ┌────┐
    │ 2B │                   │ 3B │
    │(4) │                   │(4) │
    └────┘                   └────┘

    ┌────┐                   ┌────┐
    │ 5B │                   │ 4B │
    │(4) │                   │(2) │
    └────┘                   └────┘

    ┌────┐       ┌────┐      ┌────┐
    │ 6B │       │ 1C │      │ 1B │
    │(2) │       │(6) │      │(6) │
    └────┘       └────┘      └────┘
");

                TableModel table = TableLogic.getTable(inp_Pamount);

                if (table == null)
                {
                    Console.WriteLine("No available seats at the moment.");
                }
                else
                {
                    Console.WriteLine($"{table.T_ID}, {table.T_Seats} ");
                    TableLogic.OccupiedTable(table.T_ID, true);
                    ReservationModel res = ReservationLogic.CreateReservation(inp_name, inp_Pamount, table.T_ID);
                    Console.WriteLine("Reservation has been made!");
                    // Initialize pre order module ( UI )
                    Console.WriteLine("Do you want to make a preorder? Y/N");
                    string answer;

                    answer = Console.ReadLine() ?? string.Empty;
                    if (answer == "Y")
                    {
                        preOrd = new PreOrderView(this, res);
                        preOrd.Start();
                    }
                    Console.WriteLine($"The amount is" + res.PreOrders.Count);
                    ReservationAccess.SaveReservation(ReservationLogic.GetAllReservations());
                }
                break;
            case "Show all Reservations":
                Console.WriteLine("{0,-5} {1,-10} {2,-10} {3,-25} {4,-10} {5,-10}", "R_ID", "R_Code", "Contact", "R_time", "R_TableID", "P_Amount");
                foreach (ReservationModel Res in ReservationLogic.GetAllReservations())
                {
                    Console.WriteLine("{0,-5} {1,-10} {2,-10} {3,-25} {4,-10} {5,-10}", Res.R_Id, Res.R_Code, Res.Contact, Res.R_time, Res.R_TableID, Res.P_Amount);
                }
                break;
            case "Find Reservation by Reservation ID":
                Console.WriteLine("Please enter a Reservation ID:");
                int IDinp = Convert.ToInt32(Console.ReadLine());
                ReservationModel ReservationMUD = ReservationLogic.getReservationByID(IDinp);

                if (ReservationMUD == null)
                {
                    Console.WriteLine("Cannot be found");
                }
                else
                {
                    Console.WriteLine($"Name: {ReservationMUD.Contact} | Party amount: {ReservationMUD.P_Amount} | TableID: {ReservationMUD.R_TableID} | ReservationID: {ReservationMUD.R_Id}");
                }
                break;
            case "Find Reservation by Table ID":
                string IDinput = GetString("Please enter a Table ID:");
                ReservationModel ReservationByTable = ReservationLogic.getReservationByTableID(IDinput);
                if (ReservationByTable == null)
                {
                    Console.WriteLine("Cannot be found");
                }
                else
                {
                    Console.WriteLine($"Name: {ReservationByTable.Contact} | Party amount: {ReservationByTable.P_Amount} | TableID: {ReservationByTable.R_TableID} | ReservationID: {ReservationByTable.R_Id}");
                }
                break;
            case "Delete Reservation by ID":

                Console.WriteLine("{0,-5} {1,-10} {2,-10} {3,-25} {4,-10} {5,-10}", "R_ID", "R_Code", "Contact", "R_time", "R_TableID", "P_Amount");
                foreach (ReservationModel Res in ReservationLogic.GetAllReservations())
                {
                    Console.WriteLine("{0,-5} {1,-10} {2,-10} {3,-25} {4,-10} {5,-10}", Res.R_Id, Res.R_Code, Res.Contact, Res.R_time, Res.R_TableID, Res.P_Amount);
                }

                Console.WriteLine("================================================================================");

                int userInputID = GetInt("Please enter a Reservation ID to delete:");

                if (ReservationLogic.DeleteReservationByID(userInputID))
                {
                    Console.WriteLine("Reservation has been deleted");
                }
                else
                {
                    Console.WriteLine("Reservation could not be found! Please try another code.");
                }


                break;
            case "Testing":

            TimeSpan desiredTime = TimeSpan.Parse("14:00"); // Example desired reservation time
            ReservationLogic.GetAvailableResTimes(desiredTime);





                // if (TableLogic.TableAvailableCheck("1A"))
                // {
                //     Console.WriteLine("Table is not available");
                // }
                // else
                // {
                //     Console.WriteLine("Table is available");
                // }
                // foreach (TableModel tableI in TableLogic.AllAvailabletables())
                // {
                //     Console.WriteLine($"{tableI.T_ID}, {tableI.T_Seats}, {tableI.Occupied}");
                // }

                break;
            case Constants.UI.GO_BACK:
            case Constants.UI.EXIT:
                Exit();
                break;
            default:
                Console.WriteLine("Invalid input");
                break;
        }
    }
}

//         public static void initReserve()
//         {
//             // Initiatlize the RLogic 

//             // Reservation Menu
//             while (true)
//             {
//                 Console.WriteLine("[1] Create Reservation");
//                 if (AccountsLogic.CurrentAccount.Type == "Customer")
//                 {
//                     Console.WriteLine("[2] Go Back");
//                 }
//                 if (AccountsLogic.CurrentAccount.Type == "Admin")
//                 {
//                     Console.WriteLine("[2] Show all Reservations");
//                     Console.WriteLine("[3] Find Reservation by reservation ID");
//                     Console.WriteLine("[4] Find Reservation by table ID");
//                 }
//                 // Console.WriteLine($@"[1] Create Reservation
//                 //     [2] Show all Reservations
//                 //     [3] Find Reservation by reservation ID
//                 //     [4] Find Reservation by table ID
//                 // ");
//                 string userInp = Console.ReadLine();
//                 // No error handling : 2444VIK
//                 if (userInp == "1")
//                 {
//                     Console.WriteLine("Please enter a Name:");
//                     string inp_name = Console.ReadLine();
//                     Console.WriteLine("Please enter amount of People");
//                     int inp_Pamount = Convert.ToInt32(Console.ReadLine());
//                     ReservationLogic.CreateReservation(inp_name, inp_Pamount);
//                     Console.WriteLine("Reservation has been made!");
//                     // Initialize pre order module ( UI )
//                     PreOrder preOrd = new PreOrder();
//                     preOrd.Start();
//                 }
//                 else if (userInp == "2")
//                 {

//                     if (AccountsLogic.CurrentAccount.Type == "Admin")
//                     {
//                         Console.WriteLine(@$"
//   ____                                 _   _                 
//   |  _ \ ___  ___  ___ _ ____   ____ _| |_(_) ___  _ __  ___ 
//   | |_) / _ \/ __|/ _ \ '__\ \ / / _` | __| |/ _ \| '_ \/ __|
//   |  _ <  __/\__ \  __/ |   \ V / (_| | |_| | (_) | | | \__ \
//   |_| \_\___||___/\___|_|    \_/ \__,_|\__|_|\___/|_| |_|___/
//  =============================================================");
//                         // Get all reservations || Create Table || String formatiing c:
//                         Console.WriteLine("{0,-5} {1,-20} {2,-20} {3,-10} {4,-10}", "R_ID", "Contact", "R_time", "R_TableID", "P_Amount");
//                         foreach (ReservationModel Res in ReservationLogic.GetAllReservations())
//                         {
//                             Console.WriteLine("{0,-5} {1,-20} {2,-20} {3,-10} {4,-10}", Res.R_Id, Res.Contact, Res.R_time, Res.R_TableID, Res.P_Amount);
//                         }

//                     }
//                     else
//                     {
//                         break;
//                     }
//                 }
//                 else if (userInp == "3")
//                 {
//                     Console.WriteLine("Please enter a Reservation ID:");
//                     int IDinp = Convert.ToInt32(Console.ReadLine());
//                     ReservationModel ReservationMUD = ReservationLogic.getReservationByID(IDinp);
//                     if (ReservationMUD == null)
//                     {
//                         Console.WriteLine("Cannot be found");
//                     }
//                     else
//                     {
//                         Console.WriteLine($"Name: {ReservationMUD.Contact} | Party amount: {ReservationMUD.P_Amount} | TableID: {ReservationMUD.R_TableID} | ReservationID: {ReservationMUD.R_Id}");
//                     }

//                 }
//                 else if (userInp == "4")
//                 {
//                     Console.WriteLine("Please enter a Table ID:");
//                     int IDinp = Convert.ToInt32(Console.ReadLine());
//                     ReservationModel ReservationMUD = ReservationLogic.getReservationByTableID(IDinp);
//                     if (ReservationMUD == null)
//                     {
//                         Console.WriteLine("Cannot be found");
//                     }
//                     else
//                     {
//                         Console.WriteLine($"Name: {ReservationMUD.Contact} | Party amount: {ReservationMUD.P_Amount} | TableID: {ReservationMUD.R_TableID} | ReservationID: {ReservationMUD.R_Id}");
//                     }

//                 }


//             }
//         }
//     }