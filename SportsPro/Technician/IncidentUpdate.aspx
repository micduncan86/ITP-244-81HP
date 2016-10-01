<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/master.Master"  CodeBehind="IncidentUpdate.aspx.cs" Inherits="SportsPro.Technician.IncidentUpdate" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 Select as Customer:<asp:DropDownList ID="ddlCustomer" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCustomer_SelectedIndexChanged"></asp:DropDownList>
    <br />
    <br />   
    <asp:GridView ID="grdIncidents" runat="server" DataKeyNames="ProductCode" CssClass="datagrid" AutoGenerateColumns="False" OnRowCommand="grdIncidents_RowCommand" OnRowDataBound="grdIncidents_RowDataBound">
        <AlternatingRowStyle CssClass="dgaltrow" />
        <RowStyle CssClass="dgrow" />
        <HeaderStyle CssClass="dgheader" />
        <Columns>
            <asp:BoundField DataField="IncidentID" HeaderText="ID" SortExpression="IncidentID" HeaderStyle-Width="4%" ReadOnly="true" />
            <asp:BoundField DataField="ProductCode" HeaderText="Product Code" HeaderStyle-Wrap="true" HeaderStyle-Width="10%" />
            <asp:BoundField DataField="DateOpened" DataFormatString="{0:MM/dd/yyyy}" HeaderText="Date Opened" SortExpression="DateOpened" HeaderStyle-Wrap="true" HeaderStyle-Width="10%" ItemStyle-Width="10%"/>
            <asp:BoundField DataField="DateClosed" DataFormatString="{0:MM/dd/yyyy}" HeaderText="Date Closed" SortExpression="DateClosed" HeaderStyle-Wrap="true" HeaderStyle-Width="10%" ItemStyle-Width="10%"/>
            <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" HeaderStyle-Width="25%" />
            <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description"  />
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button CssClass="btn btn-primary btn-sm" runat="server" CommandName="editIncident" CommandArgument='<%# Container.DataItemIndex %>' Text="Edit" />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:Button CssClass="btn btn-primary btn-sm" runat="server" CommandName="updateIncident" CommandArgument='<%# Container.DataItemIndex %>' Text="Update" />
                    <asp:Button CssClass="btn btn-primary btn-sm" runat="server" CommandName="cancelIncident" CommandArgument='<%# Container.DataItemIndex %>' Text="Cancel" />
                </EditItemTemplate>

            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:Label ID="lblError" runat="server" EnableViewState="false"></asp:Label>
</asp:Content>
