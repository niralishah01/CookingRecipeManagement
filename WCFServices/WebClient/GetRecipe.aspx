<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GetRecipe.aspx.cs" Inherits="WebClient.GetRecipe" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>All Recipes</title>
    <style>
        .imgThumbnail
        {
            height:200px;
            width:auto;
        }
        @media only screen and (max-width:750px){
            .card{
                /*max-width: 100%;*/
                display:block;
                clear:both;
            }        
        }
    </style>
    <meta charset="utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css"/>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.16.0/umd/popper.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
</head>
<body>
    <header>
        <h2 class="bg-secondary p-2 text-light">
            <img id="sitelogo" style="height:40px;width:40px" src="/Images/sitelogo.jpg">
            Zaiyka
        </h2>
    </header>
    <form id="form1" runat="server">
        <div class="container p-3">
            <div class="row">
                <div class="col-xl-12">
                    <div class="card">
                        <div class="card-header" id="card_h" runat="server">
                        </div>
                        <div class="card-body" id="card_b" runat="server">
                        </div>
                        <div class="card-footer bg-light" id="card_f" runat="server">
                            <center>
                                <a href="home.aspx" class="btn btn-outline-primary text-decoration-none btn-sm" value="Button" runat="server" >Back</a>
                            </center>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
