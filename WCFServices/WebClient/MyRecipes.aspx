<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyRecipes.aspx.cs" Inherits="WebClient.MyRecipes" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title></title>
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

    <!-- For ICONS -->
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.7.0/css/all.css" integrity="sha384-lZN37f5QGtY3VHgisS14W3ExzMWZxybE1SJSEsQp9S+oqd12jhcu+A56Ebc1zFSJ" crossorigin="anonymous">
</head>
<body>
    <header>
        <h2 class="bg-secondary p-2 text-light">
                <img id="sitelogo" style="height:40px;width:40px" src="/Images/sitelogo.jpg" alt="No image">
                Zaiyka
                <a href="" class="btn btn-light" runat="server" id="myhome"><i class="fas fa-home"></i> Home</a>
                <a href="#" class="btn btn-light" runat="server" id="add"><i class="fas fa-pizza-slice"></i> Add Recipes</a>
                <a href="" class="btn btn-light" runat="server" id="myrecipes"><i class="fas fa-box-open"></i> My Recipes</a>
                <a href="#" class="btn btn-light" runat="server"  id="ViewProfile" onserverclick="ViewProfile_Click"><i class="fas fa-user-alt"></i> Profile</a>
                <a href="Login.aspx" class="btn btn-light" runat="server" id="logout"><i class="fas fa-sign-out-alt"></i> Logout</a>
        </h2>
    </header>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <h2 runat="server" id="title">Your uploaded recipes</h2>
        <div class="container p-1">
            <asp:Panel ID="Panel1" runat="server">
                <div class="modal fade" id="deleteModal" role="dialog" aria-hidden="true">
                    <div class="modal-dialog">
                        <asp:UpdatePanel ID="upmodal" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="modal-content">
                                    <!-- Modal Header -->
                                    <div class="modal-header" id="modal_header" runat="server">
                                      <h4 class="modal-title" id="modal_title" runat="server"></h4>
                                      <button type="button" class="close" data-dismiss="modal">&times;</button>
                                    </div>
                                    <!-- Modal body -->
                                    <div class="modal-body" id="modal_body" runat="server">
                                    </div>
                                    <!-- Modal footer -->
                                    <div class="modal-footer"  id="modal_footer" runat="server">
                                        <asp:Button ID="modal_yes" class="btn btn-danger" runat="server" Text="Yes" OnClick="deleteRecipeConfirm"></asp:Button>
                                        <asp:Button ID="modal_no" class="btn btn-secondary" data-dismiss="modal" runat="server" Text="No"></asp:Button>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </asp:Panel>
        </div>
    <asp:Panel ID="Panel2" runat="server"></asp:Panel>
    <div class="modal fade" id="myModal" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h4 class="modal-title"><asp:Label ID="UserProfile" runat="server" Text="Profile"></asp:Label></h4>
                                    <button type="button" class="close" data-dismis="modal" aria-hidden="true">&times;</button>
                                    
                                </div>
                                <div class="modal-body">
                                    <div class="input-group">
                                        <asp:Label ID="name" class="col-form-label" runat="server" Text="Name:"></asp:Label>
                                        <asp:TextBox ID="uname" class="form-control" runat="server" type="Text" ></asp:TextBox>
                                    </div>
                                    <div class="input-group">
                                        <asp:Label ID="email" class="col-form-label" runat="server" Text="Email:"></asp:Label>
                                        <asp:TextBox ID="emailid" class="form-control" runat="server" type="email" ></asp:TextBox>
                                    </div>
                                    <div class="input-group">
                                        <asp:Button ID="update" class="btn btn-success" runat="server" Text="Update" OnClick="Update" />
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <button class="btn btn-danger" data-dismiss="modal" aria-hidden="true">Close</button>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>    
            </div>
    </form>
    </body>
</html>