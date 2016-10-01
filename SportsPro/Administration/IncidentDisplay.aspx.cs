using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SportsPro.Administration
{
    public partial class IncidentDisplay : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadIncidents();
            }
        }

        protected void LoadIncidents()
        {
            SportsProLibrary.IncidentSearch _search = new SportsProLibrary.IncidentSearch();
            _search.OrderBy = SportsProLibrary.IncidentFields.DateOpened;
            lstIncidents.DataSource = _search.Find();
            lstIncidents.DataBind();
        }

        protected void lstIncidents_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            ((DataPager)lstIncidents.FindControl("DataPager1")).SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            LoadIncidents();
        }
    }
}