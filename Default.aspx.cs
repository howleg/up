﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class _Default : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (HttpContext.Current.User.Identity.IsAuthenticated)
        {

            // REMOVED TEST CODE
          
            
        }
    }
}