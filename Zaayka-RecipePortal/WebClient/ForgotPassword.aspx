<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="WebClient.ForgotPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Label1" runat="server" Text="Have you forgot your Password??? Don't worry you can reset it.."></asp:Label>
            <br />
            <asp:Label ID="Label2" runat="server" Text="Please provide your registered email address to receive reset password link on your email."></asp:Label>
            <br />
            Email:<asp:TextBox ID="TextBox1" runat="server" ></asp:TextBox>
            <br />
            <asp:Button ID="Button1" runat="server" Text="Send" OnClick="Button1_Click" />
            <br />
            <asp:Label ID="Label3" runat="server" ForeColor="Blue" Text=""></asp:Label>
        </div>
    </form>
</body>
</html>
