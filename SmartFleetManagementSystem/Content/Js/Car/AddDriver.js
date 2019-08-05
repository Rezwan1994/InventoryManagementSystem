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
var SaveDriver = function () {


        console.log($("#CarIdVal").val());
        var Param = {

            "Id": $(".DId").text(),
            "CarId":$(".DCarId").text(),
            "DriverId": $("#Allocation").val(),
            "Note": $("#DriverNote").val()
            

        };
        console.log(Param);
        var url = "/Car/SaveDriver";
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
                        console.log("fskdfj");
                        $(".LoadDriverList").load("Car/LoadAllAssignedDriver?Id=" + Id);
                        $(".close").click();
                 
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


$(document).ready(function () {

    InitializeAllocationDropdown($('.dropdown_allocation'));
    $("#SaveDriver").click(function () {
        SaveDriver();
    })


});