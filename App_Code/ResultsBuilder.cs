using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Runtime.Serialization.Json;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.IO;
using System.Web.Configuration;
/// <summary>
/// ResultsBuilder 
/// param: 1. List of completed courses, 2. List of all courses
/// return: List of:
///                 Possible Courses
///                 Recommeded Courses
/// 
/// This class is used to determine which courses are available (pre req have been met), courses where
/// prereqs are not met are given prioritys. Priority is established by finding courses where prereqs are not met,
/// those with the highest number of uncomplete prereqs will be given the highest priority.  We then check frequency of prereqs
/// of highest priority courses, and from there return a list of "recommended classes" or classes that will
/// minimize amount of time in college.
/// </summary>
public class ResultsBuilder
{
    /**********************************************************************
  *                   CREATE YOUR CONNECTION STRINGS BELOW               *
  **********************************************************************/
    private static String reidsDB = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;


    /**********************************************************************
   * REPLACE THIS STRING WITH CONNECTIONSTRING FROM YOUR LOCAL DATABASE  *
   **********************************************************************/
     static String myDatabase = reidsDB;


    SqlConnection con = new SqlConnection(myDatabase);

    List<int> allCourses = new List<int>();       //\ list of all courses
    List<int> takenCourses = new List<int>();  //\ list of complete courses
    List<int> neededCourses = new List<int>();      //\ list of needed courses
    List<int> possibleCourses = new List<int>();     //\ list of needed courses where prereqs are met
    List<int> currentPrereqs = new List<int>();       //\ temp list used to hold pre reqs for current course      
    HashSet<int> needHash = new HashSet<int>();
    HashSet<int> removeHash = new HashSet<int>();    //\ Hashset used to store all courses where pre req
    //\ have not been met
    List<int[]> prereqList = new List<int[]>();
    int[] prereqTemp = new int[2];
    List<String[]> priorityList = new List<String[]>();       //\ list used to hold "priority values"
    public static int[] recommendedCourses = new int[5]; //\ array used to hold the top 5 recommended courses
    String id; //\ <--- testing var
    int[][] sortedRecArray;
    int[][] prereqArray = new int[162][];

    List<String> testList = new List<String>();

    List<int> posTest = new List<int>();

    List<int> testPos = new List<int>();

    List<int> testPre = new List<int>();


    



    //[][] priorityList = new String[5][];


    // constructor param: List of needed courses, List of taken courses
    public ResultsBuilder(List<int> need, List<int> taken)
    {
        //\ init variables
        takenCourses = taken;
        neededCourses = need;
        //posTest = need;
        //\ initializes a hashset will all classes needed
        foreach (int i in neededCourses)
        {
            needHash.Add(i);
        }



    }

    int[] testPrereq = new int[5];

    public int[][] getRecommended()
    {


        //for(int p = 0; p < 5; p++)
        // {
        //     testPrereq[p] = testPos[p];
        //  }

        //return recommendedCourses;
        //return recommendedCourses;
        return sortedRecArray;
    }

    public int[][] getP()
    {
        return prereqArray;
    }



    public void findRecommended()
    {
        List<int> CSAll = new List<int>();

        SqlConnection allCSCon = new SqlConnection(myDatabase);
        using (allCSCon)
        {
            SqlCommand cmd = new SqlCommand("getAllCourses", allCSCon);
            cmd.CommandType = CommandType.StoredProcedure;
            allCSCon.Open();
            using (IDataReader dataReader = cmd.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    int item = Convert.ToInt32(dataReader["course_id"]);
                    CSAll.Add(item);
                }
            }
            allCSCon.Close();
        }//\ END USING





        List<int> electivesList = new List<int>();
        List<int> nonElectives = new List<int>();

