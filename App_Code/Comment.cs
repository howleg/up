using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for Comment
/// </summary>
public class Comment
{
    /**********************************************************************
*                   CREATE YOUR CONNECTION STRINGS BELOW               *
**********************************************************************/
    private static String defaultDatabase = WebConfigurationManager.ConnectionStrings["defaultconnection"].ConnectionString;

    /**********************************************************************
    * REPLACE THIS STRING WITH CONNECTIONSTRING FROM YOUR LOCAL DATABASE  *
    **********************************************************************/
    private static String myDatabase = defaultDatabase;

    String userId;

    public Comment(String id)
    {
        userId = id;
    }


    /*


     */

    public bool addComment(String name, int rating, String comment, int courseid)
    {

        bool success = false;

        try {
            SqlConnection con = new SqlConnection(myDatabase);
            SqlCommand cmdAddComment = new SqlCommand("addComment", con);
            SqlDataAdapter da = new SqlDataAdapter(cmdAddComment);
            cmdAddComment.CommandType = CommandType.StoredProcedure;
            cmdAddComment.Parameters.AddWithValue("@userid", userId);
            cmdAddComment.Parameters.AddWithValue("@name", name);
            cmdAddComment.Parameters.AddWithValue("@rating", rating);
            cmdAddComment.Parameters.AddWithValue("@comment", comment);
            cmdAddComment.Parameters.AddWithValue("@courseid", courseid);

            con.Open();
            da.SelectCommand.ExecuteNonQuery();
            con.Close();

            success = true;
        }
        catch
        {
            success = false;
        }

        return success;
    }


    public void removeComment(int currentCourse)
    {
        SqlConnection con = new SqlConnection(myDatabase);

        using (SqlCommand cmdRemoveTaken = new SqlCommand("removeComment", con))
        {
            cmdRemoveTaken.CommandType = CommandType.StoredProcedure;
            cmdRemoveTaken.Parameters.AddWithValue("@userid", userId);
            cmdRemoveTaken.Parameters.AddWithValue("@courseid", currentCourse);
            con.Open();
            try
            {
                cmdRemoveTaken.ExecuteNonQuery();
            }
            catch (SqlException)
            {

            }
        }
        con.Close();
    }





    public DataTable bind(int currentCourse)
    {
      

        SqlConnection con = new SqlConnection(myDatabase);
        SqlCommand cmdBindComment = new SqlCommand("bindComment", con);
        cmdBindComment.CommandType = CommandType.StoredProcedure;
        cmdBindComment.Parameters.AddWithValue("@courseid", currentCourse);
        SqlDataAdapter da = new SqlDataAdapter(cmdBindComment);

        DataTable dt = new DataTable();
        da.Fill(dt);

        

        return dt;

    }

    public int check(int currentCourse)
    {
        SqlConnection con = new SqlConnection(myDatabase);
        SqlCommand cmdCheckComment = new SqlCommand("checkComment", con);
        cmdCheckComment.CommandType = CommandType.StoredProcedure;
        cmdCheckComment.Parameters.AddWithValue("@userid", userId);
        cmdCheckComment.Parameters.AddWithValue("@courseid", currentCourse);
        con.Open();
        int hasComment = Convert.ToInt32(cmdCheckComment.ExecuteScalar());
        con.Close();

        return hasComment;

    }


}