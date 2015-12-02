using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;


    public class UserDb
    {
        public UserDb()
        {
            
        }

        public DataTable GetUserList()
        {
            DataTable table = new DataTable();
            storedProcedure myProc = new storedProcedure("GetUserList");
            return table = myProc.executeReader();
        }

        public DataTable GetUserDetails(string userId)
        {
            DataTable table = new DataTable();
            storedProcedure myProc = new storedProcedure("GetUserDetails");
            myProc.addParam(new paramter("@userId", userId));

            return table = myProc.executeReader();
        }

    }