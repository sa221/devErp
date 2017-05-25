$('#MainContent_cellNumberText').keypress(function (e) {
    if (e.which == 13) // Enter key = keycode 13
    {
        $('#MainContent_discountAgainstInvoiceTaka').focus();  //Use whatever selector necessary to focus the 'next' input
        return false;
    }
});

$('#MainContent_partyIdText').keypress(function (e) {
    if (e.which == 13) // Enter key = keycode 13
    {
        $('#MainContent_productCodeText').focus();  //Use whatever selector necessary to focus the 'next' input
        return false;
    }
});

$('#MainContent_cashReceivedText').keypress(function (e) {
    if (e.which == 13) // Enter key = keycode 13
    {
        $('#MainContent_cellNumberText').focus();  //Use whatever selector necessary to focus the 'next' input
        return false;
    }
});

$('#MainContent_discountAgainstInvoiceTaka').keypress(function (e) {
    if (e.which == 13) // Enter key = keycode 13
    {
        $('#MainContent_cashReceivedText').focus();  //Use whatever selector necessary to focus the 'next' input
        return false;
    }
});

$("#MainContent_partyIdText").change(function () {
    var partyId = $("#MainContent_partyIdText").val();
    $("#MainContent_partyNameDropDownList").val(partyId);
});
$("#MainContent_partyNameDropDownList").change(function () {
    var partyId = $("#MainContent_partyNameDropDownList").val();
    $("#MainContent_partyIdText").val(partyId);
});

$("#MainContent_discountAgainstInvoicePercentage").change(function () {
    var disPercentage = $("#MainContent_discountAgainstInvoicePercentage").val() * 1;
    var netAmount = 0;
    if ($("#MainContent_totalAfterDiscount").val() == "") {
        netAmount = $("#MainContent_allItemTotalTakaText").val() * 1;
    } else {
        netAmount = $("#MainContent_totalAfterDiscount").val() * 1;
    }
    var disAmount = 0;
    if (netAmount != "" && $.trim(netAmount) != "" && disPercentage != "" && $.trim(disPercentage) != "") {
        disAmount = parseFloat(netAmount) * (parseFloat(disPercentage) / 100);
        $("#MainContent_discountAgainstInvoiceTaka").val(disAmount);
        $("#MainContent_netPaybleText").val((parseFloat(netAmount) - disAmount));
    }
    else if (disPercentage == 0 && $.trim(disPercentage) == 0) {
        $("#MainContent_discountAgainstInvoiceTaka").val("");
        if ($("#MainContent_totalAfterDiscount").val() != "") {
            $("#MainContent_netPaybleText").val($("#MainContent_totalAfterDiscount").val());
        } else {
            $("#MainContent_netPaybleText").val($("#MainContent_allItemTotalTakaText").val());
        }
    }
    //$("#MainContent_partyNameDropDownList").val(partyId);
});

