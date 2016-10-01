using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SportsPro.Technician
{
    public partial class IncidentUpdate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadCustomers();
                LoadIncidents();
            }
        }
        protected void LoadCustomers()
        {
            SportsProLibrary.CustomerSearch _Search = new SportsProLibrary.CustomerSearch();
            _Search.CustomerHasIncidents = true;
            ddlCustomer.DataSource = _Search.Find();
            ddlCustomer.DataTextField = "Name";
            ddlCustomer.DataValueField = "CustomerID";
            ddlCustomer.DataBind();
        }
        protected void LoadIncidents()
        {
            SportsProLibrary.IncidentSearch _search = new SportsProLibrary.IncidentSearch();
            _search.SearchBy = SportsProLibrary.IncidentFields.CustomerID;
            _search.SearchTerm = Convert.ToInt32(ddlCustomer.SelectedValue);
            grdIncidents.DataSource = _search.Find();
            grdIncidents.DataBind();
        }

        protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadIncidents();
        }

        protected void grdIncidents_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "editIncident":
                    ((GridView)sender).EditIndex = Convert.ToInt32(e.CommandArgument);
                    LoadIncidents();
                    break;
                case "updateIncident":
                    Update(Convert.ToInt32(e.CommandArgument));
                    grdIncidents.EditIndex = -1;
                    LoadIncidents();
                    break;
                case "cancelIncident":
                    ((GridView)sender).EditIndex = -1;
                    LoadIncidents();
                    break;
                default:
                    break;
            }
        }

        protected void Update(int indx)
        {
            GridViewRow row = grdIncidents.Rows[indx];
            if ((row.RowState & DataControlRowState.Edit) > 0)
            {
                int ID = Convert.ToInt32(row.Cells[0].Text);
                TextBox txtProductCode = (TextBox)row.Controls[1].Controls[0];
                TextBox txtOpenDate = (TextBox)row.Controls[2].Controls[0];
                TextBox txtCloseDate = (TextBox)row.Controls[3].Controls[0];
                TextBox txtTitle = (TextBox)row.Controls[4].Controls[0];
                TextBox txtDescription = (TextBox)row.Controls[5].Controls[0];


                SportsProLibrary.IncidentSearch _search = new SportsProLibrary.IncidentSearch();
                _search.SearchBy = SportsProLibrary.IncidentFields.IncidentId;
                _search.SearchTerm = ID;
                SportsProLibrary.oIncident oIncicent = _search.Find().FirstOrDefault();
                
                oIncicent.ProductCode = txtProductCode.Text;
                oIncicent.DateOpened = txtOpenDate.Text == "" ? Convert.ToDateTime(DBNull.Value) : Convert.ToDateTime(txtOpenDate.Text);
                if (txtCloseDate.Text == "")
                {
                    oIncicent.DateClosed = null;
                }
                else
                {
                    oIncicent.DateClosed = Convert.ToDateTime(txtCloseDate.Text);
                }                
                oIncicent.Title = txtTitle.Text;
                oIncicent.Description = txtDescription.Text;
                oIncicent.Save();

                lblError.ForeColor = System.Drawing.Color.Green;
                lblError.Text = String.Format("{0}-{1} has been updated.", oIncicent.ProductCode, oIncicent.Title);       
            }
        }
        protected void grdIncidents_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowState == DataControlRowState.Edit)
            {



                //((TextBox)e.Row.Controls[0].Controls[0]).Width = Unit.Percentage(95);//ID
                ((TextBox)e.Row.Controls[1].Controls[0]).Width = Unit.Percentage(95);//Product
                ((TextBox)e.Row.Controls[2].Controls[0]).Width = Unit.Percentage(95);//Open
                ((TextBox)e.Row.Controls[2].Controls[0]).Text = Convert.ToDateTime(((TextBox)e.Row.Controls[2].Controls[0]).Text).ToShortDateString();
                ((TextBox)e.Row.Controls[3].Controls[0]).Width = Unit.Percentage(95);//Close
                if (((TextBox)e.Row.Controls[3].Controls[0]).Text != "")
                {
                    ((TextBox)e.Row.Controls[3].Controls[0]).Text = Convert.ToDateTime(((TextBox)e.Row.Controls[3].Controls[0]).Text).ToShortDateString();
                }
                ((TextBox)e.Row.Controls[4].Controls[0]).Width = Unit.Percentage(95);//title
                ((TextBox)e.Row.Controls[5].Controls[0]).Width = Unit.Percentage(95);//description
                ((TextBox)e.Row.Controls[5].Controls[0]).TextMode = TextBoxMode.MultiLine;


            }
        }
    }
}