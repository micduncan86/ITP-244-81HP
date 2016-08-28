<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>XEx02Quotation</title>
    <link href="styles/site.css" rel="stylesheet" />
   
</head>
<body>
    <form id="form1" runat="server">
        <h1>Price quotation</h1>
        <section>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="validator" HeaderText="Follow error(s) occured..." ShowMessageBox="false" ValidateRequestMode="Enabled" />
        </section>
        <label for="numSalePrice">Sales price:</label>
        <asp:TextBox ID="numSalePrice" CssClass="entry" runat="server" Text="0"  TextMode="Number" step="any"></asp:TextBox>
        <asp:RequiredFieldValidator ID="reqSalePriceValidator" CssClass="validator" runat="server" ControlToValidate="numSalePrice" ErrorMessage="Sales Price is required" >*</asp:RequiredFieldValidator>
        <asp:RangeValidator ID="rngSalePriceValidator" CssClass="validator" runat="server" ControlToValidate="numSalePrice" ErrorMessage="Sale Price greater than 0."  MaximumValue="999999999" MinimumValue="0" >*</asp:RangeValidator>
        <br />

        <label for="numDiscountPercentage">Discount percent:</label>
        <asp:TextBox ID="numDiscountPercentage" CssClass="entry" runat="server" Text="0" TextMode="Number" step="any"></asp:TextBox>
        <asp:RequiredFieldValidator ID="reqDiscountValidator0" CssClass="validator" runat="server" ControlToValidate="numDiscountPercentage" ErrorMessage="Discount Amount is required">*</asp:RequiredFieldValidator>
        <asp:RangeValidator ID="rngDiscountValidator" CssClass="validator" runat="server" ControlToValidate="numDiscountPercentage" ErrorMessage="Discount Percent greater than 0."  MaximumValue="999999999" MinimumValue="0" >*</asp:RangeValidator>
        <br />

        <label for="lblCalcDiscountAmt">Discount amount:</label>
        <asp:Label ID="lblCalcDiscountAmt" runat="server" CssClass="result" ></asp:Label>
        <br />
        <br />
        <label for="lblCalcTotalAmt">Total Price:</label>
        <asp:Label ID="lblCalcTotalAmt" runat="server" CssClass="result" ></asp:Label>
        <br />
        <br />
        <asp:Button ID="btnCalculate" CssClass="button" runat="server" Text="Calculate" OnClick="Calculate" />       
        
    </form>
</body>
</html>
