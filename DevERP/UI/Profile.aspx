<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="DevERP.UI.Profile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="col-lg-12">
            <div class="row main panel panel-info">
                <div class="panel-heading">
                    <div class="panel-title">
                        <h1 class="title">Account Setting</h1>
                    </div>
                </div>
                <div class="panel-body">
                    <div class="col-sm-12">
                        <hr />
                    </div>
                    <div class="col-sm-12">
                        <ul id="name-content" class="col-sm-12 collapse in">
                            <li data-toggle="collapse" data-target="#nameMenu" class="collapsed active">
                                <a href="#">
                                    <div class="col-sm-12">
                                        <div class="col-sm-4">
                                            <span class="pull-left;">Name</span>
                                        </div>
                                        <div class="col-sm-4">
                                            <span runat="server" id="nameSpan">Name</span>
                                        </div>
                                        <div class="col-sm-4">
                                            <span class="pull-right">Edit</span>
                                        </div>
                                    </div>
                                </a>
                            </li>
                            <ul class="sub-menu collapse col-sm-12" id="nameMenu">
                                <li>
                                    <div class="col-lg-12 well">
                                        <div class="col-lg-3"></div>
                                        <div class="col-lg-6">
                                            <div class="form-group">
                                                <label class="col-sm-12 control-label">Your Name</label>
                                                <div class="col-sm-12">
                                                    <div class="input-group">
                                                        <span class="input-group-addon"><i class="fa fa-user fa" aria-hidden="true"></i></span>
                                                        <input type="text" runat="server" class="form-control" name="nameTextBox" id="nameTextBox" placeholder="Enter your Name" />
                                                    </div>
                                                </div>
                                                <div class="col-sm-12 min-height" runat="server" id="nameTextBoxError"></div>
                                            </div>
                                            <div class="col-sm-12 form-group ">
                                                <asp:Button runat="server" ID="nameSaveButton" CssClass="btn btn-default" Text="Save" OnClick="nameSaveButton_OnClick" />
                                                <input type="button" id="eameCancelButton" class="btn btn-default" data-toggle="collapse" data-target="#nameMenu" value="Cancel" />
                                            </div>
                                        </div>
                                        <div class="col-lg-3"></div>
                                    </div>
                                </li>
                            </ul>
                        </ul>
                    </div>
                    <div class="col-sm-12">
                        <hr />
                    </div>
                    <div class="col-sm-12">
                        <ul id="email-content" class="collapse in col-sm-12">
                            <li data-toggle="collapse" data-target="#emailMenu" class="collapsed active">
                                <a href="#">
                                    <div class="col-sm-12">
                                        <div class="col-sm-4">
                                            <span class="pull-left;">Email</span>
                                        </div>
                                        <div class="col-sm-4">
                                            <span runat="server" id="emailSpan">Email</span>
                                        </div>
                                        <div class="col-sm-4">
                                            <span class="pull-right">Edit</span>
                                        </div>
                                    </div>
                                </a>
                            </li>
                            <ul class="sub-menu collapse col-sm-12" id="emailMenu">
                                <li>
                                    <div class="col-lg-12 well">
                                        <div class="col-lg-3"></div>
                                        <div class="col-lg-6">
                                            <div class="form-group">
                                                <label class="col-sm-12 control-label">Your Email</label>
                                                <div class="col-sm-12">
                                                    <div class="input-group">
                                                        <span class="input-group-addon"><i class="fa fa-envelope fa" aria-hidden="true"></i></span>
                                                        <input type="text" runat="server" class="form-control" name="emailTextBox" id="emailTextBox" placeholder="Enter your Email" />
                                                    </div>
                                                </div>
                                                <div class="col-sm-12 min-height" runat="server" id="emailTextBoxError"></div>
                                            </div>
                                            <div class="col-sm-12 form-group ">
                                                <asp:Button runat="server" ID="emailSaveButton" CssClass="btn btn-default" Text="Save" OnClick="emailSaveButton_OnClick" />
                                                <input type="button" id="emailCancelButton" class="btn btn-default" data-toggle="collapse" data-target="#emailMenu" value="Cancel" />
                                            </div>
                                        </div>
                                        <div class="col-lg-3"></div>
                                    </div>
                                </li>
                            </ul>
                        </ul>
                    </div>
                    <hr />
                </div>
            </div>
        </div>
    </div>

</asp:Content>
