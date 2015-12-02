using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for Student
/// </summary>
public class Student
{
    /**********************************************************************
  *                   CREATE YOUR CONNECTION STRINGS BELOW               *
  **********************************************************************/
    private static String defaultDatabase = WebConfigurationManager.ConnectionStrings["defaultconnection"].ConnectionString;

    /**********************************************************************
    * REPLACE THIS STRING WITH CONNECTIONSTRING FROM YOUR LOCAL DATABASE  *
    **********************************************************************/
    private static String myDatabase = defaultDatabase;
    SqlConnection con = new SqlConnection(myDatabase);


    String name, userId, userName, major, aboutMe;
    List<String> coursesNeeded, coursesTaken, coursesAll = new List<String>();
    List<int> takenList = new List<int>();
    List<int> allList = new List<int>();
    List<int> needList = new List<int>();



    public Student(String id)
    {
        if(HttpContext.Current.User.Identity.IsAuthenticated)
        {

            userId = id;
            setStudentName();
            setUserName();
            setStudentMajor();
            //setAboutMe();
            setCourseLists();





        }//\ END IF AUTHENTICATED
        
    }




    /****************************************************************************************
    *****************************************************************************************
    *********************************  METHODS **********************************************
    *****************************************************************************************
    ****************************************************************************************/

    // sets current user's name
    private void setStudentName()
    {
        SqlCommand cmdName = new SqlCommand("getStudentName", con);
        cmdName.CommandType = CommandType.StoredProcedure;
        cmdName.Parameters.AddWithValue("@studentID", userId);

        con.Open();
        name = Convert.ToString(cmdName.ExecuteScalar());
        con.Close();
    }

    // sets current user's major
    private void setStudentMajor()
    {
        SqlCommand cmdMajor = new SqlCommand("getMajor", con);
        cmdMajor.CommandType = CommandType.StoredProcedure;
        cmdMajor.Parameters.AddWithValue("@studentId", userId);

        con.Open();
        major = Convert.ToString(cmdMajor.ExecuteScalar());
        con.Close();
    }

    // sets current user's about me 
    /*
    private void setAboutMe()
    {
        SqlCommand cmdAboutMe = new SqlCommand("getAboutMe", con);
        cmdAboutMe.CommandType = CommandType.StoredProcedure;
        cmdAboutMe.Parameters.AddWithValue("@studentId", userId);

        con.Open();
        aboutMe = Convert.ToString(cmdAboutMe.ExecuteScalar());
        con.Close();
    }*/

    // sets current user's userName
    private void setUserName()
    {
        SqlCommand cmdUserName = new SqlCommand("getUserName", con);
        cmdUserName.CommandType = CommandType.StoredProcedure;
        cmdUserName.Parameters.AddWithValue("@studentId", userId);

        con.Open();
        userName = Convert.ToString(cmdUserName.ExecuteScalar());
        con.Close();
    }

    // fills coursesAll, coursesTaken, and coursesNeeded Lists as Strings
    private void setCourseLists()
    {
        //\ adds all courses_taken for user to takeList
        using (con)
        {
            SqlCommand cmdGetTaken = new SqlCommand("getCourseTaken", con);
            cmdGetTaken.CommandType = CommandType.StoredProcedure;
            cmdGetTaken.Parameters.AddWithValue("@studentid", userId);
            con.Open();
            using (IDataReader dataReader = cmdGetTaken.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    int item = Convert.ToInt32(dataReader["course_id"]);
                    takenList.Add(item);
                }
            }
            con.Close();
        }

