using System.Text;

public class ReservationUI : UI
{

    public ReservationLogic ReservationLogic = new ReservationLogic();
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
    private string _subText;
    public override string SubText
    {
        get => _subText;
    }

    // This will initialize the Reservation System (Module)
    public ReservationUI(UI previousUI) : base(previousUI)
    {
        _subText = GenerateSubText();
    }

    // Gets _subText to be always up to date

    public override void CreateMenuItems()
    {
        MenuItems.Add(new MenuItem("Create Reservation", AccountLevel.Guest));
        MenuItems.Add(new MenuItem("Show all Reservations", AccountLevel.Employee));

        if (AccountsLogic.CurrentAccount != null && AccountsLogic.CurrentAccount.Reservations.Count != 0)
            MenuItems.Add(new MenuItem("Update Reservation", AccountLevel.Customer));

        MenuItems.Add(new MenuItem("Update any Reservation as Admin", AccountLevel.Admin));
        MenuItems.Add(new MenuItem("Find Reservation by Reservation ID", AccountLevel.Employee));
        MenuItems.Add(new MenuItem("Find Reservation by Table ID", AccountLevel.Employee));
        MenuItems.Add(new MenuItem("Delete Reservation by Reservation Code", AccountLevel.Employee));
        MenuItems.Add(new MenuItem("Show Available reservation times", AccountLevel.Guest));
    }

    public string GenerateSubText()
    {
        var currentAccount = AccountsLogic.CurrentAccount;
        if (currentAccount == null || currentAccount.Reservations.Count == 0)
        {
            return string.Empty;
        }
        StringBuilder sb = new();

        sb.AppendLine("======================================");
        sb.AppendLine("Your Reservations:\n");
        foreach (var code in currentAccount.Reservations)
        {
            string reservation = ReservationLogic.getReservationByCode(code)!.ToString();
            sb.AppendLine($" - {reservation}");
        }
        sb.AppendLine("======================================");
        return sb.ToString();
    }


