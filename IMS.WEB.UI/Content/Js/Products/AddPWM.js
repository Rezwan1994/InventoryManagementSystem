var InitializeWarehouseDropdown = function (dropdownitem) {
    $(dropdownitem).select2({
        placeholder: 'Products',
        allowClear: true,
        minimumInputLength: 1,
        ajax: {
            url: "/Products/GetWarehousesList",
            dataType: 'json',
            type: "GET",
            quietMillis: 50,
            data: function (term) {
                return {
                    q: term
                };
            },
            processResults: function (data, params) {
                return {
                    results: $.map(data, function (item) {

                        return {
                            text: item.WarehouseName,
                            id: item.WarehouseId
                        }
                    })
                };
            }
        }
    });
    $(dropdownitem).on("select2:closing", function (e) {

    });


}

var InitializeProductDropdown = function (dropdownitem) {
    $(dropdownitem).select2({
        placeholder: 'Products',
        allowClear: true,
        minimumInputLength: 1,
        ajax: {
            url: "/Products/GetProductsList",
            dataType: 'json',
            type: "GET",
            quietMillis: 50,
            data: function (term) {
                return {
                    q: term
                };
            },
            processResults: function (data, params) {
                return {
                    results: $.map(data, function (item) {

                        return {
                            text: item.ProductName + " (" + item.Category + " - " + item.SubCategory + ")" + " **Remains : " + item.RemainQantity ,
                            id: item.ProductId
                        }
                    })
                };
            }
        }
    });
    $(dropdownitem).on("select2:closing", function (e) {

    });


}



var SaveUsers = function () {
    console.log($("#UserIdVal").val());
    var Param = {
        "Id": $("#IdVal").val(),
        "WarehouseId": $("#Warehouses").val(),
        "ProductId": $("#Products").val(),
        "Quantity": $("#Quantity").val(),
    };
    console.log(Param)
    var url = "/Products/SavePWM";
    $.ajax({
        type: "POST",
        ajaxStart: function () { },
        url: url,
        data: JSON.stringify(Param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        cache: false,
        success: function (data) {
            console.log(data);
            if (data.result == true) {
                OpenSuccessMessageNew("Success !", "Product Warehouse mapped successfully", function () {
                    // $(".ListContents").load("/Products/LoadWarehouseList");
                    $("#LoadProductWarehouseTab").load("/Products/LoadPWMPartial");
                    OpenRightToLeftModal();
                });
            }

            else {
                OpenErrorMessageNew("Error!", data.message);
                //CloseTopToBottomModal();
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            console.log(errorThrown);
        }
    });
}

$(document).ready(function () {
    InitializeWarehouseDropdown($('.dropdown_WarehouseList'));
    InitializeProductDropdown($('.dropdown_ProductList'));
    $("#saveUsers").click(function () {
        if (CommonUiValidation()) {
            SaveUsers();
        }
    })

});