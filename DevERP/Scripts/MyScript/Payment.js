
var paydate = $("#MainContent_paymentDateTextBox");
var payDateValidate = $("#validatePayDateTextBox");

var supplier = $("#MainContent_supplierNameDropdownList");
var supplierValidate = $("#validateSupplierNameDropDownList");

var remark = $("#remarksTextArea");

var payType = $("#payTypeDropdownlist");
var payTypeValidate = $("#validatePayTypeDropDownList");

var bank = $("#MainContent_bankNameDropDownList");
var bankValidate = $("#validateBankNameDropDownList");

var account = $("#MainContent_accountNoDropDownList");
var accountValidate = $("#validateAccountNoDropDownList");
var accNoTxt = $("#MainContent_accountNoTextBox");
var accNoTxtValidate = $("#validateAccountNoTextBox");

var chequeNo = $("#MainContent_chequeNoTextBox");
var chequeNoValidate = $("#validateChequeNoTextBox");

var cheqDate = $("#MainContent_chequeDateTextBox");
var cheqDateValiadte = $("#validateChequeDateTextBox");

var payAmount = $("#MainContent_payAmountTextBox");
var payAmountValidate = $("#validatePayAmountTextBox");
$("#payTypeDropdownlist").change(function () {
    if ($(this).val() != "0") {
        var type = $(this).val();
        if (type == "Cash") {

            bank.prop('disabled', true);
            account.prop('disabled', true);
            accNoTxt.prop('disabled', true);
            chequeNo.prop('disabled', true);
            cheqDate.prop('disabled', true);
            $("#MainContent_bankNameDropDownList").val("");
            $("#MainContent_accountNoTextBox").val("");
            $("#MainContent_chequeNoTextBox").val("");
            $("#MainContent_chequeDateTextBox").val("");
            bankValidate.html("");
            accNoTxtValidate.html("");
            chequeNoValidate.html("");
            cheqDateValiadte.html("");
            payAmountValidate.html("");
            

        } else if (type == "Bank") {
            bank.prop('disabled', false);
            account.prop('disabled', false);
            accNoTxt.prop('disabled', false);
            chequeNo.prop('disabled', false);
            cheqDate.prop('disabled', false);
            bankValidate.html("");
            accNoTxtValidate.html("");
            chequeNoValidate.html("");
            cheqDateValiadte.html("");
            payAmountValidate.html("");
            loadAccHeadByType(type);
        }
       
    } else if ($(this).val() == "0") {
        $("#MainContent_bankNameDropDownList").empty();
        $("#MainContent_bankNameDropDownList").append('<option value="0">Select</option>');
        bank.prop('disabled', false);
        account.prop('disabled', false);
        accNoTxt.prop('disabled', false);
        chequeNo.prop('disabled', false);
        cheqDate.prop('disabled', false);
        bankValidate.html("");
        accNoTxtValidate.html("");
        chequeNoValidate.html("");
        cheqDateValiadte.html("");
        payAmountValidate.html("");
    }
   
});
function loadAccHeadByType(type) {
    $.ajax({
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        url: "DailyTransactions.aspx/LoadAccHeadByType",
        dataType: 'json',
        data: JSON.stringify({ type: type }),
        async: false,
        success: function (response) {
            var x = 1;
            var p = x;
            $("#MainContent_bankNameDropDownList").empty();
            $.each(response.d, function (i, value) {
                $("#MainContent_bankNameDropDownList").append('<option value="' + value.NumHeadID + '">' + value.VarHeadName + '</option>');
            });

        },
        error: function () {

        }
    });
}

//function toDateFromJson(src) {
//    var date = new Date(parseInt(src.substr(6)));
//    var displayDate = jQuery.datepicker.formatDate("dd/mm/yy", date);
//    return displayDate;
//}


