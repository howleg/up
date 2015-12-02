using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Configuration;
using System.Data.SqlClient;

/// <summary>
/// Summary description for Course
/// </summary>
public class Course
{
    /**********************************************************************
  *                   CREATE YOUR CONNECTION STRINGS BELOW               *
  **********************************************************************/
    private static String reidsDB = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

    /**********************************************************************
    * REPLACE THIS STRING WITH CONNECTIONSTRING FROM YOUR LOCAL DATABASE  *
    **********************************************************************/
    private static String myDatabase = reidsDB;
    SqlConnection con = new SqlConnection(myDatabase);

    int courseId, credit;
    String  courseName, courseTitle;
    List<int> electivesList = new List<int>();


    public Course(String cName)
    {
        courseName = cName;
    }

    public Course()
    {

    }

    public int getCreditValue()
    {
        SqlCommand cmdCredit = new SqlCommand("getCredits", con);
        cmdCredit.CommandType = CommandType.StoredProcedure;
        cmdCredit.Parameters.AddWithValue("@courseName", courseName);
        con.Open();
        credit = Convert.ToInt32(cmdCredit.ExecuteScalar());
        con.Close();

        return credit;
    }

    public List<int> getElectives()
    {

        //\ adds all courses to allList
        SqlConnection eCon = new SqlConnection(myDatabase);
        using (eCon)
        {
            SqlCommand cmd = new SqlCommand("getElectives", eCon);
            cmd.CommandType = CommandType.StoredProcedure;
            eCon.Open();
            using (IDataReader dataReader = cmd.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    int item = Convert.ToInt32(dataReader["course_id"]);
                    electivesList.Add(item);
                }
            }
            eCon.Close();

            return electivesList;
        }

    }

}