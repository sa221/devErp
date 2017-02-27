<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PurchaseUI.aspx.cs" Inherits="DevERP.UI.PurchaseUI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
     <style>
        .error {
            color: red;
            font-weight: bold;
        }
        #purchaseTable tbody td {
            padding: 0px;
        }
        .form-horizontal .form-group {
            margin: 0px !important;
            padding: 0px !important;
        } 
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <div class="col-sm-10 col-sm-offset-1" style="margin-top: -2%;">
            <div class="panel panel-primary" style="margin-top: 10px;">
                <div class="panel-heading">Purchase</div>
                <div class="panel-body">
                    <div class="container-fluid">
                        <div class="form-group">
                            <div class="col-sm-12">
                                <div id="messageLabel"></div>
                            </div>
                        </div>
                        
                        <div class="form-group">
                            <div class="col-sm-4">
                                <asp:Label ID="purchaseIdLabel" runat="server"><strong>Purchase ID</strong></asp:Label>
                                <asp:DropDownList ID="puchaseinvoiceNoDropDownList" CssClass="form-control input-sm searchableDropdownHeigh" runat="server"></asp:DropDownList>
                                <span id="validatePurchaseDropDown" class="error"></span>
                            </div>
                            <div class="col-sm-4"></div>
                            <div class="col-sm-4">
                                <asp:Label ID="dateLabel" CssClass="control-label" runat="server"><strong>Purchase Date<span style="color:red"> *</span></strong></asp:Label>
                                <div class='input-group date' id='purchasedate'>
                                    <asp:TextBox  ID="purchasedateTextBox" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                    <span class="input-group-addon" id="inputFrom">
                                        <span class="glyphicon glyphicon-calendar"></span>
                                    </span>
                                </div>
                                <span class="error" id="fromTextBoxValidation"></span>
                            </div>
                            </div>
                        <div class="form-group">
                            <div class="col-sm-4">
                                <asp:Label ID="supplierNameLabel" runat="server"><strong>Supplier Name</strong><span style="color:red"> *</span></asp:Label>
                                <asp:DropDownList ID="supplierNameDropDownList" CssClass="form-control input-sm" runat="server"></asp:DropDownList>
                                <span id="validateSupplierNameDropDown" class="error"></span>
                            </div>
                            <div class="col-sm-4">
                                <asp:Label ID="supplierIdLabel" runat="server"><strong>Supplier ID</strong><span style="color:red"> *</span></asp:Label>
                                <asp:DropDownList ID="supplierIdDropDownList" CssClass="form-control input-sm" runat="server"></asp:DropDownList>
                                <span id="validateSupplierIdDropDown" class="error"></span>
                            </div>
                            <div class="col-sm-4">
                                <asp:Label ID="chalanNoLabel" CssClass="control-label" runat="server"><strong>Chalan No.</strong><span style="color:red"> *</span></asp:Label>
                                <asp:TextBox  ID="chalanNoTextBox" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                <span id="validateChalanNo" class="error"></span>
                            </div>
                        </div>
                        <div class="form-group col-sm-12">
                            <div class="col-sm-6">
                                <label id=""><strong>Note(Remarks):</strong></label>
                                <textarea class="form-control input-sm" rows="2" name="noteTextArea" id="noteTextArea"></textarea>
                            </div>
                            <div class="col-sm-2"></div>
                            <div class="col-sm-2">
                                  <label id=""><strong>Net Total:</strong></label>
                                <asp:TextBox id="purchaseNetTotalTextBox" type="text" class="form-control input-sm" value="0" readonly="True" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-sm-2"></div>
                        </div>
                        <div class="form-group">
                            <div class="col-sm-2">
                                <asp:Label ID="partsNameLabel" runat="server"><strong>Parts Name</strong><span style="color:red"> *</span></asp:Label>
                                <asp:DropDownList ID="partsNameDropDownList" CssClass="form-control input-sm searchableDropdownHeigh" runat="server"></asp:DropDownList>                                 
                                 <input type="text" runat="server" style="display: none" class="form-control input-sm" name="hiddenFeildPrartsId" id="hiddenFeildPrartsId" />
                                <span id="validatePartsNameDropDown" class="error"></span>
                            </div>
                            <div class="col-sm-2">
                                <asp:Label ID="quantityLabel" CssClass="control-label" runat="server"><strong>Qty</strong><span style="color:red"> *</span></asp:Label>
                                <asp:TextBox ID="quantityTextBox" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                <span id="validateQuantity" class="error"></span>
                            </div>
                            <div class="col-sm-2">
                                <asp:Label ID="unitLabel" CssClass="control-label"  runat="server"><strong>Unit</strong><span style="color:red"> *</span></asp:Label>
                                <asp:TextBox ID="unitTextBox" ReadOnly="True" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                <span id="validateUnit" class="error"></span>
                            </div>
                            <div class="col-sm-2">
                                <asp:Label ID="rateLabel" CssClass="control-label" runat="server"><strong>Rate</strong><span style="color:red"> *</span></asp:Label>
                                <asp:TextBox ID="rateTextBox" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                <span id="validateRate" class="error"></span>
                            </div>
                            <div class="col-sm-2">
                                <asp:Label ID="toatalLabel"  CssClass="control-label" runat="server"><strong>Total</strong><span style="color:red"> *</span></asp:Label>
                                <asp:TextBox ID="totalTextBox" ReadOnly="True"  CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                <span id="validateTotal" class="error"></span>
                            </div>
                            <div class="col-sm-1">
                                <br/>                               
                                <button type="button" id="addButton" class="btn btn-success btn-sm"><span class="glyphicon glyphicon-saved">Add</span></button>&nbsp;                                                                                                
                            </div>
                            <div class="col-sm-1">
                                <br/>                               
                                 <button type="button" id="clearButton" class="btn btn-primary btn-sm"><span class="glyphicon glyphicon-remove-circle">Clear</span></button>&nbsp;                                
                            </div>

                        </div>
                        
                        <div class="form-group">
                            <div class="col-sm-12">
                                <div style="border: 1px solid black">
                                    <table class="table table-bordered table-responsive table-hover" id="purchaseTable">
                                        <thead>
                                            <tr>
                                                <th>S.No.</th>
                                                <th style="display: none">PID</th>
                                                <th style="display: none">PCode</th>
                                                <th>Parts Name</th>
                                                <th>Quantity</th>
                                                <th>Unit</th>
                                                <th>Rate</th>
                                                <th>Total</th>
                                                <th>Action</th>
                                            </tr>
                                        </thead>
                                        <tbody id="purchaseBody">
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    <script src="../Scripts/SearchableDropdownMy.js"></script>
    <script src="../Scripts/jquery.searchabledropdown-1.0.8.src.js"></script>
    <script src="../Scripts/bootstrap-datepicker.min.js"></script>
    <script src="../Content/media/js/jquery.dataTables.min.js"></script>   
    
        <script src="../Scripts/MyScript/Purchase.js"></script>  
  <script>
      $(function () {
          $("#bodyContentPlaceHolder_puchaseinvoiceNoDropDownList").searchable();
          $("#bodyContentPlaceHolder_partsNameDropDownList").searchable({
              exactMatch: false,
              wildcards: true,
              ignoreCase: true,
              latency: 200,
              warnMultiMatch: 'top {0} matches ...',
              warnNoMatch: 'no matches ...'
          });
      });
    </script>
</asp:Content>
