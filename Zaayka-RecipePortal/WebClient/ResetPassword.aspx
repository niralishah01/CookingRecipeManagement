<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResetPassword.aspx.cs" Inherits="WebClient.ResetPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Panel ID="Panel1" runat="server" Height="235px" Visible="False" Width="1183px">
                <asp:Label ID="Label1" runat="server" Text="Reset Password:"></asp:Label>
                <br />
                <asp:Label ID="Label3" runat="server" Text="" ForeColor="Red"></asp:Label>
                <br />
                new password:<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                <br />
                confirm password:<asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="TextBox1" ControlToValidate="TextBox2" ErrorMessage="Password didn't match"></asp:CompareValidator>
                <br />
                <asp:Button ID="Button1" runat="server" Text="ResetPassword" OnClick="Button1_Click" />
                <br />
                <asp:Label ID="Label2" runat="server" ForeColor="Green"></asp:Label>
                <br />
                <br />
                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="Login.aspx">Click Here to Login again.</asp:HyperLink>
            </asp:Panel>
        </div>
    </form>
</body>
</html>
