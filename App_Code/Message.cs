using System;
using System.Data;
using System.Configuration;

    public class Message
    {
        private int messageId;
        private string senderId;
        private string recieverId;
        private string subject;
        private string body;
        private DateTime date;
        private string status;

        public int MessageId
        {
            set
            {
                messageId = value;
            }
            get
            {
                return messageId;
            }
        }
        public string SenderId
        {
            set
            {
                senderId = value;
            }
            get
            {
                return senderId;
            }
        }
        public string RecieverId
        {
            set
            {
                recieverId = value;
            }
            get
            {
                return recieverId;
            }
        }
        public string Subject
        {
            set
            {
                subject = value;
            }
            get
            {
                return subject;
            }
        }
        public string Body
        {
            set
            {
                body = value;
            }
            get
            {
                return body;
            }
        }
        public DateTime Date
        {
            set
            {
                date = value;
            }
            get
            {
                return date;
            }
        }
        public string Status
        {
            set
            {
                status = value;
            }
            get
            {
                return status;
            }
        }
    }
