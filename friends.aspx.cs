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
public partial class friends : System.Web.UI.Page
{

    String currentlyLoggedUserID;
    String currentlyLoggedUserName;

    protected List<student22> ListOFFreinds;
    protected List<String> friendIDList;
    friendHandler friendHld;


    String visitedUserId;
    String visitedUserName;

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

        ListOFFreinds = new List<student22>();
        this.friendIDList = new List<String>();
        friendHld = new friendHandler();

        visitedUserName = "";
        if (Session["visitedUserId"] != null)
        {
            visitedUserId = Session["visitedUserId"].ToString();
            currentlyLoggedUserID = visitedUserId;
        }
        friendIDList = friendHld.getFriendsList(currentlyLoggedUserID);

        foreach (String friendID in friendIDList)
        {
            student22 myFriend = new student22(friendID, 0);
            ListOFFreinds.Add(myFriend);
        }

        createTable();
    }

    void btn_Click(object sender, EventArgs e)
    {

        string name = ((sender) as Button).ID;
        //send this username to the viewProfile page 
        // string name = usernameInSearchBox;
        Session["name"] = name;
        Response.Redirect("viewProfile.aspx");
    }


    private void createTable()
    {
        HtmlTable myTable = new HtmlTable();
        myTable.Attributes["class"] = "table table-hover";
        foreach (var student in ListOFFreinds)
        {
            HtmlTableRow row = new HtmlTableRow();


            HtmlTableCell cell1 = new HtmlTableCell();
            var userNameLabel = new Label();
            userNameLabel.Attributes["class"] = "align-right";
            userNameLabel.Text = student.getUsername();
            cell1.Controls.Add(userNameLabel);
            //cell1.InnerText = student.getUsername();
            row.Controls.Add(cell1);

            HtmlTableCell cell2 = new HtmlTableCell();
            Image img = new Image();
            img.Height = 150;
            img.Width = 150;
            img.AlternateText = "No image on file";
            img.ImageUrl = "ImageHandler.ashx?UserId=" + student.getStudent_id();
            //add the btn to cell3
            cell2.Controls.Add(img);
            //add cell3 to the row
            row.Controls.Add(cell2);

            HtmlTableCell cell3 = new HtmlTableCell();
            //create button
            Button btn = new Button();
            btn.Attributes["class"] = "btn btn-primary";
            btn.Text = "view User Profile";
            btn.ID = student.getUsername();
            //event hadler for button
            btn.Click += new EventHandler(btn_Click);
            //add the btn to cell3
            cell3.Controls.Add(btn);
            //add cell3 to the row
            row.Controls.Add(cell3);

            //add all rows to table
            myTable.Controls.Add(row);


        }
        friendsListHolder.Controls.Add(myTable);
    }

}