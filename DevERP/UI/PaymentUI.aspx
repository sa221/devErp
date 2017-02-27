<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PaymentUI.aspx.cs" Inherits="DevERP.UI.PaymentUI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="col-sm-10 col-sm-offset-1">
            <div class="panel panel-primary" style="margin-top: 10px;">
                <div class="panel-heading" style="margin-top: 0%;">Payment</div>
                <div class="panel-body">
                    <div class="container-fluid">
                        <div class="form-group">
                            <div class="col-sm-12">
                                <asp:Label ID="messageLabel" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-sm-4">
                                <label>Invoice No.</label>
                                <asp:DropDownList ID="paymentInvoiceNoDropDownList" CssClass="form-control input-sm" runat="server">
                                    <Items>
                                        <asp:ListItem Text="Select" Value="0" />

                                    </Items>

                                </asp:DropDownList>
                                <label id="validatePaymentDropDownList" class="error"></label>
                            </div>
                            <div class="col-sm-4">
                                <label>Pay.Date</label>
                                <div class="input-group">
                                    <asp:TextBox ID="paymentDateTextBox" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                    <span class="input-group-addon">
                                        <span class="glyphicon glyphicon-calendar"></span>
                                    </span>
                                </div>
                                <label class="error clearfix" id="validatePayDateTextBox" style="position: absolute;"></label>
                            </div>
                            <div class="col-sm-4">
                                <label>Supplier Name</label>
                                <asp:DropDownList ID="supplierNameDropdownList" CssClass="form-control input-sm" runat="server">
                                    <Items>
                                        <asp:ListItem Text="Select" Value="0" />
                                    </Items>
                                </asp:DropDownList>
                                <label id="validateSupplierNameDropDownList" class="error"></label>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-sm-6">
                                <label>Remarks</label>
                                <textarea class="form-control input-sm" rows="2" name="remarksTextArea" id="remarksTextArea"></textarea>
                            </div>

                        </div>
                        <div class="form-group">
                            <div class="col-sm-4">
                                <label>Pay.Type</label>
                                <select class="form-control input-sm" id="payTypeDropdownlist">
                                    <option value="0">Select</option>
                                    <option value="Cash">Cash</option>
                                    <option value="Bank">Bank</option>
                                </select>
                                <label id="validatePayTypeDropDownList" class="error"></label>
                            </div>
                            <div class="col-sm-4">
                                <label>Bank Name</label>
                                <asp:DropDownList ID="bankNameDropDownList" CssClass="form-control input-sm" runat="server">
                                    <Items>
                                        <asp:ListItem Text="Select" Value="0" />
                                    </Items>
                                </asp:DropDownList>
                                <label id="validateBankNameDropDownList" class="error"></label>
                            </div>
                            <div class="col-sm-4">
                                <label>Account No</label>
                                <%--<asp:DropDownList ID="accountNoDropDownList" CssClass="form-control input-sm" runat="server"/>                                
                                <span id="validateAccountNoDropDownList" class="error"></span>--%>
                                <asp:TextBox ID="accountNoTextBox" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                <label id="validateAccountNoTextBox" class="error"></label>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-sm-4">
                                <label>Cheque No</label>
                                <asp:TextBox ID="chequeNoTextBox" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                <label id="validateChequeNoTextBox" class="error"></label>
                            </div>
                            <div class="col-sm-4">
                                <label>Cheque Date</label>
                                <div class="input-group">
                                    <asp:TextBox ID="chequeDateTextBox" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                    <span class="input-group-addon">
                                        <span class="glyphicon glyphicon-calendar"></span>
                                    </span>
                                </div>
                                <label class="error clearfix" id="validateChequeDateTextBox" style="position: absolute;"></label>
                            </div>
                            <div class="col-sm-4">
                                <label>Pay. Ammount</label>
                                <asp:TextBox ID="payAmountTextBox" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                <label id="validatePayAmountTextBox" class="error"></label>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-sm-4">
                                <button id="btnSave" type="button" class="btn btn-success"><span class="glyphicon glyphicon-saved">Save</span></button>
                                <button id="btnClear" type="button" class="btn btn-danger"><span class="glyphicon glyphicon-remove-circle">Clear</span></button>
                                <button id="btnReport" type="button" class="btn btn-info">Report</button>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>
        <script src="../Scripts/MyScript/Payment.js"></script>
        <style>
            .form-horizontal .form-group {
                margin: 0px !important;
                padding: 0px !important;
            }            
        </style>
</asp:Content>
