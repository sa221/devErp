
function getAllReturnsDetailsByInvoiceNO(invoiceNo) {
   
    $.ajax({
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        url: "PurchaseReturnUI.aspx/GetReturnDetailsByInvoc",
        dataType: 'json',
        data: "{'invoiceNo':'" + invoiceNo + "'}",
        async: false,
        success: function (data) {
            //supplierIdDropDownList.prop('disabled', false);
            //supplierNameDropDownList.prop('disabled', false);
            //purchasedateTextBox.prop('disabled', false);
            //chalanNoTextBox.prop('disabled', false);

            supplierIdDropDownList.val(data.d.SupplierId);
            supplierNameDropDownList.val(data.d.SupplierId);
            purchasedateTextBox.val(data.d.PurchaseDate);
            chalanNoTextBox.val(data.d.ChalanNo);
            noteTextArea.val(data.d.Remarks);
            amountTextBox.val(data.d.Amount);

            supplierIdDropDownList.prop('disabled', true);
            supplierNameDropDownList.prop('disabled', true);
            purchasedateTextBox.prop('disabled', true);
            chalanNoTextBox.prop('disabled', true);
            noteTextArea.prop('disabled', true);
            amountTextBox.prop('disabled', true);
        }

    });
    getAllReturnsData(invoiceNo);
}

function getAllReturnsData(invoiceNo) {
    $.ajax({
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        url: "PurchaseReturnUI.aspx/GetAllReturnDetails",
        dataType: 'json',
        data: "{'invoiceNo':'" + invoiceNo + "'}",
        async: false,
        success: function (data) {
            var raju = 1;
            var x = raju;
            $('#returnBody').empty();
            for (var i = 0; i < data.d.length; i++) {
                $('#returnBody').append(
                    "<tr>" +
                    "<td style='text-align: center;'>" + (parseInt(i) + 1) + "</td>" +
                    "<td style='display: none;text-align: center;'>" + data.d[i].PDId + "</td>" +
                    "<td style='display: none;text-align: center;'>" + data.d[i].PartsCode + "</td>" +
                    "<td style='text-align: center;'>" + data.d[i].PartsName + "</td>" +
                    "<td style='text-align: center;'>" + data.d[i].Quantity + "</td>" +
                    "<td style='text-align: center;'>" + data.d[i].Unit + "</td>" +
                    "<td style='text-align: center;'>" + data.d[i].Rate + "</td>" +
                    "<td style='text-align: center;'>" + data.d[i].Total + "</td>" +
                    "<td>" + "<button type='button' id='editButton' class='Edit btn btn-warning btn-sm'><span class='glyphicon glyphicon-pencil'>Edit</span></button>|<button type='button' id='deleteButton' class='Delete btn btn-danger btn-sm'><span class=' glyphicon glyphicon-trash'>Delete</span></button>" + "</td>" +
                    +"</tr>");
            }
            $("#MainContent_purchaseNetTotalTextBox").val(netTotal);
            //supplierIdDropDownList.val(data.d.SupplierId);
            //$('#MainContent_puchaseinvoiceNoDropDownList >option:eq(0)').prop('selected', true);
            //purchasedateTextBox.val(data.d.PurchaseDate);
            //chalanNoTextBox.val(data.d.ChalanNo);
        }

    });
}

$(function () {
    $('#purchasedate').datepicker({
        autoclose: true,
        todayHighlight: true,
        format: 'dd-mm-yyyy',
    });
    loadTableWhenDataNotFound();
    //getAllReturnData();
    modeSaveOrEdit.val("Save");    
});

