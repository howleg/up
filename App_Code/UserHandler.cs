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

/// Author: Daniel Dingana
public class UserHandler
    {
        UserDb userDb = null;
     private List<student22> allStudentsList;

     public UserHandler()
        {
            userDb = new UserDb();
        this.allStudentsList = new List<student22>();

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

    public List<student22> GetAllStudentsList()
    {

        DataTable allUserNameTable = userDb.getAllUsernames();


        if (allUserNameTable.Rows.Count > 0)
        {
            foreach (DataRow dr in allUserNameTable.Rows)
            {
                String username = dr["UserName"].ToString();
                student22 stud = new student22(username);
                this.allStudentsList.Add(stud);
            }
        }

        else
        {
            Console.WriteLine("No rows found.");
        }

        return this.allStudentsList;

    }


}
