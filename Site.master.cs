using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SiteMaster : MasterPage
{
    private const string AntiXsrfTokenKey = "__AntiXsrfToken";
    private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
    private string _antiXsrfTokenValue;

    protected int messageCount = 0;
    MessageHandler msgHld;
    protected String UserId = "";

    protected void Page_Init(object sender, EventArgs e)
    {

        // The code below helps to protect against XSRF attacks
        var requestCookie = Request.Cookies[AntiXsrfTokenKey];
        Guid requestCookieGuidValue;
        if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
        {
            // Use the Anti-XSRF token from the cookie
            _antiXsrfTokenValue = requestCookie.Value;
            Page.ViewStateUserKey = _antiXsrfTokenValue;
        }
        else
        {
            // Generate a new Anti-XSRF token and save to the cookie
            _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
            Page.ViewStateUserKey = _antiXsrfTokenValue;

            var responseCookie = new HttpCookie(AntiXsrfTokenKey)
            {
                HttpOnly = true,
                Value = _antiXsrfTokenValue
            };
            if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
            {
                responseCookie.Secure = true;
            }
            Response.Cookies.Set(responseCookie);
        }

        Page.PreLoad += master_Page_PreLoad;
    }

    void master_Page_PreLoad(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // Set Anti-XSRF token
            ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
            ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
        }
        else
        {
            // Validate the Anti-XSRF token
            if ((string) ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                || (string) ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
            {
                throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if ((HttpContext.Current.User.Identity.IsAuthenticated))
        {
            msgHld = new MessageHandler();
            String currentUserName = HttpContext.Current.User.Identity.Name;
            UserId = Membership.GetUser(currentUserName).ProviderUserKey.ToString();
            messageCount = msgHld.getAmountOfUnreadMsg(UserId);
        }

    }



    protected void Button1_Click(object sender, EventArgs e)
    {
       
    }



    protected void ButtonSearch_Click(object sender, EventArgs e)
    {
        //Response.Redirect("~/Default.aspx");
        try
        {
            
        }
        catch (Exception)
        {
            
           //Response.Redirect("~/");
        }
       

    }

}