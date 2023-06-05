
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


    public void OccupiedTable(string checkTable, bool setBool, string time)
    {
        _Tables = TableAccess.LoadAll();
        TableModel foundTable = _Tables.Find(x => x.T_ID == checkTable && !x.Occupied);

        if (foundTable != null)
        {
            foundTable.Occupied = setBool;

             // Add a string to the ReservedTime array
            foundTable.ReservedTime.Add(time); // Replace "12:00 PM" with your desired string

            // Update the table in the list
            int index = _Tables.IndexOf(foundTable);
            _Tables[index]= foundTable;

            // Save the updated list to the JSON file
            TableAccess.WriteAll(_Tables);
        }
    }

    public void RestoreOccupiedTable(string checkTable){
        _Tables = TableAccess.LoadAll();
        TableModel foundTable = _Tables.Find(x => x.T_ID == checkTable && x.Occupied);

        if (foundTable != null)
        {
            // Update the table in the list
            int index = _Tables.IndexOf(foundTable);
            _Tables[index].Occupied = false;

            // Save the updated list to the JSON file
            TableAccess.WriteAll(_Tables);
        }
    }
}