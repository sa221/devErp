<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SupplierLedger.aspx.cs" Inherits="DevERP.ReportUI.SupplierLedger" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="../Scripts/SearchableDropdownMy.js"></script>

    <script src="../Scripts/jquery.searchabledropdown-1.0.8.src.js"></script>
    <script src="../Scripts/bootstrap-datepicker.js"></script>
    <script src="../Content/media/js/jquery.dataTables.js"></script>
    <script src="../Content/media/js/dataTablesButtonsJs.js"></script>
     <script src="../Content/media/js/DataTabelsForPrintButton.js"></script>

     <script src="../Scripts/MyScript/supplierLedger.js"></script>
    <script>
        $(function () {
            $("#supplierLedgerRptButton").click(function () {
                var fromDate = $("#MainContent_fromDateTextBox").val();
                var toDate = $("#MainContent_toDateTextBox").val();
                var supplierId = $("#MainContent_supplierDropdownlist").val();
                var supplierName = $("#MainContent_supplierDropdownlist option:selected").text();
                var myMessage = '<div style="font-weight:bold;"><h1 style="text-align:center; margin-bottom:0px; padding-bottom: 0px;">' +
                    'M/S. ISLAM TEXTILE</h1><h3 style="text-align:center;margin-top:0;">Supplier Ledger Report (Date Wise) </h3><br/>'+
                    '<h5 style="text-align:center;margin-top:-20px;"> Supplier : ' + supplierName + '</h5><br/>' +
                    '<h5 style="text-align:center;margin-top:-20px;"> From : ' + fromDate + ' To : ' + toDate + '</h5></div>';

                //M/S. ISLAM TEXTILE
                //Managing Director: Haji Md. Nasir Uddin
                //Office: South Birampur, Madhabdi, Narsingdi
                //Factory: Unit-1, South Birampur, Madhabdi, Narsingdi.
                //  Unit-2, Khilgaw, Madhabdi, Narsingdi.
                //    Mobile: 01611-607158, 01711-607158  Email:islamtextile@yahoo.com
                $.ajax({
                    type: 'Post',
                    contentType: "application/json; charset=utf-8",
                    url: "SupplierLedger.aspx/GetSupplierLedgerReport",
                    dataType: 'json',
                    data: JSON.stringify({ fromDate: fromDate, toDate: toDate, supplierId: supplierId }),
                    async: false,
                    success: function (data) {
                        var oTable = $('#supplierLedgerRptTable').dataTable();
                        oTable.fnDestroy();
                        $('#supplierLedgerRptTableBody').empty();
                        for (var i = 0; i < data.d.length; i++) {
                            if (data.d[i].FHeadsName == null || data.d[i].FHeadsName == "0") {
                                data.d[i].FHeadsName = "";
                            }
                            if (data.d[i].FDescription == null || data.d[i].FDescription == "0") {
                                data.d[i].FDescription = "";
                            }
                            if (data.d[i].FDebit == null || data.d[i].FDebit == "0") {
                                data.d[i].FDebit = "";
                            }
                            if (data.d[i].FCredit == null || data.d[i].FCredit == "0") {
                                data.d[i].FCredit = "";
                            }
                            $('#supplierLedgerRptTableBody').append(
                                "<tr>" +
                                "<td style='text-align: center;'>" + (parseInt(i) + 1) + "</td>" +
                                "<td style='text-align: center;'>" + data.d[i].FDatDate + "</td>" +
                                "<td style='text-align: center;'>" + data.d[i].FHeadsName + "</td>" +
                                "<td style='text-align: center;'>" + data.d[i].FVoucherNo + "</td>" +
                                "<td style='text-align: center;'>" + data.d[i].FDescription + "</td>" +
                                "<td style='text-align: center;'>" + data.d[i].FDebit + "</td>" +
                                "<td style='text-align: center;'>" + data.d[i].FCredit + "</td>" +
                                "<td style='text-align: center;'>" + data.d[i].FBalance + "</td>" +
                                +"</tr>");
                        }


                        $("#supplierLedgerRptTable").dataTable({
                            "scrollY": "300px",
                            "scrollCollapse": true,
                            "paging": false,
                            dom: 'Bfrtip',
                            buttons: [
                                {
                                    extend: 'print',
                                    message: myMessage,
                                    customize: function (win) {
                                        $(win.document.body)
                                            .css('font-size', '10pt')
                                            .prepend(
                                               '<img src="http://localhost:3757/images/Logo-SB.png" style="opacity: 0.1;height: 200px; width: auto; position:absolute; top: 40%; left:20%;filter: grayscale(100%);" />'
                                            );
                                        $(win.document.body).find('table')
                                            .addClass('compact')
                                            .css('font-size', 'inherit');
                                    }
                                }
                            ],
                        });
                    },
                    error: function () {
                        alert("Error");
                    }
                });
            });
        })
    </script>
    <script>
        $(function () {
            $("#MainContent_supplierDropdownlist").searchable();
        });
    </script>
    <link href="../Content/bootstrap-datepicker.css" rel="stylesheet" />
    <link href="../Content/media/css/jquery.dataTables.min.css" rel="stylesheet" />
    <link href="../Content/media/css/dataTablesButtonsCSS.css" rel="stylesheet" />
    <style>
        .error {
            color: red;
            font-weight: bold;
            font-style: italic;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <div class="col-sm-10 col-sm-offset-1">
            <div class="panel panel-primary">
                <div class="panel-heading">Supplier Ledger Reports</div>
                <div class="panel-body">
                    <div class="form-group">
                        <div class="col-sm-12">
                            <div class=" col-sm-2"></div>
                            <div class=" col-sm-4">
                                <label for="">Supplier Name</label>
                                <asp:DropDownList ID="supplierDropdownlist" CssClass="form-control input-sm searchableDropdownHeigh" runat="server"></asp:DropDownList>
                                <span id="supplierDropdownlistValidate" class="error"></span>
                            </div>
                            <div class=" col-sm-2">
                                <label for="">From<span style="color: red"> *</span></label>
                                <div class='input-group date' id='datetimepicker1'>
                                    <%--<asp:TextBox ID="fromDateTextBox" CssClass="form-control input-sm" runat="server"></asp:TextBox>--%>
                                    <input runat="server" type="text" class="form-control input-sm" id="fromDateTextBox" placeholder="DD/MM/YYYY" />
                                    <span class="input-group-addon">
                                        <span class="glyphicon glyphicon-calendar"></span>
                                    </span>
                                </div>
                                <span id="fromDateTextBoxValidate" class="error"></span>
                            </div>
                            <div class=" col-sm-2">
                                <label for="">To<span style="color: red"> *</span></label>
                                <div class='input-group date' id='datetimepicker2'>
                                    <%--<asp:TextBox ID="toDateTextBox" CssClass="form-control input-sm" runat="server"></asp:TextBox>--%>
                                    <input runat="server" type="text" class="form-control input-sm" id="toDateTextBox" placeholder="DD/MM/YYYY" />
                                    <span class="input-group-addon">
                                        <span class="glyphicon glyphicon-calendar"></span>
                                    </span>
                                </div>
                                <span id="toDateTextBoxValidate" class="error"></span>
                            </div>
                            <div class=" col-sm-2">
                                <br />
                                <%--<button  id="ledgerRptButton" type="button" class="btn btn-success btn-sm">Ledger Report</button>   --%>                             
                                 <button id="supplierLedgerRptButton" type="button" class="btn btn-primary btn-sm">Show Supplier Ledger</button>
                            </div>
                        </div>
                    </div><%--<span><img src="../images/Logo-SB.png" style="position: absolute; height: 200px; width: auto; top: 35%; -ms-opacity: 0.5; opacity: 0.5;"/></span>--%>
                    <div class="form-group">
                        <div class=" col-sm-12 ">                           
                            <div style="padding: 2px;" id="itemWiseReporDiv">                                                                                                         
                                <table id="supplierLedgerRptTable" class="table table-bordered table-responsive">
                                    <thead>
                                        <tr>
                                            <td>SL#</td>
                                            <td>Date</td>
                                            <td>Type</td>
                                            <td>Voucher No</td>
                                            <td>Description</td>
                                            <td>Debit</td>
                                            <td>Credit</td>
                                            <td>Balance</td>
                                        </tr>
                                    </thead>
                                    <tbody id="supplierLedgerRptTableBody">
                                    </tbody>

                                </table>
                            </div>
                        </div>
                    </div>
                </div>
                <div></div>
            </div>
        </div>
</asp:Content>
