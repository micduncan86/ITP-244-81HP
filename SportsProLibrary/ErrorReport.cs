using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsProLibrary
{
    public static class ErrorReport
    {
        public static void Report(Exception _ex)
        {
            if (_ex == null) _ex = System.Web.HttpContext.Current.Server.GetLastError();
            StringBuilder error = new StringBuilder();
            Dictionary<string, string> Report = new Dictionary<string, string>();
            Report.Add("Message", _ex.Message);
            Report.Add("Source", _ex.Source);
            Report.Add("Url", System.Web.HttpContext.Current.Request.RawUrl);
            Report.Add("IP", System.Web.HttpContext.Current.Request.UserHostAddress);
            Report.Add("UserAgent", System.Web.HttpContext.Current.Request.UserAgent);

            System.Web.Script.Serialization.JavaScriptSerializer json = new System.Web.Script.Serialization.JavaScriptSerializer();

            string tNow = DateTime.Today.ToString("MMddyyyy");
            string _path = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/errlog/" + tNow + ".log");
            System.IO.File.AppendAllText(_path, json.Serialize(Report));            
        }       

    }
    
}
