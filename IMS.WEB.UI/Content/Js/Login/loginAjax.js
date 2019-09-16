var DoLogin = function () {

    if (CommonUiValidation()) {

        var Param = {
            "UserName": $("#UserName").val(),
            "Password": $("#Pass").val(),
          
        };
        $(".LoaderLoginDiv").show();

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
                    $("#Pass").val('');
                    window.location.href = "/dashboards";
                    //$(".LoaderLoginDiv").hide();
                }
                else
                {
                    $("#Pass").val('');
                    alert("Incorrect username or password!")
                    $(".LoaderLoginDiv").hide();
                }
               

            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.log(errorThrown);
                $("#Pass").val('');
                $(".LoaderLoginDiv").hide();
            }
        });
    }
}

$(document).ready(function () {
    $('#Pass').keypress(function (event) {

        var keycode = (event.keyCode ? event.keyCode : event.which);
        if (keycode == '13') {
            DoLogin();
        }
        event.stopPropagation();
    });
    $("#Login").click(function () {
        DoLogin();
    })

})