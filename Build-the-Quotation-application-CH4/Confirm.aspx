<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Confirm.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>XEx02Quotation</title>
    <link href="styles/site.css" rel="stylesheet" />
   
</head>
<body>
    <form id="form1" runat="server">
        <h1>Quotation Confirmation</h1>
        <label for="lblSalePrice">Sales price:</label>
        <asp:Label ID="lblSalePrice" runat="server" CssClass="entry"></asp:Label>
        <br />
        <br />
        <label for="lblDiscountAmount">Discount Amount:</label>
        <asp:Label ID="lblDiscountAmount" runat="server" CssClass="entry"></asp:Label>
        <br />
        <br />
        <label for="lblTotalPrice">Total price:</label>
        <asp:Label ID="lblTotalPrice" runat="server" CssClass="entry" ></asp:Label>
        <br />
        <br />
        <h3>Send confirmation to</h3>
        <label for="txtConfirmationName">Name:</label>
        <asp:TextBox ID="txtConfirmationName" runat="server" CssClass="Confirmationentry" ></asp:TextBox>
        <br />
        <label for="txtConfirmationEmailAddress">Email address:</label>
        <asp:TextBox ID="txtConfirmationEmailAddress" runat="server" TextMode="Email" CssClass="Confirmationentry"></asp:TextBox>
        <br />
        <asp:Button ID="btnSendConfirmation" CssClass="button" runat="server" Text="Send Quotation" OnClick="SendQuotation" />       
        <asp:Button ID="btnReturn" CssClass="button" runat="server" Text="Return" OnClick="Return" /> 
        <br />
        <asp:Label ID="lblConfirmDirections" runat="server" Text="Click Send Quotation to send quotation via email." ForeColor="Green"  ></asp:Label>
    </form>
</body>
</html>
