<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/master.Master"  CodeBehind="CustomerIncidentDisplay.aspx.cs" Inherits="SportsPro.Technician.CustomerIncidentDisplay" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<p>
    Select as Customer:
    <asp:DropDownList ID="ddlCustomers" runat="server" OnSelectedIndexChanged="ddlCustomers_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
</p>
    <div style="height: 215px; overflow-y:scroll">
        <asp:DataList ID="dataListIncidents" runat="server" CssClass="datagrid" CellPadding="3" GridLines="Vertical" RepeatColumns="1" ShowFooter="False" Width="100%">
            <AlternatingItemStyle CssClass="dgaltrow" />
            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
            <HeaderStyle CssClass="dgheader" />
            <HeaderTemplate>
                <table style="width:100%;" class="datagrid">
                    <tr class="dgheader">
                        <th style="width:30%;">Product/Incident</th>
                        <th style="width:30%;">Tech Name</th>
                        <th style="width:20%;">Date opened</th>
                        <th style="width:20%;">Date closed</th>
                    </tr>
                </table>
            </HeaderTemplate>
            <ItemStyle CssClass="dgrow" />
            <ItemTemplate>
                <table style="width:100%;">
                    <tr>
                        <td style="width:30%;"><%# Eval("ProductCode") %></td>
                        <td style="width:30%;"><%# Eval("Tech.Name") %></td>
                        <td style="width:20%;"><%# DataBinder.Eval(Container.DataItem, "DateOpened","{0:MM/dd/yyyy}") %></td>
                        <td style="width:20%;"><%# DataBinder.Eval(Container.DataItem, "DateClosed","{0:MM/dd/yyyy}") %></td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            
                            <%# Eval("Description") %></td>
                    </tr>
                    
                </table>
            </ItemTemplate>
            <SelectedItemStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
        </asp:DataList>
</div>

</asp:Content>
