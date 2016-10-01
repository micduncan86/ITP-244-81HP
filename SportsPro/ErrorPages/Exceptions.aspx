<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Exceptions.aspx.cs" MasterPageFile="~/master.Master" Inherits="SportsPro.ErrorPages.Exceptions" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <label>The follow error has occured:</label>
    <br />
    <asp:Label ID="lblError" runat="server"></asp:Label>
    <br />
    <asp:Button ID="btnSubmitReport" runat="server" CssClass="btn btn-danger btn-sm" Text="Submit Error Report" OnClick="btnSubmitReport_Click" />
    <asp:Button ID="btnReturn" runat="server" CssClass="btn btn-primary btn-sm" Text="Return" OnClick="btnReturn_Click" OnClientClick="window.location = window.location; return false;" />
</asp:Content>