function loadTableWhenDataNotFound() {
    $('#returnBody').empty();
    $('#returnBody').append(
                     "<tr>" +
                     "<td style='text-align: center;'>" + "-" + "</td>" +
                     "<td style='display: none;text-align: center;'>" + "-" + "</td>" +
                     "<td style='display: none;text-align: center;'>" + "-" + "</td>" +
                     "<td style='text-align: center;'>" + "-" + "</td>" +
                     "<td style='text-align: center;'>" + "-" + "</td>" +
                     "<td style='text-align: center;'>" + "-" + "</td>" +
                     "<td style='text-align: center;'>" + "-" + "</td>" +
                     "<td style='text-align: center;'>" + "-" + "</td>" +
                     "<td>" + "<button type='button' id='editButton' class='Edit btn btn-warning btn-sm' disabled='disabled'><span class='glyphicon glyphicon-pencil'>Edit</span></button>|<button type='button' id='deleteButton' class='Delete btn btn-danger btn-sm' disabled='disabled'><span class=' glyphicon glyphicon-trash'>Delete</span></button>" + "</td>" +
                     +"</tr>");

    $("#returnTable").dataTable({
        "scrollY": "180px",
        "scrollCollapse": true,
        "paging": false
    });
}

$("#clearButton").click(function () {
    modeSaveOrEdit.val("Save");
    clearHeadSetupFeild();
    ClearValidatorErrorText();
    var oTable = $('#returnTable').dataTable();
    oTable.fnDestroy();
    loadTableWhenDataNotFound();
});

//RajuCode
var modeSaveOrEdit = $("#MainContent_hiddenFeildButtonMode");
var puchaseinvoiceNoDropDownList = $("#MainContent_puchaseinvoiceNoDropDownList");
var validatePurchaseDropDown = $("#validatePurchaseDropDown");

var supplierNameDropDownList = $("#MainContent_supplierNameDropDownList");
var validateSupplierNameDropDown = $("#validateSupplierNameDropDown");

var supplierIdDropDownList = $("#MainContent_supplierIdDropDownList");
var validateSupplierIdDropDown = $("#validateSupplierIdDropDown");

var partsNameDropDownList = $("#MainContent_partsNameDropDownList");
var validatePartsNameDropDown = $("#validatePartsNameDropDown");

var purchasedateTextBox = $("#MainContent_purchasedateTextBox");
var purchaseDateValidate = $("#fromTextBoxValidation");

var chalanNoTextBox = $("#MainContent_chalanNoTextBox");
var validateChalanNo = $("#validateChalanNo");

var noteTextArea = $("#noteTextArea");

var amountTextBox = $("#MainContent_purchaseNetTotalTextBox");
var validateAmount = $("validateAmount");

var unitTextBox = $("#MainContent_unitTextBox");
var validateUnit = $("#validateUnit");

var quantityTextBox = $("#MainContent_quantityTextBox");
var validateQuantity = $("#validateQuantity");

var rateTextBox = $("#MainContent_rateTextBox");
var validateRate = $("#validateRate");

var totalTextBox = $("#MainContent_totalTextBox");
var validateTotal = $("#validateTotal");

function ClearValidatorErrorText() {
    validatePurchaseDropDown.html("");
    purchaseDateValidate.html("");
    validateSupplierNameDropDown.html("");
    validateSupplierIdDropDown.html("");
    validateChalanNo.html("");
    validatePartsNameDropDown.html("");
    validateUnit.html("");
    validateQuantity.html("");
    validateRate.html("");
    validateTotal.html("");

    $("#addButton").show();
    $("#updateButton").hide();
}

function clearHeadSetupFeild() {
    $('#MainContent_puchaseinvoiceNoDropDownList >option:eq(0)').prop('selected', true);
    $('#MainContent_supplierNameDropDownList >option:eq(0)').prop('selected', true);
    $('#MainContent_supplierIdDropDownList >option:eq(0)').prop('selected', true);
    $('#MainContent_partsNameDropDownList >option:eq(0)').prop('selected', true);
    $("#MainContent_chalanNoTextBox").val("");
    $("#noteTextArea").val("");
    $("#MainContent_quantityTextBox").val("");
    $("#MainContent_unitTextBox").val("");
    $("#MainContent_rateTextBox").val("");
    $("#MainContent_purchasedateTextBox").val("");
    $("#MainContent_totalTextBox").val("");

    $("#MainContent_hiddenFeildPrartsId").val("");

    supplierIdDropDownList.prop('disabled', false);
    supplierNameDropDownList.prop('disabled', false);
    purchasedateTextBox.prop('disabled', false);
    chalanNoTextBox.prop('disabled', false);
    noteTextArea.prop('disabled', false);
    //$("#saveButton").show();
    //$("#updateButton").hide();
}
supplierNameDropDownList.change(function () {
    var supplierId = supplierNameDropDownList.val();
    if (supplierId != "0") {
        supplierIdDropDownList.val(supplierId);
    }
});
supplierIdDropDownList.change(function () {
    var supplierId = supplierIdDropDownList.val();
    if (supplierId != "0") {
        supplierNameDropDownList.val(supplierId);
    }
});

