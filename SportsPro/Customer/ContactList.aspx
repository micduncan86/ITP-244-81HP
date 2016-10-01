<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/master.Master" CodeBehind="ContactList.aspx.cs" Inherits="SportsPro.Customer.ContactList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="ContactList">
    <header>Contact List:</header>

    <asp:ListBox ID="lstContactList" runat="server"></asp:ListBox>
    

    <p style="clear:both; min-height:35px;">
    <a href="CustomerDisplay.aspx" class="btn btn-primary btn-sm" style="float:left;">Select Additional Customers</a>
        
        <asp:Button ID="btnClearList" runat="server" CssClass="btn btn-primary btn-sm ContactListButtons" Text="Clear List"  OnClick="btnClearList_Click" style="float:right; margin: 0 5px;" />
        &nbsp;
    <asp:Button ID="btnRemoveCustomer" runat="server" CssClass="btn btn-primary btn-sm ContactListButtons" Text="Remove Selected Contact"  OnClick="btnRemoveCustomer_Click" style="float:right;" />
    
    </p>
    </section>

</asp:Content>
