<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/master.Master" CodeBehind="CustomerDisplay.aspx.cs" Inherits="SportsPro.Customer.view" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <section style="margin-left:60px; margin-bottom: 5px;">
            <div class="dropdown">
                <button class="btn btn-primary dropdown-toggle" type="button" data-toggle="dropdown">
                    Select a Customer
                        <span class="caret"></span>
                </button>
                <asp:ListView ID="drpCustomer" runat="server" DataKeyNames="CustomerID">
                    <LayoutTemplate>
                        <ul class="dropdown-menu scrollable-menu">
                            <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
                        </ul>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <li>
                            <asp:LinkButton ID="lnkCustomer" runat="server" CommandArgument='<%# Eval("CustomerID") %>' OnClick="lnkCustomer_Click"><%# Eval("Name") %></asp:LinkButton> 
                        </li>
                    </ItemTemplate>
                    <EmptyDataTemplate>
                        <p>No data</p>
                    </EmptyDataTemplate>
                </asp:ListView>
                
            </div>
        </section>
        <section>
            <table>
                 <tr>
                    <td>
                        <asp:Label ID="label4" runat="server" Text="Name:"></asp:Label></td>
                    <td>
                        <pre id="pName" runat="server"></pre>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="label1" runat="server" Text="Address:"></asp:Label></td>
                    <td>
                        <pre id="pAddress" runat="server"></pre>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="label2" runat="server" Text="Phrone:"></asp:Label></td>
                    <td>
                        <pre id="pPhone" runat="server"></pre>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="label3" runat="server" Text="Email:"></asp:Label></td>
                    <td>
                        <pre><asp:Label ID="lnkEmail" runat="server"></asp:Label></pre>
                    </td>
                </tr>
            </table>
            <asp:Button ID="btnAddtoList" runat="server" CssClass="btn btn-primary btn-sm" OnClick="btnAddtoList_ServerClick" Text="Add to Contact List" />
            <asp:Button ID="btnDisplayContactList" runat="server" CssClass="btn btn-primary btn-sm" Text="Display Contact List" PostBackUrl="ContactList.aspx" />
            <asp:Label ID="lblWarning" CssClass="text-danger text-warning" runat="server" Visible="false"><br />This customer is already on the contact list</asp:Label>


            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\TechSupport.mdf;Integrated Security=True" ProviderName="System.Data.SqlClient" SelectCommand="SELECT CustomerID, Name, Address, City, State, ZipCode, Phone, Email FROM Customers ORDER BY Name"></asp:SqlDataSource>
        </section>
    </div>
</asp:Content>

