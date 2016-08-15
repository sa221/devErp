<%@ Page Title="Register" Language="C#" MasterPageFile="~/Account/Account.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="DevERP.Account.Register" %>

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
                    ctl00$MainContent$nameTextBox: {
                        required: true,
                        minlength: 2
                    },
                    ctl00$MainContent$emailTextBox: {
                        required: true,
                        email: true
                    },
                    ctl00$MainContent$usernameTextBox: {
                        required: true,
                        minlength: 6
                    },
                    ctl00$MainContent$passwordTextBox: {
                        required: true,
                        minlength: 6
                    },
                    ctl00$MainContent$confirmPasswordTextBox: {
                        required: true,
                        equalTo: "#MainContent_passwordTextBox"
                    },
                    ctl00$MainContentcheckBox: {
                        required: true
                    },
                    ctl00$MainContent$captchaTextBox: {
                        required: true
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
                        <h1 class="title">Registration</h1>
                    </div>
                </div>
                <div class="panel-body">
                    <div runat="server" id="successMessage"></div>
                    <div class="form-group required">
                        <label class="col-sm-12 control-label">Your Name</label>
                        <div class="col-sm-12">
                            <div class="input-group">
                                <span class="input-group-addon"><i class="fa fa-user fa" aria-hidden="true"></i></span>
                                <input type="text" runat="server" class="form-control" name="nameTextBox" id="nameTextBox" placeholder="Enter your Name" />
                            </div>
                        </div>
                        <div class="col-sm-12 min-height" runat="server" id="nameTextBoxError"></div>
                    </div>

                    <div class="form-group required">
                        <label class="col-sm-12 control-label">Your Email</label>
                        <div class="col-sm-12">
                            <div class="input-group">
                                <span class="input-group-addon"><i class="fa fa-envelope fa" aria-hidden="true"></i></span>
                                <input type="text" runat="server" class="form-control" name="emailTextBox" id="emailTextBox" placeholder="Enter your Email" />
                            </div>
                        </div>
                        <div class="col-sm-12 min-height" runat="server" id="emailTextBoxError"></div>
                    </div>

                    <div class="form-group required">
                        <label class="col-sm-12 control-label">Username</label>
                        <div class="col-sm-12">
                            <div class="input-group">
                                <span class="input-group-addon"><i class="fa fa-users fa" aria-hidden="true"></i></span>
                                <input type="text" runat="server" class="form-control" name="userNameTextBox" id="userNameTextBox" placeholder="Enter your Username" />
                            </div>
                        </div>
                        <div class="col-sm-12 min-height" runat="server" id="userNameTextBoxError"></div>
                    </div>

                    <div class="form-group required">
                        <label class="col-sm-12 control-label">Password</label>
                        <div class="col-sm-12">
                            <div class="input-group">
                                <span class="input-group-addon"><i class="fa fa-lock fa-lg" aria-hidden="true"></i></span>
                                <input type="password" runat="server" class="form-control" name="passwordTextBox" id="passwordTextBox" placeholder="Enter your Password" />
                            </div>
                        </div>
                        <div class="col-sm-12 min-height" runat="server" id="passwordTextBoxError"></div>
                    </div>

                    <div class="form-group required">
                        <label class="col-sm-12 control-label">Confirm Password</label>
                        <div class="col-sm-12">
                            <div class="input-group">
                                <span class="input-group-addon"><i class="fa fa-lock fa-lg" aria-hidden="true"></i></span>
                                <input type="password" runat="server" class="form-control" name="confirmPasswordTextBox" id="confirmPasswordTextBox" placeholder="Confirm your Password" />
                            </div>
                        </div>
                        <div class="col-sm-12 min-height" runat="server" id="confirmPasswordTextBoxError"></div>
                    </div>
                    <div class="form-group required">
                        <label class="col-sm-12 control-label">Captcha From Below</label>
                        <div class="col-sm-12">
                            <div class="input-group">
                                <span class="input-group-addon"><i class="fa fa-file-image-o fa-lg" aria-hidden="true"></i></span>
                                <input type="text" runat="server" class="form-control" name="captchaTextBox" id="captchaTextBox" placeholder="Enter Below Captcha" />
                            </div>
                        </div>
                        <div class="col-sm-12 min-height" runat="server" id="captchaTextBoxError"></div>
                    </div>
                    <div class="form-group required">
                        <asp:UpdatePanel ID="UP1" runat="server">
                            <contenttemplate>
                                <div class="col-sm-6">
                                    <asp:Image ID="imgCaptcha" runat="server"/>
                                </div>
                                <div class="col-sm-6">
                                    <asp:Button CssClass="form-control" ID="btnRefresh" runat="server" Text="Refresh" OnClick="btnRefresh_OnClick" formnovalidate="true"/>
                                </div>
                            </contenttemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="col-sm-12 form-group required">
                        <input type="checkbox" runat="server" id="checkBox" name="checkBox" />
                        <label class="control-label">I accept the <a target="blank" runat="server" id="termAndCondition" href="~/Account/TermAndCondition.aspx">Terms And Condition</a></label>
                    </div>
                    <div class="col-sm-12 form-group ">
                        <asp:Button runat="server" CssClass="btn btn-primary btn-lg btn-block login-button" Text="Register" OnClick="OnClick" />
                    </div>
                    <div class="col-sm-12 login-registe">
                        <a runat="server" href="~/Account/Login.aspx">Login</a>
                        <label>if already have an account</label>
                    </div>

                </div>
            </div>
        </div>
        <div class="col-sm-4"></div>
    </div>
</asp:Content>
