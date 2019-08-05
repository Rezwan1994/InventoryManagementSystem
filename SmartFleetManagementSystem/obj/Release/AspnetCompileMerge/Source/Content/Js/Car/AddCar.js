var CloseDatepicker;
var Id = $("#IdVal").val();
var CarId = $("#CarIdVal").val();
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
                                    text: item.Name ,
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
var InitializeOwnerDropdown = function (dropdownitem) {
    $(dropdownitem).select2({
        placeholder: 'Customer',
        allowClear: true,
        minimumInputLength: 1,
        ajax: {
            url: "/Car/GetOwnerList",
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
                                id: item.UserId
                            }
                        }
                        else if (typeof (item.ConcernName) != "undefined") {
                            return {
                                text: item.ConcernName,
                                id: item.ConcernId
                            }
                        }
                        else {
                            return {
                                text: item.Email,
                                id: item.UserId
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
var InitializeConcernDropdown = function (dropdownitem) {
    $(dropdownitem).select2({
        placeholder: 'Concern',
        allowClear: true,
        minimumInputLength: 1,
        ajax: {
            url: "/Car/GetConcernList",
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

                        if (typeof (item.ConcernName) != "undefined") {
                            return {
                                text: item.ConcernName,
                                id: item.ConcernId
                            }
                        }
                        else {
                            return {
                                text: item.Email,
                                id: item.ConcernId
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
var YearOfMfg;
var SaveVehicle = function () {

    if (CommonUiValidation()) {
        var IsForeCast = false;
        if ($("#IsForecast").val() == "Yes") {
            IsForeCast = true;
        }
        console.log($("#CarIdVal").val());
        var Param = {
        
            "Id": $("#IdVal").val(),
            "CarId": $("#CarIdVal").val(),
            "CompanyId": $("#Concern").val(),
            "Make": $("#Make").val(),
            "RegId": $("#RegId").val(),
            "LicenseNo": $("#LicenseNo").val(),
            "Model": $("#Model").val(),
            "YearOfMfg": $('#YearOfMfg').val(),
            "Status": $("#Status").val(),
            "InitialOdometer": $("#InitialOdometer").val(),
            "InitialVolume": $("#InitialVolume").val(),
            "CC": $("#CC").val(),
            "Financier": $("#Financier").val(),
            "FuelSystem": $("#FuelSystem").val(),
            "ChassisNo": $("#ChassisNo").val(),
            "Type": $('#VehicleType').val(),
            "SubType": $('#VehicleSubType').val(),
            "Capacity": $('#Capacity').val(),
   
        };
        var userCar = {
            "UserId": $("#LigalOwner").val(),
            "Note": $("#UserNote").val()
        }
        var PassParam = {
            "car": Param,
            "userCar": userCar,
      

        }
        console.log(Param)
        var url = "/Car/SaveVehicle";
        $.ajax({
            type: "POST",
            ajaxStart: function () { },
            url: url,
            data: JSON.stringify(PassParam),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            cache: false,
            success: function (data) {
                console.log(data);
                if (data.result == true) {
                    OpenSuccessMessageNew("Success !", "Vehicle saved successfully", function () {
                        $(".ListContents").load("/Car/LoadCarList");
                        CloseTopToBottomModal();
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

}
$(document).ready(function () {

    InitializeAllocationDropdown($('.dropdown_allocation'));
    InitializeOwnerDropdown($('.dropdown_owner'));
    InitializeConcernDropdown($('.dropdown_concern'));
   
    $("#Status").val(Status);
    if ($("#VehicleType").val() == "Commercial") {
        $("#SubType").show();
        $("#CapacityList").show();
    }
    else {
        $("#SubType").hide();
        $("#CapacityList").hide();
        $("#Capacity").val("-1");
        $("#VehicleSubType").val("-1");
    }

    $("#VehicleType").change(function () {
        if ($("#VehicleType").val() == "Commercial") {
            $("#SubType").show();
            $("#CapacityList").show();
        }
        else {
            $("#SubType").hide();
            $("#CapacityList").hide();
            $("#Capacity").val("-1");
            $("#VehicleSubType").val("-1");
        }
    })


    $("#AddDriver").click(function () {
        OpenRightToLeftModal("/car/AddDriver?CarId=" + CarId);

    })

 
    if(Id != 0)
    {
        $(".LoadDriverList").load("Car/LoadAllAssignedDriver?Id=" + Id);
        $(".LoadDocumentList").load("/Car/LoadVehicleDocumentList/?CarId=" + CarId)
    }



});
