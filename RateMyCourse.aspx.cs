using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using Microsoft.AspNet.Membership;
using System.Web.Security;
using Microsoft.AspNet.Membership.OpenAuth;
using System.Web.Configuration;



public partial class RateMyCourse : System.Web.UI.Page
{
    
    String userId;
    int currentRating = 0;
    List<int> allList = new List<int>();
    String item;
    List<String> formattedList = new List<String>();
    List<String> allCourses = new List<String>();
    String strCurrentCourse;
    int currentCourse;
    Student student;
    Comment comment;
   
    


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
            String currentUserName = HttpContext.Current.User.Identity.Name;
            userId = Membership.GetUser(currentUserName).ProviderUserKey.ToString();

            student = new Student(userId);
            comment = new Comment(userId);

           
            allCourses = student.getAllCourses();
            allCourses.Sort();

            foreach(String s in allCourses)
            {
                courseDropBox.Items.Add(s);
            }

            

            //\sets currentcourse to whats selected in dropbox on load
            strCurrentCourse = courseDropBox.SelectedValue;
            currentCourse = student.getCourseName(strCurrentCourse);


           
        }
        else
        {
            Response.Redirect("NotLoggedIn.aspx");
            Server.Transfer("NotLoggedIn.aspx");
        }


            if (!IsPostBack)
        {
            BindComment();
        }
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        
            //\ gets current rating
            currentRating = Rating1.CurrentRating;
            
            //\ adds comment, returns true if successful
            bool success = comment.addComment(txtName.Text, currentRating, txtComment.Text, currentCourse);

            //\ notify user
            if (success == true)
            {
                lblmessage.Text = "Comment posted successfully.";
                BindComment();
            }
        else { lblmessage.Text = "Error: Comment not posted!"; }
        
            
        
        clearLabels();

    }






    //\ this created datatable full of comments for current course
    //\ ui uses this to display comments dynamically
    private void BindComment()
    {
        DataTable dt = new DataTable();

        dt = comment.bind(currentCourse);


        if (dt.Rows.Count > 0)
        {
            gdvUserComment.Visible = true;
            gdvUserComment.DataSource = dt;
            gdvUserComment.DataBind();


            

        }
        else
        {
            gdvUserComment.Visible = false;
        }
        checkComment();
        
    }//\ end bindcomment



    //\ displays "remove" button when user has left a comment for current course
    private void checkComment()
    {

        int hasComment = comment.check(currentCourse);
        
        if(hasComment > 0)
        {
            btnRemove.Visible = true;
        }
        else
        {
            btnRemove.Visible = false;
        }

    }


    //\ rebinds ui to new course if user changes drop box
    protected void courseDropBox_SelectedIndexChanged(object sender, EventArgs e)
    {


        //\sets currentcourse to whats selected in dropbox on load

        strCurrentCourse = courseDropBox.SelectedItem.Text;

       
        currentCourse = student.getCourseName(strCurrentCourse);
        
        BindComment();
        clearLabels();


    }

    




    //\ this uses magic to dispell evil comments
    protected void btnRemove_Click(object sender, EventArgs e)
    {
        comment.removeComment(currentCourse);
        BindComment();
        clearLabels();
        
    }

    //\ good old label reset
    private void clearLabels()
    {
        txtName.Text = "";
        txtComment.Text = "";
        Rating1.CurrentRating = 0;
    }

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
