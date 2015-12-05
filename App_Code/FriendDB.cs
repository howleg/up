using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

/// <summary>
/// Summary description for friendDB
/// </summary>
public class FriendDB
{
    public FriendDB()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public DataTable checkIfFriend(string user_id, string friend_id)
    {

        storedProcedure myProc = new storedProcedure("checkIfFriend");
        myProc.addParam(new paramter("@user_id", user_id));
        myProc.addParam(new paramter("@friend_id", friend_id));
        DataTable table = new DataTable();

        return table = myProc.executeReader();
    }

    public DataTable getFriendsList(string user_id)
    {
        storedProcedure myProc = new storedProcedure("getFriendsList");
        myProc.addParam(new paramter("@user_id", user_id));
        DataTable table = new DataTable();
        return table = myProc.executeReader();
    }



    public void addFriend(string user_id, string friend_id)
    {

        storedProcedure myProc = new storedProcedure("addFriend");
        myProc.addParam(new paramter("@user_id", user_id));
        myProc.addParam(new paramter("@friend_id", friend_id));

        myProc.executeNonQueryParam();
    }

    public void removeFriend(string user_id, string friend_id)
    {

        storedProcedure myProc = new storedProcedure("removeFriend");
        myProc.addParam(new paramter("@user_id", user_id));
        myProc.addParam(new paramter("@friend_id", friend_id));

        myProc.executeNonQueryParam();
    }


    public DataTable mutualFriends(string user_id, string visitedUser_Id)
    {

        storedProcedure myProc = new storedProcedure("mutualFriends");
        myProc.addParam(new paramter("@user_id", user_id));
        myProc.addParam(new paramter("@visitedUser_Id", visitedUser_Id));
        DataTable table = new DataTable();

        return table = myProc.executeReader();
    }


}