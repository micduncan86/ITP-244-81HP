using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SportsPro.Customer
{
    public partial class CustomerSurvey : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadControls();
            }
        }
        private void LoadControls()
        {
           
            
            Session.Remove("SurveyContact");     
            List<ListItem> RateOptions = new List<ListItem>();
            RateOptions.Add(new ListItem("Not Satisfied", "1"));
            RateOptions.Add(new ListItem("Somewhat Satisfied", "2"));
            RateOptions.Add(new ListItem("Satisfied", "3"));
            RateOptions.Add(new ListItem("Completely Satisfied", "4"));

            foreach (var li in RateOptions)
            {
                if (RateOptions.IndexOf(li) == 0)
                {
                    li.Selected = true;
                }
                rblResponse.Items.Add(li);
                rblTech.Items.Add(li);
                rblProblem.Items.Add(li);
            }
            if (!Equals(Session["SurveyCustomerID"], null))
            {
                txtCustomerId.Text = Session["SurveyCustomerID"].ToString();
                LoadIncidents(txtCustomerId.Text);
            }
            else
            {
                EnableDisableControls(false);
            }
            

        }

        private void EnableDisableControls(bool enable = true)
        {
            lstIncidents.Enabled = enable;
            rblContactBy.Visible = enable;
            rblProblem.Enabled = enable;
            rblResponse.Enabled = enable;
            rblTech.Enabled = enable;
            chkContactMe.Enabled = enable;
            txtComments.Enabled = enable;
            btnSubmit.Enabled = enable;
        }

        protected void LoadIncidents(string CustomerID)
        {
            if (!Page.IsPostBack || Page.IsValid)
            {

                SportsProLibrary.IncidentSearch _search = new SportsProLibrary.IncidentSearch();
                _search.SearchBy = SportsProLibrary.IncidentFields.CustomerID;
                _search.SearchTerm = Convert.ToInt32(CustomerID);
                _search.ClosedOnly = true;
                List<SportsProLibrary.oIncident> IncidentList = _search.Find();
                Session["SurveyCustomerID"] = Convert.ToInt32(CustomerID);

                lstIncidents.Items.Clear();
                if (IncidentList.Count > 0)
                {
                    lstIncidents.Items.Add(new ListItem("---Select and incident---", "None"));
                    foreach (var oIncident in IncidentList)
                    {
                        ListItem li = new ListItem(oIncident.CustomerIncidentDisplay(), oIncident.IncidentID.ToString());
                        lstIncidents.Items.Add(li);
                    }
                    EnableDisableControls(true);
                }
                else
                {
                    lstIncidents.Items.Add("No closed incidents found.");
                    EnableDisableControls(false);
                }
            }
        }

        protected void btnGetIncidents_Click(object sender, EventArgs e)
        {
            LoadIncidents(txtCustomerId.Text);
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                SportsProLibrary.Survey oSurvey = new SportsProLibrary.Survey();
                oSurvey.Comments = txtComments.Text;
                oSurvey.CustomerID = Convert.ToInt32(txtCustomerId.Text);                
                oSurvey.IncidentID = Convert.ToInt32(lstIncidents.SelectedValue);
                oSurvey.Resolution = Convert.ToInt32(rblProblem.SelectedValue);
                oSurvey.ResponseTime = Convert.ToInt32(rblResponse.SelectedValue);
                oSurvey.TechEfficiency = Convert.ToInt32(rblTech.SelectedValue);
                oSurvey.Contact = chkContactMe.Checked; //set method of this property sets Session["SurveyContact"]
                oSurvey.ContactBy = rblContactBy.SelectedValue;
                Response.Redirect("CustomerSurveyComplete.aspx");
            }
        }
    }
}