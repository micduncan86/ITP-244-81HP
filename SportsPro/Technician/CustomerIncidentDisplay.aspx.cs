using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SportsPro.Technician
{
    public partial class CustomerIncidentDisplay : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindData();
            }
        }

        private void BindData()
        {            
            ddlCustomers.DataSource = SportsProLibrary.Customers.GetCustomersDataView();
            ddlCustomers.DataTextField = "Name";
            ddlCustomers.DataValueField = "CustomerID";
            ddlCustomers.DataBind();

            ddlCustomers.Items.Insert(0, new ListItem("Show all", "-1"));
            ddlCustomers.SelectedIndex = 1;


            BindIncidents(ddlCustomers.SelectedValue);
        }

        private void BindIncidents(string _customerID)
        {
            SportsProLibrary.IncidentSearch _search = new SportsProLibrary.IncidentSearch();
            _search.SearchBy = SportsProLibrary.IncidentFields.CustomerID;
            _search.SearchTerm = Convert.ToInt32(_customerID);
            dataListIncidents.DataSource = _search.Find();
            dataListIncidents.DataKeyField = "IncidentID";
            dataListIncidents.DataBind();
        }
        protected void ddlCustomers_SelectedIndexChanged(object sender, EventArgs e)
        {

            BindIncidents(ddlCustomers.SelectedValue);

        }
    }
}