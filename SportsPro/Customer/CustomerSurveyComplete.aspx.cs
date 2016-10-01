using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SportsPro.Customer
{
    public partial class CustomerSurveyComplete : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                preMessage.InnerHtml += "Thank you for your feedback!";
                if (!Equals(Session["SurveyContact"]))
                {
                    preMessage.InnerHtml += "<br/>A customer service representative will contact you within 24 hours.";
                }
            }
        }
    }
}