        //\ adds all courses to allList
        SqlConnection allCon = new SqlConnection(myDatabase);
        using (allCon)
        {
            SqlCommand cmd = new SqlCommand("getAllCourses", allCon);
            cmd.CommandType = CommandType.StoredProcedure;
            allCon.Open();
            using (IDataReader dataReader = cmd.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    int item = Convert.ToInt32(dataReader["course_id"]);
                    allList.Add(item);
                }
            }
            allCon.Close();
        }//\ END USING

        needList = allList.Except(takenList).ToList();

        coursesAll = getCourseName(allList);
        coursesTaken = getCourseName(takenList);
        coursesNeeded = getCourseName(needList);




    }//\ get set courses

    ///////////////////////////// END SET METHODS //////////////////////////////////////////////////

        ////////////////////////// START GET METHODS ////////////////////////////////////////////////


    //\ private method that converts course_id to course_number
    public List<String> getCourseName(List<int> intList)
    {
        List<String> convertedList = new List<String>();
        String currentCourseName;

        //\ gets course name for possible courses
        SqlConnection conGetName = new SqlConnection(myDatabase);

        SqlCommand cmdGetName = new SqlCommand("getCourseName", conGetName);
        cmdGetName.CommandType = CommandType.StoredProcedure;

        foreach (int c in intList)
        {

            cmdGetName.Parameters.AddWithValue("@courseID", c);
            conGetName.Open();
            currentCourseName = Convert.ToString(cmdGetName.ExecuteScalar());
            convertedList.Add(currentCourseName);
            cmdGetName.Parameters.Clear();
            conGetName.Close();
        }

        return convertedList;
    } // end getCourseName


    //\ private method that converts course_number to course_id
    public List<int> getCourseName(List<String> strList)
    {
        List<int> intChecked = new List<int>();

        SqlConnection conGetID = new SqlConnection(myDatabase);
        SqlCommand cmdGetID = new SqlCommand();

        int idValue;

        foreach (String s in strList)
        {
            cmdGetID.CommandText = "getID";
            cmdGetID.CommandType = CommandType.StoredProcedure;
            cmdGetID.Connection = conGetID;
            cmdGetID.Parameters.AddWithValue("@courseNumber", s);
            conGetID.Open();
            idValue = Convert.ToInt32(cmdGetID.ExecuteScalar());
            cmdGetID.Parameters.Clear();
            intChecked.Add(idValue);
            conGetID.Close();
        }


        return intChecked;
    }

    //\ OVERRIDE METHOD private method that converts course_number to course_id
    public int getCourseName(String str)
    {
       

        SqlConnection conGetID = new SqlConnection(myDatabase);
        SqlCommand cmdGetID = new SqlCommand();

        int idValue;

        
            cmdGetID.CommandText = "getID";
            cmdGetID.CommandType = CommandType.StoredProcedure;
            cmdGetID.Connection = conGetID;
            cmdGetID.Parameters.AddWithValue("@courseNumber", str);
            conGetID.Open();
            idValue = Convert.ToInt32(cmdGetID.ExecuteScalar());
            conGetID.Close();
        


        return idValue;
    }


    //returns count of all courses
    public int getCountAll()
    {
        int totalCourses;

        //\ This calls store procedure to return a count for all courses
        SqlConnection conCountAll = new SqlConnection(myDatabase);
        SqlCommand cmdGetCountAll = new SqlCommand("countAll", conCountAll);
        cmdGetCountAll.CommandType = CommandType.StoredProcedure;

        SqlParameter returnParameter = cmdGetCountAll.Parameters.Add("@return", SqlDbType.Int);
        returnParameter.Direction = ParameterDirection.ReturnValue;

        conCountAll.Open();
        // totalCourses = Convert.ToInt32(cmdGetCountAll.ExecuteScalar());
        cmdGetCountAll.ExecuteNonQuery();
        conCountAll.Close();

        totalCourses = Convert.ToInt32(returnParameter.Value);

        System.Diagnostics.Debug.Write("  all count = " + totalCourses);

        return totalCourses;

    }

    //\ returns count of complete courses
    public int getCountComplete()
    {
        int compCourses;

        //\ This calls store procedure to return a count for complete courses
        SqlConnection conCountComplete = new SqlConnection(myDatabase);

        SqlCommand cmdGetCountComplete = new SqlCommand("countComplete", conCountComplete);
        cmdGetCountComplete.CommandType = CommandType.StoredProcedure;
        cmdGetCountComplete.Parameters.AddWithValue("@studentID", userId);
        SqlParameter returnParameter = cmdGetCountComplete.Parameters.Add("@return", SqlDbType.Int);
        returnParameter.Direction = ParameterDirection.ReturnValue;

        conCountComplete.Open();
        //compCourses = Convert.ToInt32(cmdGetCountComplete.ExecuteScalar());
        cmdGetCountComplete.ExecuteNonQuery();
        conCountComplete.Close();

        compCourses = Convert.ToInt32(returnParameter.Value);

        System.Diagnostics.Debug.Write("compCount = " + compCourses);

        return compCourses;
    }












    //\ returns current user's name
    public String getName()
    {
        System.Diagnostics.Debug.Write("name =  " + name);
        return name;
    }

    //\ returns current user's userName
    public String getUserName()
    {
        return userName;
    }

    //\ returns current user's About Me
    public String getAboutMe()
    {
        return aboutMe;
    }

    //\ returns list of all courses for computer science
    public List<String> getAllCourses()
    {
        return coursesAll;
    }

   

    //\ returns current user's courses needed to graduate
    public List<String> getNeededCourses()
    {
        return coursesNeeded;
    }
    public List<int> getNeededInt()
    {
        return needList;
    }


    //\ returns current user's list of courses completed
    public List<String> getTakenCourses()
    {
        return coursesTaken;
    }

    public List<int> getTakenInt()
    {
        return takenList;
    }

    /****************************************************************************************
    *****************************************************************************************
    **************************  ADD  METHODS   **********************************************
    *****************************************************************************************
    ****************************************************************************************/

    //\ adds a list of courses to course_taken table for current user
    public void addCoursesTaken(List<int> addList)
    {
        con = new SqlConnection(myDatabase);
        foreach (int i in addList)
        {
            using (SqlCommand cmdAddTaken = new SqlCommand("addTaken", con))
            {
                cmdAddTaken.CommandType = CommandType.StoredProcedure;
                cmdAddTaken.Parameters.AddWithValue("@studentid", userId);
                cmdAddTaken.Parameters.AddWithValue("@courseid", i);
                con.Open();
                try
                {
                    cmdAddTaken.ExecuteNonQuery();
                }
                catch (SqlException)
                {

                }
            }
            con.Close();
        }
    }//\ END addCOurses

    //\ removes a list of courses from course_taken table for current user
    public void removeCoursesTaken(List<int> removeList)
    {
        con = new SqlConnection(myDatabase);
        foreach (int i in removeList)
        {
            using (SqlCommand cmdRemoveTaken = new SqlCommand("removeTaken", con))
            {
                cmdRemoveTaken.CommandType = CommandType.StoredProcedure;
                cmdRemoveTaken.Parameters.AddWithValue("@studentid", userId);
                cmdRemoveTaken.Parameters.AddWithValue("@courseid", i);
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
    }

}