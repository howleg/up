using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;

public partial class ReadMail : System.Web.UI.Page
{
    protected String currentlyLoggedUserID;
    String recieverID;
    string senderID;

    int MessageID;

    protected List<Message> messageList;

    public TextBoxMode MultiLine { get; private set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!(HttpContext.Current.User.Identity.IsAuthenticated))
        {

            FormsAuthentication.SignOut();
            Session.Clear();
            Response.Redirect("~/Default");
        }

        messageList = new List<Message>();
        String currentlyLoggedUserName = HttpContext.Current.User.Identity.Name;
        currentlyLoggedUserID = Membership.GetUser(currentlyLoggedUserName).ProviderUserKey.ToString();

        if (currentlyLoggedUserID == null)
        {
            Response.Redirect("~/Default");
        }

        //Check whether the proper message is being passed or not
        //the id here is the messageId

        if (Request.QueryString["id"] == null)
        {
            Response.Redirect("Default.aspx");
        }
        MessageID = Convert.ToInt32(Request.QueryString["id"]);

        MessageHandler msgHandler = new MessageHandler();
        Message msg = msgHandler.GetMessageDetails(MessageID);

        //Some one is trying to access a mail that is not supposed to see so kick him out
        if (msg == null)
        {
            Response.Redirect("Default.aspx");
        }

        //give me a list of all messages where the SenderId=him and RecieverId=me or SenderId=me and RecieverId=him order by datentime ASC

        recieverID = msg.RecieverId;
        senderID = msg.SenderId;

        //get all conversations between the reciever(currentlyLoggedUserID) and the sender
        //the sender is gotten from the messageID that is gotten when ther clicks on the message in Inbox
        DataTable dt = msgHandler.getConversation(recieverID, senderID);
        if (dt.Rows.Count > 0)
        {
            foreach (DataRow dr in dt.Rows)
            {

                int msgID = Convert.ToInt32(dr["MessageID"].ToString());
                System.Diagnostics.Debug.WriteLine("MessageID is = " + msgID);


                Message msgTemp = msgHandler.GetMessageDetails(msgID);
                System.Diagnostics.Debug.WriteLine("msgTemp is = " + msgTemp);

                this.messageList.Add(msgTemp);

            }
        }
        else
        {
            Console.WriteLine("No rows found.");
        }


        createTable();

    }//end of class ReadMail

    private void createTable()
    {
        HtmlTable myTable = new HtmlTable();
        myTable.Attributes["class"] = "table";
        foreach (var msg in messageList)
        {
            String senderID = msg.SenderId;
            String recieverID = msg.RecieverId;
            String subject = msg.Subject;
            String body = Server.HtmlDecode(msg.Body);
            String datenTime = msg.Date.ToString();

            //make sure to use the int constructor because that is the one that uses the UserID to make a student
            student22 thisStudent = new student22(senderID, 0);

            HtmlTableRow row = new HtmlTableRow();

            //row.Attributes["class"] = "table";
            if (senderID.Equals(currentlyLoggedUserID))
                row.Attributes["class"] = "alert alert-info";
            else
                row.Attributes["class"] = "alert alert-warning";

            //////////////// cell1  userNameLabel   ///////////////////////////////////////////////
            HtmlTableCell cell1 = new HtmlTableCell();
            cell1.Attributes["class"] = "panel";

            var userNameLabel = new Label();
            userNameLabel.Text = thisStudent.getUsername();
            //add the userNameLabel to cell1
            cell1.Controls.Add(userNameLabel);
            //add cell1 to the row
            row.Controls.Add(cell1);

            //////////////// cell2  img ///////////////////////////////////////////////
            HtmlTableCell cell2 = new HtmlTableCell();
            Image img = new Image();
            img.Height = 60;
            img.Width = 60;
            img.Attributes["class"] = "media-object";
            img.AlternateText = "No image on file";
            img.ImageUrl = "ImageHandler.ashx?UserId=" + thisStudent.getStudent_id();
            //add the btn to cell3
            cell2.Controls.Add(img);
            //add cell3 to the row
            row.Controls.Add(cell2);

            //////////////// cell3  msgTextbox   ///////////////////////////////////////////////
            HtmlTableCell cell3 = new HtmlTableCell();
            var msgTextLabel = new Label();
            msgTextLabel.Text = body;
            //add the msgTextbox to cell3
            //if (senderID.Equals(currentlyLoggedUserID))
            //    cell3.Attributes["class"] = "alert alert-info";
            //else
            //    cell3.Attributes["class"] = "alert alert-warning";

            cell3.Controls.Add(msgTextLabel);
            //add cell3 to the row
            row.Controls.Add(cell3);


            //////////////// cell4  dateTimeLabel   ///////////////////////////////////////////////
            HtmlTableCell cell4 = new HtmlTableCell();
            cell4.Attributes["class"] = "";
            var dateTimeLabel = new Label();
            dateTimeLabel.Text = datenTime;
            //add the userNameLabel to cell1
            cell4.Controls.Add(dateTimeLabel);
            //add cell1 to the row
            
            row.Controls.Add(cell4);




            //add all rows to table
            myTable.Controls.Add(row);


        }
        //myTable.Attributes.Add("Class", "clsTradeInCart");
        conversationLogPlaceHolder.Controls.Add(myTable);
        //PanelConversation.Attributes["class"] = "well";
        PanelConversation.Height = 400;
        PanelConversation.Attributes["width"] = "auto";
    }

    protected void Send_Click(object sender, EventArgs e)
    {
        String message = Server.HtmlEncode(messageBox.Text);

        if (!(String.IsNullOrEmpty(message)))
        {
            MessageHandler msgHandler = new MessageHandler();
            msgHandler.SendMessage(recieverID, senderID, "", Server.HtmlEncode(messageBox.Text));
            messageBox.Text = string.Empty;
            Response.Redirect(Request.RawUrl);

        }

    }


}
