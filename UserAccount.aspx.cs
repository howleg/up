using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserAccount : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!(HttpContext.Current.User.Identity.IsAuthenticated))
        {

            FormsAuthentication.SignOut();
            Session.Clear();
            Response.Redirect("~/Default");
        }
    }

    protected void btnAddCourse_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddRemoveCourse.aspx");
    }

    protected void btnShowRecommended_Click(object sender, EventArgs e)
    {
        Response.Redirect("Results.aspx");
    }

    protected void btnStatus_Click(object sender, EventArgs e)
    {
        Response.Redirect("Progress.aspx");
    }

    protected void btnRateCourse_Click(object sender, EventArgs e)
    {
        Response.Redirect("RateMyCourse.aspx");
    }

    protected void btnProfile_Click(object sender, EventArgs e)
    {
        Response.Redirect("profile.aspx");
    }
}