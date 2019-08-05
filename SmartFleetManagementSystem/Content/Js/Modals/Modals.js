
var DefaultConfirmationfunc = null;
var DefaultConfirmationRejectfunc = null;
var DefaultSuccessfunc = null;
var DefaultErrorfunc = null;
var DefaultCancelCustomerfunc = null;
var OpenConfirmationMessageNew = function (HeaderMessage, BodyMessage, ToDoFunc,RejectFunc) {
    $("#ModalConfirmationMessage .modal-title").text('Confirmation');
     
    $('#ModalConfirmationMessage p').text(BodyMessage);
    if (typeof (ToDoFunc) == "function") {
        DefaultConfirmationfunc = ToDoFunc;
    } else {
        DefaultConfirmationfunc = function () { };
    }

    if (typeof (RejectFunc) == "function") {
        DefaultConfirmationRejectfunc = RejectFunc;
    } else {
        DefaultConfirmationRejectfunc = function () { };
    }
         
    $("#ConfirmationMessageModal").click();
}
var OpenSuccessMessageNew = function (HeaderMessage, BodyMessage, ToDoFunc) {

    $("#ModalSuccessMessage .message_header_title").text("Success!");
    $("#ModalSuccessMessage p").html(BodyMessage);
    if (typeof (ToDoFunc) == "function") {
        console.log("func : " + ToDoFunc);
        DefaultSuccessfunc = ToDoFunc;
        $(".close").unbind();
            
    } else {
        DefaultSuccessfunc = function () { };
    }
    $("#SuccessMessageModal").click();
}
var OpenTextModal = function (HeaderMessage, BodyMessage, ToDoFunc) {
    console.log("dsf");
    $("#ModalOpenText .message_header_title").text("Give Your Verbal Password!");
    $("#ModalOpenText p").text(BodyMessage);
    if (typeof (ToDoFunc) == "function") {
        console.log("func : " + ToDoFunc);
        DefaultSuccessfunc = ToDoFunc;
        $(".close").unbind();

    } else {
        DefaultSuccessfunc = function () { };
    }
    $("#OpenTextModal").click();
}


var OpenCancelCustomer = function (HeaderMessage, BodyMessage, ToDoFunc) {
    $("#ModalCancelCustomer .cancel-title").text(HeaderMessage);
    $("#ModalCancelCustomer cancel-body p").text(BodyMessage);
    if (typeof (ToDoFunc) == "function") {
        DefaultCancelCustomerfunc = ToDoFunc;

    } else {
        DefaultCancelCustomerfunc = function () { };
    }
    $("#CancelCustomerModal").click();
}
var OpenErrorMessageNew= function (HeaderMessage, BodyMessage, ToDoFunc) {
    $("#ModalErrorMessage .message_header_title").text('Error!');
    $('#ModalErrorMessage p').html(BodyMessage);
    if (typeof (ToDoFunc) == "function") {
        console.log("Fancu : " + ToDoFunc)
        DefaultErrorfunc = ToDoFunc;
    } else {
        DefaultErrorfunc = function () { };
    } 
    $("#ErrorMessageModal").click();
    /*setTimeout(function () {
        if ($("#ModalErrorMessage").is(":visible")) {
            if ($(".modal-backdrop").length > 1) {
                $(".modal-backdrop").each(function () {
                    $(this).css("z-index", "999999");
                    return 0;
                }); 
            } else if ($(".modal-backdrop").is(":visible")) {
                $(".modal-backdrop").css("z-index", "1040!important");
            }
            
        }
    },1000);*/
}
var OpenTopToBottomModal = function (url) {
    var windowHeight = $(window).height();
    $(".TopToBottomModal").css('top', -window.innerHeight);
    $(".TopToBottomModal").show(); 
    $(".TopToBottomModal").animate({
        top: 0
    }, 200);
    
    var InvoiceLoaderText = "<div class='invoice-loader'><div class='uil-squares-css' style='transform:scale(1); margin:auto;'><div><div></div></div><div><div></div></div><div><div></div></div><div><div></div></div><div><div></div></div><div><div></div></div><div><div></div></div><div><div></div></div></div></div>";
    $(".TopToBottomModal .ContentsDiv").html(InvoiceLoaderText);
    setTimeout(function () {
        $(".top_to_bottom_modal_container").css("height", window.innerHeight);
    }, 300);
    setTimeout(function () {
        $(".TopToBottomModal .ContentsDiv").load(url);
    }, 700);
}
var CloseTopToBottomModal = function () {
    $(".TopToBottomModal").animate({
        top: -window.innerHeight
    }, 500);
    setTimeout(function () {
        $(".TopToBottomModal").hide();
        $(".TopToBottomModal .ContentsDiv").html("")
    }, 510);
}
var OpenRightToLeftModal = function (url) {
    $("#RightToLeftModal").click();
    var InvoiceLoaderText = "<div class='invoice-loader'><div class='uil-squares-css' style='transform:scale(1); margin:auto;'><div><div></div></div><div><div></div></div><div><div></div></div><div><div></div></div><div><div></div></div><div><div></div></div><div><div></div></div><div><div></div></div></div></div>";
    $("#Right-To-Left-Modal-Body .modal-body").html(InvoiceLoaderText);
    if ( typeof(url)!="undefined" && url != "") {
        $("#Right-To-Left-Modal-Body .modal-body").load(url);
    }   
}
var OpenRightToLeftModalMMR = function (url) {
    $("#RightToLeftModalMMR").click();
    var InvoiceLoaderText = "<div class='invoice-loader'><div class='uil-squares-css' style='transform:scale(1); margin:auto;'><div><div></div></div><div><div></div></div><div><div></div></div><div><div></div></div><div><div></div></div><div><div></div></div><div><div></div></div><div><div></div></div></div></div>";
    $("#Right-To-Left-Modal-Body-MMR .modal-body").html(InvoiceLoaderText);
    if (typeof (url) != "undefined" && url != "") {
        $("#Right-To-Left-Modal-Body-MMR .modal-body").load(url);
    }
}
var OpenRightToLeftLgModal = function (url) {
    $("#RightToLeftBigModal").click();
    var InvoiceLoaderText = "<div class='invoice-loader'><div class='uil-squares-css' style='transform:scale(1); margin:auto;'><div><div></div></div><div><div></div></div><div><div></div></div><div><div></div></div><div><div></div></div><div><div></div></div><div><div></div></div><div><div></div></div></div></div>";
    $("#Right-To-Left-big-Modal-Body .modal-body").html(InvoiceLoaderText);
    if (typeof (url) != "undefined" && url != "") {
        $("#Right-To-Left-big-Modal-Body .modal-body").load(url);
    }
}