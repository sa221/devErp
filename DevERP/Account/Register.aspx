<%@ Page Title="Register" Language="C#" MasterPageFile="~/Account/Account.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="DevERP.Account.Register" %>

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
                    <div runat="server" ID="successMessage"></div>
                    <div class="form-group">
                        <label class="col-sm-12 control-label">Your Name</label>
                        <div class="col-sm-12">
                            <div class="input-group">
                                <span class="input-group-addon"><i class="fa fa-user fa" aria-hidden="true"></i></span>
                                <input type="text" runat="server" class="form-control" name="nameTextBox" id="nameTextBox" placeholder="Enter your Name" />
                            </div>
                        </div>
                        <div class="col-sm-12 min-height" runat="server" ID="nameTextBoxError"></div>
                    </div>

                    <div class="form-group">
                        <label class="col-sm-12 control-label">Your Email</label>
                        <div class="col-sm-12">
                            <div class="input-group">
                                <span class="input-group-addon"><i class="fa fa-envelope fa" aria-hidden="true"></i></span>
                                <input type="text" runat="server" class="form-control" name="emailTextBox" id="emailTextBox" placeholder="Enter your Email" />
                            </div>
                        </div>
                        <div class="col-sm-12 min-height" runat="server" ID="emailTextBoxError"></div>
                    </div>

                    <div class="form-group">
                        <label class="col-sm-12 control-label">Username</label>
                        <div class="col-sm-12">
                            <div class="input-group">
                                <span class="input-group-addon"><i class="fa fa-users fa" aria-hidden="true"></i></span>
                                <input type="text" runat="server" class="form-control" name="usernameTextBox" id="usernameTextBox" placeholder="Enter your Username" />
                            </div>
                        </div>
                             <div class="col-sm-12 min-height" runat="server" ID="usernameTextBoxError"></div>
                    </div>

                    <div class="form-group">
                        <label class="col-sm-12 control-label">Password</label>
                        <div class="col-sm-12">
                            <div class="input-group">
                                <span class="input-group-addon"><i class="fa fa-lock fa-lg" aria-hidden="true"></i></span>
                                <input type="password" runat="server" class="form-control" name="passwordTextBox" id="passwordTextBox" placeholder="Enter your Password" />
                            </div>
                        </div>
                        <div class="col-sm-12 min-height" runat="server" ID="passwordTextBoxError"></div>
                    </div>

                    <div class="form-group">
                        <label class="col-sm-12 control-label">Confirm Password</label>
                        <div class="col-sm-12">
                            <div class="input-group">
                                <span class="input-group-addon"><i class="fa fa-lock fa-lg" aria-hidden="true"></i></span>
                                <input type="password" runat="server" class="form-control" name="confirmPasswordTextBox" id="confirmPasswordTextBox" placeholder="Confirm your Password" />
                            </div>
                        </div>
                        <div class="col-sm-12 min-height" runat="server" ID="confirmPasswordTextBoxError"></div>
                    </div>

                    <div class="col-sm-12 form-group ">
                        <asp:Button runat="server" CssClass="btn btn-primary btn-lg btn-block login-button" Text="Register" OnClick="OnClick"/>
                    </div>
                    <div class="col-sm-12 login-registe">
                        <a runat="server" href="~/Account/Login.aspx">Login</a>
                        <label> if already have an account</label>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-sm-4"></div>
    </div>
</asp:Content>
