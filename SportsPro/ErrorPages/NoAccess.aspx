<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NoAccess.aspx.cs" MasterPageFile="~/master.Master" Inherits="SportsPro.ErrorPages._NoAccess" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <label>You do not have priveleges to access this page. Please contact your system administrator.</label>
    <br />
    <asp:Button ID="btnReturn" runat="server" CssClass="btn btn-primary btn-sm" Text="Return" OnClientClick="window.location = window.location; return false;" />
</asp:Content>
