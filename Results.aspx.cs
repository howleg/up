using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using Microsoft.AspNet.Membership;
using Microsoft.AspNet.Membership.OpenAuth;
using System.Web.Security;
using System.Web.Configuration;

public partial class Results : System.Web.UI.Page
{
    /**********************************************************************
   *                   CREATE YOUR CONNECTION STRINGS BELOW               *
   **********************************************************************/
    private static String defaultDatabase = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;


    /**********************************************************************
   * REPLACE THIS STRING WITH CONNECTIONSTRING FROM YOUR LOCAL DATABASE  *
   **********************************************************************/
    String myDatabase = defaultDatabase;


    List<int> takenList = new List<int>();
    List<int> allList = new List<int>();
    List<int> possibleList = new List<int>();
    List<int> needList = new List<int>();
    List<String> strTakenList = new List<String>();
    List<String> strAllList = new List<String>();
    List<String> strPossibleList = new List<String>();
    List<String> strNeedList = new List<String>();
    int[] recIntList = new int[5];
    String[] recList = new String[5];
    List<String> formattedList = new List<String>();
    Student student;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!(HttpContext.Current.User.Identity.IsAuthenticated))
        {

            FormsAuthentication.SignOut();
            Session.Clear();
            Response.Redirect("~/Default");
        }

        if (HttpContext.Current.User.Identity.IsAuthenticated)
        {



            //\ gets userName and UserID
            String userName = HttpContext.Current.User.Identity.Name;
            String userId = Membership.GetUser(userName).ProviderUserKey.ToString();

            student = new Student(userId);

            strTakenList = student.getTakenCourses();
            takenList = student.getTakenInt();
            strAllList = student.getAllCourses();
            allList = student.getCourseName(strAllList);


            /*
            //\ adds all courses_taken for user to takeList
            using (SqlConnection sqlconn = new SqlConnection(myDatabase))
            {
                SqlCommand cmd = new SqlCommand("getCourseTaken", sqlconn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@studentID", userId);
                sqlconn.Open();
                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        int item = Convert.ToInt32(dataReader["course_id"]);
                        takenList.Add(item);
                    }
                }
                sqlconn.Close();
            }
            */
            /*
            using (SqlConnection sqlconn = new SqlConnection("Data Source=c-lomain\\cssqlserver;Initial Catalog=courseHunter540;Integrated Security=True"))
            {
                SqlCommand cmd = new SqlCommand("getCoursesTaken",sqlconn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@studentID", id);
                sqlconn.Open();
                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    while(dataReader.Read())
                    {
                        String item = Convert.ToString(dataReader["course_id"]);
                        takenList.Add(item);
                    }
                }
                sqlconn.Close();
            }
            */

            /*
            //\ adds all courses to allList
            using (SqlConnection sqlconn = new SqlConnection(myDatabase))
            {
                SqlCommand cmd = new SqlCommand("getAllCourses", sqlconn);
                cmd.CommandType = CommandType.StoredProcedure;
                sqlconn.Open();
                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        int item = Convert.ToInt32(dataReader["course_id"]);
                        allList.Add(item);
                    }
                }
                sqlconn.Close();
            }
            */
            //\ creates need list and Creates resultbuilder object
            needList = student.getNeededInt();
            ResultsBuilder rs = new ResultsBuilder(needList, takenList);
            //\ finds possible courses and inits possible list
            rs.findPossible();
            possibleList = rs.getPossible();
            rs.findRecommended();
            //recIntList = rs.getRecommended();
            int[][] recArray =   rs.getRecommended();


            String currentCourseName;

            //\ gets course name for possible courses
            SqlConnection conGetName = new SqlConnection(myDatabase);

            SqlCommand cmdGetName = new SqlCommand("getCourseName", conGetName);
            cmdGetName.CommandType = CommandType.StoredProcedure;

            foreach (int c in possibleList)
            {

                cmdGetName.Parameters.AddWithValue("@courseID", c);
                conGetName.Open();
                currentCourseName = Convert.ToString(cmdGetName.ExecuteScalar());
                formattedList.Add(currentCourseName);
                cmdGetName.Parameters.Clear();
                conGetName.Close();
            }


            //\ gets course name for recommended courses
            SqlConnection conGetRec = new SqlConnection(myDatabase);

            SqlCommand cmdGetRec = new SqlCommand("getCourseName", conGetRec);
            cmdGetRec.CommandType = CommandType.StoredProcedure;

            for (int i = 0; i < 5; i++)
            {

                cmdGetRec.Parameters.AddWithValue("@courseID", recArray[i][0]);
                conGetRec.Open();
                currentCourseName = Convert.ToString(cmdGetRec.ExecuteScalar());
                recList[i] = currentCourseName;
                cmdGetRec.Parameters.Clear();
                conGetRec.Close();
            }


            rec1.Text = " 1.  " + recList[0];
            rec2.Text = " 2.  " + recList[1];
            rec3.Text = " 3.  " + recList[2];
            rec4.Text = " 4.  " + recList[3];
            rec5.Text = " 5.  " + recList[4];

            //rec1.Text = recIntList[0].ToString();
            // rec2.Text = recIntList[1].ToString();
            //rec3.Text = recIntList[2].ToString();
            //rec4.Text = recIntList[3].ToString();
            // rec5.Text = recIntList[4].ToString();

            // int alength = prereqList.Length;

            // for (int i = 0; i < alength; i++)
            //  {

            //       allPosListBox.Items.Add(prereqList[i][0].ToString());
            //       testList.Items.Add(prereqList[i][1].ToString());
            ////       
            //   }

            formattedList.Sort();
            
            foreach (String i in formattedList)
            {
               allPosListBox.Items.Add(i.ToString());
            }

            foreach (String i in formattedList)
            {
                //allPosListBox.Items.Add(i);
            }

            /*
            using (SqlConnection sqlconn = new SqlConnection("Data Source=c-lomain\\cssqlserver;Initial Catalog=coursehunterdb;Integrated Security=True"))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM course", sqlconn);
                sqlconn.Open();
                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        String item = Convert.ToString(dataReader["course_id"]);
                        allList.Add(item);
                    }
                }
                sqlconn.Close();
            }
            */
            /*
            //\ Creates a list of all courses minus taken courses
            needList = allList.Except(takenList).ToList();
            //\ if you have taken 2 101 level sciences, remove the third
            if (takenList.Contains("BIOL U101") && takenList.Contains("CHEM U111") ||
                takenList.Contains("BIOL U101") && takenList.Contains("PHYS U211") ||
                takenList.Contains("CHEM U112") && takenList.Contains("PHYS U211"))
            {
                try
                {
                    needList.Remove("BIOL U101");
                    needList.Remove("CHEM U111");
                    needList.Remove("PHYS U211");
                }
                catch (NullReferenceException)
                {

                }
            }
            //\ if you have taken 1 102 level science, remove the other two
            if (takenList.Contains("BIOL U102") || takenList.Contains("CHEM U112") || takenList.Contains("PHYS U212"))
            {
                try
                {
                    needList.Remove("BIOL U102");
                    needList.Remove("CHEM U112");
                    needList.Remove("PHYS U212");
                }
                catch(NullReferenceException)
                {

                }
            }

            //\ NEED TO ADD CODE FOR OTHER COURSES THAT HAVE MULTIPLE OPTIONS OR REMOVE SOME OTHER WAY

            //\ creates resultsbuilder object
            ResultsBuilder rs = new ResultsBuilder(needList, takenList);
            //\ finds possible courses and inits possible list
            rs.findPossible();
            possibleList = rs.getPossible();
            rs.findRecommended();
            recList = rs.getRecommended();

            //\ this code is removing 
            if(!takenList.Contains("BIOL U101"))
            {
                possibleList.Remove("BIOL U102");
            }
            if (!takenList.Contains("CHEM U111"))
            {
                possibleList.Remove("CHEM U112");
            }

            HashSet<String> myhash = new HashSet<string>();
            foreach (String s in needList)
            {
                myhash.Add(s);
            }

            rec1.Text = recList[0];
            rec2.Text = recList[1];
            rec3.Text = recList[2];
            rec4.Text = recList[3];
            rec5.Text = recList[4];

            foreach (String s in possibleList)
            {
                allPosListBox.Items.Add(s);
            }

            */
        }
        else
        {
            Response.Redirect("NotLoggedIn.aspx");
            Server.Transfer("NotLoggedIn.aspx");
        }
    }
}