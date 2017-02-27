<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Sales.aspx.cs" Inherits="DevERP.UI.Sales" EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script>
        $("#test").keypress(function (e) {
            if (e.which == 13) {
                $(document.activeElement).next().focus();
            }
        });
    </script>
    <script>
        $(document).ready(function () {
            $("#MainContent_dateText").datepicker({
                autoclose: true,
                todayHighlight: true,
                format: "dd/mm/yyyy"
            });
            $('#MainContent_productCodeText').keypress(function (e) {
                if (e.which == 13) // Enter key = keycode 13
                {
                    $('#MainContent_qtyText').focus();  //Use whatever selector necessary to focus the 'next' input
                    return false;
                }
            });
        })
    </script>
    <script type="text/javascript">
        function setProductName() {
            var productCode = document.getElementById("MainContent_productCodeText").value;
            if (productCode != "") {
                $("#<%=productNameDropDownList.ClientID%>").val(productCode);
                //document.getElementById("MainContent_productNameDropDownList").selected = productCode;
            }
        }
        function setProductId() {
            document.getElementById("MainContent_productCodeText").value = "";
            var productId = document.getElementById("MainContent_productNameDropDownList").value;
            if (productId != "") {
                document.getElementById("MainContent_productCodeText").value = productId;
            }
        }
        function setDiscount() {
            //document.getElementById("MainContent_productCodeText").value = "";
            var qty = document.getElementById("MainContent_qtyText").value;
            var price = document.getElementById("MainContent_priceText").value;
            //var vat = document.getElementById("MainContent_vatText").value;
            //var itemDis = document.getElementById("MainContent_itemDiscountPercentageText").value;
            //var totalDiscount = 0;
            //if (itemDis != "" && itemDis > 0) {
            //    totalDiscount = (qty * price) * (itemDis / 100);
            //    document.getElementById("MainContent_itemDiscountTakaText").value = totalDiscount;
            //}
            //var vatAmount = (qty * price) * (vat / 100);
            document.getElementById("MainContent_itemTotalTakaText").value = (qty * price);

        }
        function setNetPayable() {
            //document.getElementById("MainContent_productCodeText").value = "";
            var totalAmount = document.getElementById("MainContent_allItemTotalTakaText").value;
            var discount = document.getElementById("MainContent_discountAgainstInvoiceTaka").value;
            document.getElementById("MainContent_netPayable").value = (totalAmount - discount);

        }
        function setDues() {
            //document.getElementById("MainContent_productCodeText").value = "";
            var totalAmount = document.getElementById("MainContent_netPayable").value;
            var cash = document.getElementById("MainContent_cashRcvText").value;
            var cheque = document.getElementById("MainContent_chequeAmount").value;
            if (totalAmount != "") {
                if (cash != "" && cheque != "") {
                    document.getElementById("MainContent_duesText").value = (totalAmount - (cash + cheque));
                }
                else if (cash == "" && cheque != "") {
                    document.getElementById("MainContent_duesText").value = (totalAmount - cheque);
                }
                else if (cash != "" && cheque == "") {
                    document.getElementById("MainContent_duesText").value = (totalAmount - cash);
                }
                //else  {
                //    document.getElementById("MainContent_duesText").value = (totalAmount - (cash + cheque));
                //}
            }
        }

    </script>
    <style>
        .myTextBoxMargin {
            padding-left: 2px !important;
            padding-right: 2px !important;
        }

        .myTextBoxMar {
            margin-top: 4%;
        }

        .myButtonTopMargin {
            margin-top: 5%;
        }

        .panelMargin {
            margin-top: 0%;
        }

        .panel-primary > .panel-heading {
            color: #ffffff;
            background-color: #428bca;
            border-color: #428bca;
            text-align: center;
        }

        /*.button, html input[type="button"],
        input[type="reset"], input[type="submit"] {
            height: 100% !important;
        }*/

        .table {
            width: 100%;
            margin-bottom: 0px !important;
        }

        .form-group {
            margin-bottom: 2px !important;
        }

        .table > tbody > tr > td {
            padding: 8px !important;
        }

        .header-center {
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="col-sm-12">
        <div class="panel panel-primary">
            <div class="panel-heading">
                SALES MODULE
            </div>
            <div class="panel-body">
                <div class="form-horizontal">
                    <div class="row">
                        <div class="col-sm-12" id="test">

                            <div class="form-group">
                                <div class=" col-sm-3">
                                    <label>Invoice Number</label>
                                    <asp:DropDownList runat="server" ID="invoiceNumberDropdownList" CssClass="form-control input-sm">
                                    </asp:DropDownList>
                                </div>
                                <div class=" col-sm-3">
                                </div>
                                <div class=" col-sm-3">
                                    <input type="text" runat="server" id="itemUpdateText" class="form-control input-sm" style="display: none" />
                                </div>
                                <div class=" col-sm-3">
                                    <label>Date</label>
                                    <input type="text" runat="server" id="dateText" class="form-control input-sm" />
                                </div>
                            </div>
                            <%--<asp:UpdatePanel runat="server">
                                <ContentTemplate>--%>
                            <div class="form-group">
                                <div class=" col-sm-1">
                                    <label>Pr.Code</label>
                                    <input runat="server" type="text" class="form-control input-sm" id="productCodeText" />
                                </div>
                                <div class=" col-sm-2">
                                    <label>Product Name</label>
                                    <asp:DropDownList runat="server" CssClass="form-control input-sm" ID="productNameDropDownList" AutoPostBack="False"></asp:DropDownList>
                                </div>
                                <div class="col-sm-1">
                                    <label>Qty</label>
                                    <input type="text" class="form-control input-sm" runat="server" id="qtyText" onkeyup="setDiscount()" />
                                </div>
                                <div class="col-sm-1">
                                    <label>Price</label>
                                    <input type="text" class="form-control input-sm" runat="server" id="priceText" />
                                </div>
                                <div class="col-sm-1">
                                    <label>Total</label>
                                    <input type="text" class="form-control input-sm" runat="server" id="itemTotalTakaText" style="padding-left: 2px; padding-right: 0px" />
                                </div>
                                <div class="col-sm-1">
                                    <br />
                                    <asp:Button ID="addButton" runat="server" class="btn btn-success" Text="Add" OnClick="addButton_Click" />
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-sm-12">
                                    <div class="col-lg-6">
                                        <%--OnSelectedIndexChanged="salesGridView_SelectedIndexChanged" OnRowDataBound="ItemOnRowDataBound"--%>
                                        <asp:GridView ID="salesGridView" runat="server" ShowHeaderWhenEmpty="True"
                                            OnRowCommand="salesGridView_RowCommand"
                                            CssClass="table-hover table-striped table table-bordered" GridLines="None"
                                            AutoGenerateColumns="False">
                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                            <RowStyle HorizontalAlign="Center" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr#">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="2%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Product ID" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("ProductId") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:BoundField DataField="ProductId" HeaderText="Product Id" Visible="False" />--%>
                                                <asp:BoundField DataField="ProductName" HeaderText="Product Name" HeaderStyle-CssClass="header-center">
                                                    <ItemStyle Width="70px"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Qty" HeaderText="Qty" HeaderStyle-CssClass="header-center" ><ItemStyle Width="40px"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Price" HeaderText="Price" HeaderStyle-CssClass="header-center" ><ItemStyle Width="40px"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Total" HeaderText="Total Taka" HeaderStyle-CssClass="header-center" ><ItemStyle Width="40px"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="Action" HeaderStyle-CssClass="header-center">
                                                    <HeaderStyle Width="28%" />
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnEdit" runat="server" Width="50" CssClass="btn btn-primary" Text="Edit" CommandName="EditButton"
                                                            CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                        <asp:Button ID="btnDelete" runat="server" Width="70" Text="Delete" CssClass="btn btn-danger" CommandName="DeleteButton"
                                                            CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>
                                            <HeaderStyle CssClass="GridviewScrollHeader" />
                                            <RowStyle CssClass="GridviewScrollItem" />
                                            <PagerStyle CssClass="GridviewScrollPager" />
                                        </asp:GridView>
                                    </div>
                                    <div class="col-sm-3">
                                    <div class="col-sm-12">
                                        <label>Sales Type</label>
                                        <asp:DropDownList runat="server" ID="salesTypeDropdownList" CssClass="form-control input-sm">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-12">
                                        <label>Card/Cheque No.</label>
                                        <input class="form-control input-sm" type="text" runat="server" id="cardChequeNumberText" />
                                    </div>
                                    <div class="col-sm-12">
                                        <label>Card/Cheque Date</label>
                                        <input class="form-control input-sm" type="text" runat="server" id="cardChequeDateText" />
                                    </div>
                                    <div class="col-sm-12">
                                        <label>Paid By Card(Taka)</label>
                                        <input class="form-control input-sm" type="text" runat="server" id="chequeAmount" />
                                    </div>
                                    <div class="col-sm-12">
                                        <label>Card/Cheque Bank</label>
                                        <asp:DropDownList runat="server" ID="cardChequeBankDropDownList" CssClass="form-control input-sm">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-12">
                                        <label>Diposit Bank</label>
                                        <asp:DropDownList runat="server" ID="dipositBankDropDownList" CssClass="form-control input-sm">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="col-sm-12">
                                        <label>Discount Against Invoice</label>
                                        <input class="form-control input-sm" type="text" runat="server" id="discountAgainstInvoiceTaka" />
                                    </div>
                                    <%--<div class="col-sm-12">
                                        <label>Discount Against Invoice</label>
                                        <div class="form-group">
                                            <div class="col-sm-6">
                                                <input class="form-control input-sm" type="text" runat="server" id="discountAgainstInvoicePercentage" text="%" placeholder="IV Dis %" />
                                            </div>
                                            <div class="col-sm-6">
                                                <input class="form-control input-sm" type="text" runat="server" id="discountAgainstInvoiceTaka" text="Tk" placeholder="IV Dis Tk" />
                                            </div>
                                        </div>
                                    </div>--%>
                                    <div class="col-sm-12">
                                        <label>Net payable</label>
                                        <input class="form-control input-sm" type="text" runat="server" id="netPaybleText" />
                                    </div>
                                    <div class="col-sm-12">
                                        <label>Cash Received</label>
                                        <input class="form-control input-sm" type="text" runat="server" id="cashReceivedText" />
                                    </div>
                                    <div class="col-sm-12">
                                        <label>Exchange Taka</label>
                                        <input class="form-control input-sm" type="text" runat="server" id="exchangeTakaText" />
                                    </div>
                                    <div class="col-sm-12">
                                        <label>Collected Taka</label>
                                        <input class="form-control input-sm" type="text" runat="server" id="collectedTakaText" />
                                    </div>
                                </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-sm-4">
                                    <asp:TextBox ID="partIdTextBox" runat="server" Visible="False"></asp:TextBox>
                                </div>
                                <div class="col-sm-1 myTextBoxMargin">
                                    <label>Quantity</label>
                                </div>
                                <div class="col-sm-1">
                                    <input class="form-control input-sm" type="text" runat="server" id="totalQtyText" />
                                </div>
                                <div class="col-sm-1"></div>
                                <div class="col-sm-1 myTextBoxMargin">
                                    <label>Total Taka</label>
                                </div>
                                <div class="col-sm-2">
                                    <input class="form-control input-sm" type="text" runat="server" id="allItemTotalTakaText" />
                                </div>
                                <div class="col-sm-2"></div>
                            </div>
                            <%-- </ContentTemplate>
                            </asp:UpdatePanel>--%>
                            <div class="form-group">
                                <div class="col-sm-5">
                                    <div class="form-group">
                                        <div class="col-sm-8" style="margin-bottom: 2px">
                                            <label>Customer Mobile No.</label>
                                            <input class="form-control input-sm" type="text" runat="server" id="cellNumberText" />
                                        </div>
                                        <div class="col-sm-4"></div>
                                        <div class="col-sm-12 myTextBoxMargin" style="background-color: #C6DCF5; border-right: 1px solid #ededed; padding-bottom: 2px; margin-left: 15px">
                                            <fieldset>
                                                <legend style="margin-bottom: 0px">Party Information</legend>
                                                <%--<div class="col-sm-12 myTextBoxMargin">
                                                    <div class="col-sm-2 myTextBoxMargin">
                                                        <label>Card Type:</label>
                                                    </div>
                                                    <div class="col-sm-4 myTextBoxMargin">
                                                        <asp:Label ID="cardTypeLabel" runat="server" Text=""></asp:Label>
                                                    </div>
                                                    <div class="col-sm-2 myTextBoxMargin">
                                                        <label>Dis(%):</label>
                                                    </div>
                                                    <div class="col-sm-4 myTextBoxMargin">
                                                        <asp:Label ID="disPerLabel" runat="server" Text=""></asp:Label>
                                                    </div>
                                                </div>--%>
                                                <div class="col-sm-12 myTextBoxMargin">
                                                    <div class="col-sm-2 myTextBoxMargin">
                                                        <label>ID:</label>
                                                    </div>
                                                    <div class="col-sm-4 myTextBoxMargin">
                                                        <asp:Label ID="partyIdLabel" runat="server" Text=""></asp:Label>
                                                    </div>
                                                    <div class="col-sm-2 myTextBoxMargin">
                                                        <label>Name:</label>
                                                    </div>
                                                    <div class="col-sm-4 myTextBoxMargin">
                                                        <asp:Label ID="nameLabel" runat="server" Text=""></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-12 myTextBoxMargin">
                                                    <div class="col-sm-2 myTextBoxMargin">
                                                        <label>Address:</label>
                                                    </div>
                                                    <div class="col-sm-10 myTextBoxMargin">
                                                        <asp:Label ID="addressLabel" runat="server" Text=""></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-12 myTextBoxMargin">
                                                    <div class="col-sm-2 myTextBoxMargin">
                                                        <label>Cell No:</label>
                                                    </div>
                                                    <div class="col-sm-10 myTextBoxMargin">
                                                        <asp:Label ID="cellNoLabel" runat="server" Text=""></asp:Label>
                                                    </div>
                                                </div>
                                            </fieldset>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-1"></div>
                                
                            </div>
                            <%--<div class="form-group">
                                <div class="col-sm-3">
                                    <div class="form-group">
                                        <div class="col-sm-8">
                                            <label>Customer Mobile No.</label>
                                            <input class="form-control input-sm" type="text" runat="server" id="cellNumberText" />
                                        </div>
                                        <div class="col-sm-4">
                                            <br />
                                            <button type="button" class="btn btn-info" runat="server" ID="customerSearch" OnServerClick="customerSearch_Click">
                                                <span class="glyphicon glyphicon-search"></span>Search
                                            </button>
                                        </div>
                                    </div>
                                    <label>Customer Name</label>
                                    <input class="form-control input-sm" type="text" runat="server" id="customerNameText" />
                                    <label>Address</label>
                                    <asp:TextBox ID="addressText" runat="server" TextMode="MultiLine" CssClass="form-control input-sm"></asp:TextBox>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group">

                                        <div class="col-sm-4">
                                            <label>Sales Type</label>
                                        </div>
                                        <div class="col-sm-6">
                                            <asp:DropDownList runat="server" ID="salesTypeDropdownList" CssClass="form-control input-sm" OnSelectedIndexChanged="salesTypeDropdownList_SelectedIndexChanged" AutoPostBack="True">

                                                <asp:ListItem Value="1">Cash</asp:ListItem>
                                                <asp:ListItem Value="2">Cash&amp;Cheque</asp:ListItem>
                                                <asp:ListItem Value="3">Cheque</asp:ListItem>
                                                <asp:ListItem Value="4">Credit</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-sm-4">
                                            <label>Card/Cheque No.</label>
                                        </div>
                                        <div class="col-sm-6">
                                            <input class="form-control input-sm" type="text" runat="server" id="chequeNo" readonly="readonly" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-sm-4">
                                            <label>Cheque Date</label>
                                        </div>
                                        <div class="col-sm-6">
                                            <input class="form-control input-sm" type="text" runat="server" id="chequeDate" readonly="readonly" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-sm-4">
                                            <label>Paid By Cheque(Taka)</label>
                                        </div>
                                        <div class="col-sm-6">
                                            <input class="form-control input-sm" type="text" runat="server" id="chequeAmount" readonly="readonly" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-sm-4">
                                            <label>Cheque Bank</label>
                                        </div>
                                        <div class="col-sm-6">
                                            <asp:DropDownList runat="server" ID="chequeBankDropDownList" CssClass="form-control input-sm" Enabled="False">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-sm-4">
                                            <label>Diposit Bank</label>
                                        </div>
                                        <div class="col-sm-6">
                                            <asp:DropDownList runat="server" ID="depositeBankDropDownList" CssClass="form-control input-sm" Enabled="False">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <label>Discount Against Invoice</label>
                                    <input class="form-control input-sm" type="text" runat="server" id="discountAgainstInvoiceTaka" text="%" placeholder="Discount Taka" onkeyup="setNetPayable();" />
                                    <label>Net payable</label>
                                    <input class="form-control input-sm" type="text" runat="server" id="netPayable" />
                                    <label>Cash Received</label>
                                    <input class="form-control input-sm" type="text" runat="server" id="cashRcvText" onkeyup="setDues()"/>
                                    <label>Total Dues</label>
                                    <input class="form-control input-sm" type="text" runat="server" id="duesText" />
                                </div>
                            </div>--%>
                            <div class="form-group">
                                <div class="col-sm-4">
                                    <asp:Button ID="saveButton" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="saveButton_Click" />
                                </div>
                                <div class="col-sm-8">
                                    <asp:Literal ID="messageLiteral" runat="server"></asp:Literal>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script src="../Scripts/MyScript/sales.js"></script>
</asp:Content>
