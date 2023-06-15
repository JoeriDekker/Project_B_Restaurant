
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public class TableLogic
{

    private List<TableModel> _tables;

    public List<TableModel> Tables { get => _tables; }

    //Get all Tables
    public TableLogic()
    {
        _tables = TableAccess.LoadAll();
    }
}