using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace SportsPro
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            System.Web.UI.ScriptManager.ScriptResourceMapping.AddDefinition("jquery",
                new System.Web.UI.ScriptResourceDefinition
                {
                    DebugPath = "~/scripts/jquery-3.1.0.js",
                    Path = "~/scripts/jquery-3.1.0.min.js",
                });
        }        
    }
}