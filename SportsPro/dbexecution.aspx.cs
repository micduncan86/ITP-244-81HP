using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using SportsProLibrary;
public partial class dbexecution : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            switch (Request.QueryString["Action"])
            {
                case "GetCustomerList":
                    GetCustomerList();
                    break;
       
         default:
                    break;
            }
        }
    }
    protected string GetCustomerList()
    { 
        
        System.Web.Script.Serialization.JavaScriptSerializer json = new System.Web.Script.Serialization.JavaScriptSerializer();

        List<oCustomer> Custs = Customers.GetCustomers(null);
 
        Response.ClearHeaders();
        Response.Clear();
        Response.AddHeader("Content-type", "text/json");
        Response.AddHeader("Content-type", "application/json");
        Response.Write(json.Serialize(Custs));
        Response.End();
        return "";
    }
}