$(function () {
    $("#MainContent_paymentDateTextBox").datepicker({
        autoclose: true,
        todayHighlight: true

    });
    $("#MainContent_chequeDateTextBox").datepicker({
        autoclose: true,
        todayHighlight: true,

    });
    $("#MainContent_paymentInvoiceNoDropDownList").change(function () {
        var payId = $("#MainContent_paymentInvoiceNoDropDownList").val();
        if (payId!= "0") {
            loadPaymentData(payId);
        } else {
            ClearData();
        }
    });
    function loadPaymentData(payId) {
        $.ajax({
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            url: "PaymentUI.aspx/LoadPaymentData",
            dataType: 'json',
            data: JSON.stringify({ payId: payId }),
            async: false,
            success: function (data) {
                
                //var dateString = data.d.PayDate.substr(6);
                //var currentTime = new Date(parseInt(dateString));
                //var month = currentTime.getMonth() + 1;
                //var day = currentTime.getDate();
                //var year = currentTime.getFullYear();
                //var date =  month + "/"+day + "/" + year;
               // var pDate = toDateFromJson(data.d.PayDate);
                paydate.val(data.d.DatDate);
                supplier.val(data.d.SupplierId);
                //$("#MainContent_supplierNameDropdownList").clear();
                //supplier.append('<option value="'+data.d.SupplierId+'">'+data.d.SupplierId+'</option>');
                //supplier.val(data.d.SupplierId);
                remark.val(data.d.Remarks);
                var pType = data.d.PayType;
                if (pType == "Cash") {
                    bank.prop('disabled', true);
                    account.prop('disabled', true);
                    accNoTxt.prop('disabled', true);
                    chequeNo.prop('disabled', true);
                    cheqDate.prop('disabled', true);
                    $("#MainContent_bankNameDropDownList").val("");
                    $("#MainContent_accountNoTextBox").val("");
                    $("#MainContent_chequeNoTextBox").val("");
                    $("#MainContent_chequeDateTextBox").val("");
                } else if (pType == "Bank") {
                    bank.prop('disabled', false);
                    account.prop('disabled', false);
                    accNoTxt.prop('disabled', false);
                    chequeNo.prop('disabled', false);
                    cheqDate.prop('disabled', false);
                    bank.val(data.d.BankName);
                    accNoTxt.val(data.d.AccountNo);
                    chequeNo.val(data.d.ChequeNo);
                    var cDate = toDateFromJson(data.d.ChequeDate);
                    cheqDate.val(cDate);

                } else {

                }
                payType.val(data.d.PayType);
                payAmount.val(data.d.PayAmount);



            },
            error: function () {

            }
        });

    }
    //
    $("#btnSave").click(function() {
        var pType = $("#payTypeDropdownlist").val();
        if (pType=="0") {
            payTypeValidate.html("Select Payment Type");
        } else {
            payTypeValidate.html("");
        }
    });

    $("#btnSave").click(function() {
        var supplierName = $("#MainContent_supplierNameDropdownList").val();
        if (supplierName == "" || supplierName=="0"|| supplierName==null ) {
            supplierValidate.html("Select Supplier");
        } else {
            supplierValidate.html("");
        }
    });


    $("#btnSave").click(function () {
      var  paydate1 = $("#MainContent_paymentDateTextBox").val();
       
        if (paydate1 == "") {
           payDateValidate.html("Provide Payment Date");
           
        } else {
            payDateValidate.html("");
        }
    });
    $("#btnSave").click(function () {
        var cheqdate1 = $("#MainContent_chequeDateTextBox").val();
        if ($("#payTypeDropdownlist").val()!="Cash") {
            if (cheqdate1 == "") {
                cheqDateValiadte.html("Provide Cheque Date");

            } else {
                cheqDateValiadte.html("");
            }
        }
       
    });

    $("#btnSave").click(function() {
        var payData = $("#MainContent_payAmountTextBox").val();
        if (payData=="") {
            payAmountValidate.html("Provide Payable Amount");
        } else {
            payAmountValidate.html("");
        }
    });


});

$("#btnSave").click(function () {
    var bankName = $("#MainContent_bankNameDropDownList").val();
    if ($("#payTypeDropdownlist").val()!="Cash") {
        if (bankName =="0" || bankName==null) {
            bankValidate.html("Select Bank Name");
        } else {
            bankValidate.html("");
        }
        
    } else {
        bankValidate.html("");
    }

});
$("#btnSave").click(function () {
    var bankName = $("#MainContent_accountNoTextBox").val();
    if ($("#payTypeDropdownlist").val() !="Cash") {
        if (bankName == "") {
            accNoTxtValidate.html("Provide Account No");
        } else {
            accNoTxtValidate.html("");
        }

    } else {
        accNoTxtValidate.html("");
    }

});
$("#btnSave").click(function () {
    var chequeNo = $("#MainContent_chequeNoTextBox").val();
    if ($("#payTypeDropdownlist").val() !="Cash") {
        if (chequeNo == "") {
            chequeNoValidate.html("Provide Cheque No");
        } else {
            chequeNoValidate.html("");
        }

    } else {
        chequeNoValidate.html("");
    }

});
payAmount.keyup(function() {
    if ($.isNumeric(payAmount.val()) == false) {
        payAmount.addClass('textBoxError');
        payAmountValidate.html("Only Number");
    } else {
        payAmount.removeClass('textBoxError');
        payAmountValidate.html("");
    }
});

