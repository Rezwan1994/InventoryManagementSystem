var DoLogin = function () {

    if (CommonUiValidation()) {

        var Param = {
            "UserName": $("#UserName").val(),
            "Password": $("#Pass").val(),
          
        };


        var url = "/Home/LoginAjax";
        $.ajax({
            type: "POST",
            ajaxStart: function () { },
            url: url,
            data: JSON.stringify(Param),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            cache: false,
            success: function (data) {
                if(data.result == true)
                {
                    window.location.href = "/Home/DashBoard";
                }
                else
                {
                    alert("Incorrect username or password!")
                }
               

            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.log(errorThrown);
            }
        });
    }
}

$(document).ready(function () {
    $("#Login").click(function () {
        DoLogin();
    })

})