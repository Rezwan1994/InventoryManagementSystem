var LoaderDom = "<div class='invoice-loader'><div class='uil-squares-css' style='transform:scale(1); margin:auto;'><div><div></div></div><div><div></div></div><div><div></div></div><div><div></div></div><div><div></div></div><div><div></div></div><div><div></div></div><div><div></div></div></div></div>";
var IsInventoryFilterCookie = false;
var IsServiceFilterCookie = false;
var LoadUrlContents = function (loadurl, seturl, reload) {
    if (typeof (reload) == "undefined" || reload == null) {
        reload = false;
    }
    if ((location.href.toLowerCase().indexOf(seturl.toLowerCase()) > -1) && !reload) {
        return true;
    }
    $(".LoaderWorkingDiv").show();
    $(".page-wrapper-contents").html("");
    setTimeout(function () {
        $(".page-wrapper-contents").load(loadurl);
    }, 50);
    history.pushState(null, null, seturl);
}
String.prototype.replaceAll = function (search, replacement) {
    var target = this;
    return target.split(search).join(replacement);
};
Date.prototype.addDays = function (days) {
    this.setDate(this.getDate() + parseInt(days));
    return this;
};
Date.prototype.getWeek = function () {
    var date = new Date(this.getTime());
    date.setHours(0, 0, 0, 0);
    // Thursday in current week decides the year.
    date.setDate(date.getDate() + 3 - (date.getDay() + 6) % 7);
    // January 4 is always in week 1.
    var week1 = new Date(date.getFullYear(), 0, 4);
    // Adjust to Thursday in week 1 and count number of weeks from date to week1.
    return 1 + Math.round(((date.getTime() - week1.getTime()) / 86400000 - 3 + (week1.getDay() + 6) % 7) / 7);
}
Date.prototype.toFormatedDate = function (days) {
    var Month = this.getMonth();
    var Date = this.getDate();
    var Year = this.getYear();

    if (Month < 10)
        Month = '0' + Month;
    if (Date < 10)
        Date = '0' + Date;

    return Month + '/' + Date + '/' + Year;
};
var ClosePopup = function () {
    $.magnificPopup.close();
}
var getDateOfISOWeek = function (w, y) {
    var simple = new Date(y, 0, 1 + (w - 1) * 7);
    var dow = simple.getDay();
    var ISOweekStart = simple;
    if (dow <= 4)
        ISOweekStart.setDate(simple.getDate() - simple.getDay() + 1);
    else
        ISOweekStart.setDate(simple.getDate() + 8 - simple.getDay());

    if (FirstDayOfWeek == 'Saturday') {
        return ISOweekStart.addDays(-2);
    }
    else if (FirstDayOfWeek == 'Sunday') {
        return ISOweekStart.addDays(-1);
    }

    return ISOweekStart;
}
var LoadDashboard = function (reload) {
    var loadurl = "/Home/Index";
    var seturl = "/dashboards";
    LoadUrlContents(loadurl, seturl, reload);
}
var LoadVehicle= function (reload) {
    var loadurl = "/Car/Index";
    var seturl = "/Vehicles";
    LoadUrlContents(loadurl, seturl, reload);
}

var LoadProducts = function (reload) {
    var loadurl = "/Products/Index";
    var seturl = "/Vehicles";
    LoadUrlContents(Products, seturl, reload);
}

var LoadDrivers = function (reload) {
    var loadurl = "/Driver/Index";
    var seturl = "/Drivers";
    LoadUrlContents(loadurl, seturl, reload);
}
var LoadConcerns = function (reload) {
    var loadurl = "/Concern/Index";
    var seturl = "/Concerns";
    LoadUrlContents(loadurl, seturl, reload);
}
var LoadReports = function (reload) {
    var loadurl = "/Report/Index";
    var seturl = "/Reports";
    LoadUrlContents(loadurl, seturl, reload);
}
var LoadActivities = function (reload) {
    var loadurl = "/Activity/Activities";
    var seturl = "/Activities";
    LoadUrlContents(loadurl, seturl, reload);
}

var LoadGlobalSearchByKey = function (reload, key) {
    var loadurl = "/App/GlobalSearchResult/?key=" + key;
    var seturl = "/Search/?key=" + key;
    LoadUrlContents(loadurl, seturl, reload);
}

var LoadAdvancedSearchByKey = function (reload, key) {
    var loadurl = "/App/GlobalSearchResult/?key=" + key;
    var seturl = "/Search/?key=" + key;
    LoadUrlContents(loadurl, seturl, reload);
}

var LoadReport = function (reload) {
    var loadurl = "/Reports/ReportsPartial";
    var seturl = "/Reports";
    LoadUrlContents(loadurl, seturl, reload);
}

var LoadUsers = function (reload) {
    var loadurl = "/Users/Index";
    var seturl = "/Users";
    LoadUrlContents(loadurl, seturl, reload);
}

var LoadAllNotifications = function () {
    var loadurl = "/Notification/AllNotifications";
    var seturl = "/Notifications";
    LoadUrlContents(loadurl, seturl, true);
    setTimeout(function () {
        $(".navbar-right .dropdown").removeClass('open');
        $(".flyout-overlay").hide();
    }, 80);
}

