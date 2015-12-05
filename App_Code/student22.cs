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


/// Author: Daniel Dingana
/// <summary>
/// Summary description for student
/// </summary>
public class student22
{
   private static String myDbConString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
   private  String username;
   private String student_id;
    private String profilePic;

   private String actualName;
   private String major;
   private String aboutMe;

   private SqlConnection con;


    public student22(String currentUserName)
    {

        this.username = currentUserName;

        storedProcedure myProc1 = new storedProcedure("getUserId");
        myProc1.addParam(new paramter("@UserName", currentUserName));
        this.student_id = myProc1.executeScalarParam();

        con = new SqlConnection(myDbConString);

        configStudent();
    }

    //this constructor is a little patch so that we can simulate a constructor overiding 
    //because we need to make it possible to create a class using two different types of strings so the int 0 is just to make
    //the distinction between the two 
    public student22(String UserID, int dontCareNum)
    {

        this.student_id = UserID;

        storedProcedure myProc1 = new storedProcedure("getUserNameFromUserID");
        myProc1.addParam(new paramter("@UserID", UserID));
        this.username = myProc1.executeScalarParam();

        con = new SqlConnection(myDbConString);

        configStudent();
    }



    private void configStudent()
    {

        storedProcedure myProc1 = new storedProcedure("getMajorName");
        paramter getMajorNameParam1 = new paramter("@student_id", this.student_id);
        myProc1.addParam(getMajorNameParam1);
        this.major = myProc1.executeScalarParam();


        storedProcedure myProc2 = new storedProcedure("getProfilePic");
        paramter getProfilePicParam1 = new paramter("@UserId", this.student_id);
        myProc2.addParam(getProfilePicParam1);
        this.profilePic = myProc2.executeScalarParam();


        storedProcedure myProc3 = new storedProcedure("getActualName");
        paramter getActualNameParam1 = new paramter("@student_id", this.student_id);
        myProc3.addParam(getActualNameParam1);
        this.actualName = myProc3.executeScalarParam();

        storedProcedure myProc4 = new storedProcedure("getAboutMe");
        paramter getAboutMeParam1 = new paramter("@student_id", this.student_id);
        myProc4.addParam(getAboutMeParam1);
        this.aboutMe = myProc4.executeScalarParam();
    }


    public String getProfilePic()
    {
        return this.profilePic;
    }

    /*
    public void setProfilePic(String pic)
    {
       this.profilePic;
    }
    */
    public String getStudent_id()
    {
        return this.student_id;
    }
    public String getActualName()
    {
        return this.actualName;
    }

    public String getUsername()
    {
        return this.username;
    }

    public String getMajor()
    {
        return this.major;
    }


    public String getAboutMe()
    {
        return this.aboutMe;
    }





    public void setStudentAttributes(String newName,String newSelectedMajor,String newAboutMe)
    {

        if (String.IsNullOrEmpty(newName))
            newName = this.actualName;
        else this.actualName = newName;

        if (String.IsNullOrEmpty(newSelectedMajor))
            newSelectedMajor = this.major;
        else this.major = newSelectedMajor;

        this.aboutMe = newAboutMe;


        paramter param1 = new paramter("@student_id", this.student_id);
        paramter param2 = new paramter("@actualName", newName);
        paramter param3 = new paramter("@major_name", newSelectedMajor);
        paramter param4 = new paramter("@text", newAboutMe);

        storedProcedure myProc = new storedProcedure("updateStudentInfo");
        myProc.addParam(param1);
        myProc.addParam(param2);
        myProc.addParam(param3);
        myProc.addParam(param4);

        myProc.executeNonQueryParam();
    }
}

