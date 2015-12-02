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


public partial class Progress : System.Web.UI.Page
{
   
    //\ creates lists that will hold course and user related values
    List<int> completeCoursesInt = new List<int>();
    List<String> completeCourses = new List<String>();
    List<String> formattedList = new List<String>();
    List<String> remainingCourses = new List<String>();
    String studentName;
    int totalCourses;
    int compCourses;

    public int myProg;


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!(HttpContext.Current.User.Identity.IsAuthenticated))
        {

            FormsAuthentication.SignOut();
            Session.Clear();
            Response.Redirect("~/Default");
        }
        //\ authenticates user
        if (HttpContext.Current.User.Identity.IsAuthenticated)
        {





            //\ gets userName and UserID
            String userName = HttpContext.Current.User.Identity.Name;
            String userId = Membership.GetUser(userName).ProviderUserKey.ToString();
            
            //\ Create student object
            Student student = new Student(userId);
            studentName = student.getName(); //\ returns student's name
            lblStudentName.Text = studentName; //\ sets student's name to label
            completeCourses = student.getTakenCourses(); //\ returns a list of completed courses
            remainingCourses = student.getNeededCourses();


            completeCourses.Sort();
            remainingCourses.Sort();


            //\ adds taken courses to listbox
            foreach (String s in completeCourses)
            {
                completeListBox.Items.Add(s);
            }
            //\ adds taken courses to listbox
            foreach (String s in remainingCourses)
            {
                remainingListBox.Items.Add(s);
            }


            //\ Calculates % of all courses complete
            compCourses = student.getCountComplete();
            totalCourses = student.getCountAll();
           // System.Diagnostics.Debug.Write("compCount = " + compCourses + "  all count = " + totalCourses);

            if (totalCourses < 1)
            {
                myProg = 0;
            }
            else
            {
                myProg = (compCourses * 100) / totalCourses; //\ status bar updater
            }
            
            //\ sets labels
            lblCourseComplete.Text = compCourses.ToString();
            lblCourseRemaining.Text = (totalCourses - compCourses).ToString();
            int allCredits = 108;
            int takenCredits = 0;
            
            foreach(String s in completeCourses)
            {
                //System.Diagnostics.Debug.Write("course = " + s);
                Course course = new Course(s);
                takenCredits += course.getCreditValue();
                //System.Diagnostics.Debug.Write("takenCredits = " + takenCredits.ToString());
            }
            lblCreditComplete.Text = takenCredits.ToString();
            lblCreditRemaining.Text = (allCredits - takenCredits).ToString();

        }
        else
        {
            Response.Redirect("NotLoggedIn.aspx");
            Server.Transfer("NotLoggedIn.aspx");
        }

    } // END PAGE LOAD




}
















/*
 ▄████▄   ▒█████   ██▀███  ▓█████ ▓██   ██▓     ██████  ██▓ ███▄ ▄███▓ ██▓███    ██████  ▒█████   ███▄    █ 
▒██▀ ▀█  ▒██▒  ██▒▓██ ▒ ██▒▓█   ▀  ▒██  ██▒   ▒██    ▒ ▓██▒▓██▒▀█▀ ██▒▓██░  ██▒▒██    ▒ ▒██▒  ██▒ ██ ▀█   █ 
▒▓█    ▄ ▒██░  ██▒▓██ ░▄█ ▒▒███     ▒██ ██░   ░ ▓██▄   ▒██▒▓██    ▓██░▓██░ ██▓▒░ ▓██▄   ▒██░  ██▒▓██  ▀█ ██▒
▒▓▓▄ ▄██▒▒██   ██░▒██▀▀█▄  ▒▓█  ▄   ░ ▐██▓░     ▒   ██▒░██░▒██    ▒██ ▒██▄█▓▒ ▒  ▒   ██▒▒██   ██░▓██▒  ▐▌██▒
▒ ▓███▀ ░░ ████▓▒░░██▓ ▒██▒░▒████▒  ░ ██▒▓░   ▒██████▒▒░██░▒██▒   ░██▒▒██▒ ░  ░▒██████▒▒░ ████▓▒░▒██░   ▓██░
░ ░▒ ▒  ░░ ▒░▒░▒░ ░ ▒▓ ░▒▓░░░ ▒░ ░   ██▒▒▒    ▒ ▒▓▒ ▒ ░░▓  ░ ▒░   ░  ░▒▓▒░ ░  ░▒ ▒▓▒ ▒ ░░ ▒░▒░▒░ ░ ▒░   ▒ ▒ 
  ░  ▒     ░ ▒ ▒░   ░▒ ░ ▒░ ░ ░  ░ ▓██ ░▒░    ░ ░▒  ░ ░ ▒ ░░  ░      ░░▒ ░     ░ ░▒  ░ ░  ░ ▒ ▒░ ░ ░░   ░ ▒░
░        ░ ░ ░ ▒    ░░   ░    ░    ▒ ▒ ░░     ░  ░  ░   ▒ ░░      ░   ░░       ░  ░  ░  ░ ░ ░ ▒     ░   ░ ░ 
░ ░          ░ ░     ░        ░  ░ ░ ░              ░   ░         ░                  ░      ░ ░           ░ 
░ 
*/
