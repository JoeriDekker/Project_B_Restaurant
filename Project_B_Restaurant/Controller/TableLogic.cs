
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public class TableLogic{

    private List<TableModel> _Tables;

    //Do i need this?
    static public TableModel? CurrentTable { get; private set; }

    //Get all Tables
    public TableLogic()
    {
        _Tables = TableAccess.LoadAll();
    }

    public bool TableAvailableCheck(string checkTable)
    {
        TableModel? getTable = _Tables.Find(x => x.T_ID == checkTable);

        if(getTable == null){
            return false;
        }

        return getTable.Occupied;
    }

    public List<TableModel> AllAvailabletables(){
        List<TableModel> AvailableList = new List<TableModel>();
        foreach(TableModel table in _Tables){
            if(table.Occupied == false){
                AvailableList.Add(table);
            }
        }
        return AvailableList;
    }

}