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
            var stockQty = parseFloat(document.getElementById("MainContent_stockText").value);
            var qty = parseFloat(document.getElementById("MainContent_qtyText").value);
            if (qty <= stockQty) {
                var price = document.getElementById("MainContent_priceText").value;
                var vat = document.getElementById("MainContent_vatText").value;
                var itemDis = document.getElementById("MainContent_itemDiscountPercentageText").value;
                var totalDiscount = 0;
                if ($("#MainContent_includeVatCheckBox").prop("checked") == false) {
                    if (itemDis != "" && itemDis > 0) {
                        totalDiscount = Math.round((qty * price) * (itemDis / 100));
                        document.getElementById("MainContent_itemDiscountTakaText").value = totalDiscount;
                    }
                    //var vatAmount = (qty * price) * (vat / 100);
                    document.getElementById("MainContent_itemTotalTakaText").value = (qty * price) - totalDiscount;
                } else {
                    if (itemDis != "" && itemDis > 0) {
                        totalDiscount = Math.round(((qty * price) + ((qty * price) * (vat / 100))) * (itemDis / 100));
                        document.getElementById("MainContent_itemDiscountTakaText").value = totalDiscount;
                    }
                    var vatAmount = (qty * price) * (vat / 100);
                    document.getElementById("MainContent_itemTotalTakaText").value = (qty * price) + vatAmount - totalDiscount;
                }
            }
                //} else if (qty== "") {
                //    alert("Insert valid Quantity!");
                //    $('#MainContent_qtyText').focus();
                //}
            else {
                alert("Stock Down!");
                document.getElementById("MainContent_qtyText").value = "";
                document.getElementById("MainContent_itemTotalTakaText").value = "";
                document.getElementById("MainContent_itemDiscountTakaText").value = "";
            }
        }
        function setDiscount() {
            //document.getElementById("MainContent_productCodeText").value = "";
            var stockQty = parseFloat(document.getElementById("MainContent_stockQtyText").value);
            var qty = parseFloat(document.getElementById("MainContent_qtyText").value);
            if (qty <= stockQty) {
                var price = document.getElementById("MainContent_priceText").value;
                document.getElementById("MainContent_itemTotalTakaText").value = (qty * price);
            }
            else {
                alert("Stock Down!");
                document.getElementById("MainContent_qtyText").value = "";
                document.getElementById("MainContent_itemTotalTakaText").value = "";
            }
            

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
            background-color: #fff;
            color: #000;
        }

        .form-group {
            margin-bottom: 2px !important;
        }

        .table > tbody > tr > td {
            padding: 5px !important;
        }

        .header-center {
            text-align: center;
        }

        .body {
            color: #c7c7c7 !important;
        }

        .panel-body {
            padding: 15px;
            padding-top: 0px;
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
                            <%--<asp:UpdatePanel runat="server">style="background-image: url('../image/expedia.jpg'); padding: 10px; color: #c7c7c7"
                                <ContentTemplate>--%>
                            <div class="form-group">
                                <div class=" col-sm-6">
                                    <div class=" col-sm-6">
                                        <label>Invoice Number</label>
                                        <asp:DropDownList runat="server" ID="invoiceNumberDropdownList" CssClass="form-control input-sm">
                                        </asp:DropDownList>
                                    </div>
                                    <div class=" col-sm-2">
                                        <input type="text" runat="server" id="itemUpdateText" class="form-control input-sm" style="display: none" />
                                    </div>
                                    <div class=" col-sm-4">
                                        <label>Date</label>
                                        <input type="text" runat="server" id="dateText" class="form-control input-sm" />
                                    </div>
                                    <div class=" col-sm-3">
                                        <label>Pr.Code</label>
                                        <input runat="server" type="text" class="form-control input-sm" id="productCodeText" />
                                    </div>
                                    <div class=" col-sm-6">
                                        <label>Product Name</label>
                                        <asp:DropDownList runat="server" CssClass="form-control input-sm" ID="productNameDropDownList" AutoPostBack="False"></asp:DropDownList>
                                    </div>
                                    <div class="col-sm-3">
                                        <label>Stock Qty</label>
                                        <input type="text" class="form-control input-sm" runat="server" id="stockQtyText" readonly="readonly"/>
                                    </div>
                                    <div class="col-sm-3">
                                        <label>Qty</label>
                                        <input type="text" class="form-control input-sm" runat="server" id="qtyText" onkeyup="setDiscount()" />
                                    </div>
                                    <div class="col-sm-3">
                                        <label>Price</label>
                                        <input type="text" class="form-control input-sm" runat="server" id="priceText" readonly="readonly"/>
                                    </div>
                                    <div class="col-sm-4">
                                        <label>Total</label>
                                        <input type="text" class="form-control input-sm" runat="server" id="itemTotalTakaText" style="padding-left: 2px; padding-right: 0px" readonly="readonly"/>
                                    </div>
                                    <div class="col-sm-2">
                                        <br />
                                        <asp:Button ID="addButton" runat="server" class="btn btn-success" Text="Add" OnClick="addButton_Click" />
                                    </div>

                                    <div class="col-lg-12">
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
                                                <asp:BoundField DataField="Qty" HeaderText="Qty" HeaderStyle-CssClass="header-center">
                                                    <ItemStyle Width="40px"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Price" HeaderText="Price" HeaderStyle-CssClass="header-center">
                                                    <ItemStyle Width="40px"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Total" HeaderText="Total Taka" HeaderStyle-CssClass="header-center">
                                                    <ItemStyle Width="40px"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="Action" HeaderStyle-CssClass="header-center">
                                                    <HeaderStyle Width="28%" />
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnEdit" runat="server" Width="50" CssClass="btn btn-warning btn-sm" Text="Edit" CommandName="EditButton"
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
                                    <div class="col-sm-12">
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="partIdTextBox" runat="server" Visible="False"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-2 myTextBoxMargin">
                                            <label>Quantity</label>
                                        </div>
                                        <div class="col-sm-2">
                                            <input class="form-control input-sm" type="text" runat="server" id="totalQtyText" readonly="readonly"/>
                                        </div>

                                        <div class="col-sm-2 myTextBoxMargin">
                                            <label>Total Taka</label>
                                        </div>
                                        <div class="col-sm-2">
                                            <input class="form-control input-sm" type="text" runat="server" id="allItemTotalTakaText" readonly="readonly"/>
                                        </div>

                                    </div>
                                </div>
                                <div class=" col-sm-6">
                                    <div class="col-sm-6">
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
                                    <div class="col-sm-6">
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
                                            <input class="form-control input-sm" type="text" runat="server" id="netPaybleText" readonly="readonly"/>
                                        </div>
                                        <div class="col-sm-12">
                                            <label>Cash Received</label>
                                            <input class="form-control input-sm" type="text" runat="server" id="cashReceivedText" />
                                        </div>
                                       <%-- <div class="col-sm-12">
                                            <label>Exchange Taka</label>
                                            <input class="form-control input-sm" type="text" runat="server" id="exchangeTakaText" />
                                        </div>--%>
                                        <div class="col-sm-12">
                                            <label>Dues Amount</label>
                                            <input class="form-control input-sm" type="text" runat="server" id="duesAmountText" readonly="readonly"/>
                                        </div>
                                    </div>
                                    <div class="col-sm-12">
                                        <div class="col-sm-8" style="margin-bottom: 2px">
                                            <label>Customer Mobile No.</label>
                                        </div>
                                        <div class="col-sm-4"></div>
                                        <div class="col-sm-12 myTextBoxMargin" style="background-color: #C6DCF5; border-right: 1px solid #ededed; padding-bottom: 2px; margin-left: 15px">
                                            <fieldset>
                                                <legend style="margin-bottom: 0px">Party Information</legend>
                                                <div class="col-sm-2 myTextBoxMargin">
                                                    <label>Mobile No.</label>
                                                </div>
                                                <div class="col-sm-4 myTextBoxMargin">
                                                    <input class="form-control input-sm" type="text" runat="server" id="cellNumberText" />
                                                </div>
                                                <div class="col-sm-2 myTextBoxMargin">
                                                    <label>Name:</label>
                                                </div>
                                                <div class="col-sm-4 myTextBoxMargin">
                                                    <input class="form-control input-sm" type="text" runat="server" id="nameText" />
                                                </div>
                                                <div class="col-sm-2 myTextBoxMargin">
                                                    <label>Address:</label>
                                                </div>
                                                <div class="col-sm-4 myTextBoxMargin">
                                                    <input class="form-control input-sm" type="text" runat="server" id="addressText" />
                                                </div>

                                                <div class="col-sm-2 myTextBoxMargin">
                                                    <label>Cont.Person:</label>
                                                </div>
                                                <div class="col-sm-4 myTextBoxMargin">
                                                    <input class="form-control input-sm" type="text" runat="server" id="contactPersonText" />
                                                </div>

                                            </fieldset>
                                        </div>
                                    </div>
                                </div>

                            </div>


                            <%-- </ContentTemplate>
                            </asp:UpdatePanel>--%>
                            <div class="form-group">
                                <div class="col-sm-4">
                                    <asp:Button ID="saveButton" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="saveButton_Click" />
                                </div>
                                <div class="col-sm-8">
                                    <asp:Label ID="messageLabel" runat="server" Text=""></asp:Label>
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
