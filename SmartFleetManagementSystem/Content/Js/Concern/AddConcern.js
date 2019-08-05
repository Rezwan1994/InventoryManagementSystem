var SaveConcern = function () {

    if (CommonUiValidation() ) {
    
        var Param = {
            "Id": $("#IdVal").val(),
            "ConcernName": $("#ConcernName").val(),
            "ConcernType": $("#ConcernType").val(),
            "Address": $("#Address").val(),
            "Phone": $("#Phone").val(),
        
        };


        var url = "/Concern/SaveConcern";
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
                        $(".ListContents").load("/Concern/LoadConcernList");
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
}
    

