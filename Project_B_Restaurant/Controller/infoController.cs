using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System.Text.Json;

class infoController
{
 
    //Static properties are shared across all instances of the class
    //This can be used to get the current logged in account from anywhere in the program
    //private set, so this can only be set by the class itself
    public InfoModel Restaurant { get; private set; }

    public infoController()
    {
        Restaurant = InfoAccess.LoadAll()[0];
    }

    public void UpdateInfo()
    {
        InfoAccess.WriteAll(Restaurant);
    }
}