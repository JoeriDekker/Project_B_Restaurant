public class AdminUI
{
    public void AddEmployee()
    {
        var type = AccountLevel.Employee;
        Console.WriteLine("What their full name?");
        string fullName = Console.ReadLine();
        Console.WriteLine("What is their e-mailaddress?");
        string emailAddress = Console.ReadLine();
        Console.WriteLine("Enter a password:");
        string password = Console.ReadLine();
        Console.WriteLine("Confirm the password:");
        string confirm_password = Console.ReadLine();
        while (password != confirm_password)
        {
            Console.WriteLine("The passwords do not match. Please try again.");
            Console.WriteLine("Enter a password:");
            password = Console.ReadLine();
            Console.WriteLine("Confirm the password:");
            confirm_password = Console.ReadLine();
        }
        int id = accountsLogic.GetLastId() + 1;
        AccountModel acc = new AccountModel(id, emailAddress, password, fullName, type);
        accountsLogic.UpdateList(acc);

        Console.WriteLine("You have succesfulle created an employee account!");
    }
}