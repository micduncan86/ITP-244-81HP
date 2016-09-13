<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Request.aspx.cs" Inherits="XEx06Reservation.Request" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Chapter 6: Reservations</title>
    <link href="styles/main.css" rel="stylesheet" />
    <link href="styles/request.css" rel="stylesheet" />
</head>
<body>
    <header>
        <h1>Royal Inns and Suites</h1>
        <h2>Where you&rsquo;re always treated like royalty</h2>
    </header>
    <section>
        <form id="form1" runat="server" defaultbutton="btnSubmit" defaultfocus="txtArrivalDate">
            <h1>Reservation Request</h1>
            <h2>Request data</h2>
            <label class="label">Arrival date</label>
            <asp:TextBox ID="txtArrivalDate" runat="server" TextMode="Date"></asp:TextBox><br />
            <label class="label">Departure date</label>
            <asp:TextBox ID="txtDepartureDate" runat="server" TextMode="Date"></asp:TextBox>
            <br />
            <label class="label">Number of people</label>
            <asp:DropDownList ID="ddlNumberofPeople" runat="server">
            </asp:DropDownList>
            <br />

            <label class="label">Bed type</label>
            <asp:RadioButtonList ID="rblBedTypes" runat="server" RepeatDirection="Horizontal"></asp:RadioButtonList>
            <br />

            <h2>Special requests</h2>
            <asp:TextBox ID="txtSpecialRequest" runat="server" TextMode="MultiLine"></asp:TextBox>
            <br />

            <h2>Contact information</h2>
            <label class="label">First name</label>
            <asp:TextBox ID="txtFirstName" runat="server" CssClass="entry"></asp:TextBox><br />
            <label class="label">Last name</label>
            <asp:TextBox ID="txtLastName" runat="server" CssClass="entry"></asp:TextBox>
            <br />
            <label class="label">Email address</label>
            <asp:TextBox ID="txtEmailAddress" runat="server" CssClass="entry" TextMode="Email"></asp:TextBox>
            <br />
            <label class="label">Telephone number</label>
            <asp:TextBox ID="txtTelephone" runat="server" CssClass="entry" TextMode="Phone"></asp:TextBox>
            <br />
            <label class="label">Preferred method</label>
            <asp:DropDownList ID="ddlPreferredMethod" runat="server">
                <asp:ListItem Text="Telephone" Value="tel"></asp:ListItem>
                <asp:ListItem Text="Email" Value="email"></asp:ListItem>
            </asp:DropDownList>
            
            <br />

            <label class="label">&nbsp;</label>
            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="button" OnClick="btnSubmit_Click" />&nbsp;
            <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="button" OnClick="btnClear_Click" CausesValidation="false" /><br />
            <p>
                <asp:Label ID="lblMessage" runat="server"></asp:Label>
            </p>
        </form>
    </section>
    <footer>
        <p>&copy; 2013, Royal Inns and Suites</p>
    </footer>
</body>
</html>
