using System;
using System.Data;
using System.Configuration;


    public class UserHandler
    {
        UserDb userDb = null;

        public UserHandler()
        {
            userDb = new UserDb();
        }

        public DataTable GetUserList()
        {
            return userDb.GetUserList();
        }

        public bool IsValidUser(string userId)
        {
            DataTable table = userDb.GetUserDetails(userId);

            if (table.Rows.Count == 0)
            {
                return false;
            }
            return true;
        }

        public bool ValidateUser(string name, string password)
        {
            DataTable table = userDb.GetUserDetails(name);

            try
            {
                if (table.Rows[0][1].ToString() == password)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return false;
        }

        public string GetUserName(string userId)
        {
            DataTable table = userDb.GetUserDetails(userId);

            if (table.Rows[0][1] != null)
            {
                return table.Rows[0]["username"].ToString();
            }
            return string.Empty;
        }

       
    }
