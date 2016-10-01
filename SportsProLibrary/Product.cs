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
    public enum ProductFields
    {
        None, ProductCode, Name, Version, ReleaseDate
    }
    public class Products
    {

        public static List<oProduct> GetProducts(ProductSearch _search = null)
        {
            if (_search == null)
            {
                _search = new ProductSearch();
            }

            StringBuilder sql = new StringBuilder();
            SqlCommand cmd = new SqlCommand();
            sql.Append("SELECT * FROM Products");
            if (_search.SearchBy != ProductFields.None && _search.SearchTerm != null)
            {
                sql.Append(string.Format(" WHERE {0} = @SearchTerm", _search.SearchBy.ToString()));
                cmd.Parameters.AddWithValue("@SearchTerm", _search.SearchTerm);
            }
            if (_search.OrderBy != ProductFields.None)
            {
                sql.Append(string.Format(" ORDER BY {0} {1}", _search.OrderBy.ToString(), _search.ResultsAsc == true ? "ASC" : "DESC"));
            }
            cmd.CommandText = sql.ToString();

            DataTable dtresult = DBUtl.SQLSelect(cmd);
            List<oProduct> Products = new List<oProduct>();

            foreach (DataRow row in dtresult.Rows)
            {
                oProduct Product = new oProduct(row);
                Products.Add(Product);
            }
            return Products;
        }

    }

    public class ProductSearch
    {
        public ProductFields SearchBy { get; set; }
        public ProductFields OrderBy { get; set; }

        public object SearchTerm { get; set; }
        public bool ResultsAsc { get; set; }

        public ProductSearch()
        {
            this.SearchBy = ProductFields.None;
            this.OrderBy = ProductFields.Name;
            this.SearchTerm = null;
            this.ResultsAsc = true;
        }
        public List<oProduct> Find()
        {
            return Products.GetProducts(this);
        }
    }


    [Table(Name = "Products")]
    public class oProduct
    {
        [Column(Name = "ProductCode", IsPrimaryKey = true)]
        public string ProductCode { get; set; }
        [Column(Name = "Name")]
        public string Name { get; set; }
        [Column(Name = "Version")]
        public string Version { get; set; }
        [Column(Name = "ReleaseDate")]
        public DateTime? ReleaseDate { get; set; }

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
        public oProduct()
        {
        }
        public oProduct(DataRow row)
        {
            this.Name = row["Name"].ToString();
            this.Version = row["Version"].ToString();
            DateTime dReleaseDate;
            if (DateTime.TryParse(row["ReleaseDate"].ToString(), out dReleaseDate))
            {
                this.ReleaseDate = dReleaseDate;
            }
            this.ProductCode = row["ProductCode"].ToString();
        }

        public string Register(int _CustomerID)
        {
            oRegistration oReg = new oRegistration();
            oReg.CustomerID = _CustomerID.ToString();
            oReg.ProductCode = this.ProductCode;
            oReg.RegistrationDate = DateTime.Now;
            return oReg.Save();
        }

    }

}
