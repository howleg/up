using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


/// Author: Daniel Dingana
/// 
/// <summary>
/// Summary description for paramter
/// </summary>
public class paramter
{
    private String columnName;
    private String columnValue;
    private string v;   
    private byte[] imageArray;

    public paramter(String columnName, String columnValue)
    {
        this.columnName = columnName;
         this.columnValue = columnValue;
    }

    public paramter(String columnName, byte[] imageArray)
    {
        this.columnName = columnName;
        this.imageArray = imageArray;
    }

    public String getColumnName()
    {
        return this.columnName;
    }

    public void setColumnName(String columnName)
    {
        this.columnName = columnName;
    }

    public String getColumnValue()
    {
        return this.columnValue;
    }

    public void setColumnValue(String columnValue)
    {
        this.columnValue = columnValue;
    }
}