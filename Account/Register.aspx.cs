



using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Membership.OpenAuth;
using System.Data.SqlClient;
using System.Data;
using Microsoft.AspNet.Membership;
using System.Web.Configuration;

public partial class Account_Register : Page
{
    /**********************************************************************
   *                   CREATE YOUR CONNECTION STRINGS BELOW               *
   **********************************************************************/
    private static String reidsDB = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;


    /**********************************************************************
   * REPLACE THIS STRING WITH CONNECTIONSTRING FROM YOUR LOCAL DATABASE  *
   **********************************************************************/
    String myDatabase = reidsDB;

    protected void Page_Load(object sender, EventArgs e)
    {
        RegisterUser.ContinueDestinationPageUrl = Request.QueryString["ReturnUrl"];
        
    }

    protected void RegisterUser_CreatedUser(object sender, EventArgs e)
    {
        FormsAuthentication.SetAuthCookie(RegisterUser.UserName, createPersistentCookie: false);      

          // This saves name to student table, need db finished

        //\ gets the value of the Name box
        TextBox nameText = RegisterUser.CreateUserStep.ContentTemplateContainer.FindControl("iName") as TextBox;
        String curName = nameText.Text;

        //\ gets value for userId
        String userId = Membership.GetUser((sender as CreateUserWizard).UserName).ProviderUserKey.ToString();

        //\ debug
        System.Diagnostics.Debug.WriteLine("name = " + curName + "  userName = " + userId);

        //\ adds user to studentTable
        SqlConnection con = new SqlConnection(myDatabase);
        con.Open();

       {
            using (SqlCommand cmd = new SqlCommand("addUser", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@studentName", curName);
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException)
                {

                }
            }

            con.Close();
        }

    



        string continueUrl = RegisterUser.ContinueDestinationPageUrl;
        if (!OpenAuth.IsLocalUrl(continueUrl))
        {
            continueUrl = "~/";
        }
        Response.Redirect(continueUrl);
    }

    
}

