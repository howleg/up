<%@ WebHandler Language="C#" Class="ImageHandler"  %>
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
using System.IO;
using System.Web.Configuration;

/// Author: Daniel Dingana
/// 
public class ImageHandler  : IHttpHandler
{
    private static String myDbConString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

    static string connectionstring = myDbConString;

    public void ProcessRequest (HttpContext context)
    {
        if(String.IsNullOrEmpty(context.Request.QueryString["UserId"]))
            HttpContext.Current.Response.Redirect("~/Default.aspx");

        //gets the string from 
        String UserId = context.Request.QueryString[0];
        
        /*
        Guid userKey = new Guid(UserId);
        MembershipUser mu = Membership.GetUser(userKey);

        
        string userName = mu.UserName;
        String studentID = userKey.ToString();
        */
        student22 currentStudent = new student22(UserId,0);

        String image = currentStudent.getProfilePic();
        System.Diagnostics.Debug.WriteLine("image is = " + image);


        if (!(String.IsNullOrEmpty(image)))
        {
            System.Diagnostics.Debug.WriteLine("UserId is = " + UserId);
            SqlConnection con = new SqlConnection(connectionstring);
            String query = "select ImageData from profilePic where UserId =@UserId";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@UserId", SqlDbType.UniqueIdentifier).Value = UserId;

            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                context.Response.BinaryWrite((Byte[])dr[0]);
            }

            context.Response.End();

            con.Close();
        }

        context.Response.End();
    }
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}