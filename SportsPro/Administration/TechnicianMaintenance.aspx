<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/master.Master" CodeBehind="TechnicianMaintenance.aspx.cs" Inherits="SportsPro.Administration.TechnicianMaintenance" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    Select a Technician:<asp:DropDownList ID="ddlTechnicians" runat="server" OnSelectedIndexChanged="ddlTechnicians_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
    <asp:Label ID="lblError" runat="server" EnableViewState="false"></asp:Label>
    <br />
    <br />
    <asp:FormView ID="frmTechnician" runat="server" DataKeyNames="TechID" CssClass="datagrid" GridLines="Vertical" Width="100%" OnItemCommand="frmTechnician_ItemCommand" OnDataBound="frmTechnician_DataBound">
        <EditRowStyle CssClass="dgrow" />
        <FooterStyle  />
        <HeaderStyle CssClass="dgheader"/>
        <ItemTemplate>
            <table class="datagrid">
                <tr class="dgheader">
                    <td class="col-md-1">TechID</td>
                    <td class="col-md-3">Name</td>
                    <td class="col-md-5">Email</td>
                    <td class="col-md-3">Phone</td>
                </tr>
                <tr class="dgrow">
                    <td><%# DataBinder.Eval(Container.DataItem,"TechID") %></td>
                    <td><%# DataBinder.Eval(Container.DataItem,"Name") %></td>
                    <td><%# DataBinder.Eval(Container.DataItem,"Email") %></td>
                    <td><%# DataBinder.Eval(Container.DataItem,"Phone") %></td>
                </tr>  
                <tr>
                    <td colspan="4">
                        <div style="margin:5px;">
                        <asp:Button ID="btnEdit" CssClass="btn btn-primary btn-sm" runat="server" CommandName="editTech" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"TechID") %>' Text="Edit"/>
                            <asp:Button ID="btnDelete" CssClass="btn btn-primary btn-sm" runat="server" CommandName="deleteTech" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"TechID") %>' Text="Delete" OnClientClick="return confirm('Are you sure you want to delete this technician?');"/>
                            <asp:Button ID="btnAdd" CssClass="btn btn-primary btn-sm" runat="server" CommandName="newTech" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"TechID") %>' Text="New"/>
                        </div>
                    </td>
                </tr>             
            </table>
        </ItemTemplate>
        <EditItemTemplate>
            <table class="datagrid">
                <tr class="dgheader">
                    <td class="col-md-1">TechID</td>
                    <td class="col-md-3">Name</td>
                    <td class="col-md-5">Email</td>
                    <td class="col-md-3">Phone</td>
                </tr>
                <tr class="dgrow">
                    <td>
                        <%--<asp:TextBox ID="txtTechID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"TechID") %>' style="width: 95%;margin: 0 5px;">
                        </asp:TextBox>--%>
                        <%# DataBinder.Eval(Container.DataItem,"TechID") %>
                        </td>
                    <td>
                        <asp:RequiredFieldValidator ID="rqName" runat="server" CssClass="validator" Display="Dynamic" ControlToValidate="txtName" ValidationGroup="val-edit" ErrorMessage="Technician name is required.">&nbsp;</asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Name") %>' style="width: 95%;margin: 0 5px;"></asp:TextBox>                        
                        </td>
                    <td>
                        <asp:RequiredFieldValidator ID="rqEmail" runat="server" CssClass="validator" Display="Dynamic" ControlToValidate="txtEmail" ValidationGroup="val-edit" ErrorMessage="Technician email is required.">&nbsp;</asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtEmail" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Email") %>' style="width: 95%;margin: 0 5px;"></asp:TextBox>
                        </td>
                    <td>
                        <asp:RequiredFieldValidator ID="rqPhone" runat="server" CssClass="validator" Display="Dynamic" ControlToValidate="txtPhone" ValidationGroup="val-edit" ErrorMessage="Technician phone is required.">&nbsp;</asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtPhone" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Phone") %>' style="width: 95%;margin: 0 5px;"></asp:TextBox>
                        </td>
                </tr>  
                <tr>
                    <td colspan="4">
                        <div style="margin:5px;">
                        <asp:Button ID="btnUpdate" CssClass="btn btn-primary btn-sm" runat="server" ValidationGroup="val-edit" CommandName="updateTech" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"TechID") %>' Text="Update"/>
                            <asp:Button ID="btnCancel" CssClass="btn btn-primary btn-sm" runat="server" CommandName="cancelTech" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"TechID") %>' Text="Cancel"/>
                        </div>
                    </td>
                </tr>             
            </table>
            <asp:ValidationSummary ID="valsumEdit" runat="server" ValidationGroup="val-edit" CssClass="validator" />
        </EditItemTemplate>
        <InsertItemTemplate>
            <table class="datagrid">
                <tr class="dgheader">
                    <td class="col-md-1">TechID</td>
                    <td class="col-md-3">Name</td>
                    <td class="col-md-5">Email</td>
                    <td class="col-md-3">Phone</td>
                </tr>
                <tr class="dgrow">
                    <td><asp:RequiredFieldValidator ID="rqID" runat="server" CssClass="validator" Display="Dynamic" ControlToValidate="txtTechID" ValidationGroup="val-add" ErrorMessage="Technician ID is required.">&nbsp;</asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtTechID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"TechID") %>' style="width: 95%;margin: 0 5px;">
                        </asp:TextBox>
                             </td>
                    <td><asp:RequiredFieldValidator ID="rqName" runat="server" CssClass="validator" Display="Dynamic" ControlToValidate="txtName" ValidationGroup="val-add" ErrorMessage="Technician name is required.">&nbsp;</asp:RequiredFieldValidator><asp:TextBox ID="txtName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Name") %>' style="width: 95%;margin: 0 5px;"></asp:TextBox>
                        </td>
                    <td><asp:RequiredFieldValidator ID="rqEmail" runat="server" CssClass="validator" Display="Dynamic" ControlToValidate="txtEmail" ValidationGroup="val-add" ErrorMessage="Technician email is required.">&nbsp;</asp:RequiredFieldValidator><asp:TextBox ID="txtEmail" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Email") %>' style="width: 95%;margin: 0 5px;"></asp:TextBox>
                        </td>
                    <td><asp:RequiredFieldValidator ID="rqPhone" runat="server" CssClass="validator" Display="Dynamic" ControlToValidate="txtPhone" ValidationGroup="val-add" ErrorMessage="Technician phone is required.">&nbsp;</asp:RequiredFieldValidator><asp:TextBox ID="txtPhone" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Phone") %>' style="width: 95%;margin: 0 5px;"></asp:TextBox>
                        </td>
                </tr>  
                <tr>
                    <td colspan="4">
                        <div style="margin:5px;">
                        <asp:Button ID="btnAdd" CssClass="btn btn-primary btn-sm" runat="server" ValidationGroup="val-add" CommandName="addTech" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"TechID") %>' Text="Add"/>
                            <asp:Button ID="btnCancel" CssClass="btn btn-primary btn-sm" runat="server" CommandName="cancelTech" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"TechID") %>' Text="Cancel"/>
                        </div>
                    </td>
                </tr>             
            </table>
        </InsertItemTemplate>
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <RowStyle CssClass="dgrow" />

    </asp:FormView>
</asp:Content>
