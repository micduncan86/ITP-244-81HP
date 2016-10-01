using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SportsPro.Customer
{
    public partial class ContactList : System.Web.UI.Page
    {

        SportsProLibrary.CustomerList CustList;
        protected void Page_Load(object sender, EventArgs e)
        {
            CustList = SportsProLibrary.CustomerList.GetCustomers();
            if (!Page.IsPostBack)
            {
                LoadList();
            }
        }
        private void LoadList()
        {                     
            lstContactList.Items.Clear();

            for (var idx = 0; idx < CustList.count; idx++)
            {
                SportsProLibrary.oCustomer oCustomer = CustList[idx];
                ListItem li = new ListItem();
                li.Value = string.Format("{0}",idx);
                li.Text = oCustomer.ContactDisplay();               
                lstContactList.Items.Add(li);
            }
            

        }

        protected void btnRemoveCustomer_Click(object sender, EventArgs e)
        {
            if (lstContactList.SelectedIndex != -1)
            {
                CustList.RemoveAt(lstContactList.SelectedIndex);
                lstContactList.Items.RemoveAt(lstContactList.SelectedIndex);
            }
               
        }

        protected void btnClearList_Click(object sender, EventArgs e)
        {
            CustList.Clear();
            lstContactList.Items.Clear();
        }
    }
}