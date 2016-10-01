using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SportsPro.ErrorPages
{
    public partial class Exceptions : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadError();
                //btnReturn.OnClientClick = "window.location = window.location; return false;";
            }
        }
        protected void LoadError()
        {
            Exception ex = HttpContext.Current.Server.GetLastError();
            if (ex != null)
            {
                if (ex.GetBaseException() != null)
                {
                    ex = ex.GetBaseException();
                }
                Session["ErrorPageError"] = ex;
            }
            else
            {
                if (Session["ErrorPageError"] != null)
                {
                    ex = (Exception)Session["ErrorPageError"];
                }
            }
            Session["ErrorPageReturnBackURL"] = Request.RawUrl;
            Server.ClearError();
            lblError.Text = ex.Message + "<br />" + ex.Source;
        }

        protected void btnSubmitReport_Click(object sender, EventArgs e)
        {
            if (Session["ErrorPageError"] != null)
            {
                SportsProLibrary.ErrorReport.Report((Exception)Session["ErrorPageError"]);
            }
            Response.Redirect(Session["ErrorPageReturnBackURL"].ToString());
        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }
    }
}