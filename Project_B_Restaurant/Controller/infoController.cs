using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System.Text.Json;

class infoController
{
 
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