using System;
using System.Web.UI;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        decimal _SalePrice = 0;
        decimal _DiscountAmt = 0;
        decimal _TotalAmt = 0;

        //setting jQery validation to false
        UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
        if (!IsPostBack)
        {
            //conditional check for session variable and assignment to local variable.
            _SalePrice = Equals(Session["Sale_Price"], null) ? -1 : decimal.Parse(Session["Sale_Price"].ToString());
            _DiscountAmt = Equals(Session["Discount_Amount"], null) ? -1 : decimal.Parse(Session["Discount_Amount"].ToString());
            _TotalAmt = Equals(Session["Total_Price"], null) ? -1 : decimal.Parse(Session["Total_Price"].ToString());


            // session value must be >=0 as -1 represent null (easier condition checking)
            if (_SalePrice >= 0 && _DiscountAmt >= 0 && _TotalAmt >= 0 )
            {
                //format values for label display
                lblSalePrice.Text = String.Format("{0:C}", _SalePrice);
                lblDiscountAmount.Text = String.Format("{0:C}", _DiscountAmt);
                lblTotalPrice.Text = String.Format("{0:C}", _TotalAmt);
            }
            else
            {
                //clear and abandon session when direct accessing confirmation page.
                Session.Clear();
                Session.Abandon();
                Response.Redirect("Default.aspx");
            }
        }

    }
    protected void SendQuotation(object sender, EventArgs e)
    {
        string _ConfirmName = null;
        string _ConfirmEmail = null;

        //Conditional check on enter name and email and assignment to locla variable
        _ConfirmName = Equals(txtConfirmationName.Text, "") ? null : txtConfirmationName.Text.Trim();
        _ConfirmEmail = Equals(txtConfirmationEmailAddress.Text, "") ? null : txtConfirmationEmailAddress.Text.Trim();

        //using custom function to check validity of email address. Always validate on client and server side together.
        if (!Equals(_ConfirmName, null) && isValidEmail(_ConfirmEmail))
        {
            lblConfirmDirections.Text = "Quotation Sent via email. Function has not been implemented.";
            //rought implemenetation of smtpclient and send email.
            SendEmail(_ConfirmName, _ConfirmEmail);
        }
        lblConfirmDirections.Text = "Please provide a name and email address to send quotation.";
    }
    protected void Return(object sender, EventArgs e)
    {
        //clear and abandon session when user request to return to quotation page.
        Session.Clear();
        Session.Abandon();
        Response.Redirect("Default.aspx");
    }

    private bool isValidEmail(string emailaddress)
    {
        try
        {
            // trys to establish a .net framework mailaddress from supplied variable.
            // could have implemented custom regEx here but sticking to industry defaults.
            System.Net.Mail.MailAddress mAddress = new System.Net.Mail.MailAddress(emailaddress);
            return true;
        }catch (FormatException)
        {
            return false;
        }
    }
    private int SendEmail(string Name, string email)
    {
        // return value indicator
        // 0 = success
        // 1 = .....


        //instaniate a new mailmessage object.
        System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
        using (msg)
        {
            //define the various components required to sending an email.
            msg.To.Add(email);
            msg.From = new System.Net.Mail.MailAddress("sales@quotation.org","Michael Duncan");
            msg.Subject = String.Format("Quotation for {0} for {1}", string.Format("{0:C}", decimal.Parse(Session["Total_Price"].ToString())), Name);
            msg.IsBodyHtml = true;


            // using string builder for slight improved performance with string concatenation 
            System.Text.StringBuilder msgBld = new System.Text.StringBuilder();
            msgBld.AppendLine(String.Format("Sale Price: {0}", string.Format("{0:C}", decimal.Parse(Session["Sale_Price"].ToString())), Name));
            msgBld.AppendLine(String.Format("Discount Amount: {0}", string.Format("{0:C}", decimal.Parse(Session["Discount_Amount"].ToString())), Name));
            msgBld.AppendLine(String.Format("Total Price: {0}", string.Format("{0:C}", decimal.Parse(Session["Total_Price"].ToString())), Name));

            msg.Body = msgBld.ToString();
        }

        //instaniate new smtpClient object. providing smtp url (using placeholders)
        System.Net.Mail.SmtpClient smtpClient = new System.Net.Mail.SmtpClient("<insert smtphere>");
        smtpClient.UseDefaultCredentials = false;
        // rough use of username/password credentials. usually require for authorized access to smtpserver.
        smtpClient.Credentials = new System.Net.NetworkCredential("<username here>", "<password here>");

        try
        {
            smtpClient.Send(msg);
            return 0;
        } catch (System.Net.Mail.SmtpException ex)
        { 
            throw new ApplicationException("SMTPException has occurred. " + ex.Message);

        }catch (Exception ex)
        {
            throw ex;
        }
    }
}