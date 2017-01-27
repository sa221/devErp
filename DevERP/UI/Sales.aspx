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
        .header-center{
        text-align:center;
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
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <div class="form-group">
                                        <div class=" col-sm-3">
                                            <label>Invoice Number</label>
                                            <asp:DropDownList runat="server" ID="invoiceNumberDropdownList" CssClass="form-control input-sm">
                                            </asp:DropDownList>
                                        </div>
                                        <div class=" col-sm-3">
                                        </div>
                                        <div class=" col-sm-3">
                                            <input type="text" runat="server" id="itemUpdateText" class="form-control input-sm" style="display: block" />
                                        </div>
                                        <div class=" col-sm-3">
                                            <label>Date</label>
                                            <input type="text" runat="server" id="dateText" class="form-control input-sm" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class=" col-sm-2">
                                            <label>Product Code</label>
                                            <input runat="server" type="text" class="form-control input-sm" id="productCodeText" onchange="setProductName()" />
                                        </div>
                                        <div class=" col-sm-2">
                                            <label>Product Name</label>
                                            <asp:DropDownList runat="server" CssClass="form-control input-sm" ID="productNameDropDownList" AutoPostBack="True" onclick="setProductId()" OnSelectedIndexChanged="productNameDropDownList_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                        <div class="col-sm-2">
                                            <label>Qty</label>
                                            <input type="text" class="form-control input-sm" runat="server" id="qtyText" onkeyup="setDiscount()" />
                                        </div>
                                        <div class="col-sm-2">
                                            <label>Price</label>
                                            <input type="text" class="form-control input-sm" runat="server" id="priceText" />
                                        </div>
                                        <div class="col-sm-2">
                                            <label>Total</label>
                                            <input type="text" class="form-control input-sm" runat="server" id="itemTotalTakaText" style="padding-left: 0px; padding-right: 0px" />
                                        </div>
                                        <div class="col-sm-2 myTextBoxMar">
                                            <asp:Button ID="addButton" runat="server" class="btn btn-success" Text="Add" OnClick="addButton_Click" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-sm-12">
                                            <div class="col-lg-12">
                                                <%--OnSelectedIndexChanged="salesGridView_SelectedIndexChanged" OnRowDataBound="ItemOnRowDataBound"--%>
                                                <asp:GridView ID="salesGridView" runat="server" ShowHeaderWhenEmpty="True"
                                                     OnRowCommand="salesGridView_RowCommand"
                                                    CssClass="table-hover table-striped table table-bordered" GridLines="None"
                                                    AutoGenerateColumns="False" >
                                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                    <RowStyle HorizontalAlign="Center" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sr#" >
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
                                                        <asp:BoundField DataField="ProductName" HeaderText="Product Name" HeaderStyle-CssClass="header-center"/>
                                                        <asp:BoundField DataField="Qty" HeaderText="Qty" HeaderStyle-CssClass="header-center"/>
                                                        <asp:BoundField DataField="Price" HeaderText="Price" HeaderStyle-CssClass="header-center"/>
                                                        <asp:BoundField DataField="Total" HeaderText="Total Taka" HeaderStyle-CssClass="header-center"/>
                                                        <asp:TemplateField HeaderText="Action" HeaderStyle-CssClass="header-center">
                                                            <HeaderStyle Width="14%" />
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
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-sm-4"></div>
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
                                    <div class="form-group">
                                        <div class="col-sm-3">
                                            <label>Discount Card Number</label>
                                            <input class="form-control input-sm" type="password" runat="server" id="discountCardNumberText" />
                                        </div>
                                        <div class="col-sm-2">
                                            <label>D. Card Type</label>
                                            <input class="form-control input-sm" type="text" runat="server" id="discountCardTypeText" />
                                        </div>
                                        <div class="col-sm-2">
                                            <label>Dis(%)</label>
                                            <input class="form-control input-sm" type="text" runat="server" id="specialDiscountText" />
                                        </div>
                                        <div class="col-sm-2">
                                        </div>
                                        <div class="col-sm-3">
                                            <label>Total After Discount</label>
                                            <input class="form-control input-sm" type="text" runat="server" id="totalAfterDiscount" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-sm-3">
                                            <label>Customer Id</label>
                                            <input class="form-control input-sm" type="text" runat="server" id="customerIdText" />
                                        </div>
                                        <div class="col-sm-2">
                                            <label>Customer Name</label>
                                            <input class="form-control input-sm" type="text" runat="server" id="customerNameText" />
                                        </div>
                                        <div class="col-sm-2">
                                            <label>Cell No.</label>
                                            <input class="form-control input-sm" type="text" runat="server" id="cellNumberText" />
                                        </div>
                                        <div class="col-sm-2">
                                        </div>
                                        <div class="col-sm-3">
                                            <label>Discount Against Invoice</label>
                                            <div class="form-group">
                                                <div class="col-sm-6">
                                                    <input class="form-control input-sm" type="text" runat="server" id="discountAgainstInvoicePercentage" text="%" placeholder="IV Dis %" />
                                                </div>
                                                <div class="col-sm-6">
                                                    <input class="form-control input-sm" type="text" runat="server" id="discountAgainstInvoiceTaka" text="Tk" placeholder="IV Dis Tk" />
                                                </div>
                                            </div>
                                            <%--<div class="col-sm-1">
                                    <input class="form-control" type="text" runat="server" id="daiText" text="%" />
                                </div>
                                <div class="col-sm-1">
                                    <input class="form-control" type="text" runat="server" id="frfText" text="Tk" />
                                </div>--%>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-sm-7">
                                            <label>Address</label>
                                            <input class="form-control input-sm" type="text" runat="server" id="addressText" />
                                        </div>
                                        <div class="col-sm-2">
                                        </div>
                                        <div class="col-sm-3">
                                            <label>Net payable</label>
                                            <input class="form-control input-sm" type="text" runat="server" id="netPaybleText" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-sm-3">
                                            <label>Sales Type</label>
                                            <asp:DropDownList runat="server" ID="salesTypeDropdownList" CssClass="form-control input-sm">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-6"></div>
                                        <div class="col-sm-3">
                                            <label>Cash Received</label>
                                            <input class="form-control input-sm" type="text" runat="server" id="cashReceivedText" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-sm-3">
                                            <label>Card/Cheque No.</label>
                                            <input class="form-control input-sm" type="text" runat="server" id="cardChequeNumberText" />
                                        </div>
                                        <div class="col-sm-2">
                                            <label>Card/Cheque Date</label>
                                            <input class="form-control input-sm" type="text" runat="server" id="cardChequeDateText" />
                                        </div>
                                        <div class="col-sm-2">
                                            <label>Paid By Card(Taka)</label>
                                            <input class="form-control input-sm" type="text" runat="server" id="paidByCardTakaText" />
                                        </div>
                                        <div class="col-sm-2">
                                        </div>
                                        <div class="col-sm-3">
                                            <label>Exchange Taka</label>
                                            <input class="form-control input-sm" type="text" runat="server" id="exchangeTakaText" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-sm-3">
                                            <label>Card/Cheque Bank</label>
                                            <asp:DropDownList runat="server" ID="cardChequeBankDropDownList" CssClass="form-control input-sm">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-3">
                                            <label>Diposit Bank</label>
                                            <asp:DropDownList runat="server" ID="dipositBankDropDownList" CssClass="form-control input-sm">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-3"></div>
                                        <div class="col-sm-3">
                                            <label>Collected Taka</label>
                                            <input class="form-control input-sm" type="text" runat="server" id="collectedTakaText" />
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
