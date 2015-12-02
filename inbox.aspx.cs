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

public partial class inbox : System.Web.UI.Page
{
    protected String UserId;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!(HttpContext.Current.User.Identity.IsAuthenticated))
        {

            FormsAuthentication.SignOut();
            Session.Clear();
            Response.Redirect("~/Default");
        }

        String currentUserName = HttpContext.Current.User.Identity.Name;
        UserId = Membership.GetUser(currentUserName).ProviderUserKey.ToString();


        if (UserId == null)
        {
            Response.Redirect("~/Default");
        }

        MessageHandler msghandler = new MessageHandler();

        DataTable msgDataTable = msghandler.GetAllMessages(UserId);


        if (msgDataTable.Rows.Count < 1)
        {
            DoYouHaveMessages.Visible = true;
            DoYouHaveMessages.Text = "You have no messages";
            Repeater1.Visible = false;
        }
        else
        {
            DoYouHaveMessages.Visible = false;
            Repeater1.Visible = true;
        }
        //GetAllMessages is FetchMessages in the database
        Repeater1.DataSource = msgDataTable;
        Repeater1.DataBind();
    }
}