var GlobalSearchSuggestionTemplate =
    "<div class='tt-suggestion tt-selectable' data-select='{7}' data-type='{5}' data-id='{0}' data-description='{6}'>"
    + "<span class='EquipmentImage'>{7}</span>"
    + "<p class='tt-sug-text'>"
    + "<em class='tt-sug-type'>{5}</em>{1}"
    + "<em class='tt-eq-price'>{2}</em>"
    + "</p> "
    + "</div>";

var PropertySuggestionclickbind = function (item) {
    $('.GlobalEstInvCustSearchDiv .tt-suggestion').click(function () {
        var clickitem = this;
        $('.GlobalEstInvCustSearchDiv .tt-menu').hide();
        $(item).val($(clickitem).attr('data-select'));
        $(item).attr('data-id', $(clickitem).attr('data-id'));
        GlobalSearchButtonClick();
    });
    $('.GlobalEstInvCustSearchDiv .tt-suggestion').hover(function () {
        var clickitem = this;
        $('.GlobalEstInvCustSearchDiv .tt-suggestion').removeClass("active");
        $(clickitem).addClass('active');
    });
}
var GlobalSearchKeyUp = function (item, event) {
    console.log(event.keyCode);
    if (event.keyCode == 40 || event.keyCode == 38 || event.keyCode == 13)
        return false;
    $.ajax({
        url: "/App/GetGlobalSearchByKey",
        data: {
            key: $(item).val()
        },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            var resultparse = JSON.parse(data.result);

            if (resultparse.length > 0) {
                var searchresultstring = "<div class='NewGlobalSuggestion'>";
                for (var i = 0; i < resultparse.length; i++) {
                    searchresultstring = searchresultstring + String.format(GlobalSearchSuggestionTemplate,
                        /*0*/resultparse[i].EquipmentId,
                        /*1*/ resultparse[i].PhoneNumber,
                        /*2*/ resultparse[i].EmailAddress,
                        /*3*/resultparse[i].Reorderpoint,
                        /*4*/ resultparse[i].QuantityAvailable,
                        /*5*/ resultparse[i].Type,
                        /*6*/resultparse[i].EquipmentDescription,
                        /*7*/resultparse[i].Name);
                }
                searchresultstring += "</div>";
                var ttdom = $($(item).parent()).find('.tt-menu');
                var ttdomComplete = $($(item).parent()).find('.tt-dataset-autocomplete');
                $(ttdomComplete).html(searchresultstring);
                $(ttdom).show();

                PropertySuggestionclickbind(item);
                if (resultparse.length > 4) {
                    $(".NewGlobalSuggestion").height(285);
                    $(".NewGlobalSuggestion").css('position', 'relative');
                    $(".NewGlobalSuggestion").perfectScrollbar()
                }
            }
            if (resultparse.length == 0)
                $('.tt-menu').hide();
        }
    });
}
var GlobalSearchKeyDown = function (item, event) {

    if (event.keyCode == 13) {
        $('.tt-menu').hide();
    }
    if (event.keyCode == 40) {
        if ($('.tt-suggestion').length > 0) {
            if ($('.tt-suggestion.active').length == 0) {
                $($('.tt-suggestion').get(0)).addClass('active');
                $(item).val($($('.tt-suggestion').get(0)).attr('data-select'))
            }
            else {
                var suggestionlist = $('.tt-suggestion');
                var activesuggestion = $('.tt-suggestion.active');
                var indexactive = -1;
                for (var id = 0; id < suggestionlist.length; id++) {
                    if ($(suggestionlist[id]).hasClass('active'))
                        indexactive = id;
                }
                if (indexactive < suggestionlist.length - 1) {
                    $('.tt-suggestion').removeClass('active');
                    var possibleactive = $('.tt-suggestion').get(indexactive + 1);
                    $($('.tt-suggestion').get(indexactive + 1)).addClass('active');
                    $(possibleactive).addClass('active');
                    $(item).val($(possibleactive).attr('data-select'));
                }
            }
        }
        event.preventDefault();
    }
    if (event.keyCode == 38) {
        if ($('.tt-suggestion').length > 0 && $('.tt-suggestion.active').length > 0) {
            var suggestionlist = $('.tt-suggestion');
            var activesuggestion = $('.tt-suggestion.active');
            var indexactive = -1;
            for (var id = 0; id < suggestionlist.length; id++) {
                if ($(suggestionlist[id]).hasClass('active'))
                    indexactive = id;
            }
            if (indexactive > 0) {
                $('.tt-suggestion').removeClass('active');
                var possibleactive = $('.tt-suggestion').get(indexactive - 1);
                $(possibleactive).addClass('active');
                $(item).val($(possibleactive).attr('data-select'))
            }
        }
        event.preventDefault();
    }
}

