using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SportsPro.Administration
{
    public partial class CustomerMaintenance : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindCustomers();
            }
        }

        private void BindCustomers()
        {       
            if (Session["CustomerList"] == null || !Page.IsPostBack)
            {
                Session["CustomerList"] = SportsProLibrary.Customers.GetCustomers(null);
            }
            grdCustomers.DataSource = (List<SportsProLibrary.oCustomer>)Session["CustomerList"];
            grdCustomers.DataBind();
        }

        protected void grdCustomers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdCustomers.PageIndex = e.NewPageIndex;
            BindCustomers();
        }
        protected void LoadCustomerDetails(int _CustomerID)
        {
            var _cust = ((List<SportsProLibrary.oCustomer>)Session["CustomerList"]).Where(x => x.CustomerID == _CustomerID).ToList();
            dtlCustomer.DataSource = _cust;
            dtlCustomer.DataBind();
        }

        protected void grdCustomers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "selectCustomer":
                    GridViewRow row = ((GridView)sender).Rows[Convert.ToInt32(e.CommandArgument)];
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        var custID = ((GridView)sender).DataKeys[Convert.ToInt32(e.CommandArgument)].Value;
                        LoadCustomerDetails((int)custID);
                    }
                    break;
                case "sortCustomer":
                    if (e.CommandArgument.ToString() == "City")
                    {
                        Session["CustomerList"] = ((List<SportsProLibrary.oCustomer>)Session["CustomerList"]).OrderBy(p => p.City).ToList();
                    }
                    else if (e.CommandArgument.ToString() == "State")
                    {
                        Session["CustomerList"] = ((List<SportsProLibrary.oCustomer>)Session["CustomerList"]).OrderBy(p => p.State).ToList();
                    }
                    else
                    {
                        Session["CustomerList"] = ((List<SportsProLibrary.oCustomer>)Session["CustomerList"]).OrderBy(p => p.Name).ToList();
                    }
                    BindCustomers();
                    break;
                default:

                    break;
            }

        }

        protected void dtlCustomer_DataBound(object sender, EventArgs e)
        {
            DetailsView _view = (DetailsView)sender;
            if (_view.CurrentMode == DetailsViewMode.Edit)
            {
                ((TextBox)_view.FindControl("txtName")).Text = DataBinder.Eval(_view.DataItem, "Name").ToString();
                ((TextBox)_view.FindControl("txtAddress")).Text = DataBinder.Eval(_view.DataItem, "Address").ToString();
                ((TextBox)_view.FindControl("txtCity")).Text = DataBinder.Eval(_view.DataItem, "City").ToString();
                ((TextBox)_view.FindControl("txtState")).Text = DataBinder.Eval(_view.DataItem, "State").ToString();
                ((TextBox)_view.FindControl("txtZipCode")).Text = DataBinder.Eval(_view.DataItem, "ZipCode").ToString();
                ((TextBox)_view.FindControl("txtPhone")).Text = DataBinder.Eval(_view.DataItem, "Phone").ToString();
                ((TextBox)_view.FindControl("txtEmail")).Text = DataBinder.Eval(_view.DataItem, "Email").ToString();

            }
        }
        protected void DeleteCustomer(DetailsView _view, int _custID)
        {
            SportsProLibrary.oCustomer _cust = ((List<SportsProLibrary.oCustomer>)Session["CustomerList"]).FirstOrDefault(x => x.CustomerID == (int)_custID);
            _cust.Delete();
            ((List<SportsProLibrary.oCustomer>)Session["CustomerList"]).Remove(_cust);

        }
        protected int AddCustomer(DetailsView _view)
        {
            SportsProLibrary.oCustomer _cust = new SportsProLibrary.oCustomer();
            _cust.Name = ((TextBox)_view.FindControl("txtName")).Text;
            _cust.Address = ((TextBox)_view.FindControl("txtAddress")).Text;
            _cust.City = ((TextBox)_view.FindControl("txtCity")).Text;
            _cust.State = ((TextBox)_view.FindControl("txtState")).Text;
            _cust.ZipCode = ((TextBox)_view.FindControl("txtZipCode")).Text;
            _cust.Phone = ((TextBox)_view.FindControl("txtPhone")).Text;
            _cust.Email = ((TextBox)_view.FindControl("txtEmail")).Text;
            _cust.Add();
            ((List<SportsProLibrary.oCustomer>)Session["CustomerList"]).Add(_cust);
            return _cust.CustomerID;
        }
        protected void UpdateCustomer(DetailsView _view, int _custID)
        {
            SportsProLibrary.oCustomer _cust = ((List<SportsProLibrary.oCustomer>)Session["CustomerList"]).FirstOrDefault(x => x.CustomerID == (int)_custID);
            _cust.Name = ((TextBox)_view.FindControl("txtName")).Text;
            _cust.Address = ((TextBox)_view.FindControl("txtAddress")).Text;
            _cust.City = ((TextBox)_view.FindControl("txtCity")).Text;
            _cust.State = ((TextBox)_view.FindControl("txtState")).Text;
            _cust.ZipCode = ((TextBox)_view.FindControl("txtZipCode")).Text;
            _cust.Phone = ((TextBox)_view.FindControl("txtPhone")).Text;
            _cust.Email = ((TextBox)_view.FindControl("txtEmail")).Text;
            _cust.Save();
        }

        protected void dtlCustomer_ItemCommand(object sender, DetailsViewCommandEventArgs e)
        {
            try
            {


                DetailsView _view = (DetailsView)sender;
                var _custID = _view.DataKey.Value;
                switch (e.CommandName)
                {
                    case "editCustomer":
                        _view.ChangeMode(DetailsViewMode.Edit);
                        LoadCustomerDetails((int)_custID);
                        break;
                    case "cancelCustomer":
                        _view.ChangeMode(DetailsViewMode.ReadOnly);
                        LoadCustomerDetails((int)_custID);
                        break;
                    case "updateCustomer":
                        UpdateCustomer(_view, (int)_custID);
                        _view.ChangeMode(DetailsViewMode.ReadOnly);
                        LoadCustomerDetails((int)_custID);
                        BindCustomers();
                        break;
                    case "deleteCustomer":
                        DeleteCustomer(_view, (int)_custID);
                        dtlCustomer.DataSource = null;
                        dtlCustomer.DataBind();
                        BindCustomers();
                        break;
                    case "newCustomer":
                        _view.ChangeMode(DetailsViewMode.Insert);
                        LoadCustomerDetails((int)_custID);
                        break;
                    case "addCustomer":
                        _view.ChangeMode(DetailsViewMode.ReadOnly);
                        LoadCustomerDetails(AddCustomer(_view));
                        BindCustomers();
                        break;
                    default:

                        break;
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }
    }
}