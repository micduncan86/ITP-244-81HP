using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SportsPro.Administration
{
    public partial class TechnicianIncidentSummary : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadTechList();
            }
        }

        protected void LoadTechList() {
            if (Session["TechnicianList"] == null)
            {
                SportsProLibrary.Technicians Technicians = new SportsProLibrary.Technicians();
                Session["TechnicianList"] = Technicians.GetTechnicians();
            }

            ddlTechnician.DataSource = (List<SportsProLibrary.oTechnician>)Session["TechnicianList"];
            ddlTechnician.DataTextField = "Name";
            ddlTechnician.DataValueField = "TechID";
            ddlTechnician.DataBind();

            //LoadOpenIncidents(Convert.ToInt32(ddlTechnician.SelectedValue));
            LoadIncidentGrid();

        }
        protected void LoadIncidentGrid()
        {

            SportsProLibrary.IncidentSearch _search = new SportsProLibrary.IncidentSearch();
            _search.SearchBy = SportsProLibrary.IncidentFields.TechID;
            _search.SearchTerm = Convert.ToInt32(ddlTechnician.SelectedValue);
            _search.OrderBy = SportsProLibrary.IncidentFields.DateOpened;

            grdIncidents.DataSource = _search.Find();
            grdIncidents.DataBind();

        }

        protected void ddlTechnician_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadIncidentGrid();
        }
    }
}