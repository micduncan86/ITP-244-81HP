<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/master.Master"  CodeBehind="IncidentDisplay.aspx.cs" Inherits="SportsPro.Administration.IncidentDisplay" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ListView ID="lstIncidents" runat="server" OnPagePropertiesChanging="lstIncidents_PagePropertiesChanging" >
              
        <EmptyDataTemplate>
            <table runat="server" class="datagrid">
                <tr class="row">
                    <td>No data was returned.</td>
                </tr>
            </table>
        </EmptyDataTemplate>        
        <ItemTemplate>
            <tr class="dgrow">          
                <td>
                    <asp:Label ID="ProductCodeLabel" runat="server" Text='<%# Eval("ProductCode") %>' />
                </td>
                <td>
                    <asp:Label ID="CustomerLabel" runat="server" Text='<%# Eval("Customer.Name") %>' />
                </td>
                <td>
                    <asp:Label ID="TechLabel" runat="server" Text='<%# Eval("Tech.Name") %>' />
                </td>     
            </tr>
            <tr class="dgrow">
                <td colspan="3">
                    <table style="margin-left:36%;">
                        <tr>
                            <td>Date Opened:</td>
                            <td><asp:Label ID="Label1" runat="server" Text='<%# Eval("DateOpened")  %>' /></td>
                        </tr>
                        <tr>
                            <td>Date Closed:</td>
                            <td><asp:Label ID="Label2" runat="server" Text='<%# Eval("DateClosed")  %>' /></td>
                        </tr>
                        <tr>
                            <td>Title:</td>
                            <td><asp:Label ID="Label3" runat="server" Text='<%# Eval("Title")  %>' /></td>
                        </tr>
                        <tr>
                            <td>Description:</td>
                            <td><asp:Label ID="Label4" runat="server" Text='<%# Eval("Description") %>' /></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <tr class="dgaltrow">          
                <td>
                    <asp:Label ID="ProductCodeLabel" runat="server" Text='<%# Eval("ProductCode") %>' />
                </td>
                <td>
                    <asp:Label ID="CustomerLabel" runat="server" Text='<%# Eval("Customer.Name") %>' />
                </td>
                <td>
                    <asp:Label ID="TechLabel" runat="server" Text='<%# Eval("Tech.Name") %>' />
                </td>     
            </tr>
            <tr class="dgaltrow">
                <td colspan="3">
                    <table style="margin-left:36%;">
                        <tr>
                            <td>Date Opened:</td>
                            <td><asp:Label ID="Label1" runat="server" Text='<%# Eval("DateOpened")  %>' /></td>
                        </tr>
                        <tr>
                            <td>Date Closed:</td>
                            <td><asp:Label ID="Label2" runat="server" Text='<%# Eval("DateClosed")  %>' /></td>
                        </tr>
                        <tr>
                            <td>Title:</td>
                            <td><asp:Label ID="Label3" runat="server" Text='<%# Eval("Title")  %>' /></td>
                        </tr>
                        <tr>
                            <td>Description:</td>
                            <td><asp:Label ID="Label4" runat="server" Text='<%# Eval("Description") %>' /></td>
                        </tr>
                    </table>
                     <br />
                </td>
            </tr>
        </AlternatingItemTemplate>
        <LayoutTemplate>
            <table runat="server" style="width:100%;" class="datagrid">
                <tr runat="server">
                    <td runat="server">
                        <table id="itemPlaceholderContainer" runat="server" class="datagrid">
                            <tr runat="server" class="dgheader">
                                <th runat="server" class="col-md-6">Product</th>
                                <th runat="server" class="col-md-3">Customer</th>
                                <th runat="server" class="col-md-3">Technician</th>   
                            </tr>
                            <tr id="itemPlaceholder" runat="server">
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr runat="server" class="dgheader">
                    <td runat="server" style="text-align: center;">
                        <asp:DataPager ID="DataPager1" runat="server" PageSize="4" PagedControlID="lstIncidents">
                            <Fields>
                                <asp:NextPreviousPagerField ButtonType="Link" ShowFirstPageButton="false" ShowLastPageButton="false" RenderDisabledButtonsAsLabels="true" />
                            </Fields>
                        </asp:DataPager>
                    </td>
                </tr>
            </table>

        </LayoutTemplate>        
    </asp:ListView>
</asp:Content>
