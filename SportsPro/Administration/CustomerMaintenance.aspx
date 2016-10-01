<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/master.Master" CodeBehind="CustomerMaintenance.aspx.cs" Inherits="SportsPro.Administration.CustomerMaintenance" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label ID="lblError" runat="server" CssClass="validator" EnableViewState="false"></asp:Label>
    <div class="row">
        <div class="col-md-7">
            <asp:GridView ID="grdCustomers" runat="server" DataKeyNames="CustomerID" CssClass="datagrid" GridLines="Vertical" AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanging="grdCustomers_PageIndexChanging" OnRowCommand="grdCustomers_RowCommand" Width="100%" AllowSorting="True">
                <AlternatingRowStyle CssClass="dgaltrow" />
                <Columns>                    
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:LinkButton ID="lnkName" runat="server" CommandName="sortCustomer" CommandArgument="Name" Text="Name"></asp:LinkButton>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem,"Name") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:LinkButton ID="lnkCity" runat="server" CommandName="sortCustomer" CommandArgument="City" Text="City"></asp:LinkButton>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem,"City") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:LinkButton ID="lnkState" runat="server" CommandName="sortCustomer" CommandArgument="State" Text="State"></asp:LinkButton>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem,"State") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>                        
                        <ItemTemplate>
                            <asp:Button ID="btnSelect" runat="server" CommandName="selectCustomer" CommandArgument='<%# Container.DisplayIndex %>' CssClass="btn btn-primary btn-sm" Style="margin: 5px;" Text="Select" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>                
                <HeaderStyle CssClass="dgheader" />
                <PagerStyle CssClass="dgheader" HorizontalAlign ="Center" />
                <RowStyle CssClass="dgrow" />  
                <PagerSettings Mode="NextPreviousFirstLast" FirstPageText="First" LastPageText="Last" NextPageText="Next" PreviousPageText="Prev" />
            </asp:GridView>
        </div>
        <div class="col-md-5">
            <asp:DetailsView ID="dtlCustomer" DataKeyNames="CustomerID" runat="server" CssClass="datagrid dgcustomerdtl" GridLines="Vertical" AutoGenerateRows="false" OnDataBound="dtlCustomer_DataBound" OnItemCommand="dtlCustomer_ItemCommand">
                <AlternatingRowStyle CssClass="dgaltrow"/>  
                <EditRowStyle CssClass="dgrow"/>
                <Fields>
                    <asp:TemplateField HeaderText="ID">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem,"CustomerID") %>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem,"CustomerID") %>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Name">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem,"Name") %>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox id="txtName" runat="server" style="margin:5px;"></asp:TextBox>
                        </EditItemTemplate>
                        <InsertItemTemplate>
                            <asp:TextBox id="txtName" runat="server" style="margin:5px;"></asp:TextBox>
                        </InsertItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Address">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem,"Address") %>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox id="txtAddress" runat="server" style="margin:5px;"></asp:TextBox>
                        </EditItemTemplate>
                        <InsertItemTemplate>
                            <asp:TextBox id="txtAddress" runat="server" style="margin:5px;"></asp:TextBox>
                        </InsertItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="City">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem,"City") %>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox id="txtCity" runat="server" style="margin:5px;"></asp:TextBox>
                        </EditItemTemplate>
                        <InsertItemTemplate>
                            <asp:TextBox id="txtCity" runat="server" style="margin:5px;"></asp:TextBox>
                        </InsertItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="State">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem,"State") %>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox id="txtState" runat="server" style="margin:5px;"></asp:TextBox>
                        </EditItemTemplate>
                        <InsertItemTemplate>
                            <asp:TextBox id="txtState" runat="server" style="margin:5px;"></asp:TextBox>
                        </InsertItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Zipcode">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem,"ZipCode") %>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox id="txtZipCode" runat="server" style="margin:5px;"></asp:TextBox>
                        </EditItemTemplate>
                        <InsertItemTemplate>
                            <asp:TextBox id="txtZipCode" runat="server" style="margin:5px;"></asp:TextBox>
                        </InsertItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Phone">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem,"Phone") %>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox id="txtPhone" runat="server" style="margin:5px;"></asp:TextBox>
                        </EditItemTemplate>
                        <InsertItemTemplate>
                            <asp:TextBox id="txtPhone" runat="server" style="margin:5px;"></asp:TextBox>
                        </InsertItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Email">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem,"Email") %>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox id="txtEmail" runat="server" style="margin:5px;"></asp:TextBox>
                        </EditItemTemplate>
                        <InsertItemTemplate>
                            <asp:TextBox id="txtEmail" runat="server" style="margin:5px;"></asp:TextBox>
                        </InsertItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="btnEdit" CommandName="editCustomer" CommandArgument='<%# Container.DataItemIndex %>' runat="server" CssClass="btn btn-primary btn-sm" Style="margin: 5px;" Text="Edit" />
                            <asp:Button ID="btnDelete" CommandName="deleteCustomer" CommandArgument='<%# Container.DataItemIndex %>' runat="server" CssClass="btn btn-primary btn-sm" Style="margin: 5px;" Text="Delete" OnClientClick="return confirm('Are you sure you want to remove this customer?');" />
                            <asp:Button ID="btnNew" runat="server" CommandName="newCustomer" CommandArgument='<%# Container.DataItemIndex %>' CssClass="btn btn-primary btn-sm" Style="margin: 5px;" Text="New" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Button ID="btnUpdate" CommandName="updateCustomer" CommandArgument='<%# Container.DataItemIndex %>' runat="server" CssClass="btn btn-primary btn-sm" Style="margin: 5px;" Text="Update" />
                            <asp:Button ID="btnCancel" CommandName="cancelCustomer" runat="server" CssClass="btn btn-primary btn-xs" Style="margin: 5px;" Text="Cancel" />
                        </EditItemTemplate>
                        <InsertItemTemplate>
                            <asp:Button ID="btnAdd" CommandName="addCustomer" CommandArgument='<%# Container.DataItemIndex %>' runat="server" CssClass="btn btn-primary btn-sm" Style="margin: 5px;" Text="Add" />
                            <asp:Button ID="btnCancel" CommandName="cancelCustomer" runat="server" CssClass="btn btn-primary btn-sm" Style="margin: 5px;" Text="Cancel" />
                        </InsertItemTemplate>
                    </asp:TemplateField>
                </Fields>
                  <HeaderStyle CssClass="dgheader"/>
                <PagerStyle CssClass="dgheader" HorizontalAlign ="Center" />
                <RowStyle CssClass="dgrow" />
            </asp:DetailsView>
        </div>
    </div>    
</asp:Content>
