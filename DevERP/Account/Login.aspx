<%@ Page Title="Log in" Language="C#" MasterPageFile="~/Account/Account.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="DevERP.Account.Login" Async="true" %>

<asp:Content runat="server" ID="HeadContent" ContentPlaceHolderID="HeadContent">
    <style>
        .error {
            color: red;
            display: inline-block !important;
        }
    </style>
    <script>
        $(document).ready(function () {
            $("#MyForm").validate({
                rules: {
                    ctl00$MainContent$usernameTextBox: {
                        required: true,
                        minlength: 6
                    },
                    ctl00$MainContent$passwordTextBox: {
                        required: true,
                        minlength: 6
                    }
                }
            });

        });
    </script>
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div class="container">
        <div class="col-sm-4"></div>
        <div class="col-sm-4">
            <div class="row main panel panel-primary">
                <div class="panel-heading ">
                    <div class="panel-title text-center">
                        <h1 class="title">Login</h1>
                    </div>
                </div>
                <div class="panel-body">
                    <div runat="server" id="successMessage"></div>
                    <div class="form-group">
                        <label class="col-sm-12 control-label">Username</label>
                        <div class="col-sm-12">
                            <div class="input-group">
                                <span class="input-group-addon"><i class="fa fa-users fa" aria-hidden="true"></i></span>
                                <input type="text" runat="server" class="form-control" name="userNameTextBox" id="userNameTextBox" placeholder="Enter your Username" />
                            </div>
                        </div>
                        <div class="col-sm-12 min-height" runat="server" id="userNameTextBoxError"></div>
                    </div>

                    <div class="form-group">
                        <label class="col-sm-12 control-label">Password</label>
                        <div class="col-sm-12">
                            <div class="input-group">
                                <span class="input-group-addon"><i class="fa fa-lock fa-lg" aria-hidden="true"></i></span>
                                <input type="password" runat="server" class="form-control" name="passwordTextBox" id="passwordTextBox" placeholder="Enter your Password" />
                            </div>
                        </div>
                        <div class="col-sm-12 min-height" runat="server" id="passwordTextBoxError"></div>
                    </div>

                    <div class="col-sm-12 form-group">
                        <input type="checkbox" runat="server" id="checkBox" name="checkBox" />
                        <label class="control-label">Remamber Me</label>
                    </div>
                    <div class="col-sm-12 form-group ">
                        <asp:Button runat="server" CssClass="btn btn-primary btn-lg btn-block login-button" Text="Login" OnClick="OnClick" />
                    </div>
                    <div class="col-sm-12 login-registe">
                        <a runat="server" href="~/Account/Register.aspx">Register</a>
                        <label>if have no account</label>
                    </div>

                </div>
            </div>
        </div>
        <div class="col-sm-4"></div>
    </div>
</asp:Content>
