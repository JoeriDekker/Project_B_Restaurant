using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System.Text.Json;

class infoController
{
    private static List<InfoModel> _info;

    //Static properties are shared across all instances of the class
    //This can be used to get the current logged in account from anywhere in the program
    //private set, so this can only be set by the class itself
    static public InfoModel? Info { get; private set; }

    public infoController()
    {
        _info = InfoAccess.LoadAll();
    }

    public static InfoModel getInfo()
    {
        Console.WriteLine($"test1{_info} ss");
        return _info[0];
    }
    public void UpdateInfo()
    {
    //    _info[0][ToChange] = Value;
    //    Console.WriteLine(_info);
    //    InfoAccess.WriteAll(_info);
    }
}