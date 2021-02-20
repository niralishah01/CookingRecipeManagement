<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="WebClient.Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.7.0/css/all.css" integrity="sha384-lZN37f5QGtY3VHgisS14W3ExzMWZxybE1SJSEsQp9S+oqd12jhcu+A56Ebc1zFSJ" crossorigin="anonymous">
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.5.3/dist/css/bootstrap.min.css" integrity="sha384-TX8t27EcRE3e/ihU7zmQxVncDAy5uIKz4rEkgIXeMed4M0jlfIDPvg6uqKI2xXr2" crossorigin="anonymous">
    
    
    <style>
        body{
            background:linear-gradient(cyan,yellow);
            background-position:center;
            background-repeat:no-repeat;
            background-size:cover;
            background-attachment:fixed;
        }
        .centered{
            top:20%;
        }
    </style>
</head>
<body> 
    <div class="container">
        <center>
        <div class="bg-white centerd p-2 w-25 rounded shadow-lg">
        <form id="form1" runat="server">
            <a class="btn btn-primary" href="Index.aspx">Home</a><br />
            <h3>Register yourself here...</h3><br />
            <asp:Label ID="Label4" style="font-size:1vw;" runat="server" Text="" ForeColor="Red"></asp:Label>
            <br />
            <div class="input-group">
                <asp:Label ID="Label1" style="font-size:1.2vw;" runat="server" Text="Name:"></asp:Label>
                <asp:TextBox ID="TextBox1" class="form-control" style="font-size:1.2vw;" runat="server" required></asp:TextBox>
            </div>
                       
            <br />
            <div class="input-group">
                <asp:Label ID="Label2" style="font-size:1.2vw;" runat="server" Text="Email:"></asp:Label>
            <asp:TextBox ID="TextBox2"  class="form-control" style="font-size:1.2vw;" runat="server" required></asp:TextBox>
            </div>
            <br />
            <div class="input-group">
                <asp:Label ID="Label3" style="font-size:1.2vw;" runat="server" Text="Password:" required></asp:Label>
                <asp:TextBox ID="TextBox3" type="password"  class="form-control" style="font-size:1.2vw;"  runat="server"></asp:TextBox>
                    <div class="input-group-append">
                        <button id="show" class="btn btn-primary"><span class="fa fa-eye-slash icon"></span></button>
                    </div>
            </div>
            
            <br />
            <asp:Button ID="Button1" class="form-control btn btn-success" runat="server" Text="SignUp" OnClick="Button1_Click" />
        </form>
        Already Registered? SignIn here..<br />
        <a class="btn btn-primary" href="Login.aspx">Sign In</a>
        </div>
            </center>
    </div>    
    <script type="text/javascript">
        $(document).ready(function () {
            $(#show).hover(function showp() {
                $(#TextBox3).attr('type', 'text');
                $('.icon').removeClass('fa fa-eye-slash').addClass('fa fa-eye');
            },
                function() {
                    $(#TextBox3).attr('type', 'password');
                    $('.icon').removeClass('fa fa-eye').addClass('fa fa-eye-slash');
                });
        })
    </script>
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js" integrity="sha384-DfXdz2htPH0lsSSs5nCTpuj/zy4C+OGpamoFVy38MVBnE+IbbVYUew+OrCXaRkfj" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/popper.js@1.16.1/dist/umd/popper.min.js" integrity="sha384-9/reFTGAW83EW2RDu2S0VKaIzap3H66lZH81PoYlFhbGU+6BZp6G7niu735Sk7lN" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.5.3/dist/js/bootstrap.min.js" integrity="sha384-w1Q4orYjBQndcko6MimVbzY0tgp4pWB4lZ7lr30WKz0vr/aWKhXdBNmNb5D92v7s" crossorigin="anonymous"></script>
</body>
</html>
