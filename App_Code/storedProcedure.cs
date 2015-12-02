using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

/// <summary>
/// Summary description for storedProcedure
/// </summary>
public class storedProcedure
{
    private static String myDbConString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

    private String procName;
    private List<paramter> paramList;

    private SqlConnection con;

    public storedProcedure(String procName)
    {
        con = new SqlConnection(myDbConString);

        this.procName = procName;        
        this.paramList = new List<paramter>();
    }

    public storedProcedure(String procName, List<paramter> myParams)
    {
        this.paramList = new List<paramter>();
        this.paramList = myParams;
        this.procName = procName;
        con = new SqlConnection(myDbConString);
    }

    public void addParam(paramter para)
    {
        paramList.Add(para);
    }

    public bool executeNonQueryParam()
    {
        int res = 0;
        SqlCommand cmd = new SqlCommand();
            cmd.CommandText = this.procName;
            cmd.CommandType = CommandType.StoredProcedure;

            if (paramList.Count > 0)
                for (int i = 0; i < paramList.Count; i++)
                {
                    cmd.Parameters.AddWithValue(paramList[i].getColumnName(), paramList[i].getColumnValue());
                }

            cmd.Connection = con;
            try
            {
                con.Open();
            //execute the stored procedure 
            res = cmd.ExecuteNonQuery();
            con.Close();
            }
            catch (SqlException)
            {
                Console.WriteLine("Can not open connection ! ");
                con.Close();
            }

        if (res >= 1)
        {
            return true;
        }
        return false;

    }

    public String executeScalarParam()
    {
        String attribute = "";
            SqlCommand cmd1 = new SqlCommand();
            cmd1.CommandText = this.procName;
            cmd1.CommandType = CommandType.StoredProcedure;

            if (paramList.Count > 0)
                for (int i = 0; i < paramList.Count; i++)
                {
                    cmd1.Parameters.AddWithValue(paramList[i].getColumnName(), paramList[i].getColumnValue());
                }

            cmd1.Connection = con;
            try
            {
                      con.Open();

                attribute = Convert.ToString(cmd1.ExecuteScalar());

                con.Close();

                return attribute;
            }
            catch (SqlException)
            {
                Console.WriteLine("Can not open connection ! ");
                con.Close();
                return "";
            }

        }
    

    public DataTable executeReader()
    {
         
            SqlCommand cmd2 = new SqlCommand();
            cmd2.CommandText = this.procName;
            cmd2.CommandType = CommandType.StoredProcedure;

            if (paramList.Count > 0)
                for (int i = 0; i < paramList.Count; i++)
                {
                    cmd2.Parameters.AddWithValue(paramList[i].getColumnName(), paramList[i].getColumnValue());
                }

            cmd2.Connection = con;          

            con.Open();
            SqlDataReader reader = cmd2.ExecuteReader();

            //create datatale so store table in reader in it. because a variable of type SqlDataReader cannot
            //be returned outside its method of declaration since it is tied to the sql connection that created it.
            //if this connection is not live the reader is not live as well and so closed and cant  be accessed.
            DataTable dt = new DataTable();
            dt.Load(reader);
            con.Close();
            return dt;           

        }
      

}