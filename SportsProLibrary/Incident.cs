using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Data.Linq.Mapping;

namespace SportsProLibrary
{
    public enum IncidentFields
    {
        None,IncidentId,CustomerID,ProductCode,TechID,DateOpened,DateClosed,Title,Description
    }
    public class Incidents
    {
        public static List<oIncident> GetIncidents(IncidentSearch _search)
        {
            if (_search == null)
            {
                _search = new IncidentSearch();
            }

            List<oIncident> lstIncidents = new List<oIncident>();

            StringBuilder sql = new StringBuilder();
            SqlCommand cmd = new SqlCommand();

            sql.Append("SELECT Incidents.*,");
            sql.Append("Customers.CustomerID as Customer_ID,Customers.Name as Customer_Name,Customers.Address as Customer_Address,Customers.City as Customer_City,Customers.State as Customer_State,Customers.ZipCode as Customer_Zipcode,Customers.Phone as Customer_Phone,Customers.Email as Customer_Email,");
            sql.Append("Technicians.TechID as Technician_ID,Technicians.Name as Technician_Name,Technicians.Email as Technician_Email,Technicians.Phone as Technician_Phone");
            sql.Append(" FROM Incidents");
            sql.Append(" INNER JOIN Customers ON Incidents.CustomerID = Customers.CustomerID");
            sql.Append(" INNER JOIN Technicians ON Incidents.TechID = Technicians.TechID");
            if (_search.SearchBy != IncidentFields.None && _search.SearchTerm != null)
            {
                sql.Append(string.Format(" WHERE {0} = @SearchTerm", "Incidents." + _search.SearchBy.ToString()));
                cmd.Parameters.AddWithValue("@SearchTerm", _search.SearchTerm);
            }
            if (_search.ClosedOnly)
            {
                sql.Append(string.Format(" {0}  ISNULL(Incidents.DateClosed,'') <> ''", sql.ToString().Contains(" WHERE ") ? "AND": "WHERE"));
            }
            if (_search.OrderBy != IncidentFields.None)
            {
                sql.Append(string.Format(" ORDER BY {0} {1}", "Incidents." + _search.OrderBy.ToString(), _search.ResultsAsc == true ? "ASC" : "DESC"));
            }
            cmd.CommandText = sql.ToString();

            DataTable dtresult = DBUtl.SQLSelect(cmd);

            foreach (DataRow row in dtresult.Rows)
            {
                oIncident Incident = new oIncident(row);
                lstIncidents.Add(Incident);
            }
            return lstIncidents;


        }
              
    }

    public class IncidentSearch
    {
        public IncidentFields SearchBy { get; set; }
        public IncidentFields OrderBy { get; set; }

        public object SearchTerm { get; set; }
        public bool ResultsAsc { get; set; }

        public bool ClosedOnly { get; set; }

        public IncidentSearch()
        {
            this.SearchBy = IncidentFields.None;
            this.OrderBy = IncidentFields.DateOpened;
            this.SearchTerm = null;
            this.ResultsAsc = true;
            this.ClosedOnly = false;
        }
        public IncidentSearch(IncidentFields _searchby, object _searchterm, IncidentFields _orderBy = IncidentFields.None)
        {
            this.SearchBy = _searchby;
            this.SearchTerm = _searchterm;
        }
        public List<oIncident> Find()
        {

            return Incidents.GetIncidents(this);
        }
    }

    [Table(Name = "Incidents")]
    public class oIncident
    {
        [Column(Name ="IncidentID", IsPrimaryKey = true)]
        public int IncidentID { get; set; }
        [Column(Name = "CustomerID")]
        public int CustomerID { get; set; }

        //public oCustomer Customer
        //{
        //    get
        //    {
        //        var lst = new Customers().GetCustomers(Customers.CustomerFields.CustomerID, Customers.CustomerFields.Name, this.CustomerID, false);
        //        if (lst.Count > 0)
        //        {
        //            return lst[0];
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //    set
        //    {
        //        this.Customer = value;
        //    }
        //}


        public oCustomer Customer { get; set; }

        [Column(Name = "ProductCode")]
        public string ProductCode { get; set; }

        [Column(Name = "TechID")]
        public int TechID { get; set; }

        public oTechnician Tech { get; set; }
        //public oTechnician Tech
        //{
        //    get
        //    {
        //        var lst = new Technicians().GetTechnicians(this.TechID);
        //        if (lst.Count > 0)
        //        {
        //            return lst[0];
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //    set
        //    {
        //        this.Tech = value;
        //    }
        //}

        [Column(Name = "DateOpened")]
        public DateTime? DateOpened { get; set; }

        [Column(Name = "DateClosed")]
        public DateTime? DateClosed { get; set; }

        [Column(Name = "Title")]
        public string Title { get; set; }

        [Column(Name = "Description")]
        public string Description { get; set; }

        public string Save()
        {
            return DBUtl.UPDATE(this);
        }
        public string Add()
        {
            return DBUtl.INSERT(this);
        }
        public string Delete()
        {
            return DBUtl.DELETE(this);
        }
        public oIncident()
        {            
        }
        public oIncident(DataRow dr)
        {
             this.IncidentID = Convert.ToInt32(dr["CustomerID"].ToString());
            this.ProductCode = dr["ProductCode"].ToString();
            this.Title = dr["Title"].ToString();
            this.Description = dr["Description"].ToString();
            DateTime dClose;
            DateTime dOpen;
            if (DateTime.TryParse(dr["DateClosed"].ToString(), out dClose))
            {
                this.DateClosed = dClose;
            }
            if (DateTime.TryParse(dr["DateOpened"].ToString(), out dOpen))
            {
                this.DateOpened = dOpen;
            }

            this.TechID = Convert.ToInt32(dr["TechID"].ToString());
            this.Tech = new oTechnician();
            this.Tech.TechID = this.TechID;
            this.Tech.Name = dr["Technician_Name"].ToString();
            this.Tech.Email = dr["Technician_Email"].ToString();
            this.Tech.Phone = dr["Technician_Phone"].ToString();

            this.CustomerID = Convert.ToInt32(dr["CustomerID"].ToString());
            this.Customer = new oCustomer();
            this.Customer.CustomerID = this.CustomerID;
            this.Customer.Address = dr["Customer_Address"].ToString();
            this.Customer.City = dr["Customer_City"].ToString();
            this.Customer.State = dr["Customer_State"].ToString();
            this.Customer.ZipCode = dr["Customer_Zipcode"].ToString();
            this.Customer.Email = dr["Customer_Email"].ToString();
            this.Customer.Name = dr["Customer_Name"].ToString();
            this.Customer.Phone = dr["Customer_Phone"].ToString();
        }
        public string CustomerIncidentDisplay()
        {
            return string.Format("Incident for product {0} closed {1} ({2})", this.ProductCode, this.DateClosed.Value.ToShortDateString(), this.Title);
        }


    }

    public class Survey
    {
        private bool _contact;
        public int CustomerID { get; set; }
        public int IncidentID { get; set; }
        public int ResponseTime { get; set; }
        public int TechEfficiency { get; set; }
        public int Resolution { get; set; }
        public string Comments { get; set; }
        public bool Contact
        {
            get { return this._contact; }
            set
            {
                this._contact = value;
                System.Web.HttpContext.Current.Session["SurveyContact"] = value;
            }
        }
        public string ContactBy { get; set; }

        public Survey()
        {

        }

    }
}