//Clear Data Code
$("#btnClear").click(function () {
    ClearData();

});

function ClearData() {
    $("#MainContent_paymentInvoiceNoDropDownList").val(0);
    bank.prop('disabled', false);
    account.prop('disabled', false);
    accNoTxt.prop('disabled', false);
    chequeNo.prop('disabled', false);
    cheqDate.prop('disabled', false);
    $("#MainContent_paymentDateTextBox").val("");
    $("#MainContent_supplierNameDropdownList").val("0");
    $("#remarksTextArea").val("");
    $("#payTypeDropdownlist").val("0");
    $("#MainContent_bankNameDropDownList").val("0");
    $("#MainContent_accountNoTextBox").val("");
    $("#MainContent_chequeNoTextBox").val("");
    $("#MainContent_chequeDateTextBox").val("");
    $("#MainContent_payAmountTextBox").val("");

    payDateValidate.html("");
    supplierValidate.html("");
    payTypeValidate.html("");
    bankValidate.html("");
    accNoTxtValidate.html("");
    chequeNoValidate.html("");
    cheqDateValiadte.html("");
    payAmountValidate.html("");
}
//Save Payment Data Code
$("#btnSave").click(function() {
    if (
        paydate.val() != ""
        && supplier.val() != "0"
        && payType.val() != "0"
        //&& bank.val() != "0" || bank.val() == ""
        //&& accNoTxt.val() != "" || accNoTxt.val()==""
        //&& chequeNo.val() != "" || chequeNo.val()==""
        //&& cheqDate.val() != "" || cheqDate.val()==""
        && payAmount.val() != "" 
        && $.trim(payAmount.val()).length!="" 
        && $.isNumeric(payAmount.val()) == true) {

        var pmtInvNo = $("#MainContent_paymentInvoiceNoDropDownList").val();
        var pmtDate = $("#MainContent_paymentDateTextBox").val();
        var pmtSupplier = $("#MainContent_supplierNameDropdownList").val();
        var pmtRemark = $("#remarksTextArea").val();
        var pmtType = $("#payTypeDropdownlist").val();
        var pmtBank = $("#MainContent_bankNameDropDownList").val();
        var pmtAccount = $("#MainContent_accountNoTextBox").val();
        var pmtCheque = $("#MainContent_chequeNoTextBox").val();
        var pmtChequeDate = $("#MainContent_chequeDateTextBox").val();
        var pmtAmount = $("#MainContent_payAmountTextBox").val();

        var payment= {
            PayInvoiceNo: pmtInvNo,
            PayDate: pmtDate,
            SupplierId: pmtSupplier,
            Remarks: pmtRemark,
            PayType: pmtType,
            BankName: pmtBank,
            AccountNo: pmtAccount,
            ChequeNo: pmtCheque,
            ChequeDate: pmtChequeDate,
            PayAmount: pmtAmount         
        }
        $.ajax({
            type: 'post',
            contentType: 'application/json; charset=utf-8',
            url: "PaymentUI.aspx/SavePayment",
            data: JSON.stringify({ payment: payment }),
            success: function (response) {
                //if (response.d.PayInvoiceNo != "") {
                //    getAllPurchaseData(response.d.PayInvoiceNo);
                //    $("#MainContent_paymentInvoiceNoDropDownList").append($("<option>").val(response.d.PayInvoiceNo).text(response.d.PayInvoiceNo));
                //    $('#MainContent_paymentInvoiceNoDropDownList').val(response.d.PayInvoiceNo);
                //}
                $("#MainContent_messageLabel").html(response.d.Message);
            },
            error: function () {
                alert("Error");
            }
        });

    }
});






