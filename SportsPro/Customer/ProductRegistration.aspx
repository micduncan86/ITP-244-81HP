<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/master.Master" CodeBehind="ProductRegistration.aspx.cs" Inherits="SportsPro.Customer.ProductRegistration" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="displayformat">
        <label>Enter Customer ID:</label>        
        <asp:TextBox ID="txtCustomerId" runat="server"></asp:TextBox>
        <asp:Button ID="btnGetCustomer" runat="server" CssClass="btn btn-primary btn-sm" Text="Get Customer" OnClick="btnGetCustomer_Click" ValidationGroup="chkCustomer" />
        <asp:CustomValidator ID="cValCustomerID" runat="server" ValidationGroup="chkCustomer" CssClass="validator" ControlToValidate="txtCustomerId" ErrorMessage="" OnServerValidate="FindCustomerByID" Display="Dynamic">Customer ID not found.</asp:CustomValidator>
    </section>
    <section class="displayformat" style="padding: 10px 0;">
        <label>Customer:</label>
        <asp:Label ID="lblCustomerName" runat="server" Text="----No Customer loaded.---"></asp:Label>
        <asp:HiddenField ID="hdnCustomerID" runat="server" />
        <br />
        <label>Product:</label>
        <asp:DropDownList ID="ddlProducts" runat="server"></asp:DropDownList>
    </section>
    <asp:Button ID="btnRegister" runat="server" Text="Register Product" Enabled="false" OnClick="btnRegister_Click" />
    <asp:Label ID="lblError" runat="server" EnableViewState="false"></asp:Label>    
</asp:Content>
