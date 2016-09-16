using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Request : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            InitControls();
        }
    }
    private void InitControls()
    {
        //Populates the Number of people drop down list using a for loop.
        for (int i = 1; i < 5; i++)
        {
            ListItem li = new ListItem(i.ToString(), i.ToString());
            ddlNumberofPeople.Items.Add(li);
        }

        // create a listitem dynamically and adds to the radiobutton list
        rblBedTypes.Items.Add(new ListItem("King", "king"));
        rblBedTypes.Items.Add(new ListItem("Two Queen", "2queen"));
        rblBedTypes.Items.Add(new ListItem("One Queen", "1queen"));
        rblBedTypes.SelectedValue = "king";

        //set Arrival value to current date
        txtArrivalDate.Text = DateTime.Today.ToShortDateString();
        txtArrivalDate.Focus();

        // set the default submit and focus properites of the form.
        form1.DefaultButton = btnSubmit.UniqueID;
        form1.DefaultFocus = txtArrivalDate.UniqueID;

        txtDepartureDate.Attributes.Add("placeholder", Convert.ToDateTime(txtArrivalDate.Text).AddDays(1).ToShortDateString());
        txtSpecialRequest.Attributes.Add("placeholder", "Put any special request here.");
        txtFirstName.Attributes.Add("placeholder", "First Name Here");
        txtLastName.Attributes.Add("placeholder", "Last Name Here");
        txtEmailAddress.Attributes.Add("placeholder", "xxxx.xxx@xxxx.xxx");
        txtTelephone.Attributes.Add("placeholder", "xxx-xxx-xxxx");




    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        //string builder is not required but im using this as a force of habit.
        System.Text.StringBuilder strBlder = new System.Text.StringBuilder();

        strBlder.AppendLine(string.Format("Thank you for your request."));
        strBlder.AppendLine(string.Format("We will get back with you within 24 hours."));

        lblMessage.Text = strBlder.ToString();
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        rblBedTypes.SelectedValue = "king";
        txtArrivalDate.Text = DateTime.Today.ToShortDateString();
        ddlNumberofPeople.SelectedIndex = 0;
        ddlPreferredMethod.SelectedIndex = 0;
        txtDepartureDate.Text = "";
        txtEmailAddress.Text = "";
        txtFirstName.Text = "";
        txtLastName.Text = "";
        txtSpecialRequest.Text = "";
        txtTelephone.Text = "";
    }
}