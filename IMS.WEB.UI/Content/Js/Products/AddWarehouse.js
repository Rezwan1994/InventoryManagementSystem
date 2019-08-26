
var SaveUsers = function () {
    console.log($("#UserIdVal").val());
    var Param = {
        "Id": $("#IdVal").val(),
        "WarehouseName": $("#WarehouseName").val(),
        "Address": $("#Address").val(),
        "Description": $("#Description").val(),
    };
    console.log(Param)
    var url = "/Products/SaveWarehouse";
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
                OpenSuccessMessageNew("Success !", "Warehouse saved successfully", function () {
                   // $(".ListContents").load("/Products/LoadWarehouseList");
                    $("#LoadWarehouseTab").load("/Products/LoadWarehousePartial");
                    OpenRightToLeftModal();
                });
            }

            else {
                OpenErrorMessageNew("Error!", "Please check your given input.");
                //CloseTopToBottomModal();
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            console.log(errorThrown);
        }
    });
}

$(document).ready(function () {
    $("#saveUsers").click(function () {
        if (CommonUiValidation()) {
            SaveUsers();
        }
    })

});