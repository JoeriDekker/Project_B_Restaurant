
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public class TableLogic{

    private List<TableModel> _Tables;

    //Do i need this?
    static public TableModel? CurrentTable { get; private set; }

    public List<TableModel> Tables { get => _Tables; }

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


public void OccupiedTable(string checkTable, bool setBool, string date, string time)
{
    _Tables = TableAccess.LoadAll();
    TableModel foundTable = _Tables.Find(x => x.T_ID == checkTable && !x.Occupied);

    if (foundTable != null)
    {
        foundTable.Occupied = setBool;

        if (foundTable.ReservedTime == null)
        {
            foundTable.ReservedTime = new Dictionary<string, List<string>>();
        }

        // Create a new list for the time
        List<string> timeList = new List<string> { time };

        // Check if the date already exists in the ReservedTime dictionary
        if (foundTable.ReservedTime.ContainsKey(date))
        {
            // Add the time list to the existing date key
            foundTable.ReservedTime[date].AddRange(timeList);
        }
        else
        {
            // Create a new entry for the date and add the time list
            foundTable.ReservedTime[date] = timeList;
        }

        // Update the table in the list
        int index = _Tables.IndexOf(foundTable);
        _Tables[index] = foundTable;

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