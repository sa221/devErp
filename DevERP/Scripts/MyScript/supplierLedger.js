$(function () {
    $("#MainContent_fromDateTextBox").datepicker({
        autoclose: true,
        todayHighlight: true,
        format: 'dd-mm-yyyy',
    });
    $("#MainContent_toDateTextBox").datepicker({
        autoclose: true,
        todayHighlight: true,
        format: 'dd-mm-yyyy',
    });
    $("#supplierLedgerRptTable").dataTable({
        "scrollY": "300px",
        "scrollCollapse": true,
        "paging": false
    });

});