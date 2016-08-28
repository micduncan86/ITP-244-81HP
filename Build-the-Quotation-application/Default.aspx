<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>XEx02Quotation</title>
    <style>
        #tblPriceQuotation td:nth-child(1){
            width:150px;
        }
        #tblPriceQuotation tr{
            min-height:24px;
            display:block;
        }
        #tblPriceQuotation #numSalePrice {
            font-weight:bold;
        }
        input[type='number']{
            min-width:175px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <h1>Price quotation</h1>
        <table id="tblPriceQuotation">
            <tr>
                <td>
                    <asp:Label ID="lblSalePrice" runat="server">Sales price:</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="numSalePrice" runat="server" Text="0" Font-Bold="true" TextMode="Number" step="any"></asp:TextBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="reqSalePriceValidator" runat="server" ControlToValidate="numSalePrice" ErrorMessage="Sales Price is required" ForeColor="Red"></asp:RequiredFieldValidator>
                    <asp:RangeValidator ID="rngSalePriceValidator" runat="server" ControlToValidate="numSalePrice" ErrorMessage="Please enter an amount greater than 0." ForeColor="Red" MaximumValue="999999999" MinimumValue="0"></asp:RangeValidator>
                </td>
            </tr>
            <tr>
                <td></td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblDiscountPercentage" runat="server">Discount percent:</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="numDiscountPercentage" runat="server" Text="0" TextMode="Number" step="any"></asp:TextBox></td>
                <td>
                    <asp:RequiredFieldValidator ID="reqDiscountValidator0" runat="server" ControlToValidate="numDiscountPercentage" ErrorMessage="Discount Amount is required" ForeColor="Red"></asp:RequiredFieldValidator>
                    <asp:RangeValidator ID="rngDiscountValidator" runat="server" ControlToValidate="numDiscountPercentage" ErrorMessage="Please enter an amount greater than 0." ForeColor="Red" MaximumValue="999999999" MinimumValue="0"></asp:RangeValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblDiscountAmt" runat="server">Discount amount:</asp:Label></td>
                <td>
                    <asp:Label ID="lblCalcDiscountAmt" runat="server" Font-Bold="true"></asp:Label></td>
                <td></td>
            </tr>
            <tr>
                <td></td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblTotalPrice" runat="server">Total Price:</asp:Label></td>
                <td>
                    <asp:Label ID="lblCalcTotalAmt" runat="server" Font-Bold="true"></asp:Label></td>
                <td></td>
            </tr>
            <tr>
                <td></td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnCalculate" runat="server" Text="Calculate" OnClick="Calculate" />
                    </td>
                <td></td>
                <td></td>
            </tr>

        </table>
    </form>
</body>
</html>
