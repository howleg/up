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
using System.Collections;
using System.Web.Configuration;


/// Author: Daniel Dingana
/// <summary>
/// Summary description for majors
/// </summary>
public class majors
{
    private static String myDbConString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
     
    private List<String> majorsList;

    public majors()
    {
        this.majorsList = new List<String>();
        configure();

    }

    private void configure()
    {
        storedProcedure myProcReader = new storedProcedure("getAllMajors");
        DataTable dt = myProcReader.executeReader();

       
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    String majorName = dr["major_name"].ToString();
                    this.majorsList.Add(majorName);
                }
            }

        else
            {
                Console.WriteLine("No rows found.");
            }
           

    }

    public List<string> getMajorsList()
    {
        //return deep copy
        List<String> temp = new List<string>();
        foreach (String theMajorName in majorsList)
        {
            temp.Add(theMajorName);
        }
        return temp;
    }



}