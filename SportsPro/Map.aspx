<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/master.Master" CodeBehind="Map.aspx.cs" Inherits="SportsPro.Map" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <asp:TreeView ID="TreeView1" runat="server" DataSourceID="SiteMapDataSource1" BackColor="#5c9641">
        </asp:TreeView>
        <asp:SiteMapDataSource ID="SiteMapDataSource1" runat="server" />
</asp:Content>

