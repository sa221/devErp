<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProductSale.aspx.cs" Inherits="DevERP.UI.ProductSale" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container body-content">
        <div class="form-horizontal" style="margin: 0 auto; margin-top: 50px; display: block; position: relative; left: 0%;">
            <%--<div class="col-lg-2"></div>--%>
            <div class="col-lg-12 ">
                <div class="col-lg-8">
                    <div class="col-lg-12">
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <h1 style="font-size: 20px" class="panel-title text-center">Sales Entry</h1>
                            </div>
                            <div class="panel-body">
                                <div class="col-lg-12">
                                    <div class="col-sm-4 form-group">
                                        <div class="col-lg-12">
                                            <b>Invoice No: </b>
                                            <asp:TextBox ID="invoiceNoTextBox" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4 form-group">
                                        <div class="col-lg-12">
                                            <br />
                                            <asp:Button ID="searchButton" runat="server" Text="Search" CssClass="btn btn-info btn-sm " />
                                        </div>
                                    </div>
                                    <div class="col-sm-4 form-group">
                                        <div class="col-lg-12">
                                            <b>Date: </b>
                                            <asp:TextBox ID="dateTextBox" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-12">
                                    <div class="col-sm-4 form-group">
                                        <div class="col-lg-12">
                                            <b>Product: </b>
                                            <asp:DropDownList ID="productNameDropDownList" runat="server" CssClass="form-control"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-4 form-group">
                                        <div class="col-lg-12">
                                            <b>Stock: </b>
                                            <asp:TextBox ID="stockQtyTextBox" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4 form-group">
                                        <div class="col-lg-12">
                                            <b>Rate: </b>
                                            <asp:TextBox ID="ratesTextBox" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>

                                </div>
                                <div class="col-lg-12">
                                    <div class="col-sm-4 form-group">
                                        <div class="col-lg-12">
                                            <b>Qty: </b>
                                            <asp:TextBox ID="qtyTextBox" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4 form-group">
                                        <div class="col-lg-12">
                                            <b>Total: </b>
                                            <asp:TextBox ID="totalTakaTextBox" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4 form-group">
                                        <div class="col-lg-12">
                                            <br />
                                            <asp:Button ID="addButton" runat="server" Text="ADD" CssClass="btn btn-info btn-sm " />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-12">
                                    <div class="col-sm-12 form-group">
                                        <asp:UpdatePanel ID="up" runat="server">
                                            <ContentTemplate>
                                                <asp:GridView ID="productWithSizeRateGridView" runat="server" UseAccessibleHeader="true"
                                                    CssClass="table table-hover table-striped table table-bordered" GridLines="None"
                                                    AutoGenerateColumns="False">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="SL#">
                                                            <ItemTemplate>
                                                                <%#Container.DataItemIndex+1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText=" P.Type">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("ProductType") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="P.Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("ProdectName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Size">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label3" runat="server" Text='<%# Eval("ProductSize") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Rate">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label4" runat="server" Text='<%# Eval("Rate") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Full Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label5" runat="server" Text='<%# Eval("FullProductName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <RowStyle CssClass="cursor-pointer" />
                                                </asp:GridView>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <%--<div class="col-sm-12 form-group">
                                        <div class="col-lg-12">
                                            <b>Product Rate: </b>
                                            <asp:TextBox ID="rateTextBox" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 form-group">
                                        <div class="col-lg-5"></div>
                                        <div class="col-lg-2">
                                            <div class="col-md-offset-0 col-md-9">
                                                <asp:Button ID="sizeRateSaveButton" runat="server" Text="Save" CssClass="btn btn-primary btn-sm " />
                                            </div>
                                        </div>
                                        <div class="col-lg-5"></div>
                                    </div>--%>
                                <div class="col-sm-12 form-group">
                                    <span class="label label-warning" style="float: left; font-size: 20px; position: relative; background-color: #f0ad4e" id="Span1" runat="server"></span>
                                    <span class="label label-success" style="float: left; font-size: 20px; position: relative; background-color: #5cb85c" id="Span2" runat="server"></span>
                                    <br />
                                </div>

                                <%--<div class="col-lg-7">
                                    <div class="col-sm-12 form-group">
                                        <asp:UpdatePanel ID="up" runat="server">
                                            <ContentTemplate>
                                                <asp:GridView ID="productWithSizeRateGridView" runat="server" UseAccessibleHeader="true"
                                                    CssClass="table table-hover table-striped table table-bordered" GridLines="None"
                                                    AutoGenerateColumns="False">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="SL#">
                                                            <ItemTemplate>
                                                                <%#Container.DataItemIndex+1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText=" P.Type">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("ProductType") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="P.Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("ProdectName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Size">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label3" runat="server" Text='<%# Eval("ProductSize") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Rate">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label4" runat="server" Text='<%# Eval("Rate") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Full Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label5" runat="server" Text='<%# Eval("FullProductName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <RowStyle CssClass="cursor-pointer" />
                                                </asp:GridView>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>--%>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-4">
                    <div class="col-lg-12">
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <h1 style="font-size: 20px" class="panel-title text-center">Party Info</h1>
                            </div>
                            <div class="panel-body">
                                <div class="col-lg-12 form-group">
                                    <div class="col-lg-6"><b>Mobile: </b>
                                        <asp:TextBox ID="mobileTextBox" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-6"><br />
                                        <asp:Button ID="customerSearchButton" runat="server" Text="Search" CssClass="btn btn-info btn-sm " />
                                    </div>
                                </div>
                                <div class="col-lg-12 form-group">
                                    <div class="col-lg-6">
                                        <b>Name: </b>
                                        <asp:TextBox ID="customerNameTextBox" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-6">
                                        <b>Company: </b>
                                        <asp:TextBox ID="customerCompanyTextBox" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-12 form-group">
                                    <div class="col-lg-6">
                                        <b>Address: </b>
                                        <asp:TextBox ID="customerAddressTextBox" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-6">
                                        <b>Email: </b>
                                        <asp:TextBox ID="emailTextBox" runat="server" CssClass="form-control"></asp:TextBox>
                                        <b>Phone:</b><asp:TextBox ID="TextBox1" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>

                              

                            </div>
                        </div>
                    </div>
                    <div class="col-lg-12">
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <h1 style="font-size: 20px" class="panel-title text-center">Payment Procedure</h1>
                            </div>

                            <div class="panel-body">
                                <div class="col-lg-12 form-group">
                                   <%-- <div class="col-lg-4"><b>Payment Type: </b></div>--%>
                                    <div class="col-lg-6">
                                        <b>Pay.Type: </b>
                                        <asp:DropDownList ID="paymentTypeDropDownList" runat="server" CssClass="form-control">
                                            <asp:ListItem Value="1">CASH</asp:ListItem>
                                            <asp:ListItem Value="2">CHEQUE</asp:ListItem>
                                            <asp:ListItem Value="3">CASH&amp;CHEQUE</asp:ListItem>
                                        </asp:DropDownList>

                                    </div>
                                     <div class="col-lg-6">
                                        <b>Total Amount: </b>
                                        <asp:TextBox ID="totalAmountTextBox" runat="server" CssClass="form-control"></asp:TextBox>
                                        
                                    </div>
                                </div>
                                <div class="col-lg-12 form-group">
                                   
                                    <div class="col-lg-6">
                                        <b>Discount: </b>
                                        <asp:TextBox ID="discountPercentageTextBox" runat="server" CssClass="form-control" placeholder="%"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-6">
                                        <b>Dis. In Taka </b>
                                        <asp:TextBox ID="discountTakaTextBox" runat="server" CssClass="form-control" placeholder="TAKA"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-12 form-group">
                                    <div class="col-lg-10"></div>
                                    <div class="col-lg-2">
                                        <div class="col-md-offset-0 col-md-9">
                                            <asp:Button ID="productNameSaveButton" runat="server" Text="Save" CssClass="btn btn-primary btn-primary btn-sm " />
                                        </div>
                                    </div>
                                    <%--<div class="col-lg-2"></div>--%>
                                </div>
                                <div class="col-lg-12 form-group">
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


            </div>
            <%--<div class="col-lg-2"></div>--%>
        </div>
        <div class="clearfix"></div>

    </div>
    <script src="../Scripts/ScrollableGridViewPlugin_ASP.NetAJAXmin.js" type="text/javascript"></script>
    <%-- <script type="text/javascript">
        $(document).ready(function () {
            $('#<%=productWithSizeRateGridView.ClientID %>').Scrollable({
                ScrollHeight: 400,
                IsInUpdatePanel: true
            });
        });
    </script>--%>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#<%=productNameGridView.ClientID %>').Scrollable({
                ScrollHeight: 100,
                IsInUpdatePanel: true
            });
        });
    </script>
   
</asp:Content>
