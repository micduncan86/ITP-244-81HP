using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SportsPro.Customer
{
    public partial class view : System.Web.UI.Page
    {
        private DataView CustomerList;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindData();

            }
        }
        private void BindData()
        {
            drpCustomer.DataSourceID = SqlDataSource1.UniqueID;
            //drpCustomer.DataTextField = "Name";
            //drpCustomer.DataValueField = "CustomerID";
            drpCustomer.DataBind();

            CustomerList = (DataView)SqlDataSource1.Select(DataSourceSelectArguments.Empty);
            CustomerList.RowFilter = " CustomerID = " + drpCustomer.DataKeys[0].Value;
            if (CustomerList.Count == 1)
            {
                LoadCustomerInfo(CustomerList[0]);
            }
        }

        private void LoadCustomerInfo(DataRowView data)
        {
            SportsProLibrary.oCustomer Customer = new SportsProLibrary.oCustomer(data);
            Session["oCustomer"] = Customer;
            pName.InnerText = Customer.Name;
            pAddress.InnerText = Customer.CustomerAddress(data);
            pPhone.InnerText = Customer.Phone;
            lnkEmail.Text = Customer.Email;   
            lblWarning.Visible = false;
        }



        protected void btnAddtoList_ServerClick(object sender, EventArgs e)
        {            
            SportsProLibrary.oCustomer oCusotomer = (SportsProLibrary.oCustomer)Session["oCustomer"];
            SportsProLibrary.CustomerList custList = SportsProLibrary.CustomerList.GetCustomers();            
            if (Equals(custList[oCusotomer.Name], null))
            {
                custList.AddItem(oCusotomer);
                lblWarning.Visible = false;
                Response.Redirect("ContactList.aspx");
            }
            else
            {
                lblWarning.Visible = true;
            }
        }

        protected void lnkCustomer_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            CustomerList = (DataView)SqlDataSource1.Select(DataSourceSelectArguments.Empty);
            CustomerList.RowFilter = " CustomerID = " + lnk.CommandArgument;
            if (CustomerList.Count == 1)
            {
                LoadCustomerInfo(CustomerList[0]);
            }
        }
    }
}