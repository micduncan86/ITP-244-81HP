<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/master.Master" CodeBehind="ProductMaintenance.aspx.cs" Inherits="SportsPro.Administration.ProductMaintenance" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ValidationSummary ID="valSumGrid" runat="server" EnableViewState="false" ValidationGroup="inline-edit" CssClass="validator" />
    <div style="height: 220px; overflow-y: scroll; margin-bottom: 10px; padding: 2px;">
        <asp:GridView ID="grdProducts" runat="server" CssClass="datagrid" GridLines="Vertical" AutoGenerateColumns="False" OnRowCommand="grdProducts_RowCommand" OnRowDataBound="grdProducts_RowDataBound" >
            <AlternatingRowStyle CssClass="dgaltrow" />
            <Columns> 
                <asp:TemplateField HeaderText="Product code" HeaderStyle-Width="125" ItemStyle-Width="125">
                    <ItemTemplate>
                        <asp:Label ID="lblProductCode" runat="server" Text=""><%# DataBinder.Eval(Container.DataItem,"ProductCode") %></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:Label ID="lblProductCode" runat="server" Text=""></asp:Label>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Name">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem,"Name") %>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:RequiredFieldValidator ID="rqName" runat="server" Display="Dynamic" CssClass="validator" ControlToValidate="txtName" ErrorMessage="Product Name is required." ValidationGroup="inline-edit">&nbsp;</asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtName" runat="server" Text="" Width="96%"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Version" HeaderStyle-Width="75" ItemStyle-Width="75">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem,"Version") %>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:RequiredFieldValidator ID="rqVersion" runat="server" Display="Dynamic" CssClass="validator" ControlToValidate="txtVersion" ErrorMessage="Product Version is required" ValidationGroup="inline-edit">&nbsp;</asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtVersion" runat="server" Text="" Width="50"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Release date" HeaderStyle-Width="125" ItemStyle-Width="125">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem,"ReleaseDate","{0:MM/dd/yyyy}") %>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:RequiredFieldValidator ID="rqRelease" runat="server" Display="Dynamic" CssClass="validator" ControlToValidate="txtRelease" ErrorMessage="Product Release date is required." ValidationGroup="inline-edit">&nbsp;</asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="cmpReleaseDate" CssClass="validator" runat="server" Display="Dynamic" ControlToValidate="txtRelease" Operator="DataTypeCheck" Type="Date" CultureInvariantValues="True" ErrorMessage="Release date must be in date format. mm/dd/yyyy" ValidationGroup="inline-edit">&nbsp;</asp:CompareValidator>
                        <asp:TextBox ID="txtRelease" runat="server" Text="" Width="90">&nbsp;</asp:TextBox>
                        
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-Width="165" ItemStyle-Width="165">
                    <ItemTemplate>
                        <div style="padding: 5px;">
                            <asp:Button ID="btnEdit" runat="server" CommandName="editProduct" CommandArgument='<%# Container.DataItemIndex %>' Text="Edit" CssClass="btn btn-primary btn-sm" style="width:63px;" />
                            <asp:Button ID="btnDelete" runat="server" CommandName="deleteProduct" CommandArgument='<%# Container.DataItemIndex %>' Text="Delete" CssClass="btn btn-primary btn-sm" style="width:63px;" OnClientClick="return confirm('Are you sure you want to delete this product?');" />
                        </div>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <div style="padding: 5px;">
                        <asp:Button ID="btnUpdate" runat="server" CommandName="updateProduct" CommandArgument='<%# Container.DataItemIndex %>' Text="Update" CssClass="btn btn-primary btn-sm" style="width:63px;" ValidationGroup="inline-edit" />
                            <asp:Button ID="btnCancel" runat="server" CommandName="cancelProduct" CommandArgument='<%# Container.DataItemIndex %>' Text="Cancel" CssClass="btn btn-primary btn-sm" style="width:63px;" />
                            </div>
                    </EditItemTemplate>
                </asp:TemplateField>
            </Columns>            
            <HeaderStyle CssClass="dgheader" />            
            <RowStyle CssClass="dgrow" />     
        </asp:GridView>
    </div>    
    <div id="divAddProduct"  class="jumbotron">
        <header>To add a new product, enter the product information and click Add Product</header>
        <label>Product Code:</label>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display="Dynamic" CssClass="validator" ControlToValidate="txtProductCode" ErrorMessage="Product code is required." ValidationGroup="add-product">&nbsp;</asp:RequiredFieldValidator>
        <asp:TextBox ID="txtProductCode" runat="server"></asp:TextBox>
        <br />
        <label>Name:</label>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" CssClass="validator" ControlToValidate="txtName" ErrorMessage="Product name is required." ValidationGroup="add-product">&nbsp;</asp:RequiredFieldValidator>
        <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
        <br />
        <label>Version:</label>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" CssClass="validator" ControlToValidate="txtVersion" ErrorMessage="Product verison is required." ValidationGroup="add-product">&nbsp;</asp:RequiredFieldValidator>
        <asp:TextBox ID="txtVersion" runat="server"></asp:TextBox>
        <br />
        <label>Releast date:</label>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic" CssClass="validator" ControlToValidate="txtReleaseDate" ErrorMessage="Product Release date is required." ValidationGroup="add-product">&nbsp;</asp:RequiredFieldValidator>
         <asp:CompareValidator ID="cmpReleaseDateAdd" CssClass="validator" runat="server" Display="Dynamic" ControlToValidate="txtReleaseDate" Operator="DataTypeCheck" Type="Date" CultureInvariantValues="True" ErrorMessage="Release date must be in date format. mm/dd/yyyy" ValidationGroup="add-product">&nbsp;</asp:CompareValidator>
        <asp:TextBox ID="txtReleaseDate" runat="server" placeholder="mm/dd/yy"></asp:TextBox>
        <br />
        <asp:Button ID="btnAddProduct" runat="server" CssClass="btn btn-primary btn-sm" Text="Add Product" ValidationGroup="add-product" OnClick="btnAddProduct_Click" />
        <br />
        <asp:Label ID="lblError" runat="server" EnableViewState="false"></asp:Label>
        <asp:ValidationSummary ID="valSumAdd" runat="server" EnableViewState="false" ValidationGroup="add-product" CssClass="validator" />
    </div>
</asp:Content>
