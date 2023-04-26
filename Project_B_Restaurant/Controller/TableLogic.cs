
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

    public TableModel getTable(int peopleAmount){
         foreach(TableModel table in _Tables){
            if(table.Occupied == false && table.T_Seats >= peopleAmount){
                return table;
            }
        }
        return null;
    }

    public void OccupiedTable(string checkTable){
    
    bool availableTable = TableAvailableCheck(checkTable);

    if(!availableTable){
        foreach(TableModel table in _Tables){
            if(table.Occupied == false && table.T_ID == checkTable){
                table.Occupied = true;
            }
        }
        // Create a new list with the updated table objects
        List<TableModel> updatedTables = new List<TableModel>(_Tables);

        // Call the WriteAll method to write the updated list back to the JSON file
        TableAccess.WriteAll(updatedTables);    
     }
    }

}