using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Data.Linq.Mapping;

namespace SportsProLibrary
{
    public enum RegistrationField
    {
        None, CustomerID, ProductCode, RegistrationDate
    }
    public class Registration
    {
        public static string RegisterProduct(oRegistration _Registraiton)
        {
            return DBUtl.INSERT(_Registraiton);
        }
        public static List<oRegistration> GetRegistrations(RegistrationSearch _search)
        {
            if (_search == null)
            {
                _search = new RegistrationSearch();
            }

            StringBuilder sql = new StringBuilder();
            SqlCommand cmd = new SqlCommand();
            sql.Append("SELECT * FROM Registrations");
            if (_search.SearchBy != RegistrationField.None && _search.SearchTerm != null)
            {
                sql.Append(string.Format(" WHERE {0} = @SearchTerm", _search.SearchBy.ToString()));
                cmd.Parameters.AddWithValue("@SearchTerm", _search.SearchTerm);
            }
            if (_search.OrderBy != RegistrationField.None)
            {
                sql.Append(string.Format(" ORDER BY {0} {1}", _search.OrderBy.ToString(), _search.ResultsAsc == true ? "ASC" : "DESC"));
            }
            cmd.CommandText = sql.ToString();

            DataTable dtresult = DBUtl.SQLSelect(cmd);
            List<oRegistration> Registrations = new List<oRegistration>();

            foreach (DataRow row in dtresult.Rows)
            {
                oRegistration Registration = new oRegistration(row);
                Registrations.Add(Registration);
            }
            return Registrations;

        }
    }

    public class RegistrationSearch
    {
        public RegistrationField SearchBy { get; set; }
        public RegistrationField OrderBy { get; set; }
        public object SearchTerm { get; set; }
        public bool ResultsAsc { get; set; }
        public RegistrationSearch()
        {
            this.ResultsAsc = true;
            this.SearchBy = RegistrationField.None;
            this.OrderBy = RegistrationField.ProductCode;
            this.SearchTerm = null;
        }

        public List<oRegistration> PerformSearch()
        {
            return Registration.GetRegistrations(this);
        }
    }

    [Table (Name ="Registrations")]
    public class oRegistration
    {
        [Column (Name = "CustomerID")]
        public string CustomerID { get; set; }
        [Column(Name = "ProductCode")]
        public string ProductCode { get; set; }
        [Column(Name = "RegistrationDate")]
        public DateTime? RegistrationDate { get; set; }

        public oRegistration() {
            this.RegistrationDate = DateTime.Now;
        }
        public oRegistration(DataRow row)
        {
            this.CustomerID = row["CustomerID"].ToString();
            this.ProductCode = row["ProductCode"].ToString();
            this.RegistrationDate = DateTime.Parse(row["RegistrationDate"].ToString());
        }

        public string Save()
        {
            string results = Registration.RegisterProduct(this);
            return results;
        }
    }
}
