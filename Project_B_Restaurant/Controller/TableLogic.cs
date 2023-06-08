
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public class TableLogic{

    private List<TableModel> _tables;

    //Do i need this?
    static public TableModel? CurrentTable { get; private set; }

    public List<TableModel> Tables { get => _tables; }

    //Get all Tables
    public TableLogic()
    {
        _tables = TableAccess.LoadAll();
    }

    // public bool TableAvailableCheck(string checkTable)
    // {
    //     TableModel? getTable = _tables.Find(x => x.T_ID == checkTable);

    //     if(getTable == null){
    //         return false;
    //     }

    //     return getTable.Occupied;
    // }

    // public List<TableModel> AllAvailabletables(){
    //     List<TableModel> AvailableList = new List<TableModel>();
    //     foreach(TableModel table in _tables){
    //         if(table.Occupied == false){
    //             AvailableList.Add(table);
    //         }
    //     }
    //     return AvailableList;
    // }

    // public TableModel getTable(int peopleAmount){
    //       foreach(TableModel table in _tables){
    //         if(table.Occupied == false && table.T_Seats >= peopleAmount){
    //             return table;
    //         }
    //     }
    //     return null;
    // }
}