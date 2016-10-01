<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomerSurvey.aspx.cs" MasterPageFile="~/master.Master" Inherits="SportsPro.Customer.CustomerSurvey" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <asp:ValidationSummary ID="valSummary" runat="server" ValidationGroup="incident" />
        <div>
            <header style="width: 615px; margin-bottom: 5px;">
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 25%;"></td>
                        <td style="text-align: right;">Enter Customer ID:
                            
                            <asp:RequiredFieldValidator ID="reqCustomerId" CssClass="validator" runat="server" ControlToValidate="txtCustomerId" ErrorMessage="Customer ID required" Text="*" ValidationGroup="incident"></asp:RequiredFieldValidator>
                            <asp:TextBox ID="txtCustomerId" runat="server" Style="margin: 0 2px;" CausesValidation="true" Text=""></asp:TextBox>
                            <asp:CompareValidator ID="CustIdInteger" CssClass="validator" runat="server" Display="Dynamic" ControlToValidate="txtCustomerId" Operator="DataTypeCheck" Type="Integer" ErrorMessage="Customer ID is numeric only" Text="*" ValidationGroup="incident"></asp:CompareValidator>
                            
                            <asp:Button ID="btnGetIncidents" CausesValidation="true" runat="server" class="btn btn-primary btn-sm" Text="Get Incidents" OnClick="btnGetIncidents_Click" ValidationGroup="incident"></asp:Button>
                        </td>
                    </tr>
                </table>
            </header>
            <p>
                
                <asp:CompareValidator ID="compIncident" CssClass="validator" runat="server" ControlToValidate="lstIncidents" Operator="DataTypeCheck" Type="Integer" ErrorMessage="Incident selection required for survey." Display="Dynamic" Text="&nbsp;" ValidationGroup="survey"></asp:CompareValidator>
                <asp:ListBox ID="lstIncidents" runat="server" Width="615" Height="200"></asp:ListBox>
            </p>
        </div>
        <p style="font-weight: bold;">Please rate this incident by the following categories:</p>
        <table style="vertical-align: top;">
            <tr>
                <td>Response Time:</td>
                <td>
                    <asp:RadioButtonList ID="rblResponse" CssClass="rblSurvey" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow"></asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>Technician efficiency:</td>
                <td>

                    <asp:RadioButtonList ID="rblTech" CssClass="rblSurvey" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow"></asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>Problem resolution:</td>
                <td>

                    <asp:RadioButtonList ID="rblProblem" CssClass="rblSurvey" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow"></asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td style="vertical-align:top;">Additional Comments:</td>
                <td>
                    <asp:TextBox ID="txtComments" runat="server" TextMode="MultiLine" Rows="5" Width="480"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:CheckBox ID="chkContactMe" runat="server" Text="Please contact me to discuss this incident." TextAlign="Right" />
                    <br />
                    <asp:RadioButtonList ID="rblContactBy" runat="server" Style="margin-left: 25px;">
                        <asp:ListItem Text="Contact by email" Value="email" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="Contact by phone" Value="phone"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
        </table>
        <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary btn-sm" Text="Submit" ValidationGroup="survey" OnClick="btnSubmit_Click" />
</asp:Content>

