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

//MessageHandler connects to the messageDb object which makes the connnection to the database and returns the datatables and datasets
//of the data in the database when the methods in messageDb are called.
//The MessageHandler then controls all the methods in messageDb.
/* The MessageHandler  also controls the Message class. The message class keeps track a 
single record in the Message table in the database for a specific usee*/
public class MessageHandler
    {
        MessageDb messageDb = null;
    String currentlyLoggedUserID;
    String currentlyLoggedUserName;


    public MessageHandler()
        {
            messageDb = new MessageDb();
        currentlyLoggedUserName = HttpContext.Current.User.Identity.Name;
        currentlyLoggedUserID = Membership.GetUser(currentlyLoggedUserName).ProviderUserKey.ToString();
    }

        public DataTable GetAllMessages(string userID)
        {
            return messageDb.GetAllMessages(userID);
        }

        public int getAmountOfUnreadMsg(string UserId)
        {
            DataTable table = messageDb.getAmountOfUnreadMsg(UserId);
            int count = 0;
            count= table.Rows.Count;
             return count;
        }

        public DataTable getConversation(string recieverID, string senderID)
        {
            return messageDb.getConversation(recieverID, senderID);
        }

         public bool SendMessage(string senderID, string recieverID, string subject, string body)
        {
            return messageDb.SendMessage(senderID, recieverID, subject, body);
        }

        public Message GetMessageDetails(int messageId)
        {
            DataTable table = messageDb.GetMessageDetails(messageId);

            if (table.Rows.Count == 0)
            {
                return null;
            }

            Message msg = new Message();

            msg.Date = Convert.ToDateTime(table.Rows[0]["datentime"].ToString());
            msg.MessageId = Convert.ToInt32(table.Rows[0]["MessageID"].ToString());
            msg.RecieverId = table.Rows[0]["recieverID"].ToString();
            msg.Status = table.Rows[0]["status"].ToString();
            msg.SenderId = table.Rows[0]["senderID"].ToString();
            msg.Subject = table.Rows[0]["subject"].ToString();
            msg.Body = table.Rows[0]["body"].ToString();

        //Before returning lets mark this message as read
        if ((msg.RecieverId.Equals(currentlyLoggedUserID)))
            messageDb.MarkMessageRead(messageId);

            //returns shallow copy of messsage
            return msg;
        }

        public DataTable GetSentMessages(string userID)
        {
            return messageDb.GetSentMessages(userID);
        }
    }
