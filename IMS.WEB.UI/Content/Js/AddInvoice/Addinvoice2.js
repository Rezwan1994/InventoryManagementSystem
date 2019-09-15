var TotalAmount = 0; /*TotalAmount: Invoice Table -> Amount*/
var FinalTotal = 0;/*FinalTotal: Invoice Table -> TotalAmount*/
var DiscountAmount = 0;
var ShippingAmount = 0;
var DepositAmount = 0;

var DiscountDBPercent = 0;
var DiscountDBAmount = 0;
var TaxAmount = 0;
var SendEmailUrl = "";
var mailAdd = "";
var InvoiceDatepicker;
var DueDatepicker;
var shippingDatePicker;
var Fdiscountamount = 0;


var NewEquipmentRow = "<tr>"
                        + "<td valign='top' class='rowindex'></td>"
                        + "<td valign='top'><input type='text'class='ProductName' onkeydown='SearchKeyDown(this,event)' onkeyup='SearchKeyUp(this,event)' />"
                            + "<div class='tt-menu'>"
                                + "<div class='tt-dataset tt-dataset-autocomplete'> </div> "
                            + "</div>"
                            + "<span class='spnProductName'></span>"
                        + "</td>"
                        + "<td valign='top'>"
                            + "<input type='text' onkeydown='OthersKeyDown(this,event)' class='txtProductDesc' />"
                            + "<span class='spnProductDesc'></span>"
                        + "</td>"
                        + "<td valign='top'>"
                            + "<input type='text' onkeydown='OthersKeyDown(this,event)' class='txtProductQuantity' />"
                            + "<span class='spnProductQuantity'></span>"
                        + "</td>"
                         + "<td valign='top'>"
                            + "<div class='C_S I_G'>"
                                + "<div class='input-group-prepend'>"
                                    + "<div class='input-group-text'></div>"
                                + "</div>"
                                + "<input type='text' onkeydown='OthersKeyDown(this,event)' class='txtProductRate' />"
                            + "</div>"
                            + "<span class='spnProductRate'></span>"
                        + "</td>"
                        + "<td valign='top'>"
                            + "<div class='C_S I_G'>"
                                + "<div class='input-group-prepend'>"
                                    + "<div class='input-group-text'></div>"
                                + "</div>"
                                + "<input type='text' onkeydown='OthersKeyDown(this,event)' class='txtProductAmount' />"
                            + "</div>"
                        //+ "<input type='text' onkeydown='OthersKeyDown(this,event)' class='txtProductAmount' />"
                            + "<span class='spnProductAmount'></span>"
                        + "</td>"
                        + "<td valign='top' class='tableActions'>"
                            + "<i class='fa fa-trash-o' aria-hidden='true'></i>"
                        + "</td>"
    + "</tr>";


