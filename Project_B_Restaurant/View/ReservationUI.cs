// public class ReservationUI : UI
// {

//     public override string Header
//     {
//         get => @"
//     _____                                _   _
//     |  __ \                              | | (_)
//     | |__) |___  ___  ___ _ ____   ____ _| |_ _  ___  _ __  ___
//     |  _  // _ \/ __|/ _ \ '__\ \ / / _` | __| |/ _ \| '_ \/ __|
//     | | \ \  __/\__ \  __/ |   \ V / (_| | |_| | (_) | | | \__ \
//     |_|  \_\___||___/\___|_|    \_/ \__,_|\__|_|\___/|_| |_|___/
//     ============================================================
//     ";
//     }
//     // This will initialize the Reservation System (Module)
//     public ReservationUI(UI previousUI) : base(previousUI)
//     {
//     }

//     public static void initReserve()
//     {
//         // Initiatlize the RLogic 
//         ReservationLogic RLogic = new ReservationLogic();

//         // Reservation Menu
//         while (true)
//         {
//             Console.WriteLine("[1] Create Reservation");
//             if (AccountsLogic.CurrentAccount.Type == "Customer")
//             {
//                 Console.WriteLine("[2] Go Back");
//             }
//             if (AccountsLogic.CurrentAccount.Type == "Admin")
//             {
//                 Console.WriteLine("[2] Show all Reservations");
//                 Console.WriteLine("[3] Find Reservation by reservation ID");
//                 Console.WriteLine("[4] Find Reservation by table ID");
//             }
//             // Console.WriteLine($@"[1] Create Reservation
//             //     [2] Show all Reservations
//             //     [3] Find Reservation by reservation ID
//             //     [4] Find Reservation by table ID
//             // ");
//             string userInp = Console.ReadLine();
//             // No error handling : 2444VIK
//             if (userInp == "1")
//             {
//                 Console.WriteLine("Please enter a Name:");
//                 string inp_name = Console.ReadLine();
//                 Console.WriteLine("Please enter amount of People");
//                 int inp_Pamount = Convert.ToInt32(Console.ReadLine());
//                 RLogic.CreateReservation(inp_name, inp_Pamount);
//                 Console.WriteLine("Reservation has been made!");
//                 // Initialize pre order module ( UI )
//                 PreOrder preOrd = new PreOrder();
//                 preOrd.Start();
//             }
//             else if (userInp == "2")
//             {

//                 if (AccountsLogic.CurrentAccount.Type == "Admin")
//                 {
//                                                         Console.WriteLine(@$"
//   ____                                 _   _                 
//   |  _ \ ___  ___  ___ _ ____   ____ _| |_(_) ___  _ __  ___ 
//   | |_) / _ \/ __|/ _ \ '__\ \ / / _` | __| |/ _ \| '_ \/ __|
//   |  _ <  __/\__ \  __/ |   \ V / (_| | |_| | (_) | | | \__ \
//   |_| \_\___||___/\___|_|    \_/ \__,_|\__|_|\___/|_| |_|___/
//  =============================================================");
//     // Get all reservations || Create Table || String formatiing c:
//     Console.WriteLine("{0,-5} {1,-20} {2,-20} {3,-10} {4,-10}", "R_ID", "Contact", "R_time", "R_TableID", "P_Amount");
//         foreach (ReservationModel Res in RLogic.GetAllReservations())
//         {
//             Console.WriteLine("{0,-5} {1,-20} {2,-20} {3,-10} {4,-10}", Res.R_Id, Res.Contact, Res.R_time, Res.R_TableID, Res.P_Amount);
//         }
                
//                 }
//                 else
//                 {
//                     break;
//                 }
//             }
//             else if (userInp == "3")
//             {
//                 Console.WriteLine("Please enter a Reservation ID:");
//                 int IDinp = Convert.ToInt32(Console.ReadLine());
//                 ReservationModel ReservationMUD = RLogic.getReservationByID(IDinp);
//                 if (ReservationMUD == null)
//                 {
//                     Console.WriteLine("Cannot be found");
//                 }
//                 else
//                 {
//                     Console.WriteLine($"Name: {ReservationMUD.Contact} | Party amount: {ReservationMUD.P_Amount} | TableID: {ReservationMUD.R_TableID} | ReservationID: {ReservationMUD.R_Id}");
//                 }

//             }
//             else if (userInp == "4")
//             {
//                 Console.WriteLine("Please enter a Table ID:");
//                 int IDinp = Convert.ToInt32(Console.ReadLine());
//                 ReservationModel ReservationMUD = RLogic.getReservationByTableID(IDinp);
//                 if (ReservationMUD == null)
//                 {
//                     Console.WriteLine("Cannot be found");
//                 }
//                 else
//                 {
//                     Console.WriteLine($"Name: {ReservationMUD.Contact} | Party amount: {ReservationMUD.P_Amount} | TableID: {ReservationMUD.R_TableID} | ReservationID: {ReservationMUD.R_Id}");
//                 }

//             }


//         }
//     }
// }