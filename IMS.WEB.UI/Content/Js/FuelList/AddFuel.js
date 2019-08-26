var InitializeAllocationDropdown = function (dropdownitem) {
    $(dropdownitem).select2({
        placeholder: 'Customer',
        allowClear: true,
        minimumInputLength: 1,
        ajax: {
            url: "/Car/GetAllocaionList",
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

                        if (typeof (item.Name) != "undefined") {
                            return {
                                text: item.Name,
                                id: item.DriverId
                            }
                        }
                        else {
                            return {
                                text: item.EmailAddress,
                                id: item.DriverId
                            }
                        }


                    })
                };
            }
        }
    });
    $(dropdownitem).on("select2:closing", function (e) {

    });


}

var InitializeCarDropdown = function (dropdownitem) {
    console.log("ddfsd");
    $(dropdownitem).select2({
        placeholder: 'Vehicles',
        allowClear: true,
        minimumInputLength: 1,
        ajax: {
            url: "/Car/GetCarList",
            dataType: 'json',
            type: "GET",
            quietMillis: 50,
            data: function (term) {
                return {
                    q: term
                };
            },
            processResults: function (data, params) {
                console.log(data);
                return {
                    results: $.map(data, function (item) {

                        if (typeof (item.Make) != "undefined") {
                            return {
                                text: "[" + item.Make + " " + item.Model + "](" + item.RegId + ")",
                                id: item.CarId,
                            }
                        }
                    })
                };
            }
        }
    });
    $(dropdownitem).on("select2:closing", function (e) {

    });


}

var InitializeCarDropdown2 = function (dropdownitem) {
    console.log("ddfsd");
    $(dropdownitem).select2({
        placeholder: 'Vehicles',
        allowClear: true,
        minimumInputLength: 1,
        ajax: {
            url: "/Car/GetCarList",
            dataType: 'json',
            type: "GET",
            quietMillis: 50,
            data: function (term) {
                return {
                    q: term
                };
            },
            processResults: function (data, params) {
                console.log(data);
                return {
                    results: $.map(data, function (item) {

                        if (typeof (item.Make) != "undefined") {
                            return {
                                text: "[" + item.Make + " " + item.Model + "](" + item.RegId + ")",
                                id: item.CarId + ":" + item.InitialOdometer,
                            }
                        }
                    })
                };
            }
        }
    });
    $(dropdownitem).on("select2:closing", function (e) {

    });


}


var SaveDailyFuel = function () {
    console.log("hlw");
    var carId = "";
    var Usage = 0.0;
    var LastReading = $(".lastreading").text();
    if ($("#FuelVehicle").val() != null && $("#FuelVehicle").val() != "" && $("#FuelVehicle").val() != "undefined") {
        var splitArray = $("#FuelVehicle").val().split(":");
        carId = splitArray[0];
    }
    if ($('#Odometer').val() > LastReading) {
        $(".odometererror").removeClass("hidden");
    }
    if (CommonUiValidation() && carId != "" && $('#Odometer').val() > LastReading) {
        Usage = parseFloat($('#Odometer').val()) - parseFloat(LastReading)
        var FuelBill = {
            "Id": $("#IdVal").val(),
            "CarId": carId,
            "DriverId": $("#DriverId").val(),
            "FuelSystem": $("#FuelSystem option:selected").text(),
            "FuelAmount": $("#FuelAmount").val(),
            "UnitPrice": $("#unitprice").attr("data-val"),
            "LastReading": LastReading,
            "Odometer": $('#Odometer').val(),
            "Usage": Usage,
            "DocumentSrc": $("#UploadedPath").val(),
            "VoucherNo": $("#VoucherNo").val(),
            "IssueDate": IssueDate.getDate(),

        };
        var Param = {
            "FuelBill": FuelBill,
            "CarId": CarId
        }
        var url = "/Fuel/SaveDailyFuel";
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
                    OpenSuccessMessageNew("Success !", data.message, function () {
                        $(".ListDailyFuelSystem").load("/Fuel/LoadFuelSystemList");
                        CloseTopToBottomModal();
                    });
                }
                else {
                    OpenErrorMessageNew("Error!", "Please check your given input.");

                }


            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.log(errorThrown);
            }
        });
    }

}
var IssueDate;
$(document).ready(function () {
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

    InitializeCarDropdown($('.dropdown_car'));
    InitializeCarDropdown2($('.dropdown_carfuel'));
    InitializeAllocationDropdown($('.dropdown_allocation'));
    IssueDate = new Pikaday({ format: 'DD/MM/YYYY', maxDate: new Date(), field: $('#IssueDate')[0] });
    IssueDate.setDate(new Date())
    $("#FuelSystem").change(function () {
        var selectedValue = $("#FuelSystem option:selected").text();
        if (selectedValue == "Octane" || selectedValue == "Diesel") {
            var res = $("#FuelSystem").val() + " ৳/Litre";
            $("#unitprice").attr("data-val", $("#FuelSystem").val()) 
            $("#unitprice").val(res);
        }
        else {
            var res = $("#FuelSystem").val() + " ৳/m³";
            $("#unitprice").attr("data-val", $("#FuelSystem").val())
            $("#unitprice").val(res);
        }
    })

    $("#FuelVehicle").change(function () {
        console.log($("#FuelVehicle").val())
        if ($("#FuelVehicle").val() != null && $("#FuelVehicle").val() != "" && $("#FuelVehicle").val() != "undefined") {
            var splitArray = $("#FuelVehicle").val().split(":");
            $(".lastreading").text(splitArray[1]);
        }
        else {
            $(".lastreading").text("0");
        }
    })
})