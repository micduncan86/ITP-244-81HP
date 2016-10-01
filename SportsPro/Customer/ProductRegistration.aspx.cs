using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SportsPro.Customer
{
    public partial class ProductRegistration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

            }
        }

        protected bool GetCustomer(string _CustomerID)
        {
            SportsProLibrary.CustomerSearch _search = new SportsProLibrary.CustomerSearch();
            _search.SearchBy = SportsProLibrary.CustomerFields.CustomerID;
            _search.SearchTerm = _CustomerID;


            var Customer = SportsProLibrary.Customers.GetCustomers(_search).FirstOrDefault();
            if (Customer != null)
            {
                hdnCustomerID.Value = Customer.CustomerID.ToString();
                lblCustomerName.Text = Customer.Name;
                LoadProducts();
                LoadRegistations(hdnCustomerID.Value);
                btnRegister.Enabled = true;
                return true;
            }
            else
            {
                hdnCustomerID.Value = "";
                ddlProducts.Items.Clear();
                btnRegister.Enabled = false;
                return false;
            }


        }
        protected void LoadRegistations(string CustomerID)
        {
            SportsProLibrary.RegistrationSearch _Search = new SportsProLibrary.RegistrationSearch();
            _Search.SearchBy = SportsProLibrary.RegistrationField.CustomerID;
            _Search.SearchTerm = CustomerID;
            var regs = _Search.PerformSearch();

            foreach (ListItem li in ddlProducts.Items)
            {
                if (regs.FirstOrDefault(x => x.ProductCode == li.Value) != null)
                {
                    li.Text += " (registered)";
                    li.Attributes.Add("style", "background-color: #337ab7;");
                }
            }

        }
        protected void LoadProducts()
        {
            if (Session["Products"] == null)
            {
                Session["Products"] = SportsProLibrary.Products.GetProducts();
            }
            ddlProducts.DataSource = Session["Products"];
            ddlProducts.DataTextField = "Name";
            ddlProducts.DataValueField = "ProductCode";
            ddlProducts.DataBind();
        }
        protected void btnGetCustomer_Click(object sender, EventArgs e)
        {
            GetCustomer(txtCustomerId.Text);
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            if (hdnCustomerID.Value != "" && ddlProducts.SelectedValue != "")
            {
                int Success = 0;
                SportsProLibrary.oRegistration oReg = new SportsProLibrary.oRegistration();
                oReg.CustomerID = hdnCustomerID.Value;
                oReg.ProductCode = ddlProducts.SelectedValue;
                string results = oReg.Save();
                Int32.TryParse(results, out Success);
                if (Success == 1)
                {
                    SendEmail(oReg);
                    lblError.Text = "An email has been sent. Thank you.";
                    LoadRegistations(hdnCustomerID.Value);
                }
                else
                {
                    lblError.Text = string.Format("This product has already been registered.");
                }
            }
        }

        protected void FindCustomerByID(object source, ServerValidateEventArgs args)
        {
            args.IsValid = GetCustomer(args.Value);
        }

        private string EmailBody()
        {
            System.Text.StringBuilder s = new System.Text.StringBuilder();

            s.Append("<table>");
            s.Append("<tbody><tr>");
            s.Append("<td> Phone:</td> ");
            s.Append("<td>");
            s.Append("<pre > 1 - 800 - 555 - 0400 < br > Weekdays, 8 to 5 Pacific Time </ pre >");
            s.Append("</td >");
            s.Append("</tr>");
            s.Append("<tr>");
            s.Append("<td> E - mail:</td>");
            s.Append("<td><pre><a href ='mailto: sportspro@sportsprosoftware.com'>sportspro@sportsprosoftware.com </ a></pre></td>");
            s.Append("</tr>");
            s.Append("<tr>");
            s.Append("<td>Fax:</td>");
            s.Append("<td>");
            s.Append("<pre> 1 - 559 - 555 - 2732 </pre>");
            s.Append("</td>");
            s.Append("</tr>");
            s.Append("<tr>");
            s.Append("<td>Address:</ td >");
            s.Append("<td>");
            s.Append("<pre> SportsPro Software Inc.< br > 1500 N.Main Street < br > Fresno, California 93710 </pre>");
            s.Append("</td>");
            s.Append("</tr>");
            s.Append("</tbody></table>");

            return s.ToString();
        }

        private int SendEmail(SportsProLibrary.oRegistration _Registration)
        {
            // return value indicator
            // 0 = success
            // 1 = .....

            //pattern: \w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*

            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();

            msg.To.Add(new System.Net.Mail.MailAddress(new SportsProLibrary.oCustomer(Convert.ToInt32(_Registration.CustomerID)).Email));
            msg.Bcc.Add(new System.Net.Mail.MailAddress("mbd2682@email.vccs.edu"));
            msg.From = new System.Net.Mail.MailAddress("sportspor@sportsprosoftware.com", "Sports Pro Software");
            msg.Subject = String.Format("Product Registration of {0}", _Registration.ProductCode);
            msg.IsBodyHtml = true;

            msg.Body = "<html><body>";
            msg.Body += string.Format("<{0}>{1}</{0}>", "section", "");

            msg.Body += string.Format("<{0}>Thank you for registering {1}  with us. You will be notified of any future updates.</{0}>", "section", _Registration.ProductCode);

            msg.Body += string.Format("<{0}>{1}</{0}>", "section", "");

            msg.Body += EmailBody();
            msg.Body += "</body></html>";



            System.Text.StringBuilder msgBld = new System.Text.StringBuilder();
            msgBld.AppendFormat("Thank you for registering {0} with us. You will be notified of any future updates.", _Registration.ProductCode);
            msgBld.AppendLine("<br />");
            msgBld.AppendLine("The Sports Pro Team");


            System.Net.Mail.AlternateView plainText = System.Net.Mail.AlternateView.CreateAlternateViewFromString(msgBld.ToString());

            msg.AlternateViews.Add(plainText);

            System.Net.Mail.SmtpClient smtpClient = new System.Net.Mail.SmtpClient("localhost", 25);

            using (smtpClient)
            {
                try
                {
                    smtpClient.Send(msg);
                    return 0;
                }
                catch (System.Net.Mail.SmtpException ex)
                {
                    throw new ApplicationException("SMTPException has occurred. " + ex.InnerException.Message);

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

        }
    }
}