partsNameDropDownList.change(function () {
    var partsCode = partsNameDropDownList.val();
    if (partsCode != "0") {
        $.ajax({
            type: 'post',
            contentType: "application/json; charset=utf-8",
            url: "PurchaseUI.aspx/GetPartsDetailsById",
            data: "{'partsCode':'" + partsCode + "'}",
            async: false,
            success: function (data) {
                $("#MainContent_unitTextBox").val(data.d.Unit);
            },
            error: function () { alert("error"); }
        });
    }
});
$("#MainContent_puchaseinvoiceNoDropDownList").change(function () {
    var invoiceNo = puchaseinvoiceNoDropDownList.val();
    var oTable = $('#returnTable').dataTable();
    oTable.fnDestroy();
    if (invoiceNo != "0") {
        getAllReturnsDetailsByInvoiceNO(invoiceNo);

        $("#returnTable").dataTable({
            "scrollY": "180px",
            "scrollCollapse": true,
            "paging": false
        });
    } else {
        loadTableWhenDataNotFound();
        supplierIdDropDownList.prop('disabled', false);
        supplierNameDropDownList.prop('disabled', false);
        purchasedateTextBox.prop('disabled', false);
        chalanNoTextBox.prop('disabled', false);
        noteTextArea.prop('disabled', false);

        $('#MainContent_supplierNameDropDownList >option:eq(0)').prop('selected', true);
        $('#MainContent_supplierIdDropDownList >option:eq(0)').prop('selected', true);
        $("#MainContent_purchasedateTextBox").val("");
        $("#MainContent_chalanNoTextBox").val("");
        $("#noteTextArea").val("");
    }

});
$("#addButton").click(function () {
    if (purchasedateTextBox.val() == "") {
        purchaseDateValidate.html("please provide date");
        return false;
    } else {
        purchaseDateValidate.html("");
    }

});
$("#addButton").click(function () {
    if (supplierIdDropDownList.val() == "0") {
        validateSupplierIdDropDown.html("Select Supplier ID");
        return false;
    } else {
        validateSupplierIdDropDown.html("");
    }
});
$("#addButton").click(function () {
    if (supplierNameDropDownList.val() == "0") {
        validateSupplierNameDropDown.html("Select Supplier ID");
        return false;
    } else {
        validateSupplierNameDropDown.html("");
    }
});
$("#addButton").click(function () {
    if (partsNameDropDownList.val() == "0") {
        validatePartsNameDropDown.html("Please Select Parts Name.");
        return false;
    } else {
        validatePartsNameDropDown.html("");
    }
});
$("#addButton").click(function () {
    var data = chalanNoTextBox.val();
    if (data == "") {
        validateChalanNo.html("This Is Required");
        return false;
    } else {
        var white = $.trim(data).length;
        if (white == "") {
            validateChalanNo.html("White Spaces are not allowed ");
            return false;
        } else {
            validateChalanNo.html("");
        }
    }
});
$("#addButton").click(function () {
    var white = $.trim(quantityTextBox.val()).length;
    if (white == "") {
        validateQuantity.html("White Spaces are not allowed ");
        return false;
    } else {
        if ($.isNumeric(quantityTextBox.val()) == false) {
            validateQuantity.html("Please Insert Number");
            return false;
        } else {
            validateQuantity.html("");
        }

    }
});
$("#addButton").click(function () {
    var data = unitTextBox.val();
    if (data == "") {
        validateUnit.html("This Is Required");
        return false;
    } else {
        var white = $.trim(data).length;
        if (white == "") {
            validateUnit.html("White Spaces are not allowed ");
            return false;
        } else {
            validateUnit.html("");
        }
    }
});
$("#addButton").click(function () {
    var white = $.trim(rateTextBox.val()).length;
    if (white == "") {
        validateRate.html("White Spaces are not allowed ");
        return false;
    } else {
        if ($.isNumeric(rateTextBox.val()) == false) {
            validateRate.html("Please Insert Number");
            return false;
        } else {
            validateRate.html("");
        }

    }
});
quantityTextBox.keyup(function () {

    if ($.isNumeric(quantityTextBox.val()) == false) {
        quantityTextBox.addClass('textBoxError');
        validateQuantity.html("Only Number");
    } else {
        var rate = rateTextBox.val();
        var qty = quantityTextBox.val();
        if ($.isNumeric(rate)) {
            var total = (parseFloat(rate) * parseFloat(qty));
            totalTextBox.val(total);
        }

        quantityTextBox.removeClass('textBoxError');
        validateQuantity.html("");
    }
});
rateTextBox.keyup(function () {
    if ($.isNumeric(rateTextBox.val()) == false) {
        rateTextBox.addClass('textBoxError');
        validateRate.html("Only Number");
    } else {
        var rate = rateTextBox.val();
        var qty = quantityTextBox.val();
        if ($.isNumeric(qty)) {
            var total = (parseFloat(rate) * parseFloat(qty));
            totalTextBox.val(total);
        }

        rateTextBox.removeClass('textBoxError');
        validateRate.html("");
    }
});
rateTextBox.keyup(function () {

    if ($.isNumeric(rateTextBox.val()) == false) {
        rateTextBox.addClass('textBoxError');
        validateRate.html("Only Number");
    } else {
        rateTextBox.removeClass('textBoxError');
        validateRate.html("");
    }
});
$("#MainContent_rateTextBox").focusout(function () {

    if ($("#MainContent_rateTextBox").val() == null || $("#MainContent_rateTextBox").val() == "") {
        $("#validateRate").html("Please Input Rate");
        return false;
    } else {
        if ($("#MainContent_rateTextBox").val() != null && $("#MainContent_quantityTextBox").val() != null) {
            var qty, rate;
            qty = parseInt($("#MainContent_quantityTextBox").val());
            rate = parseInt($("#MainContent_rateTextBox").val());
            $("#MainContent_totalTextBox").val(qty * rate);
            var netTotalPrev = $("#MainContent_purchaseNetTotalTextBox").val();
            var netTotalNow = (parseFloat(netTotalPrev) + (qty * rate));
            $("#MainContent_purchaseNetTotalTextBox").val(netTotalNow);

        } else {
            $("#validateRate").html("");
        }

    }
});
$("#MainContent_quantityTextBox").focusout(function () {
    if ($("#MainContent_quantityTextBox").val() == null || $("#MainContent_quantityTextBox").val() == "") {
        $("#validateQuantity").html("Please Input Quantity");
        return false;
    } else {
        $("#validateRate").html("");
    }
});

