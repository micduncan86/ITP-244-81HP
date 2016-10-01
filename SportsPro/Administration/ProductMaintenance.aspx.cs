using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SportsPro.Administration
{
    public partial class ProductMaintenance : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindGrid();
            }
        }

        private void BindGrid(SportsProLibrary.Products Products = null)
        {
            if (Equals(Products, null))
            {
                Products = new SportsProLibrary.Products();
            }
            grdProducts.Width = Unit.Percentage(100);

            SportsProLibrary.ProductSearch _search = new SportsProLibrary.ProductSearch();
            grdProducts.DataSource = _search.Find();
            grdProducts.DataBind();
        }

        protected void grdProducts_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                {

                    TextBox _txtName = (TextBox)e.Row.FindControl("txtName");
                    TextBox _txtVersion = (TextBox)e.Row.FindControl("txtVersion");
                    TextBox _txtRelease = (TextBox)e.Row.FindControl("txtRelease");
                    Label _lblProductCode = (Label)e.Row.FindControl("lblProductCode");

                    _lblProductCode.Text = DataBinder.Eval(e.Row.DataItem, "ProductCode").ToString();
                    _txtName.Text = DataBinder.Eval(e.Row.DataItem, "Name").ToString();
                    _txtVersion.Text = DataBinder.Eval(e.Row.DataItem, "Version").ToString();
                    _txtRelease.Text = DataBinder.Eval(e.Row.DataItem, "ReleaseDate", "{0:MM/dd/yyyy}").ToString();

                }
                else if ((e.Row.RowState & DataControlRowState.Normal) == 0)
                {
                    Label _lblProductCode = (Label)e.Row.FindControl("lblProductCode");
                    _lblProductCode.Text = DataBinder.Eval(e.Row.DataItem, "ProductCode").ToString();
                }
            }
        }
        private void UpdateProduct(GridViewRow row)
        {
            if ((row.RowState & DataControlRowState.Edit) > 0)
            {
                TextBox _txtName = (TextBox)row.FindControl("txtName");
                TextBox _txtVersion = (TextBox)row.FindControl("txtVersion");
                TextBox _txtRelease = (TextBox)row.FindControl("txtRelease");
                Label _lblProductCode = (Label)row.FindControl("lblProductCode");




                SportsProLibrary.oProduct Product = new SportsProLibrary.oProduct();
                Product.Name = _txtName.Text;
                Product.Version = _txtVersion.Text;
                Product.ReleaseDate = Convert.ToDateTime(_txtRelease.Text);
                Product.ProductCode = _lblProductCode.Text;
                Product.Save();

                lblError.ForeColor = System.Drawing.Color.Green;
                lblError.Text = String.Format("{0}-{1} has been updated.", _lblProductCode.Text, txtName.Text);

            }
        }
        protected void grdProducts_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                GridViewRow row = ((GridView)sender).Rows[Convert.ToInt32(e.CommandArgument)];
                if (row.RowType == DataControlRowType.DataRow)
                {
                    switch (e.CommandName)
                    {
                        case "editProduct":
                            grdProducts.EditIndex = Convert.ToInt32(e.CommandArgument);
                            BindGrid();
                            break;
                        case "cancelProduct":
                            grdProducts.EditIndex = -1;
                            BindGrid();
                            break;
                        case "updateProduct":
                            UpdateProduct(row);
                            grdProducts.EditIndex = -1;
                            BindGrid();
                            break;
                        case "deleteProduct":
                            SportsProLibrary.oProduct Product = new SportsProLibrary.oProduct();
                            Product.ProductCode = ((Label)row.FindControl("lblProductCode")).Text;
                            Product.Delete();
                            lblError.ForeColor = System.Drawing.Color.Green;
                            lblError.Text = String.Format("{0} has been removed.", ((Label)row.FindControl("lblProductCode")).Text);
                            grdProducts.EditIndex = -1;
                            BindGrid();
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                lblError.ForeColor = System.Drawing.Color.Red;
                lblError.Text = ex.Message;
            }
        }

        protected void btnAddProduct_Click(object sender, EventArgs e)
        {
            
            SportsProLibrary.oProduct Product = new SportsProLibrary.oProduct();
            Product.Name = txtName.Text;
            Product.Version = txtVersion.Text;
            Product.ReleaseDate = Convert.ToDateTime(txtReleaseDate.Text);
            Product.ProductCode = txtProductCode.Text;
            string results = Product.Add();
            if (results  == "1") { 
                lblError.ForeColor = System.Drawing.Color.Green;
                lblError.Text = String.Format("{0}-{1} has been added.", txtProductCode.Text, txtName.Text);
                txtName.Text = "";
                txtVersion.Text = "";
                txtReleaseDate.Text = "";
                txtProductCode.Text = "";
                BindGrid();
            }
            else
            {
                lblError.ForeColor = System.Drawing.Color.Red;
                lblError.Text = results;
                //throw ex;
            }
        }
    }
}