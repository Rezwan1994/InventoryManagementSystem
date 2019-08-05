var SessionCheckerTimeOut = (1000 * 60);
var SessionChecker = function () {
    setTimeout(function () {
        SessionChecker();
    }, SessionCheckerTimeOut);
    $.ajax({
        type: "POST",
        url: "/App/SessionChecker",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.result == false) {
                OpenErrorMessageNew("Error!", data.message, function () {
                    location.href = "/login";
                });
            } else if (data.result==true) {
                $(".notification_counter").text(data.notificationCount);
                if (data.notificationCount > 0) {
                    $(".notification_counter").removeClass("hidden");
                } else {
                    $(".notification_counter").addClass("hidden");
                }

                if (data.IsClockedIn) {
                    $(".ClockInOutIconLi").removeClass("clock_out_color");
                    $(".ClockInOutIconLi").addClass("clock_in_color"); 
                } else {
                    $(".ClockInOutIconLi").removeClass("clock_in_color");
                    $(".ClockInOutIconLi").addClass("clock_out_color");
                }
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            OpenErrorMessageNew("Error!", "Your session has been timed out. Please login.", function () {
                location.href = "/login";
            });
        }
    });
}
//SessionChecker();