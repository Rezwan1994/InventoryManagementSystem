
var SaveUsers = function () {
    console.log($("#UserIdVal").val());
    var Param = {
        "Id": $("#IdVal").val(),
        "ProductName": $("#ProductName").val(),
        "Category": $("#Category").val(),
        "SubCategory": $("#SubCategory").val(),
        "BuyingPrice": $("#BuyingPrice").val(),
        "SellingPrice": $("#SellingPrice").val(),
        "Quantity": $("#Quantity").val(),
        "ImageUrl": $("#UploadedPath").val()
    };
    console.log(Param)
    var url = "/Products/SaveProduct";
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
                OpenSuccessMessageNew("Success !", "Product saved successfully", function () {
                    $(".loadProductlist").load("/Products/LoadProductList");
                    OpenRightToLeftModal();
                });
            }

            else {
                OpenErrorMessageNew("Error!", "Please check your given input.");
                CloseTopToBottomModal();
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
    if ($("#UploadedPath").val() != "" || $("#UploadedPath").val() != null) {
        $("#UploadSuccessMessage").removeClass('hidden');
        //$("#UploadCustomerFileBtn").addClass('hidden');
        var spfile = $("#UploadedPath").val().split('.');
        //if (spfile[1] == "png" || spfile[1] == "jpg" || spfile[1] == "jpeg") {
        //    $(".Upload_Doc").addClass('hidden');

        //    $(".LoadPreviewDocument").removeClass('hidden');
        //    $("#Preview_Doc").attr('src', $("#UploadedPath").val());
        //}
        $(".fileborder").removeClass('red-border');
        $("#uploadfileerror").addClass("hidden");

        var index = spfile.length - 1;
        if (spfile[index] == "png" || spfile[index] == "PNG" || spfile[index] == "jpg" || spfile[index] == "JPG" || spfile[index] == "jpeg" || spfile[index] == "JPEG") {
            //$(".Upload_Doc").addClass('hidden');
            //$(".LoadPreviewDocument").removeClass('hidden');
            //$("#Preview_Doc").attr('src', $("#UploadedPath").val());
            $("#UploadCustomerFileBtn").attr('src', $("#UploadedPath").val())
            $(".chooseFilebtn").addClass("hidden");
            $(".changeFilebtn").removeClass("hidden");
            $(".deleteDoc").removeClass("hidden");
            $("#UploadCustomerFileBtn").addClass('custom-file');
            $("#UploadCustomerFileBtn").removeClass('otherfileposition');
            $(".fileborder").addClass('border_none');
        }
        else if (spfile[index] == "pdf") {
            $(".chooseFilebtn").addClass("hidden");
            $(".changeFilebtn").removeClass("hidden");
            $(".deleteDoc").removeClass("hidden");
            $("#UploadCustomerFileBtn").attr('src', '/Content/Icons/pdf.png');
            $("#UploadCustomerFileBtn").addClass('otherfileposition');
            $("#UploadCustomerFileBtn").removeClass('custom-file');
            $(".fileborder").removeClass('border_none');
        }
        else if (spfile[index] == "doc" || spfile[index] == "docx") {
            $(".chooseFilebtn").addClass("hidden");
            $(".changeFilebtn").removeClass("hidden");
            $(".deleteDoc").removeClass("hidden");
            $("#UploadCustomerFileBtn").attr('src', '/Content/Icons/docx.png');
            $("#UploadCustomerFileBtn").addClass('otherfileposition');
            $("#UploadCustomerFileBtn").removeClass('custom-file');
            $(".fileborder").removeClass('border_none');
        }
        else if (spfile[index] == "mp4" || spfile[index] == "mov") {
            $(".chooseFilebtn").addClass("hidden");
            $(".changeFilebtn").removeClass("hidden");
            $(".deleteDoc").removeClass("hidden");
            $("#UploadCustomerFileBtn").attr('src', '/Content/Icons/mp4.png');
            //$("#UploadCustomerFileBtn").addClass('otherfileposition');
            //$("#UploadCustomerFileBtn").removeClass('custom-file');
            $(".fileborder").removeClass('border_none');
        }
        //else {
        //    $(".Upload_Doc").addClass('hidden');
        //    $(".LoadPreviewDocument").addClass('hidden');
        //    $(".LoadPreviewDocument1").removeClass('hidden');
        //    $("#Frame_Doc").attr('src', $("#UploadedPath").val());
        //}
    }

});