$("#addButton").click(function () {
    if (
         purchasedateTextBox.val() != "" && supplierIdDropDownList.val() != "0" && supplierNameDropDownList.val() != "0" &&
        partsNameDropDownList.val() != "0" && chalanNoTextBox.val() != "" && $.trim(chalanNoTextBox.val()).length != "" &&
        $.isNumeric(quantityTextBox.val()) == true && quantityTextBox.val() != "" &&
        unitTextBox.val() != "" && $.trim(unitTextBox.val()).length != "" &&
        $.isNumeric(rateTextBox.val()) == true && rateTextBox.val() != ""
        ) {
        //var purchaseInvText = $("#MainContent_puchaseinvoiceNoDropDownList option:selected").text();
        var purchaseInvNo = $("#MainContent_puchaseinvoiceNoDropDownList").val();
        var purchaseDate = $("#MainContent_purchasedateTextBox").val();
        //var supplierName = $("#MainContent_supplierNameDropDownList : selected").text();
        var supplierId = $("#MainContent_supplierNameDropDownList").val();
        var chalanNo = $("#MainContent_chalanNoTextBox").val();
        var noteArea = $("#noteTextArea").val();
        var amount = $("#MainContent_purchaseNetTotalTextBox").val();

        var partsName = $("#MainContent_partsNameDropDownList").val();
        var mode = modeSaveOrEdit.val();

        var x = $("#MainContent_hiddenFeildPrartsId").val();
        var pdId;
        if (x != "") {
            pdId = x;
        } else {
            pdId = 0;
        }
        var quantity = $("#MainContent_quantityTextBox").val();
        var unit = $("#MainContent_unitTextBox").val();
        var rate = $("#MainContent_rateTextBox").val();
        var total = $("#MainContent_totalTextBox").val();

        var purchase = {
            //PurchaseInvNoText: purchaseInvText,
            PurchaseInvNo: purchaseInvNo,
            SupplierId: supplierId,
            ChalanNo: chalanNo,
            Remarks: noteArea,
            Amount: amount
        }
        var purchaseDetail = {
            PDId: pdId,
            PartsCode: partsName,
            Quantity: quantity,
            Unit: unit,
            Rate: rate,
            Total: total,
            ModeEditOrSave: mode
        }

        $.ajax({
            type: 'post',
            contentType: 'application/json; charset=utf-8',
            url: "PurchaseReturnUI.aspx/SaveReturn",
            dataType: 'json',
            data: JSON.stringify({ purchase: purchase, purchaseDetails: purchaseDetail, purchaseDate: purchaseDate }),
            async: false,
            success: function (response) {
                if (response.d.InvoiceNo != "") {
                    getAllReturnsData(response.d.InvoiceNo);
                    $("#MainContent_puchaseinvoiceNoDropDownList").append($("<option>").val(response.d.InvoiceNo).text(response.d.InvoiceNo));
                    $('#MainContent_puchaseinvoiceNoDropDownList').val(response.d.InvoiceNo);
                }
                $("#messageLabel").html(response.d.Message);
            },
            error: function () {
                alert("Error");
            }
        });

    }


});
$("body").on("click", "[id=returnTable] .Edit", function () {
    ClearValidatorErrorText();
    modeSaveOrEdit.val("");
    var pdId = $(this).parents("tr").find("td:eq(1)").html();
    var pcode = $(this).parents("tr").find("td:eq(2)").html();
    var qty = $(this).parents("tr").find("td:eq(4)").html();
    var unit = $(this).parents("tr").find("td:eq(5)").html();
    var rate = $(this).parents("tr").find("td:eq(6)").html();
    var total = $(this).parents("tr").find("td:eq(7)").html();


    $("#MainContent_hiddenFeildPrartsId").val(pdId);
    partsNameDropDownList.val(pcode);

    quantityTextBox.val(qty);
    unitTextBox.val(unit);
    rateTextBox.val(rate);
    totalTextBox.val(total);
    modeSaveOrEdit.val("Edit");
});
$("body").on("click", "[id=purchaseTable] .Delete", function () {
    // ClearValidatorErrorText();
    //var partsCode = $(this).parent().siblings().eq(0).find("span").text();
    var pdId = $(this).parents("tr").find("td:eq(1)").html();
    var row = $(this).closest('tr');
    var confirmMessg = confirm("Are You Sure To Delete This Item");
    if (confirmMessg == true) {

        $.ajax({
            type: 'Post',
            contentType: "application/json; charset=utf-8",
            url: "PurchaseUI.aspx/DeleteItem",
            data: "{'pdId':'" + pdId
                + "'}",
            async: false,
            success: function (data) {
                row.remove();
                $("#messageLabel").html(data.d);
            },
            error: function () {
                alert("Error");
            }
        });
        return false;
    } else {
        $("#messageLabel").html("<div class='alert alert-success alert-dismissible' role='alert'><button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>&times;</span></button>Delete Operation Canceled</div>");
        return false;
    }
});















   








