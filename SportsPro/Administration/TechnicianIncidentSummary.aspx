<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/master.Master"  CodeBehind="TechnicianIncidentSummary.aspx.cs" Inherits="SportsPro.Administration.TechnicianIncidentSummary" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    Technician: <asp:DropDownList ID="ddlTechnician" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlTechnician_SelectedIndexChanged" ></asp:DropDownList>
    <br /><br />    
    <asp:GridView ID="grdIncidents" runat="server" CssClass="datagrid" AutoGenerateColumns="False">
        <Columns>
            <asp:BoundField DataField="DateOpened" HeaderText="DateOpened" SortExpression="DateOpened" DataFormatString="{0:MM/dd/yyyy}" />
            <asp:BoundField DataField="ProductCode" HeaderText="ProductCode" SortExpression="ProductCode" />
            <asp:BoundField DataField="Customer.Name" HeaderText="Customer" />
        </Columns>
        <AlternatingRowStyle CssClass="dgaltrow" />
        <HeaderStyle CssClass="dgheader" />
        <RowStyle CssClass="dgrow" />
        <EmptyDataTemplate>
            No incidents found.
        </EmptyDataTemplate>
    </asp:GridView>    
</asp:Content>