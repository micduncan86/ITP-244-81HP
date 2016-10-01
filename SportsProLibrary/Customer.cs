using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Data.Linq.Mapping;

/// <summary>
/// Summary description for Customer
/// </summary>
namespace SportsProLibrary
{
    public enum CustomerFields
    {
        None, CustomerID, Name, Address, City, State, ZipCode, Phone, Email
    }
    public class Customers
    {       
        public static DataTable GetCustomersDataView()
        {
            SqlCommand cmd = new SqlCommand("Select CustomerID, Name from Customers ORDER BY Name ASC");
            return DBUtl.SQLSelect(cmd);
        }      

       
        public static List<oCustomer> GetCustomers(CustomerSearch _search = null)
        {
            if (_search == null)
            {
                _search = new CustomerSearch();
            }

            StringBuilder sql = new StringBuilder();
            SqlCommand cmd = new SqlCommand();
            sql.Append("SELECT * FROM Customers");
            if (_search.SearchBy != CustomerFields.None && _search.SearchTerm != null)
            {
                sql.Append(string.Format(" WHERE {0} = @SearchTerm", _search.SearchBy.ToString()));
                cmd.Parameters.AddWithValue("@SearchTerm", _search.SearchTerm);
            }
            if (_search.CustomerHasIncidents)
            {
                sql.Append(string.Format(" {0} CustomerID IN ",sql.ToString().Contains(" WHERE ") ? "AND" : "WHERE"));
                sql.Append(" (SELECT DISTINCT CustomerID FROM Incidents WHERE TechID IS NOT NULL)");
            }
            if (_search.OrderBy != CustomerFields.None)
            {
                sql.Append(string.Format(" ORDER BY {0} {1}", _search.OrderBy.ToString(), _search.ResultsAsc == true ? "ASC" : "DESC"));
            }
            cmd.CommandText = sql.ToString();

            DataTable dtresult = DBUtl.SQLSelect(cmd);
            List<oCustomer> Customers = new List<oCustomer>();

            foreach(DataRow row in dtresult.Rows)
            {
                oCustomer Customer = new oCustomer(row);                
                Customers.Add(Customer);
            }
            return Customers;
        }
        


    }
    public class CustomerSearch
    {
        public CustomerFields SearchBy { get; set; }
        public CustomerFields OrderBy { get; set; }

        public object SearchTerm { get; set; }
        public bool ResultsAsc { get; set; }

        public bool CustomerHasIncidents { get; set; }

        public CustomerSearch()
        {
            this.SearchBy = CustomerFields.Name;
            this.OrderBy = CustomerFields.Name;
            this.SearchTerm = null;
            this.ResultsAsc = true;
            this.CustomerHasIncidents = false;
        }
        public List<oCustomer> Find()
        {
            return Customers.GetCustomers(this);
        }

    }
    [Table(Name ="Customers")]
    public class oCustomer
    {
        [Column(Name = "CustomerID", IsPrimaryKey = true)]
        public int CustomerID { get; set; }
        [Column(Name = "Name")]
        public string Name { get; set; }
        [Column(Name = "Address")]
        public string Address { get; set; }
        [Column(Name = "City")]
        public string City { get; set; }
        [Column(Name = "State")]
        public string State { get; set; }
        [Column(Name = "ZipCode")]
        public string ZipCode { get; set; }
        [Column(Name = "Phone")]
        public string Phone { get; set; }
        [Column(Name = "Email")]
        public string Email { get; set; }
        public string CustomerAddress(DataRowView data)
        {
            return string.Format("{0}{1}, {2}", this.Address + Environment.NewLine, this.City + " " + this.State, this.ZipCode);
        }
        public string ContactDisplay()
        {
            return String.Format("{0}:\t\t\t{1};\t\t\t{2}", this.Name, this.Phone, this.Email);
        }
        public string Add()
        {
            return DBUtl.INSERT(this);
        }
        public string Save()
        {
            return DBUtl.UPDATE(this);
        }
        public string Delete()
        {
            return DBUtl.DELETE(this);
        }
        public oCustomer()
        {
        }
        public oCustomer(int CustomerID)
        {
            CustomerSearch _Search = new CustomerSearch();
            _Search.SearchBy = CustomerFields.CustomerID;
            _Search.SearchTerm = CustomerID;
            _Search.OrderBy = CustomerFields.CustomerID;
            var cust = _Search.Find().FirstOrDefault();
            this.Name = cust.Name;
            this.Address = cust.Address;
            this.City = cust.City;
            this.State = cust.State;
            this.ZipCode = cust.ZipCode;
            this.Phone = cust.Phone;
            this.Email = cust.Email;
        }
        public oCustomer(DataRowView data)
        {
            this.Name = data["Name"].ToString();
            this.Address = data["Address"].ToString();
            this.City = data["City"].ToString();
            this.State = data["State"].ToString();
            this.ZipCode = data["ZipCode"].ToString();
            this.Phone = data["Phone"].ToString();
            this.Email = data["Email"].ToString();
            this.CustomerID = Convert.ToInt32(data["CustomerID"].ToString());
        }
        public oCustomer(DataRow data)
        {
            this.Name = data["Name"].ToString();
            this.Address = data["Address"].ToString();
            this.City = data["City"].ToString();
            this.State = data["State"].ToString();
            this.ZipCode = data["ZipCode"].ToString();
            this.Phone = data["Phone"].ToString();
            this.Email = data["Email"].ToString();
            this.CustomerID = Convert.ToInt32(data["CustomerID"].ToString());
        }
    }

    public class CustomerList
    {
        public int count
        {
            get { return this._customerlist.Count(); }
        }
        private List<oCustomer> _customerlist;

        public oCustomer this[int _Index]
        {
            get
            {
                if (this._customerlist.Count > _Index)
                {
                    return this._customerlist[_Index];
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (_Index > this._customerlist.Count)
                {
                    this._customerlist.Add(value);
                }
                else
                {
                    this._customerlist[_Index] = value;
                }
            }
        }
        public oCustomer this[string _Name]
        {
            get
            {
                return this._customerlist.Find(x => x.Name == _Name);
            }
        }

        public CustomerList(List<oCustomer> customers = null)
        {
            if (!Equals(customers, null))
            {
                this._customerlist = customers;
            }
            else
            {
                this._customerlist = new List<oCustomer>();
            }

        }
        public static CustomerList GetCustomers()
        {
            if (!Equals(System.Web.HttpContext.Current.Session["ContactList"], null))
            {
                return new CustomerList((List<SportsProLibrary.oCustomer>)System.Web.HttpContext.Current.Session["ContactList"]);
            }
            else {
                System.Web.HttpContext.Current.Session["ContactList"] = new List<oCustomer>();
                return new CustomerList();
            }
        }
        public void AddItem(oCustomer customer)
        {
            if (!Equals(customer, null))
            {
                this._customerlist.Add(customer);
            }

        }

        public void RemoveAt(int _index)
        {
            this._customerlist.RemoveAt(_index);
        }

        public void Clear()
        {
            this._customerlist.Clear();
        }
    }
}