var CheckValue = function (component, text) {
    if (component.value == text)
        component.value = "";
    else if (component.value == "")
        component.value = text;
}
var GlobalSearchButtonClick = function () {
    //var GlobalSearchtext = $(".GlobalSearchInp").val().split(' ')[0];
    var GlobalSearchtext = encodeURIComponent($(".GlobalSearchInp").val());
    if (GlobalSearchtext != "") {
        LoadGlobalSearchByKey(true, GlobalSearchtext);
    }
}
var initHeight = function () {
    var contentHeight = window.innerHeight - ($(".navbar.navbar-inverse.navbar-fixed-top").height() + 5);
    if (Device.MobileGadget()) {
        contentHeight = window.innerHeight - 54;
    }
    $("#page-wrapper").height(contentHeight);
    $("#page-wrapper").css("overflow-y", "scroll");

}

var initMenuClicks = function () {
    $("#LoadDashBoard").click(function () {
        LoadDashboard(false);
    });
    $("#LoadSettings").click(function () {
        LoadSettings(false);
    });
    $("#LoadReport").click(function () {
        LoadReport(false);
    });
    $("#LoadFuelSystems").click(function () {
        LoadFuelSystems(false);
    });
    $("#LoadActivities").click(function () {
        LoadUsers(false);
    });
}
var LoadNotifications = function () {
    $(".LoadNotification_div").html("<div class='notification_loader_container'><div class='notification_loader'></div></div>");
    $(".LoadNotification_div").load("/Notification/GetUserNotifications");
}
var MarkAllNotificationAsRead = function () {
    $.ajax({
        type: "POST",
        url: "/Notification/MarkAllNotificationAsRead",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.result == true) {
                $(".notification_counter").text('0');
                $(".notification_counter").addClass("hidden");
            }
        }
    });
}
var initNavbarClicks = function () {
    $(".navbar-right .dropdown .AdvanceSearchSearchBarDiv").click(function (event) {
        event.stopPropagation();
        console.log(event.target.id);
    });
    $(".navbar-right .dropdown").click(function (event) {
        if (($(this).hasClass('open') && $(event.target).hasClass('dropdown-toggle')) ||
            ($(this).hasClass('open') && $(event.target).hasClass('open')) ||
            ($(this).hasClass('open') && $(event.target).hasClass('fa'))) {
            $(this).removeClass('open');
            $(".flyout-overlay").hide();
        } else if ($(this).parent().children('.dropdown').hasClass('open')) {
            $(this).parent().children('.dropdown').removeClass('open');
            $(this).addClass('open');
        } else {
            $(".flyout-overlay").show();
            $(this).addClass('open');
            if ($(this).hasClass('notification_dropdown')) {
                LoadNotifications();
            }
        }
    });
    $(".flyout-overlay, .submenuelement a").click(function () {
        setTimeout(function () {
            $(".navbar-right .dropdown").removeClass('open');
            $(".flyout-overlay").hide();
        }, 80);
    });

}
var AdvancedSearchButtonClick = function () {
    if ($("#AdvancedSearchText").val() == "") {
        return;
    }
    $(".navbar-right .dropdown").removeClass('open');
    $(".flyout-overlay").hide();
    var SearchKey = encodeURIComponent($("#AdvancedSearchText").val());
    SearchKey += "&SearchFor=" + $("#AdvancedSearchOptions").val();
    LoadAdvancedSearchByKey(true, SearchKey);
}
var OpenEstById = function (invId) {
    if (typeof (invId) != "undefined" && invId > 0) {
        OpenTopToBottomModal("/Estimate/AddEstimate/?Id=" + invId);
    }
}
var OpenInvById = function (invId) {
    if (typeof (invId) != "undefined" && invId > 0) {
        OpenTopToBottomModal("/Invoice/AddInvoice/?Id=" + invId);
    }
}
var OpenTicketById = function (ticketId) {
    if (typeof (ticketId) != "undefined" && ticketId > 0) {
        OpenTopToBottomModal("/Ticket/AddTicket/?Id=" + ticketId);
    }
}
var ShowCustomerChange = function (CustomerId) {
    window.location.href = "/Customer/ShowCustomerChange?CustomerId=" + CustomerId;
}
var OpenUrl = function (newurl) {
    if (typeof (newurl) != "undefined" && newurl != "") {
        location.href = newurl;
    }
}

var cntrlIsPressed = false;
$(document).keydown(function (event) {
    if (event.which == "17")
        cntrlIsPressed = true;
});
$(document).keyup(function (event) {
    cntrlIsPressed = false;
});

$(document).ready(function () {
    $('ul li a').click(function () {
        $('li a').removeClass("focus");
        $(this).addClass("focus");
    });

    $("#AdvancedSearchBtn").click(function () {
        AdvancedSearchButtonClick();
    });
    $("#AdvancedSearchText").bind('keypress', function (e) {
        if (e.keyCode == 13) {
            AdvancedSearchButtonClick();
        }
    });
    initHeight();
    initMenuClicks();
    initNavbarClicks();
    $('ul li a').click(function () {
        $(this).addClass("focus");
    });
    $("#GlobalSearchButton").click(function () {
        $(".GlobalEstInvCustSearchDiv .tt-menu").hide();
        GlobalSearchButtonClick();
    });
    $(document).click(function () { $('.GlobalEstInvCustSearchDiv .tt-menu').hide() })
});
$(window).resize(function () {
    initHeight();
});
