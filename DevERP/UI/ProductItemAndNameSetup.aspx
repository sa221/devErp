<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProductItemAndNameSetup.aspx.cs" Inherits="DevERP.UI.ProductItemAndNameSetup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container body-content">
        <div class="form-horizontal" style="margin: 0 auto; margin-top: 50px; display: block; position: relative; left: 0%;">
            <%--<div class="col-lg-2"></div>--%>
            <div class="col-lg-12 ">
                <div class="col-lg-6">
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            <h1 style="font-size: 20px" class="panel-title text-center">Product Type Entry</h1>
                        </div>
                        <div class="panel-body">
                            <div class="col-lg-12 form-group">
                                <div class="col-lg-4"><b>Product Type: </b></div>
                                <div class="col-lg-8">
                                    <asp:TextBox ID="productTypeTextBox" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-12 form-group">
                                <div class="col-lg-10"></div>
                                <div class="col-lg-2">
                                    <div class="col-md-offset-0 col-md-9">
                                        <asp:Button ID="productTypeSaveButton" runat="server" Text="Save" CssClass="btn btn-primary btn-primary btn-sm " OnClick="productTypeSaveButton_Click" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-12">
                                <span class="label label-warning" style="float: left; font-size: 20px; position: relative; background-color: #f0ad4e" id="failStatusLabel" runat="server"></span>
                                <span class="label label-success" style="float: left; font-size: 20px; position: relative; background-color: #5cb85c" id="successStatusLabel" runat="server"></span>
                                <br />
                            </div>

                            <div class="col-lg-12 form-group">
                                <asp:GridView ID="productTypeGridview" runat="server" UseAccessibleHeader="true"
                                    CssClass="table table-hover table-striped table table-bordered" GridLines="None"
                                    AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SL#">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Type Name">
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("ProductType") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <RowStyle CssClass="cursor-pointer" />
                                </asp:GridView>
                            </div>

                        </div>
                    </div>
                </div>
                <div class="col-lg-6">
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            <h1 style="font-size: 20px" class="panel-title text-center">Product Name Entry</h1>
                        </div>

                        <div class="panel-body">
                                <div class="col-lg-12 form-group">
                                    <div class="col-lg-4"><b>Product Type: </b></div>
                                    <div class="col-lg-8">
                                        <asp:DropDownList ID="productTypeDropDownList" runat="server" CssClass="form-control"></asp:DropDownList>

                                    </div>
                                </div>
                                <div class="col-lg-12 form-group">
                                    <div class="col-lg-4"><b>Product Name: </b></div>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="productNameTextBox" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-12 form-group">
                                    <div class="col-lg-10"></div>
                                    <div class="col-lg-2">
                                        <div class="col-md-offset-0 col-md-9">
                                            <asp:Button ID="productNameSaveButton" runat="server" Text="Save" CssClass="btn btn-primary btn-primary btn-sm " OnClick="productNameSaveButton_Click" />
                                        </div>
                                    </div>
                                    <%--<div class="col-lg-2"></div>--%>
                                </div>
                                <div class="col-lg-12">
                                    <span class="label label-warning" style="float: left; font-size: 20px; position: relative; background-color: #f0ad4e" id="failStatusLabelProductName" runat="server"></span>
                                    <span class="label label-success" style="float: left; font-size: 20px; position: relative; background-color: #5cb85c" id="successStatusLabelProductName" runat="server"></span>
                                    <br />
                                </div>

                                <div class="col-lg-12">
                                    <asp:GridView ID="productNameGridView" runat="server" UseAccessibleHeader="true"
                                        CssClass="table table-hover table-striped table table-bordered" GridLines="None"
                                        AutoGenerateColumns="False">
                                        <Columns>
                                            <asp:TemplateField HeaderText="SL#">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Type Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("ProductType") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Product Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("ProdectName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <RowStyle CssClass="cursor-pointer" />
                                    </asp:GridView>
                                </div>

                            
                    </div>
                    </div>
                </div>
            </div>
            <%--<div class="col-lg-2"></div>--%>
        </div>
        <div class="clearfix"></div>
    </div>
</asp:Content>
