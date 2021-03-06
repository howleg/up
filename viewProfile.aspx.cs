﻿using System;
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

public partial class viewProfile : System.Web.UI.Page
{
    private static bool areFriends;
    int numOfMutualFriends;
    friendHandler friendHld;

    String currentlyLoggedUserID;
    String currentlyLoggedUserName;

    String visitedUserId;
    String visitedUserName;
    student22 visitedStudent;

    String studentName;
    String majorName;
    String AboutYourselve;

    public static String name = "";

    private static String myDbConString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!(HttpContext.Current.User.Identity.IsAuthenticated))
        {

            FormsAuthentication.SignOut();
            Session.Clear();
            Response.Redirect("~/Default");
        }
        //getting information about currently logged in user
        currentlyLoggedUserName = HttpContext.Current.User.Identity.Name;
        currentlyLoggedUserID = Membership.GetUser(currentlyLoggedUserName).ProviderUserKey.ToString();

        if (currentlyLoggedUserID == null)
        {
            Response.Redirect("~/Default");
        }

        //recieve a username from another page (from 
        visitedUserName = "";
        if (Session["name"] != null)
        {
            visitedUserName = Session["name"].ToString();
            visitedStudent = new student22(visitedUserName);
            visitedUserId = visitedStudent.getStudent_id();
        }
        else
            Response.Redirect("~/Default");

        friendHld = new friendHandler();

        if (currentlyLoggedUserName.Equals(visitedUserName))
        {
            btnFriends.Text = "Your friends";
            btnMutalFriends.Visible = false;
            btnAddFriend.Visible = false;
        }
        else
        {
            btnFriends.Text = visitedUserName + "'s friends";

            numOfMutualFriends = friendHld.getNumberOfMutualFriends(currentlyLoggedUserID, visitedUserId);
            btnMutalFriends.Text = "" + numOfMutualFriends + " Mutual friends";
            //check to see if they are friends or not
            //btnAddFriend.Text = "Unfriend" if they are;
            areTheyFriends();
        }


        //for debugging 
        System.Diagnostics.Debug.WriteLine("userName is = " + visitedUserName);



        Image1.ImageUrl = "ImageHandler.ashx?UserId=" + visitedUserId;
        //get information about the currently logged in student
        studentName = visitedStudent.getActualName();
        majorName = visitedStudent.getMajor();
        AboutYourselve = visitedStudent.getAboutMe();


        //set labels in front end
        studentNameLabel.Text = studentName;
        usernameLabel.Text = visitedUserName;
        majorNameLabel.Text = majorName;
        //summaryLabelBox.Text = AboutYourselve;

        summaryLabel.Text = AboutYourselve;


        //make the h3 tag change dynamically when the page is loaded to reflect change in username
        messageh3.InnerText = "Send a message to: " + visitedUserName;

        //dynamically change the width if 
        /*
        int charRows = 0;
        string tbCOntent;
        int chars = 0;
        tbCOntent = AboutYourselve;
        summaryLabelBox.Columns = 60;
        chars = tbCOntent.Length;
        charRows = chars / summaryLabelBox.Columns;
        int remaining = chars - charRows * summaryLabelBox.Columns;
        if (remaining == 0)
        {
            summaryLabelBox.Rows = charRows;
            summaryLabelBox.TextMode = TextBoxMode.MultiLine;
        }
        else
        {
            summaryLabelBox.Rows = charRows + 1;
            summaryLabelBox.TextMode = TextBoxMode.MultiLine;
        }
        */
    }

    private bool areTheyFriends()
    {
        areFriends = friendHld.checkIfFriend(currentlyLoggedUserID, visitedUserId);
        if (areFriends)
        {
            btnAddFriend.Text = "Unfriend";
        }
        else
        {
            btnAddFriend.Text = "Add Friend";
        }

        return areFriends;
    }


    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    public void addFriend_Click(object sender, EventArgs e)
    {
        areFriends = areTheyFriends();

        if (areFriends)
        {
            friendHld.removeFriend(currentlyLoggedUserID, visitedUserId);
        }
        else
        {
            friendHld.addFriend(currentlyLoggedUserID, visitedUserId);
        }

        areTheyFriends();
    }
    public void Message_Click(object sender, EventArgs e)
    {
        DivMessage.Visible = true;

    }

    public void Cancel_Click(object sender, EventArgs e)
    {
        DivMessage.Visible = false;

    }

    public void Send_Click(object sender, EventArgs e)
    {

        MessageHandler msgHandler = new MessageHandler();
        if (!(String.IsNullOrEmpty(messageBox.Text)))
            msgHandler.SendMessage(currentlyLoggedUserID, visitedUserId, "", Server.HtmlEncode(messageBox.Text));
        messageBox.Text = string.Empty;
        Response.Redirect(Request.RawUrl);
    }

    public void btnFriends_Click(object sender, EventArgs e)
    {

        Session["id"] = visitedStudent.getStudent_id();
        Response.Redirect("friends.aspx");

    }

    public void btnMutualFriends_Click(object sender, EventArgs e)
    {

        Session["mutualClick"] = visitedStudent.getStudent_id();
        Response.Redirect("friends.aspx");

    }



}