using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System.Text.Json;


public class AccountsLogic
{
    private List<AccountModel> _accounts;

    //Static properties are shared across all instances of the class
    //This can be used to get the current logged in account from anywhere in the program
    //private set, so this can only be set by the class itself
    static public AccountModel? CurrentAccount { get; set; }

    public AccountsLogic()
    {
        _accounts = AccountsAccess.LoadAll();
    }

    public List<AccountModel> GetAccountModels(){
        return _accounts;
    }

    public void UpdateList(AccountModel acc)
    {
        //Find if there is already an model with the same id
        int index = _accounts.FindIndex(s => s.Id == acc.Id);

        if (index != -1)
        {
            //update existing model
            _accounts[index] = acc;
        }
        else
        {
            //add new model
            _accounts.Add(acc);
        }
        AccountsAccess.WriteAll(_accounts);

    }

    public AccountModel? GetById(int id)
    {
        return _accounts.Find(i => i.Id == id);
    }

    public AccountModel? GetByEmail(string email)
    {
        return _accounts.Find(i => i.EmailAddress == email);
    }
    public AccountModel? CheckLogin(string email, string password)
    {
        try
        {
            CurrentAccount = _accounts.Find(i => i.EmailAddress == email && i.Password == password);
            return CurrentAccount;
        }
        catch (ArgumentNullException)
        {
            Console.WriteLine("Account not Found");
            return null;
        }
    }
    public static string Encrypt(string password)
    {
        string secret_key = "Imnotverysecret";

        byte[] key = Encoding.UTF8.GetBytes(password);
        byte[] secret = Encoding.UTF8.GetBytes(secret_key);

        // XOR the key and secret key
        for (int i = 0; i < key.Length; i++)
        {
            key[i] = (byte)(key[i] ^ secret[i % secret.Length]);
        }
        // return the password
        return Convert.ToBase64String(key);
    }

    public static string Decrypt(string password)
    {
        string secret_key = "Imnotverysecret";

        byte[] key = Convert.FromBase64String(password);
        byte[] secret = Encoding.UTF8.GetBytes(secret_key);

        // XOR the key and secret key
        for (int i = 0; i < key.Length; i++)
        {
            key[i] = (byte)(key[i] ^ secret[i % secret.Length]);
        }
        // return the password
        return Encoding.UTF8.GetString(key);
    }
    public void RemoveReservationCode(string code)
    {
        foreach (AccountModel acc in _accounts)
            if (acc.Reservations.Contains(code))
                acc.Reservations.Remove(code);
    }

    public int GetLastId()
    {
        AccountModel last = _accounts.Last();
        return last.Id;
    }
}




