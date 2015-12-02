using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Configuration;
using Microsoft.AspNet.Membership;
using Microsoft.AspNet.Membership.OpenAuth;
using System.Web.Security;
using System.Web.Configuration;

public partial class AddRemoveCourse : System.Web.UI.Page
{
    private String userId;

    char[][] transferArray = new char[99][];
    String[] checkedArray;
    List<String> checkedList = new List<String>();
    public List<String> courseList = new List<String>();
    public List<String> neededList = new List<String>();
    List<String> preReqList = new List<String>();
    Student student;

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!(HttpContext.Current.User.Identity.IsAuthenticated))
        {

            FormsAuthentication.SignOut();
            Session.Clear();
            Response.Redirect("~/Default");
        }

        //\ checks if current user is registered
        if (HttpContext.Current.User.Identity.IsAuthenticated)
        {

            //\ gets userName and UserID
            String currentUserName = HttpContext.Current.User.Identity.Name;
            userId = Membership.GetUser(currentUserName).ProviderUserKey.ToString();

            student = new Student(userId);

           
            



            //\ adds optional courses to dropbox
            ns101DropBox.Items.Add("BIOL U101");
            ns102DropBox.Items.Add("BIOL U102");
            ns101DropBox.Items.Add("CHEM U111");
            ns102DropBox.Items.Add("CHEM U112");
            ns101DropBox.Items.Add("PHYS U211");
            ns102DropBox.Items.Add("PHYS U212");
            ns103DropBox.Items.Add("BIOL U101");
            ns103DropBox.Items.Add("CHEM U111");
            ns103DropBox.Items.Add("PHYS U211");

            art101DropBox.Items.Add("AFAM U204");
            art101DropBox.Items.Add("ARTH U101");
            art101DropBox.Items.Add("ARTH U105");
            art101DropBox.Items.Add("ARTH U106");
            art101DropBox.Items.Add("MUSC U110");
            art101DropBox.Items.Add("MUSC U140");
            art101DropBox.Items.Add("THEA U161");
            art101DropBox.Items.Add("THEA U170");

            his101DropBox.Items.Add("HIST U101");
            his101DropBox.Items.Add("HIST U102");
            his101DropBox.Items.Add("HIST U105");
            his101DropBox.Items.Add("HIST U106");

            hum101DropBox.Items.Add("AMST U101");
            hum101DropBox.Items.Add("AMST U102");
            hum101DropBox.Items.Add("ENGL U250");
            hum101DropBox.Items.Add("ENGL U252");
            hum101DropBox.Items.Add("ENGL U275");
            hum101DropBox.Items.Add("ENGL U279");
            hum101DropBox.Items.Add("ENGL U280");
            hum101DropBox.Items.Add("ENGL U283");
            hum101DropBox.Items.Add("ENGL U289");
            hum101DropBox.Items.Add("ENGL U290");
            hum101DropBox.Items.Add("ENGL U291");
            hum101DropBox.Items.Add("FILM U240");
            hum101DropBox.Items.Add("PHIL U102");
            hum101DropBox.Items.Add("PHIL U211");
            hum101DropBox.Items.Add("RELG U103");
            hum101DropBox.Items.Add("AFAM U204");
            hum101DropBox.Items.Add("ARTH U101");
            hum101DropBox.Items.Add("ARTH U105");
            hum101DropBox.Items.Add("ARTH U106");
            hum101DropBox.Items.Add("MUSC U110");
            hum101DropBox.Items.Add("MUSC U140");
            hum101DropBox.Items.Add("THEA U161");
            hum101DropBox.Items.Add("THEA U170");


            for101DropBox.Items.Add("SPAN U101");
            for101DropBox.Items.Add("CHIN U101");
            for101DropBox.Items.Add("FREN U101");
            for101DropBox.Items.Add("GERM U101");
            for101DropBox.Items.Add("ASLG U101");

            soc101DropBox.Items.Add("AFAM U201");
            soc101DropBox.Items.Add("ANTH U102");
            soc101DropBox.Items.Add("ECON U221");
            soc101DropBox.Items.Add("ECON U222");
            soc101DropBox.Items.Add("GEOG U101");
            soc101DropBox.Items.Add("GEOG U103");
            soc101DropBox.Items.Add("POLI U101");
            soc101DropBox.Items.Add("POLI U200");
            soc101DropBox.Items.Add("POLI U320");
            soc101DropBox.Items.Add("PSYC U101");
            soc101DropBox.Items.Add("SOCY U101");
            soc101DropBox.Items.Add("WGST U101");

            soc102DropBox.Items.Add("AFAM U201");
            soc102DropBox.Items.Add("ANTH U102");
            soc102DropBox.Items.Add("ECON U221");
            soc102DropBox.Items.Add("ECON U222");
            soc102DropBox.Items.Add("GEOG U101");
            soc102DropBox.Items.Add("GEOG U103");
            soc102DropBox.Items.Add("POLI U101");
            soc102DropBox.Items.Add("POLI U200");
            soc102DropBox.Items.Add("POLI U320");
            soc102DropBox.Items.Add("PSYC U101");
            soc102DropBox.Items.Add("SOCY U101");
            soc102DropBox.Items.Add("WGST U101");

            Course c = new Course();                    //\ creates course object

            //\ these lists hold string and int lists for electives
            List<int> intElectives = new List<int>();   
            List<String> strElectives = new List<String>();

            //\ this gets value for lists
            intElectives = c.getElectives();
            strElectives = student.getCourseName(intElectives);


            //\ this adds the list of electives to the listboxes in ui
            foreach (String s in strElectives)
            {
                e1DropBox.Items.Add(s);
                e2DropBox.Items.Add(s);
                e3DropBox.Items.Add(s);
                e4DropBox.Items.Add(s);
            }


            //\ sets courses as complete or incomplete
            setTakenCourses();



        }
        //\ if user not logged in ~ show notLoggedIn page
        else
        {
            Response.Redirect("NotLoggedIn.aspx");
            Server.Transfer("NotLoggedIn.aspx");
        }

    }


    /********************************************************************************************************************
   **************************************************************************************  BUTTONS    ******************
   ********************************************************************************************************************/

    //\ submit button
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        
            //\ gets a string list of checked items
            List<String> strChecked = new List<String>();
            strChecked = getChecked();
            //\ gets a int list of checked items
            List<int> intChecked = new List<int>();
            intChecked = student.getCourseName(strChecked);
            //\ adds all selected to courses_taken table
            student.addCoursesTaken(intChecked);


            //\ redirects user to progress page
            Response.Redirect("Progress.aspx");
            Server.Transfer("Progress.aspx");

        //\ call setTakenCourses
        setTakenCourses();
    }



    //\ Remove Button

    protected void btnRemove_Click(object sender, EventArgs e)
    {
        
            //\ gets list of checked checkboxes
            List<String> removeChecked = new List<String>();
            removeChecked = getChecked();
            //\ converts strings to int
            List<int> removeInt = new List<int>();
            removeInt = student.getCourseName(removeChecked);

            //\ call removeCourseTaken from student object
            student.removeCoursesTaken(removeInt);

        //\ redirect user to progress page
            Response.Redirect("Progress.aspx");
            Server.Transfer("Progress.aspx");  
    }





    /********************************************************************************************************************
    **************************************************************************************   METHODS   ******************
    ********************************************************************************************************************/


    //\ private method that gets selected value from a droplistbox 
    //\ parameters: curID is the id of the checkbox
    private String getID(String curID)
    {
        //\ this tells us which droplistbox we need to get a value from
        switch (curID)
        {
            case "ns101": curID = ns101DropBox.SelectedValue.ToString();
                break;
            case "ns102": curID = ns102DropBox.SelectedValue.ToString();
                break;
            case "ns103": curID = ns103DropBox.SelectedValue.ToString();
                break;
            case "art101": curID = art101DropBox.SelectedValue.ToString();
                break;
            case "his101": curID = his101DropBox.SelectedValue.ToString();
                break;
            case "hum101": curID = hum101DropBox.SelectedValue.ToString();
                break;
            case "for101": curID = for101DropBox.SelectedValue.ToString();
                break;
            case "soc101": curID = soc101DropBox.SelectedValue.ToString();
                break;
            case "soc102": curID = soc102DropBox.SelectedValue.ToString();
                break;
            case "e1":
                curID = e1DropBox.SelectedValue.ToString();
                break;
            case "e2":
                curID = e2DropBox.SelectedValue.ToString();
                break;
            case "e3":
                curID = e3DropBox.SelectedValue.ToString();
                break;
            case "e4":
                curID = e4DropBox.SelectedValue.ToString();
                break;
        }
        return curID;
    }//\ END getID

   



    //\ Returns a formatted List<String> of all courses checked
    private List<String> getChecked()
    {
        List<String> returnList = new List<String>();
        
   
        foreach (Control c in addPanel.Controls)
        {
            if (c is CheckBox && ((CheckBox)c).Checked)
            {
                String formattedID = c.ID; //holds value of formatted ID
                //\ if id < 7 than it is in dropbox and is formatted, so we call getID method to return it
                if (c.ID.Length < 7)
                {
                    formattedID = getID(c.ID); //\ calls getID method
                }
                //\ if not grab the id of the checkbox and add a space(because of id formatting)
                else
                {
                    formattedID = formattedID.Insert(4, " ");
                }
                //\ this adds each courseID (formatted properly) that was "checked" to a list
                returnList.Add(formattedID);
            }
        }
        return returnList;
    }






   
    //\ this methods 
    private void setTakenCourses()
    {

        //\ gets lists of courses taken
        List<String> strCoursesTakenRAW = new List<String>();
        strCoursesTakenRAW = student.getTakenCourses();
        List<String> strCoursesTaken = new List<String>();

        //\ formats the items that are in list boxes
        foreach (String s in strCoursesTakenRAW)
        {
            String formattedID = s;
            formattedID = formattedID.Remove(4, 1);
            
                strCoursesTaken.Add(formattedID);
        }

        //\ this looks for checkboxes and gets the id of each. Then it checks
        //\ if course is in the taken list and labels as complete or incomplete
        foreach (WebControl c in addPanel.Controls.OfType<CheckBox>())
        {
            String tempID;
            if(c.ID.Length < 6)
            {
                tempID = getID(c.ID);
            }
            else
            {
                tempID = c.ID;
            }           
              
            

            //\ if/else adds or removes "complete" status from labels
            if (strCoursesTaken.Contains(c.ID) || strCoursesTakenRAW.Contains(tempID))
            {
               
                    ((CheckBox)c).BackColor = System.Drawing.Color.Red;
                ((CheckBox)c).Text = "Complete";


            }//end if
            else
            {
               
                    ((CheckBox)c).BackColor = System.Drawing.Color.Green;
                ((CheckBox)c).Text = "";

            }
        }//end foreach


    }





    //\ These update the page when the user alters a drop box
    protected void his101DropBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        dropBoxAltered();
    }


    protected void ns101DropBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        dropBoxAltered();
    }

    protected void ns102DropBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        dropBoxAltered();

    }

    protected void art101DropBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        dropBoxAltered();

    }

    protected void hum101DropBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        dropBoxAltered();

    }

    protected void for101DropBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        dropBoxAltered();

    }

    protected void soc101DropBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        dropBoxAltered();

    }

    protected void soc102DropBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        dropBoxAltered();

    }

    protected void ns103DropBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        dropBoxAltered();
       
    }
    protected void e1DropBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        dropBoxAltered();

    }

    protected void e2DropBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        dropBoxAltered();
    }

    protected void e3DropBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        dropBoxAltered();

    }

    protected void e4DropBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        dropBoxAltered();

    }


    //\ if a dropbox is altered it checks if the current value of the dropbox is a complete course
    //\ and sets as complete or incomplete 
    private void dropBoxAltered()
    {
        List<String> strCoursesTakenRAW = new List<String>();
        strCoursesTakenRAW = student.getTakenCourses();

        foreach (WebControl d in addPanel.Controls.OfType<DropDownList>())
        {
            String curVal = ((DropDownList)d).SelectedValue.ToString();

            if (strCoursesTakenRAW.Contains(curVal))
            {
                switch (d.ID)
                {
                    case "ns101DropBox":
                        ns101.BackColor = System.Drawing.Color.Red;
                        ns101.Text = "Complete";
                        break;
                    case "ns102DropBox":
                        ns102.BackColor = System.Drawing.Color.Red;
                        ns102.Text = "Complete";
                        break;
                    case "ns103DropBox":
                        ns103.BackColor = System.Drawing.Color.Red;
                        ns103.Text = "Complete";
                        break;
                    case "art101DropBox":
                        art101.BackColor = System.Drawing.Color.Red;
                        art101.Text = "Complete";
                        break;
                    case "his101DropBox":
                        his101.BackColor = System.Drawing.Color.Red;
                        his101.Text = "Complete";
                        break;
                    case "hum101DropBox":
                        hum101.BackColor = System.Drawing.Color.Red;
                        hum101.Text = "Complete";
                        break;
                    case "for101DropBox":
                        for101.BackColor = System.Drawing.Color.Red;
                        for101.Text = "Complete";
                        break;
                    case "soc101DropBox":
                        soc101.BackColor = System.Drawing.Color.Red;
                        soc101.Text = "Complete";
                        break;
                    case "soc102DropBox":
                        soc102.BackColor = System.Drawing.Color.Red;
                        soc102.Text = "Complete";
                        break;
                    case "e1DropBox":
                        e1.BackColor = System.Drawing.Color.Red;
                        e1.Text = "Complete";
                        break;
                    case "e2DropBox":
                        e2.BackColor = System.Drawing.Color.Red;
                        e2.Text = "Complete";
                        break;
                    case "e3DropBox":
                        e3.BackColor = System.Drawing.Color.Red;
                        e3.Text = "Complete";
                        break;
                    case "e4DropBox":
                        e4.BackColor = System.Drawing.Color.Red;
                        e4.Text = "Complete";
                        break;

                }


            }//end if
            else
            {


                switch (d.ID)
                {
                    case "ns101DropBox":
                        ns101.BackColor = System.Drawing.Color.Green;
                        break;
                    case "ns102DropBox":
                        ns102.BackColor = System.Drawing.Color.Green; break;
                    case "ns103DropBox":
                        ns103.BackColor = System.Drawing.Color.Green; break;
                    case "art101DropBox":
                        art101.BackColor = System.Drawing.Color.Green; break;
                    case "his101DropBox":
                        his101.BackColor = System.Drawing.Color.Green; break;
                    case "hum101DropBox":
                        hum101.BackColor = System.Drawing.Color.Green; break;
                    case "for101DropBox":
                        for101.BackColor = System.Drawing.Color.Green; break;
                    case "soc101DropBox":
                        soc101.BackColor = System.Drawing.Color.Green; break;
                    case "soc102DropBox":
                        soc102.BackColor = System.Drawing.Color.Green; break;
                    case "e1DropBox":
                        e1.BackColor = System.Drawing.Color.Green; break;
                    case "e2DropBox":
                        e2.BackColor = System.Drawing.Color.Green; break;
                    case "e3DropBox":
                        e3.BackColor = System.Drawing.Color.Green; break;
                    case "e4DropBox":
                        e4.BackColor = System.Drawing.Color.Green; break;
                }
            }
        }//end foreach
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
