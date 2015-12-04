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
using System.IO;
using System.Web.Configuration;

public partial class profile : System.Web.UI.Page
{
    protected String UserId;
    String currentUserName;
    String studentName;
    String majorName;
    String AboutYourselve;
    student22 currentStudent;

    /**********************************************************************
*                   CREATE YOUR CONNECTION STRINGS BELOW               *
**********************************************************************/
    private static String myDbConString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

    protected List<String> majorsList;


    protected void Page_Load(object sender, EventArgs e)
    {

        if (!(HttpContext.Current.User.Identity.IsAuthenticated))
        {

            FormsAuthentication.SignOut();
            Session.Clear();
            Response.Redirect("~/Default.aspx");
        }
        String currentUserName = HttpContext.Current.User.Identity.Name;
        UserId = Membership.GetUser(currentUserName).ProviderUserKey.ToString();


        if (UserId == null)
        {
            Response.Redirect("~/Default");
        }


        //if (!IsPostBack)   //if this is the first time the page is loading 
        currentStudent = new student22(currentUserName);

        Label2.Visible = false;

        //  if (!IsPostBack) you actually want it to get data every time the page is loaded
        //so comment the postback method instead


        //get information about the currently logged in student
        Image1.ImageUrl = "ImageHandler.ashx?UserId=" + UserId;
        studentName = currentStudent.getActualName();
        majorName = currentStudent.getMajor();
        AboutYourselve = currentStudent.getAboutMe();


        //set labels in front end
        studentNameLabel.Text = studentName;
        usernameLabel.Text = currentUserName;
        majorNameLabel.Text = majorName;

        if (!IsPostBack)   //if this is the first time the page is loading      
            aboutMeBox.Text = AboutYourselve;




        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //get all major names in the major table and add them to the drop down menu called majorsDropDown
        if (!IsPostBack)
        {
            majors majorsObject = new majors();

            majorsList = majorsObject.getMajorsList();

            majorsDropDown.Items.Clear();
            majorsDropDown.Items.Insert(0, new ListItem("Select Major", ""));

            foreach (String theMajorName in majorsList)
            {
                majorsDropDown.Items.Add(theMajorName);
            }



        }//end of if (!IsPostBack)
         /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
         //Image.ImageUrl= currentStudent.getProfilePic();

    }

    private void upload2()
    {
        if (FileUpload1.HasFile)
        {
            //finding size of file
            int length = FileUpload1.PostedFile.ContentLength;
            //creating binary variable of above length
            byte[] imgbyte = new byte[length];
            //saving image in memory
            HttpPostedFile img = FileUpload1.PostedFile;
            //loading image into binary variable
            img.InputStream.Read(imgbyte, 0, length);
            string imgname = "";

            SqlConnection con = new SqlConnection(myDbConString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "updatePic";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UserId", SqlDbType.UniqueIdentifier).Value = UserId;
            cmd.Parameters.Add("@ImageName", SqlDbType.VarChar, 100).Value = imgname;
            cmd.Parameters.Add("@ImageData", SqlDbType.Image).Value = imgbyte;

            cmd.Connection = con;
            int count = 0;
            try
            {
                con.Open();
                //execute the stored procedure 
                count = cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (SqlException)
            {
                Console.WriteLine("Can not open connection ! ");
                con.Close();
            }


            Label2.Visible = true;
            Label2.Text = "Image uploaded succcesfully";
            if (count == 1)
            {
                Image1.ImageUrl = "ImageHandler.ashx?UserId=" + UserId;
            }
        }
    }



    public void Update_Click(object sender, EventArgs e)
    {
        upload2();

        String newName = actualNameBox.Text;
        String newSelectedMajor = majorsDropDown.SelectedItem.Value;
        String aboutMe = aboutMeBox.Text;

        currentStudent.setStudentAttributes(newName, newSelectedMajor, aboutMe);

        studentNameLabel.Text = currentStudent.getActualName();

        majorNameLabel.Text = currentStudent.getMajor();

        aboutMeBox.Text = currentStudent.getAboutMe();


        //reset boxes and dropdown menu
        actualNameBox.Text = string.Empty;
        majorsDropDown.ClearSelection();


        //Response.Redirect(Request.RawUrl);

    }

    public void addUser_Click(object sender, EventArgs e)
    {

        Console.WriteLine("No rows found.");
    }


}
