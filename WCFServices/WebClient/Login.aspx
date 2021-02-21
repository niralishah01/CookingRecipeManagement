<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebClient.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.7.0/css/all.css" integrity="sha384-lZN37f5QGtY3VHgisS14W3ExzMWZxybE1SJSEsQp9S+oqd12jhcu+A56Ebc1zFSJ" crossorigin="anonymous">
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.5.3/dist/css/bootstrap.min.css" integrity="sha384-TX8t27EcRE3e/ihU7zmQxVncDAy5uIKz4rEkgIXeMed4M0jlfIDPvg6uqKI2xXr2" crossorigin="anonymous">
        <style>
        body{
            background-color:rgb(240,240,240);
            background-position:center;
            background-repeat:no-repeat;
            background-size:cover;
            background-attachment:fixed;
        }
    </style>
</head>
<body>
    <div class="container">
        <center>
            <div class="bg-white centerd p-1 w-25 rounded shadow-lg mt-5">
                <form id="form1" runat="server">
                    <div class="row p-1">
                        <div class="col col-lg-12">
                            <h3>Login Page</h3>
                        </div>
                    </div>
                    <div class="row p-1">
                        <div class="col col-lg-12">
                            <asp:Label ID="Label3" runat="server" style="font-size:1vw;" ForeColor="Red"></asp:Label>
                        </div>
                    </div>
                    <div class="row p-1">
                        <div class="col col-lg-3">
                            <asp:Label ID="Label1" runat="server" style="font-size:1.2vw;" Text="Email:"></asp:Label>
                        </div>
                        <div class="col col-lg-9">
                            <asp:TextBox ID="TextBox1" class="form-control rounded-pill shadow-sm" runat="server" style="font-size:1.2vw;"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row p-1">
                        <div class="col col-lg-3">
                            <asp:Label ID="Label2" runat="server" style="font-size:1.2vw;" Text="Password:"></asp:Label>
                        </div>
                        <div class="col col-lg-9">
                            <asp:TextBox ID="TextBox2" class ="form-control rounded-pill shadow-sm" runat="server" TextMode="Password" style="font-size:1.2vw;"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row p-1">
                        <div class="col col-lg-12 align-items-center">
                            <asp:Button ID="Button1" class="btn btn-success w-75" runat="server" Text="Login" OnClick="Button1_Click" />
                        </div>
                    </div>
                    <div class="row p-1">
                        <div class="col col-lg-12">
                            <asp:HyperLink ID="HyperLink1" class="text-danger" runat="server" NavigateUrl="ForgotPassword.aspx">Forgot Password??</asp:HyperLink>
                        </div>
                    </div>
                    <div class="row p-1">
                        <div class="col col-lg-12">
                            <a class="btn btn-primary w-50" href="Index.aspx">Home</a>
                        </div>
                    </div>
                </form>
            </div>
        </center>
    </div>
    
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js" integrity="sha384-DfXdz2htPH0lsSSs5nCTpuj/zy4C+OGpamoFVy38MVBnE+IbbVYUew+OrCXaRkfj" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/popper.js@1.16.1/dist/umd/popper.min.js" integrity="sha384-9/reFTGAW83EW2RDu2S0VKaIzap3H66lZH81PoYlFhbGU+6BZp6G7niu735Sk7lN" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.5.3/dist/js/bootstrap.min.js" integrity="sha384-w1Q4orYjBQndcko6MimVbzY0tgp4pWB4lZ7lr30WKz0vr/aWKhXdBNmNb5D92v7s" crossorigin="anonymous"></script>

</body>
</html>