        //\ Gets a list of all electives
        using (SqlConnection sqlconn = new SqlConnection(myDatabase))
        {
            SqlCommand cmd = new SqlCommand("getElectives", sqlconn);
            cmd.CommandType = CommandType.StoredProcedure;
            sqlconn.Open();
            using (IDataReader dataReader = cmd.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    int item = Convert.ToInt32(dataReader["course_id"]);
                    electivesList.Add(item);
                }
            }
            sqlconn.Close();
        }

        nonElectives = possibleCourses.Except(electivesList).ToList();

        List<int> importantCourses = new List<int>();

        foreach(int c in possibleCourses)
        {
            if(CSAll.Contains(c))
            {
                importantCourses.Add(c);
            }
        }


        //\ makes an array of form [courseid][numOfPrereqs]
        int[][] recommendedArray = new int[importantCourses.Count()][];
        int iCounter = 0;
        foreach (int p in importantCourses)
        {
            int recCount = 0;
            for (int i = 0; i < prereqArray.Length; i++)
            {
                if(prereqArray[i][0] == p)
                {
                    recCount++;
                }

                
            }

            recommendedArray[iCounter] = new int[] { p, recCount };
            iCounter++;

        }

        sortedRecArray = new int[importantCourses.Count()][];
        sortedRecArray = InsertionSort(recommendedArray);




        int prioritySize = possibleCourses.Count();
        //String[][] priorityArray = new String[prioritySize][];
        Dictionary<int, int> priBook = new Dictionary<int, int>();
        // int i = 0;
        int pri = 0;


        foreach (int s in nonElectives)
        {

            foreach (int[] p in prereqList)
            {
                if (p[1] == s)
                {
                    pri++;
                }
            }
            priBook.Add(s, pri);

        }
        List<KeyValuePair<int, int>> priList = priBook.ToList();
        priList.Sort(
            delegate(KeyValuePair<int, int> firstPair,
            KeyValuePair<int, int> nextPair)
            {
                return firstPair.Value.CompareTo(nextPair.Value);
            });


        int recCounter = 0;
        foreach (KeyValuePair<int, int> kv in priList)
        {
            if (recCounter < 5)
            {
                recommendedCourses[recCounter] = kv.Key;
                recCounter++;
            }
        }



    } //\ END FIND RECOMMENDED


    public int[][] InsertionSort(int[][] intArray)
    {
        //for (int i = 0; i < intArray.Length; i++)
        // {
        // Console.WriteLine(intArray[i]);
        //  }

        int[] temp;
        int j;
        for (int i = 1; i < intArray.Length; i++)
        {
            temp = intArray[i];
            j = i - 1;

            while (j >= 0 && intArray[j][1] < temp[1])
            {
                intArray[j + 1] = intArray[j];
                j--;
            }

            intArray[j + 1] = temp;
        }

        return intArray;
    }



    // getter - returns all courses where requirements are met
    public List<int> getPossible()
    {

        //for (int p = 0; p < 5; p++)
       // {
           // testPos.Add(prereqList[p][0]);
       // }
        //return neededCourses;
        //return testList;
        return possibleCourses;
        // return testPre;
    }



    //\ *****************************************************************************************************
    //\ *****        findPossible Method          ***********************************************************
    //\ *****************************************************************************************************

    public void findPossible()
    {


        //\ opens connection to sql server
        using (SqlConnection sqlconn = new SqlConnection(myDatabase))
        {
            //\ This will check each course you need and see if you meet prereqs
            //  foreach(String s in neededCourses)
            //  {

            SqlCommand cmdCount = new SqlCommand();
            cmdCount.CommandText = "getPrereqCount";
            cmdCount.CommandType = CommandType.StoredProcedure;
            cmdCount.Connection = sqlconn;


            sqlconn.Open();

            int prereqSize = cmdCount.ExecuteNonQuery();





            //\ This cmd will give a list of all prereq for current course
            

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "getPrereqs";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlconn;

            //possibleCourses = neededCourses;

            int prereqCounter = 0;
            //\ adds all prereqs for current couses to list
            using (IDataReader dataReader = cmd.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    //String groupID = Convert.ToString(dataReader["group_id"]);
                    //String courseID = Convert.ToString(dataReader["course_id"]);
                    int groupID = Convert.ToInt32(dataReader["group_id"]);
                    int courseID = Convert.ToInt32(dataReader["course_id"]);
                    prereqTemp[0] = groupID;
                    prereqTemp[1] = courseID;
                    if (!takenCourses.Contains(courseID))
                    {
                        if (neededCourses.Contains(groupID))
                        {
                            posTest.Add(groupID);
                        }
                    }


                    //possibleCourses.Add(groupID);
                    //testPre.Add(prereqTemp[0]);
                    
                     
                        prereqArray[prereqCounter] = new int[] { groupID, courseID };
                    
                    prereqCounter++;
                    prereqList.Add(prereqTemp); //\ a list of current prereqs for current course 's'
                }
            }


            // for(int i = 0; i < prereqArray.Length; i++)
            //  {
            //     testPos.Add(prereqArray[i][0]);
            //}

            sqlconn.Close(); //\ closes current sql connection



            possibleCourses = neededCourses.Except(posTest).ToList();

            // foreach(String s in removeHash)
            //{
            //     needHash.Remove(s);
            // }


            // for(int i = 0; i < 106; i++)
            // {
            //     String currentPre = prereqArray[i][1];
            //   if(!takenCourses.Contains(currentPre))
            //     {
            //        needHash.Remove(prereqArray[i][0]);
            //    }
            // }

            /*

            //\ This removes every course that prereqs are not met, and adds this course
            //\ to the prereqnotmet list
            foreach (String[] p in prereqList)
                {
               
                if (takenCourses.Contains(p[1]))
                    {
                    //testList.Add(p[0]);
                    try
                        {
                       
                           // needHash.Remove(p[0]); //\ after method this will contain all
                            //prereqNotMet.Add(p[0]);        //\ courses in which user mets requirements
                                               //\ do not meet requirements.
                            
                        }
                        catch(NullReferenceException ex)
                        {
                        }
                    }
                else
                {
                    //testList.Add(p[0]);
                }
                }

            */
            // }

        }

        //foreach (int s in possibleCourses)
       // {
       //     possibleCourses.Add(s);
       // }


    }//\ end findPossible




} //\ end CLASS



