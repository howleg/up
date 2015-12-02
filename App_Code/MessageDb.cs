using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;


    public class MessageDb
    {
        public MessageDb()
        {
            
        }

        public DataTable GetAllMessages(string userID)
        {
            DataTable table = new DataTable();
            storedProcedure myProc = new storedProcedure("FetchMessages");
            myProc.addParam(new paramter("@recieverId", userID));

           return table = myProc.executeReader();
        }

    public DataTable getAmountOfUnreadMsg (string UserId)
    {
        DataTable table = new DataTable();
        storedProcedure myProc = new storedProcedure("getAmountOfUnreadMsg");
        myProc.addParam(new paramter("@UserId", UserId));
        return table = myProc.executeReader();
    }

    public DataTable getConversation(string recieverID, string senderID)
    {

        storedProcedure myProc = new storedProcedure("getConversation");
        myProc.addParam(new paramter("@recieverID", recieverID));
        myProc.addParam(new paramter("@senderID", senderID));
        DataTable table = new DataTable();

        return table = myProc.executeReader();
    }


    public bool SendMessage(string senderID, string recieverID, string subject, string body)
        {
        
            storedProcedure myProc = new storedProcedure("SendMessage");
            myProc.addParam(new paramter("@senderID", senderID));
            myProc.addParam(new paramter("@recieverID", recieverID));
            myProc.addParam(new paramter("@subject", subject));
            myProc.addParam(new paramter("@body", body));

            return myProc.executeNonQueryParam();
       }

        public DataTable GetMessageDetails(int messageId)
        {
            DataTable table = new DataTable();
            storedProcedure myProc = new storedProcedure("ReadMessage");
            myProc.addParam(new paramter("@id", messageId.ToString()));

            return table = myProc.executeReader();
       }

        public DataTable GetSentMessages(string userID)
        {
            DataTable table = new DataTable();
            storedProcedure myProc = new storedProcedure("GetSentMessages");
            myProc.addParam(new paramter("@userId", userID));
            return table = myProc.executeReader();
        }

        public bool MarkMessageRead(int msgId)
        {
            storedProcedure myProc = new storedProcedure("MarkAsRead");
            myProc.addParam(new paramter("@id", msgId.ToString()));

            return myProc.executeNonQueryParam();
        }
    }
