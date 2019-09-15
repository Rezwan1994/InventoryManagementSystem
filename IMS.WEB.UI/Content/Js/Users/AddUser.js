
var SaveUsers = function () {
    console.log($("#UserIdVal").val());
        var Param = {
            "Id": $("#IdVal").val(),
            "UserType": $("#UserType").val(),
            "Mobile": $(".Mobile").val(),
            "Email": $(".Email").val(),
            "Address": $(".Address").val(),
            "Name": $(".Name").val()
        };
        console.log(Param)
        var url = "/Users/SaveUser";
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
                    OpenSuccessMessageNew("Success !", "User saved successfully", function () {

                        location.href = "/Users/LoadUserDetails?id=" + data.UserId;
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

  

});