    public override void UserChoosesOption(int choice)
    {
        switch (UserOptions[choice].Name)
        {
            case "Create Reservation":
                CreateReservation();
                _subText = GenerateSubText();
                break;
            case "Show all Reservations":
                ShowAllReservations();
                break;
            case "Update Reservation": // TODO ?
                UpdateReservation();
                _subText = GenerateSubText();
                break;
            case "Find Reservation by Reservation ID":
                FindReservationByReservationID();
                break;
            case "Find Reservation by Table ID":
                FindReservationByTableID();
                break;
            case "Delete Reservation by Reservation Code":
                DeleteReservationByID();
                _subText = GenerateSubText();
                break;
            case "Show Available reservation times":
                ShowAvailableReservations();
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

    public void FindReservationByReservationID()
    {
        Console.WriteLine("Please enter a Reservation ID:");
        int IDinp = Convert.ToInt32(Console.ReadLine());
        ReservationModel ReservationMUD = ReservationLogic.getReservationByID(IDinp);

        if (ReservationMUD == null)
        {
            Console.WriteLine("Cannot be found");
        }
        else
        {
            ShowSingleReservation(ReservationMUD);
        }
    }

    public void UpdateReservation()
    {
        string code = GetString("Provide the Code of the Reservation you wish to update.");

        if (!AccountsLogic.CurrentAccount!.Reservations.Contains(code.ToUpper()))
        {
            Console.WriteLine("Incorrect Reservation Code");
            return;
        }
        UpdateReservationUI updateReservationUI = new(this, ReservationLogic.getReservationByCode(code.ToUpper())!);
        updateReservationUI.Start();
    }
    public void ShowAvailableReservations()
    {
        DateTime date = GetDate();
        int partySize = GetInt("How Many People?");

        var availableTimes = ReservationLogic.GetAvailableTimesToReserve(date, partySize);
        PrintAllAvailableTimes(availableTimes);
    }

    public void DeleteReservationByID()
    {
        ShowAllReservations();

        Console.WriteLine("================================================================================");

        string userInputID = GetString("Please enter a Reservation code to delete:").ToUpper();

        if (ReservationLogic.DeleteReservationByID(userInputID))
        {
            Console.WriteLine("Reservation has been deleted");
            if (AccountsLogic.CurrentAccount!.Reservations.Contains(userInputID))
            {
                AccountsLogic.CurrentAccount!.Reservations.Remove(userInputID);
            }
        }
        else
        {
            Console.WriteLine("Reservation could not be found! Please try another code.");
        }
    }

    public void ShowSingleReservation(ReservationModel Res)
    {
        Console.WriteLine("{0,-5} {1,-10} {2,-10} {3,-25} {4,-15} {5,-10}", "R_ID", "R_Code", "Contact", "R_time", "R_TableID", "P_Amount");
        Console.WriteLine("---------------------------------------------------------------------------------");
        Console.WriteLine("{0,-5} {1,-10} {2,-10} {3,-25} {4,-15} {5,-10}", Res.R_Id, Res.R_Code, Res.Contact, Res.R_Date, string.Join(", ", Res.R_TableID), Res.P_Amount);
    }

    public bool DeleteReservationByRCode()
    {
        string res_code = GetString("Please enter the reservation code to delete your reservation:");
        return ReservationLogic.DeleteReservationByRCode(res_code);
    }


    public void FindReservationByTableID()
    {
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
    }

    public void ShowAllReservations()
    {
        Console.WriteLine();
        Console.WriteLine("=================================================================================");
        Console.WriteLine("{0,-5} {1,-10} {2,-10} {3,-25} {4,-10} {5,-10}", "R_ID", "R_Code", "Contact", "R_time", "R_TableID", "P_Amount");
        Console.WriteLine("---------------------------------------------------------------------------------");

        foreach (ReservationModel Res in ReservationLogic.GetAllReservations())
        {
            Console.WriteLine("{0,-5} {1,-10} {2,-10} {3,-25} {4,-15} {5,-10}", Res.R_Id, Res.R_Code, Res.Contact, Res.R_Date, string.Join(", ", Res.R_TableID), Res.P_Amount);
        }
    }

    //TODO: Infinite loop If there are no timeslots available for given party size
    public void CreateReservation()
    {

        /*
            (1) De user geeft een datum en hoeveelheid mensen.

            (2) We zoeken beschikbaren tijden op voor gegeven datum
                -
            (3) Laat tijden zien aan user
                -
            (4) User kiest een tijd
                -
            (5) registreer reservering.
                -   
        */


        //Enter name for reservation
        string name = GetString("Please enter a Name");

        //Enter Party size for reservation
        int partySize = GetInt("Please enter amount of People");

        //Ask for date they wanna make a reservation on
        DateTime date = GetDate();

        // Dictionary<DateTime, List<TableModel>>
        var availableTimes = ReservationLogic.GetAvailableTimesToReserve(date, partySize);

        //Available times
        var rewriteTimes = PrintAllAvailableTimes(availableTimes);


        // Selected time
        DateTime selectedTime = GetUserSelectedTime(rewriteTimes);



        // Show the selected date and time
        Console.WriteLine($"Selected Date and Time: {selectedTime.ToString("yyyy-MM-dd HH:mm")}");


        //Create Reservation
        ReservationModel res = ReservationLogic.CreateReservation(name, partySize, availableTimes, selectedTime);

        if (AccountsLogic.CurrentAccount != null)
        {
            AccountsLogic accountsLogic = new AccountsLogic();
            AccountModel account = accountsLogic.GetById(AccountsLogic.CurrentAccount.Id);
            account.Reservations.Add(res.R_Code);
            accountsLogic.UpdateList(account);
        }

        Console.WriteLine("Do you want to make a preorder? Y/N");
        string answer = Console.ReadLine()!.ToUpper() ?? string.Empty;
        if (answer == "Y")
        {
            preOrd = new PreOrderView(this, res);
            preOrd.Start();
        }

        // Console.ReadLine();

        //         Console.WriteLine(@$"
        //     ┌────┐    ┌────┐         ┌────┐
        //     │ 3A │    │ 1A │         │ 2A │
        //     │(2) │    │(2) │         │(2) │
        //     └────┘    └────┘         └────┘

        //     ┌────┐                   ┌────┐
        //     │ 6A │                   │ 4A │
        //     │(2) │                   │(4) │
        //     └────┘                   └────┘

        //     ┌────┐                   ┌────┐
        //     │ 5A │                   │ 7A │
        //     │(4) │                   │(2) │
        //     └────┘                   └────┘

        //     ┌────┐                   ┌────┐
        //     │ 2B │                   │ 3B │
        //     │(4) │                   │(4) │
        //     └────┘                   └────┘

        //     ┌────┐                   ┌────┐
        //     │ 5B │                   │ 4B │
        //     │(4) │                   │(2) │
        //     └────┘                   └────┘

        //     ┌────┐       ┌────┐      ┌────┐
        //     │ 6B │       │ 1C │      │ 1B │
        //     │(2) │       │(6) │      │(6) │
        //     └────┘       └────┘      └────┘
        // ");

        // string availableTableId = null;

        //         foreach (TableModel table in availableTables)
        //         {
        //             if (true)
        //             {
        //                 //TableLogic.OccupiedTable(table.T_ID, true, Convert.ToString(inp_getDate), Convert.ToString(desiredTime));
        //                 Console.WriteLine("Reservation made at table: " + table.T_ID);
        //                 Console.WriteLine($"{table.T_ID}, {table.T_Seats}");

        //                 //ReservationModel res = ReservationLogic.CreateReservation(inp_name, inp_Pamount, table.T_ID, Convert.ToString(desiredTime), Convert.ToString(inp_getDate));
        //                 Console.WriteLine("Reservation has been made!");

        //                 if (AccountsLogic.CurrentAccount != null)
        //                 {
        //                     AccountsLogic accountsLogic = new AccountsLogic();
        //                     AccountModel account = AccountsLogic.CurrentAccount;
        //                     //account.Reservations.Add(res.R_Code); //needs to be fixed
        //                     accountsLogic.UpdateList(account);
        //                 }

        //                 //Console.WriteLine($"Your Reservation Code: {res.R_Code}\n");


        //             }
        // }
    }

    public Dictionary<int, DateTime> PrintAllAvailableTimes(Dictionary<DateTime, List<TableModel>> availableTimes)
    {
        int option = 1;
        Dictionary<int, DateTime> availableTimesDict = new Dictionary<int, DateTime>();

        foreach (KeyValuePair<DateTime, List<TableModel>> entry in availableTimes)
        {
            if (entry.Value.Count == 0)
                continue;

            string time = entry.Key.ToString("HH:mm");
            Console.WriteLine($"{option}: {time}");

            availableTimesDict[option] = entry.Key;
            option++;
        }

        return availableTimesDict;
    }
    public DateTime GetUserSelectedTime(Dictionary<int, DateTime> availableTimesDict)
    {
        int selectedOption;
        bool isValidOption = false;

        do
        {
            Console.WriteLine("0: Go Back");
            Console.WriteLine("Please enter your selection:");
            string input = Console.ReadLine();

            if (int.TryParse(input, out selectedOption) && availableTimesDict.ContainsKey(selectedOption))
            {
                isValidOption = true;
            }
            else if (input == "0")
            {
                Start();
            }
            else
            {
                Console.WriteLine("Invalid option. Please try again.");
            }
        } while (!isValidOption);

        return availableTimesDict[selectedOption];
    }
    public DateTime GetDate()
    {
        string input;
        DateTime Date = new();

        DateTime today = DateTime.Today;
        DateTime sevenDaysAhead = today.AddDays(7);
        do
        {
            Console.WriteLine($"Please provide the date in the following format: {DateTime.Today.ToShortDateString()}");
            Console.Write("?: > ");
            input = Console.ReadLine() ?? string.Empty;
        }
        while (!DateTime.TryParse(input, out Date));

        if (Date > sevenDaysAhead)
        {
            Console.WriteLine("You can only reserve 7 days ahead\n");
            return GetDate();
        }
        return Date;
    }
}