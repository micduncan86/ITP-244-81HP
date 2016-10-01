<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/master.Master" CodeBehind="Default.aspx.cs" Inherits="SportsPro.Default" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section>
        <font style="font-size: 50px;">Welcome to Sports Pro</font>
        <br />
        Not only do we provide efficent means to maintaining high level of technical support; we also help delevop a contact relations system to better keep in touch with your audience. Based out of Central Virginia, we bring that down to earth home grown partnerships that last. We look forward to be involved with each and everyone one of you.
    </section>
    <div class="row">
        <div class="col-md-4">
            <h3>Customer Support</h3>
            <section>
                <article>
                    <a href="Customer/ProductRegistration.aspx"><u>Register</u></a> your products, complete product <a href="Customer/CustomerSurvey.aspx"><u>surveys</u></a>, or find out how to <a href="ContactUs.aspx"><u>each out to us</u></a>.
                </article>
            </section>
        </div>
        <div class="col-md-4">
            <h3>Technician Support</h3>
            <section>
                <article>
                    Manage and <a href="Technician/IncidentUpdate.aspx"><u>Update incidents</u></a>, display customers with <a href="Technician/CustomerIncidentDisplay.aspx"><u>open incident tickets</u></a>.
                </article>
            </section>
        </div>
        <div class="col-md-4">
            <h3>Administration</h3>
            <section>
                <article>
                    Manage <a href="Administration/ProductMaintenance.aspx"><u>products</u></a>, <a href="Administration/CustomerMaintenance.aspx"><u>customers</u></a>, and system <a href="Administration/TechnicianMaintenance.aspx"><u>technicians</u></a>.
                </article>
            </section>
        </div>
    </div>
</asp:Content>