var CalculateNewAmount = function () {
    console.log("hlww");
    var amount = parseFloat('0');
    $(".txtProductAmount").each(function () {
        var _CalAmt = $(this).val().trim();
        _CalAmt = _CalAmt.replaceAll(',', '');

        var currAmount = parseFloat(_CalAmt);
        if (!isNaN(currAmount)) {
            amount += currAmount;
        }
    });
    amount = parseFloat(amount).toFixed(2);
    amount1 = amount.replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,");
    $(".amount").text(amount1);
    TotalAmount = amount;
    FinalTotal = amount;
    BalanceDue = amount;
  
        var a = 0;
        var Fval = 0;
        var discountAmount = 0;
        if ($("#discountAmount").length > 0) {
            if ($("#discountAmount").val() == "") {
                discountAmount = 0;
            }
            else {
                discountAmount = $("#discountAmount").val();
            }
        }
        Fdiscountamount = TotalAmount - ((amount / 100) * discountAmount);
        if (discountAmount != "" && Fdiscountamount > 0) {
            var discountAmountPercent = parseFloat(discountAmount);
            DiscountDBPercent = discountAmountPercent;
            DiscountDBAmount = a;
            DiscountAmount = (amount / 100) * discountAmountPercent;
            FinalTotal = TotalAmount - DiscountAmount;
            BalanceDue = FinalTotal;
            $(".shippingAmountTxt").text(DiscountAmount.toFixed(2).replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"));
            if (DiscountAmount == 0) {
                $(".Discount-total").addClass('hidden');
            }
            else {
                $(".DiscountAmountTxt").text(Fdiscountamount.toFixed(2).replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"));
            }
        }
        else {
            $(".shippingAmountTxt").text( "0.00");
            $("#discountAmount").val("");
            $(".DiscountAmountTxt").text( "0.00");
        }
 
    var DA = 0;

    if ($("#discountAmount").val() != "") {
        BalanceDue = parseFloat(Fdiscountamount) + parseFloat(TaxAmount) + parseFloat(ShippingAmount) - parseFloat(DA);
        FinalTotal = parseFloat(Fdiscountamount) + parseFloat(TaxAmount) + parseFloat(ShippingAmount);
    }
    else {
        BalanceDue = parseFloat(TotalAmount) + parseFloat(TaxAmount) + parseFloat(ShippingAmount) - parseFloat(DA);
        FinalTotal = parseFloat(TotalAmount) + parseFloat(TaxAmount) + parseFloat(ShippingAmount);
    }

    $(".FinalTotalTxt").text(FinalTotal.toFixed(2).replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"));
    $(".balanceDueAmount").text(BalanceDue.toFixed(2).replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"));
    $(".amount-big").text(BalanceDue.toFixed(2).replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"));
}

var InvoiceEqSuggestionclickbind = function (item) {
    console.log("fd");
    $('.CustomerInvoiceTab .tt-suggestion').click(function () {
        console.log("fvfd");
        console.log("Enter fired");
        var clickitem = this;
        $('.CustomerInvoiceTab .tt-menu').hide();
        $(item).val($(clickitem).attr('data-select'));
        $(item).attr('data-id', $(clickitem).attr('data-id'));
        var itemName = $(item).parent().find('span');
        $(itemName).text($(item).val());

        $(item).parent().parent().attr('data-id', $(clickitem).attr('data-id'));
        $(item).parent().parent().addClass('HasItem');

        /*Item Rate Set*/
        var spnItemRate = $(item).parent().parent().find('.spnProductRate');
        $(spnItemRate).text($(this).attr('data-price'));
        var txtItemRate = $(item).parent().parent().find('.txtProductRate');
        $(txtItemRate).val($(this).attr('data-price'));
        /*Item Description Set*/
        var spnItemRate = $(item).parent().parent().find('.spnProductDesc');
        $(spnItemRate).text($(this).attr('data-description'));
        var txtItemRate = $(item).parent().parent().find('.txtProductDesc');
        $(txtItemRate).val($(this).attr('data-WarehouseId'));
        /*Item Quantity Set*/
        var spnItemRate = $(item).parent().parent().find('.spnProductQuantity');
        $(spnItemRate).text(1);
        var txtItemRate = $(item).parent().parent().find('.txtProductQuantity');
        $(txtItemRate).val(1);
        /*Item Amount Set*/
        var spnItemRate = $(item).parent().parent().find('.spnProductAmount');
        $(spnItemRate).text($(this).attr('data-price'));
        var txtItemRate = $(item).parent().parent().find('.txtProductAmount');
        $(txtItemRate).val($(this).attr('data-price'));
        CalculateNewAmount();
    });
  
}

var OthersKeyDown = function (item, event) {
    console.log("fired");
   

}


var SearchKeyDown = function (item, event) {

    if (event.keyCode == 13) {/*Enter*/
        var selectedTTMenu = $(event.target).parent().find('.tt-suggestion.active');
        $(selectedTTMenu).click();
        $('.tt-menu').hide();
    }
    if (event.keyCode == 40) {/*Down*/
        var ttSugstionDom = $(event.target).parent().find('.tt-suggestion');
        var ttSugActive = $(event.target).parent().find('.tt-suggestion.active');
        if ($(ttSugstionDom).length > 0 && $(ttSugstionDom).is(':visible')) {
            if ($(ttSugActive).length == 0) {
                $($(ttSugstionDom).get(0)).addClass('active');
                $(item).val($($(ttSugstionDom).get(0)).attr('data-select'))
            }
            else {
                var suggestionlist = $(ttSugstionDom);
                var activesuggestion = $(ttSugActive);
                var indexactive = -1;
                for (var id = 0; id < suggestionlist.length; id++) {
                    if ($(suggestionlist[id]).hasClass('active'))
                        indexactive = id;
                }
                if (indexactive < suggestionlist.length - 1) {
                    $(ttSugstionDom).removeClass('active');
                    var possibleactive = $(ttSugstionDom).get(indexactive + 1);
                    $($(ttSugstionDom).get(indexactive + 1)).addClass('active');
                    $(possibleactive).addClass('active');
                    $(item).val($(possibleactive).attr('data-select'));
                }
            }

            event.preventDefault();
        }
        else {

            var trselected = $($(event.target).parent()).parent();
            $(trselected).removeClass('focusedItem');
            $($(trselected).next('tr')).addClass('focusedItem');
            if ($(event.target).hasClass('ProductName')) {
                $($(trselected).next('tr')).find('input.ProductName').focus();
            }
        }
    }
    if (event.keyCode == 38) {/*UP*/
        var ttSugstionDom = $(event.target).parent().find('.tt-suggestion');
        var ttSugActive = $(event.target).parent().find('.tt-suggestion.active');
        if ($(ttSugstionDom).length > 0 && $(ttSugActive).length > 0 && $(ttSugstionDom).is(':visible')) {
            var suggestionlist = $(ttSugstionDom);
            var activesuggestion = $(ttSugActive);
            var indexactive = -1;
            for (var id = 0; id < suggestionlist.length; id++) {
                if ($(suggestionlist[id]).hasClass('active'))
                    indexactive = id;
            }
            if (indexactive > 0) {
                $(ttSugstionDom).removeClass('active');
                var possibleactive = $(ttSugstionDom).get(indexactive - 1);
                $(possibleactive).addClass('active');
                $(item).val($(possibleactive).attr('data-select'))
            }
            event.preventDefault();
        }
        else {
            var trselected = $($(event.target).parent()).parent();
            $(trselected).removeClass('focusedItem');
            $($(trselected).prev('tr')).addClass('focusedItem');
            $($(trselected).prev('tr')).find('input.ProductName').focus();
        }
    }

}

var PropertyUserSuggestiontemplate =
    "<div class='tt-suggestion tt-selectable' style = 'display:block' data-select='{1}' data-price='{2}' data-type='{5}' data-id='{0}' data-description='{4}' data-WarehouseId='{5}'  >"
    /*
    *For Equipment Image
    *+ "<img src='{7}' class='EquipmentImage'>"*/
    + "<p class='tt-sug-text'>"
    + "<em class='tt-sug-type'></em>{1}" + "<br /><em class='tt_sug_manufac'>Quantity on hand: {3}</em>"
    + "<em class='tt-eq-price'>${2}</em>"
    + "<br />"
    + "</p> "
    + "</div>";

var SearchKeyUp = function (item, event) {
    if (event.keyCode == 40 || event.keyCode == 38 || event.keyCode == 13 || event.keyCode == 9)
        return false;
    var ExistEquipment = "";
    var ExistEquipmentInner = "";
    $(".HasItem").each(function () {
        ExistEquipmentInner += "'" + $(this).attr('data-id') + "',";
    });
    if (ExistEquipmentInner.length > 0) {
        ExistEquipmentInner = ExistEquipmentInner.slice(0, -1);
        ExistEquipment = "(" + ExistEquipmentInner + ")";
    }
    $.ajax({
        url: "/Products/GetEquipmentListByKey",
        data: {
            key: $(item).val(),
            ExistEquipment: ExistEquipment,
            Warehouse: $("#warehouse").val()
        },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            var resultparse = JSON.parse(data.result);

            if (resultparse.length > 0) {
                var searchresultstring = "<div class='NewProjectSuggestion'>";
                for (var i = 0; i < resultparse.length; i++) {
                    searchresultstring = searchresultstring + String.format(PropertyUserSuggestiontemplate,

                        /*0*/resultparse[i].ProductId,
                        /*1*/ resultparse[i].ProductName,
                        /*2*/ resultparse[i].SellingPrice,
                       
                        /*3*/ resultparse[i].QuantityOnHand,
                      /*4*/resultparse[i].WarehouseName,
                    /*4*/resultparse[i].WarehouseId);
                }
                searchresultstring += "</div>";
                var ttdom = $($(item).parent()).find('.tt-menu');
                var ttdomComplete = $($(item).parent()).find('.tt-dataset-autocomplete');
                $(ttdomComplete).html(searchresultstring);
                $(ttdom).show();

                InvoiceEqSuggestionclickbind(item);
                if (resultparse.length > 4) {
                    $(".NewProjectSuggestion").height(352);
                    $(".NewProjectSuggestion").css('position', 'relative');
                    /*$(".NewProjectSuggestion").perfectScrollbar()*/
                }
            }
            if (resultparse.length == 0)
                $('.tt-menu').hide();
        }
    });
}
var SaveAndNew = function () {
    SaveInvoice();
    if ($(".HasItem").length != 0) {
        OpenTopToBottomModal("/Invoice/AddInvoice/?customerid=" + customerId);
    }
}

var SaveInvoice = function () {

    $(".InvoiceSaveButton").click(function () {
        SaveAndNew();
    });

    console.log("Save invoice fired");
    if ($(".HasItem").length == 0) {
        OpenErrorMessageNew("Error!", "You have to select at least one equipment to proceed", function () { });
        return;
    }
 
    var DetailList = [];
    $(".HasItem").each(function () {
        DetailList.push({
            ProductId: $(this).attr('data-id'),
            Quantity: $(this).find('.txtProductQuantity').val(),
            Price: $(this).find('.txtProductRate').val().trim().replaceAll(',', ''),
            Total: ($(this).find('.txtProductQuantity').val() * parseFloat($(this).find('.txtProductRate').val().trim().replaceAll(',', ''))).toString(),
            WarehouseId: $(this).find('.txtProductDesc').val()
        });
    });
    var url = "/Invoice/AddInvoice";

    var param = JSON.stringify({
        "SalesOrder.Id": SalesOrderIdInt,
        "SalesOrder.SalesOrderId": SalesOrderId,
        "SalesOrder.CustomerId": CustomerId,
        "SalesOrder.Amount": BalanceDue,
        "SalesOrder.OrderDate": $(".OrderDate").val(),
        "SalesOrder.DelivaryDate": $(".DeliveryDate").val(),
        "SalesOrder.DiscountAmount": DiscountDBAmount,

        "SalesOrder.PaymentAmount": $("#paymentamount").val(),
        "SalesOrder.PaymentNote": $("#paymentnote").val(),
        "SalesOrder.PaymentDate": $("#paymentdate").val(),
        SalesOrderDetails : DetailList
    });

    $.ajax({
        type: "POST",
        ajaxStart: $(".AddInvoiceLoader").removeClass('hidden'),
        url: url,
        data: param,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        cache: false,
        success: function (data) {
            if (data.result == true) {
                OpenSuccessMessageNew("Success!", data.message, function () {
                    CloseTopToBottomModal();
                    $(".AllUnPaidInvoice_Load").load("/Invoice/InvoiceListPartial/?CustomerId=" + CustomerId + "&InvoiceType=unpaid");
                });
            }
            else {
                OpenErrorMessageNew("", data.message, function () {
                    CloseTopToBottomModal();
                })
            }
         
        },
        error: function (jqXHR, textStatus, errorThrown) {
            console.log(errorThrown);
            $(".AddInvoiceLoader").addClass('hidden');
        }
    });

}


$(document).ready(function () {
 
  
    //$("#CustomerInvoiceTab tbody").on('focusout', 'input.ProductName', function () {
    //    $(".tt-menu").hide();
    //});
    OrderDatePicker = new Pikaday({
        field: $('.OrderDate')[0],
        //trigger: $('#ShippingDateArea')[0],
        format: 'MM/DD/YYYY',
        firstDay: 1
    });
    DeliveryDatePicker = new Pikaday({
        field: $('.DeliveryDate')[0],
        //trigger: $('#ShippingDateArea')[0],
        format: 'MM/DD/YYYY',
        firstDay: 1
    });

    $("#discountAmount").change(function () {
        CalculateNewAmount();
    });

    $("#CustomerInvoiceTab tbody").on('blur', 'tr', function (item) {
        if (typeof ($(item.target).parent().parent().attr('data-id')) == 'undefined'
            && typeof ($(item.target).parent().parent().parent().attr('data-id')) == 'undefined') {
            var trdom = $(item.target).parent().parent();
            $(trdom).find("input.ProductName").val('');
            $(trdom).find("span.spnProductName").text('');

            $(trdom).find("input.txtProductDesc").val('');
            $(trdom).find("span.spnProductDesc").text('');

            $(trdom).find("input.txtProductQuantity").val('');
            $(trdom).find("span.spnProductQuantity").text('');

            $(trdom).find("input.txtProductRate").val('');
            $(trdom).find("span.spnProductRate").text('');

            $(trdom).find("input.txtProductAmount").val('');
            $(trdom).find("span.spnProductAmount").text('');
            CalculateNewAmount();
        }
    });
    /*focus table row*/
    $("#CustomerInvoiceTab tbody").on('click', 'tr', function (e) {
        if ($(e.target).hasClass('fa') || $(e.target).hasClass('tt-sug-text') || $(e.target).hasClass('tt-suggestion')) {
            return;
        }
        //console.log(e.target);

        $("#CustomerInvoiceTab tr").removeClass("focusedItem");
        $($(e.target).parent()).addClass("focusedItem");
        $(e.target).find('input').focus();
    });

        
   
    /*Add new row*/
    $("#CustomerInvoiceTab tbody").on('click', 'tr:last', function (e) {
        if ($(e.target).hasClass('fa')) {
            return;
        }
      
        $("#CustomerInvoiceTab tbody tr:last").after(NewEquipmentRow);
        var i = 1;
        $(".CustomerInvoiceTab tbody tr td:first-child").each(function () {
            $(this).text(i);
            i += 1;
        });
    });

    $("#CustomerInvoiceTab tbody").on('click', 'tr', function (e) {
        if ($(e.target).hasClass('fa') || $(e.target).hasClass('tt-sug-text') || $(e.target).hasClass('tt-suggestion')) {
            return;
        }
        //console.log(e.target);
  
            if ($(e.target).hasClass("spnProductName") || $(e.target).hasClass("spnProductDesc")
                || $(e.target).hasClass("spnProductQuantity") || $(e.target).hasClass("spnProductRate")
                || $(e.target).hasClass("spnProductAmount")) {

                $("#CustomerInvoiceTab tr").removeClass("focusedItem");
                $($(e.target).parent().parent()).addClass("focusedItem");
                $(e.target).parent().find('input').focus();
            } else if (e.target.tagName.toUpperCase() == 'INPUT') {
                return;
            }
            else {
                $("#CustomerInvoiceTab tr").removeClass("focusedItem");
                $($(e.target).parent()).addClass("focusedItem");
                $(e.target).find('input').focus();
            }

        
    });



    $("#CustomerInvoiceTab tbody").on('click', 'tr:last', function (e) {
        if ($(e.target).hasClass('fa')) {
            return;
        }
    
        $("#CustomerInvoiceTab tbody tr:last").after(NewEquipmentRow);
        var i = 1;
        $(".CustomerInvoiceTab tbody tr td:first-child").each(function () {
            $(this).text(i);
            i += 1;
        });
    });
    /*Remove last row*/
    $(".CustomerInvoiceTab tbody").on('click', 'tr td i.fa', function (e) {
        $(this).parent().parent().remove();
        if ($(".CustomerInvoiceTab tbody tr").length < 2) {
            $("#CustomerInvoiceTab tbody tr:last").after(NewEquipmentRow);
        }
        var i = 1;
        $(".CustomerInvoiceTab tbody tr td:first-child").each(function () {
            $(this).text(i);
            i += 1;
        });
        CalculateNewAmount();
    });

    $(".CustomerInvoiceTab tbody").on('change', "tr td .txtProductQuantity", function () {
        var ProductQuantityDom = $(this).parent().find('span.spnProductQuantity');
        var productQuantity = $(this).parent().parent().find('input.txtProductQuantity');
        $(ProductQuantityDom).text($(this).val());
        var ProductRateDom = $(this).parent().parent().find('input.txtProductRate');
        if ($(productQuantity).val() > 0) {
            if ($(ProductRateDom).val() != "" && parseFloat($(ProductRateDom).val().trim().replaceAll(',', '')) > 0) {
                var NewProductAmount = $(this).val() * parseFloat($(ProductRateDom).val().trim().replaceAll(',', ''));
                console.log(NewProductAmount);
                var txtProductAmountDom = $(this).parent().parent().find('input.txtProductAmount');
                var spnProductAmountDom = $(this).parent().parent().find('span.spnProductAmount');
                $(txtProductAmountDom).val(NewProductAmount.toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, '$1,'));
                $(spnProductAmountDom).text(NewProductAmount.toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, '$1,'));
                CalculateNewAmount();
            }
        }
        else {
            console.log($(this).parent().find('span.spnProductQuantity').text("1"));
            $(productQuantity).val("1");
            $(ProductQuantityDom).val("1");

            if ($(ProductRateDom).val() != "" && parseFloat($(ProductRateDom).val().trim().replaceAll(',', '')) > 0) {
                var NewProductAmount = $(this).val() * parseFloat($(ProductRateDom).val().trim().replaceAll(',', ''));
                console.log(NewProductAmount);
                var txtProductAmountDom = $(this).parent().parent().find('input.txtProductAmount');
                var spnProductAmountDom = $(this).parent().parent().find('span.spnProductAmount');
                $(txtProductAmountDom).val(NewProductAmount.toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, '$1,'));
                $(spnProductAmountDom).text(NewProductAmount.toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, '$1,'));
                CalculateNewAmount();
            }
        }

    });
    $(".CustomerInvoiceTab tbody").on('change', "tr td .txtProductAmount", function () {

        console.log("Product Amount Change");
        var ProductAmount = $(this).parent().parent().find('input.txtProductAmount');

        var ProductQuantityDom = $(this).parent().parent().parent().find('input.txtProductQuantity');
        var ProductRateDom = $(this).parent().parent().parent().find('input.txtProductRate');
        var spnProductRateDom = $(this).parent().parent().parent().find('span.spnProductRate');
        if ($(ProductAmount).val() != "" && parseFloat($(ProductAmount).val().trim().replaceAll(',', '')) > 0) {
            var ProductAmountDom = $(this).parent().parent().find('span.spnProductAmount');
            $(ProductAmountDom).text("$" + parseFloat($(this).val().trim().replaceAll(',', '')).toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, '$1,'));
            if ($(ProductQuantityDom).val() != "" && $(ProductQuantityDom).val() > 0) {
                var NewProductRate = (parseFloat($(this).val().trim().replaceAll(',', '')) / $(ProductQuantityDom).val());
                $(ProductRateDom).val(NewProductRate.toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, '$1,'));
                $(spnProductRateDom).text("$" + NewProductRate.toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, '$1,'));
            }
            CalculateNewAmount();
        }
        else {
            var CalculateAmount = parseFloat($(ProductRateDom).val().trim().replaceAll(',', '')) * $(ProductQuantityDom).val();
            $(ProductAmount).val(CalculateAmount);
            var ProductAmountDom = $(this).parent().parent().find('span.spnProductAmount');
            $(ProductAmountDom).text("$" + parseFloat($(this).val().trim().replaceAll(',', '')).toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, '$1,'));
            if ($(ProductQuantityDom).val() != "" && $(ProductQuantityDom).val() > 0) {
                var NewProductRate = (parseFloat($(this).val().trim().replaceAll(',', '')) / $(ProductQuantityDom).val());
                $(ProductRateDom).val(NewProductRate.toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, '$1,'));
                $(spnProductRateDom).text("$" + NewProductRate.toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, '$1,'));
            }
            CalculateNewAmount();
        }
    });
    $(".CustomerInvoiceTab tbody").on('change', "tr td .txtProductRate", function () {
        /*
        *If product rate changes make change to amount.
        */
        var ProductQuantityDom = $(this).parent().parent().parent().find('input.txtProductQuantity');
        var ProductRate = $(this).parent().parent().find('input.txtProductRate');
        var txtProductAmountDom = $(this).parent().parent().find('input.txtProductAmount');
        if ($(ProductRate).val() != "" && parseFloat($(ProductRate).val().trim().replaceAll(',', '')) > 0) {
            var ProductRateDom = $(this).parent().parent().find('span.spnProductRate');
            $(ProductRateDom).text("$" + parseFloat($(this).val().trim().replaceAll(',', '')).toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, '$1,'));
            if ($(ProductQuantityDom).val() != "" && $(ProductQuantityDom).val() > 0) {
                var ProductAmount = parseFloat($(this).val().trim().replaceAll(',', '')) * $(ProductQuantityDom).val();


                var txtProductAmountDom = $(this).parent().parent().parent().find('input.txtProductAmount');
                $(txtProductAmountDom).val(ProductAmount.toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, '$1,'));
                var spnProductAmountDom = $(this).parent().parent().parent().find('span.spnProductAmount');
                $(spnProductAmountDom).text("$" + ProductAmount.toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, '$1,'));
                CalculateNewAmount();
            }
        }
        else {

            var txtProductAmountDom = $(this).parent().parent().parent().find('input.txtProductAmount');
            var CalculateRate = parseFloat($(txtProductAmountDom).val().trim().replaceAll(',', '')) / $(ProductQuantityDom).val();
            $(ProductRate).val(CalculateRate);
            var ProductRateDom = $(this).parent().parent().find('span.spnProductRate');
            $(ProductRateDom).text("$" + parseFloat($(this).val().trim().replaceAll(',', '')).toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, '$1,'));
            if ($(ProductQuantityDom).val() != "" && $(ProductQuantityDom).val() > 0) {
                var ProductAmount = parseFloat($(this).val().trim().replaceAll(',', '')) * $(ProductQuantityDom).val();


                var txtProductAmountDom = $(this).parent().parent().parent().find('input.txtProductAmount');
                $(txtProductAmountDom).val(ProductAmount.toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, '$1,'));
                var spnProductAmountDom = $(this).parent().parent().parent().find('span.spnProductAmount');
                $(spnProductAmountDom).text("$" + ProductAmount.toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, '$1,'));
                CalculateNewAmount();
            }
        }
    });
    $(".CustomerInvoiceTab tbody").on('change', "tr td .txtProductDesc", function () {

        var ProductQuantityDom = $(this).parent().find('span.spnProductDesc');
        $(ProductQuantityDom).text($(this).val());
    });
    $(".InvoiceSaveButton").click(function () {

        if ($(".HasItem").length == 0) {
            OpenErrorMessageNew("Error!", "You have to select at least one equipment to proceed", "");
        } else {
            SaveInvoice(false, true, "others", null);
            var Invoval = $("#Invoice_Status").val();
            if (Invoval == "Cancel") {
                $.ajax({
                    url: "/Invoice/ConvertInvoiceStatus/",
                    data: { Invoval, Invoice_int_Id },
                    type: "Post",
                    dataType: "Json",
                    success: function (data) {
                        if (data == true) {
                            CloseTopToBottomModal();
                            OpenInvoiceTab();
                        }
                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                        console.log(errorThrown);
                    }
                })
            }
        }
    });
    /*Remove last row*/
    //$(".CustomerInvoiceTab tbody").on('click', 'tr td i.fa', function (e) {
       
    //    $(this).parent().parent().remove();
    //    if ($(".CustomerInvoiceTab tbody tr").length < 2) {
    //        $("#CustomerInvoiceTab tbody tr:last").after(NewEquipmentRow);
    //    }
    //    var i = 1;
    //    $(".CustomerInvoiceTab tbody tr td:first-child").each(function () {
    //        $(this).text(i);
    //        i += 1;
    //    });
    //    //CalculateNewAmount();
    //});

});
