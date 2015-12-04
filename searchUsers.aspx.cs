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
using System.Web.UI.HtmlControls;

public partial class searchUsers : System.Web.UI.Page
{
    String usernameInSearchBox="";
    String userId = "";
    
    protected static string tempUserID="";

    private static bool areFriends;
    friendHandler friendHld;

    String currentlyLoggedUserID;
    String currentlyLoggedUserName;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!(HttpContext.Current.User.Identity.IsAuthenticated))
        {

            FormsAuthentication.SignOut();
            Session.Clear();
            Response.Redirect("~/Default");
        }

        currentlyLoggedUserName = HttpContext.Current.User.Identity.Name;
        currentlyLoggedUserID = Membership.GetUser(currentlyLoggedUserName).ProviderUserKey.ToString();

        if (currentlyLoggedUserID == null)
        {
            Response.Redirect("~/Default");
        }

        if (!IsPostBack)
            seachAllUSers.Visible = false;
        userFoundLabel.Visible = false;
        usernameInSearchBox = searchBox.Text;

        friendHld = new friendHandler();
        //check to see if they are friends or not

      


    }//end of searchUsers class



    private bool areTheyFriends(String visitedUserId)
    {
        areFriends = friendHld.checkIfFriend(currentlyLoggedUserID, visitedUserId);

        return areFriends;
    }

    void btn_Click(object sender, EventArgs e)
    {

        string name= ((sender) as Button).ID;
        //send this username to the viewProfile page 
       // string name = usernameInSearchBox;
        Session["name"] = name;
        Response.Redirect("viewProfile.aspx");
    }

    void btnAddFriend_Click(object sender, EventArgs e)
    {

        string friendId = ((sender) as Button).ID;
        bool aretheyfriends = areTheyFriends(friendId);
        if (aretheyfriends)
        {
            friendHld.removeFriend(currentlyLoggedUserID, friendId);
            ((sender) as Button).Text = "Add Friend";
        }
        else
        {
            friendHld.addFriend(currentlyLoggedUserID, friendId);
            ((sender) as Button).Text = "Unfriend";
        }

    }

    private void createTable()
    {

        UserHandler myUserHld = new UserHandler();
        List<student22> studentList = myUserHld.GetAllStudentsList();


        HtmlTable myTable = new HtmlTable();
        myTable.Attributes["class"] = "table";
        foreach (var student in studentList)
         {

            if (!(student.getStudent_id().Equals(currentlyLoggedUserID)))
            {


                HtmlTableRow row = new HtmlTableRow();
                row.Attributes["class"] = "";

                HtmlTableCell cell1 = new HtmlTableCell();
                cell1.Attributes["class"] = "";



                var userNameLabel = new Label();
                userNameLabel.Text = student.getUsername();
                cell1.Controls.Add(userNameLabel);
                //cell1.InnerText = student.getUsername();
                row.Controls.Add(cell1);

                HtmlTableCell cell2 = new HtmlTableCell();
                Image img = new Image();
                img.Attributes["class"] = "media-object";
                img.Height = 100;
                img.Width = 100;
                img.AlternateText = "No image on file";
                img.ImageUrl = "ImageHandler.ashx?UserId=" + student.getStudent_id();
                //add the btn to cell3
                cell2.Controls.Add(img);
                //add cell3 to the row
                row.Controls.Add(cell2);

                HtmlTableCell cell3 = new HtmlTableCell();
                //create button
                Button btn = new Button();
                btn.Attributes["class"] = "btn btn-success";
                btn.Text = "view User Profile";
                btn.ID = student.getUsername();
                //event hadler for button
                btn.Click += new EventHandler(btn_Click);
                //add the btn to cell3
                cell3.Controls.Add(btn);
                //add cell3 to the row
                row.Controls.Add(cell3);

                HtmlTableCell cell4 = new HtmlTableCell();
                //create button
                Button btnAddFriend = new Button();
                btnAddFriend.Attributes["class"] = "btn btn-primary";
                bool aretheyfriends = areTheyFriends(student.getStudent_id());
                if (aretheyfriends)
                {
                    btnAddFriend.Text = "Unfriend";
                }
                else
                {
                    btnAddFriend.Text = "Add Friend";
                }
                btnAddFriend.ID = student.getStudent_id();
                //event hadler for button
                btnAddFriend.Click += new EventHandler(btnAddFriend_Click);
                //add the btn to cell3
                cell4.Controls.Add(btnAddFriend);
                //add cell3 to the row
                row.Controls.Add(cell4);

                //add all rows to table
                myTable.Controls.Add(row);

            }
        }
        PlaceHolder1.Controls.Add(myTable);
    }

    public void search_Click(object sender, EventArgs e)
    {
        //if the user enters anything in the search  box
        if (!(String.IsNullOrEmpty(usernameInSearchBox)))
        {
            //check the entered text to see if it exist in the users table
            storedProcedure myProc = new storedProcedure("checkUserName");
            paramter param1 = new paramter("@theUsername", usernameInSearchBox);
            myProc.addParam(param1);
            //if that username exist the userID will be returned
            userId = myProc.executeScalarParam();

            //if the userId is not null or empty means the user was found
            if (!(String.IsNullOrEmpty(userId)))
            {
                userFoundLabel.Text = "user " + usernameInSearchBox + " found";
                userFoundLabel.Visible = true;

                //send this username to the viewProfile page 
                string name = usernameInSearchBox;
                Session["name"] = name;
                Response.Redirect("viewProfile.aspx");
            }
            else
            {
                userFoundLabel.Text = "" + usernameInSearchBox + " is not a valid username";
                userFoundLabel.Visible = true;
            }

        }
        else
        {
            userFoundLabel.Text ="Enter a username";
            userFoundLabel.Visible = true;
           
        }


    }//end of search_Click

    public void allUSers_Click(object sender, EventArgs e)
    {
        createTable();
        seachAllUSers.Visible = true;
    }

}