$("#MainContent_discountAgainstInvoiceTaka").change(function () {
    var disTaka = $("#MainContent_discountAgainstInvoiceTaka").val() * 1;
    var netAmount = $("#MainContent_netPaybleText").val() * 1;
    //if ($("#MainContent_totalAfterDiscount").val() == "") {
    //    netAmount = $("#MainContent_allItemTotalTakaText").val() * 1;
    //} else {
    //    netAmount = $("#MainContent_totalAfterDiscount").val() * 1;
    //}

    var disAmount = 0;
    if (netAmount != "" && $.trim(netAmount) != "") {
        disAmount = (parseFloat(disTaka) * 100) / (parseFloat(netAmount));
        $("#MainContent_discountAgainstInvoicePercentage").val(disAmount.toFixed(2));
        $("#MainContent_netPaybleText").val((parseFloat(netAmount) - parseFloat(disTaka)));
    }
    else if (disTaka == 0 && $.trim(disTaka) == 0) {
        $("#MainContent_discountAgainstInvoicePercentage").val("");
        if ($("#MainContent_totalAfterDiscount").val() != "") {
            $("#MainContent_netPaybleText").val($("#MainContent_totalAfterDiscount").val());
        } else {
            $("#MainContent_netPaybleText").val($("#MainContent_allItemTotalTakaText").val());
        }
    }
    //$("#MainContent_partyNameDropDownList").val(partyId);
});
//-----Cash Recive on-change----
$("#MainContent_cashReceivedText").change(function () {
    var netAmount = $("#MainContent_netPaybleText").val();
    var cashAmount = $("#MainContent_cashReceivedText").val();
    

    var dusAmount = 0;
    if (netAmount != "" && $.trim(netAmount) != "" && parseFloat(netAmount)>0) {
        dusAmount = parseFloat(netAmount) - parseFloat(cashAmount);
        if (dusAmount > -1) {
            $("#MainContent_duesAmountText").val(dusAmount);
        } else {
            $("#MainContent_duesAmountText").val("0");
        }
        $('#MainContent_cellNumberText').focus();
    }
    else {
        $("#MainContent_cashReceivedText").val("0");
        $("#MainContent_duesAmountText").val("0");
    }
    //$("#MainContent_partyNameDropDownList").val(partyId);
});
$("#MainContent_cellNumberText").change(function () {
    var cellNumberOrId = $("#MainContent_cellNumberText").val();
    $.ajax({
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        url: "Sales.aspx/GetCardDetails",
        dataType: 'json',
        data: JSON.stringify({ cellNumberOrId: cellNumberOrId }),
        async: false,
        success: function (response) {

            //$("#MainContent_cardTypeLabel").html(response.d[0].DisCardType);
            //$("#MainContent_disPerLabel").html(response.d[0].DisAMT);
            $("#MainContent_partyIdLabel").html(response.d[0].SupplierId);
            $("#MainContent_nameLabel").html(response.d[0].OrganizationName);
            $("#MainContent_addressLabel").html(response.d[0].Address);
            $("#MainContent_cellNoLabel").html(response.d[0].ContactNo);

            //var totalAmount = $("#MainContent_allItemTotalTakaText").val();
            //var disAmount = 0;
            //if (totalAmount != "" && $.trim(totalAmount) != "") {
            //    disAmount = parseFloat(totalAmount) * (parseFloat(response.d[0].DisAMT) / 100);
            //    $("#MainContent_totalAfterDiscount").val((parseFloat(totalAmount) - disAmount));
            //    $("#MainContent_netPaybleText").val((parseFloat(totalAmount) - disAmount));
            //}
            //$("#MainContent_messageLabel").addClass('label-success');
        },
        error: function () {
            //$("#MainContent_messageLabel").html("Error Found");
            //$("#MainContent_messageLabel").addClass('label-danger');
        }
    });
});

function loadItemDetails(productId) {

    $.ajax({
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        url: "Sales.aspx/GetProductDetails",
        dataType: 'json',
        data: JSON.stringify({ productId: productId }),
        async: false,
        success: function (response) {
            if (response.d.length > 0) {
                $("#MainContent_stockQtyText").val(response.d[0].Quantity);
                $("#MainContent_priceText").val(response.d[0].SalesPrice);
                $('#MainContent_qtyText').focus();
            } else {
                alert("Invalid Product Code");
                $('#MainContent_productCodeText').val("").focus();
                $("#MainContent_productNameDropDownList").val("");
            }

            //$("#MainContent_messageLabel").addClass('label-success');
        },
        error: function () {
            //$("#MainContent_messageLabel").html("Error Found");
            //$("#MainContent_messageLabel").addClass('label-danger');
        }
    });
}

$("#MainContent_productNameDropDownList").change(function () {
    $("#MainContent_qtyText").val("");
    //$("#MainContent_itemDiscountTakaText").val("");
    var productId = $("#MainContent_productNameDropDownList").val();
    $("#MainContent_productCodeText").val(productId);
    loadItemDetails(productId);
});
$("#MainContent_productCodeText").change(function () {
    $("#MainContent_qtyText").val("");
    //$("#MainContent_itemDiscountTakaText").val("");
    var productId = $("#MainContent_productCodeText").val();
    $("#MainContent_productNameDropDownList").val(productId);
    loadItemDetails(productId);
});
