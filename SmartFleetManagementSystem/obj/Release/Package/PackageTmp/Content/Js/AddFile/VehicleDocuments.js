var UserFileUploadjqXHRData;
var CarId = $("#DCarIdVal").val();
var IssueDate;
var ExpireDate;
var SaveDocuments= function () {

    if (CommonUiValidation()) {
        var Param = {

            "Id": $("#docId").val(),
            "UserId": $("#DCarIdVal").val(),
            "FileSource": $("#UploadedPath").val(),
            "IssueDate": IssueDate.getDate(),
            "ExpireDate": ExpireDate.getDate(),
            "LicenseNo": $("#DriverLicense").val(), 
            "DocumentsType": $("#DocumentsType").val()
        };

        console.log(Param)
        var url = "/Car/SaveDocuments";
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
                    OpenSuccessMessageNew("Success !", "Documents saved successfully", function () {
                        $(".LoadDocumentList").load("/Car/LoadVehicleDocumentList/?CarId=" + CarId)
                        $(".close").click();
                    });
                }

                else {
                    OpenErrorMessageNew("Error!", "Please check your given input.");
                    //CloseTopToBottomModal();
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.log(errorThrown);
            }
        });
    }

}
$("#DocumentsType").change(function () {
    if ($("#DocumentsType").val() == "Registration Certificate") {
        $(".expiredate").hide()
    }
    else {
        $(".expiredate").show()
    }
    $('#UploadedFile').fileupload({
        pasteZone: null,
        url: '/File/UploadVehicleDocuments/?type=' + $("#DocumentsType").val(),
        dataType: 'json',
        add: function (e, data) {
            var ext = data.files[0].name.split(".");
            if ($("#description").val() == "") {
                var filename = data.fileInput[0].value.split('\\').pop();
                $("#description").val(filename);
            }
            if (ext[1] == 'doc' || ext[1] == 'docx' || ext[1] == 'xls' || ext[1] == 'xlsx' || ext[1] == 'jpeg' || ext[1] == 'jpg' || ext[1] == 'gif' || ext[1] == 'png' || ext[1] == 'rtf' || ext[1] == 'pdf' || ext[1] == 'txt' || ext[1] == 'mp4' || ext[1] == 'mov') {

                if (data.files[0].size <= 50000000) {
                    UserFileUploadjqXHRData = data;
                }
                else {
                    OpenErrorMessageNew("Error!", "File size is more then 50 mb.", function () {
                        $(".close").click();
                    })
                }

            }
            else {
                OpenErrorMessageNew("Error!", "File formet not valid.", function () {
                    $(".close").click();
                })
            }

        },
        progress: function (e, data) {
            var percentVal = parseInt(data.loaded / data.total * 100, 10);
            $(".file-progress").show();
            $(".file-progress .progress-bar").animate({
                width: percentVal + "%"
            }, 40);
            $(".file-progress .progress-bar span").text(percentVal + '%');
        },
        done: function (event, data) {
            console.log("dfdf");
            setTimeout(function () {
                $(".file-progress").hide();
                $(".file-progress .progress-bar").animate({
                    width: 0 + "%"
                }, 0);
                $(".file-progress .progress-bar span").text(0 + '%');
            }, 500);

            if ((typeof (data.result.isUploaded) != "undefined") && data.result.isUploaded == true) {
                $("#UploadSuccessMessage").removeClass('hidden');
                //$("#UploadCustomerFileBtn").addClass('hidden');
                $("#UploadedPath").val(data.result.filePath);
                var spfile = data.result.FullFilePath.split('.');
                //if (spfile[1] == "png" || spfile[1] == "jpg" || spfile[1] == "jpeg") {
                //    $(".Upload_Doc").addClass('hidden');

                //    $(".LoadPreviewDocument").removeClass('hidden');
                //    $("#Preview_Doc").attr('src', data.result.FullFilePath);
                //}
                $(".fileborder").removeClass('red-border');
                $("#uploadfileerror").addClass("hidden");

                var index = spfile.length - 1;
                if (spfile[index] == "png" || spfile[index] == "PNG" || spfile[index] == "jpg" || spfile[index] == "JPG" || spfile[index] == "jpeg" || spfile[index] == "JPEG") {
                    //$(".Upload_Doc").addClass('hidden');
                    //$(".LoadPreviewDocument").removeClass('hidden');
                    //$("#Preview_Doc").attr('src', data.result.FullFilePath);
                    $("#UploadCustomerFileBtn").attr('src', data.result.FullFilePath)
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
                else {
                    $(".chooseFilebtn").addClass("hidden");
                    $(".changeFilebtn").removeClass("hidden");
                    $(".deleteDoc").removeClass("hidden");
                    $("#UploadCustomerFileBtn").attr('src', '/Content/Icons/docx.png');
                    $("#UploadCustomerFileBtn").addClass('otherfileposition');
                    $("#UploadCustomerFileBtn").removeClass('custom-file');
                    $(".fileborder").removeClass('border_none');
                }
                //else {
                //    $(".Upload_Doc").addClass('hidden');
                //    $(".LoadPreviewDocument").addClass('hidden');
                //    $(".LoadPreviewDocument1").removeClass('hidden');
                //    $("#Frame_Doc").attr('src', data.result.FullFilePath);
                //}
            }
        },
        fail: function (event, data) {
            //if (data.files[0].error) {
            //    //alert(data.files[0].error);
            //}
        }
    });
})
$(document).ready(function () {
    IssueDate = new Pikaday({ format: 'DD/MM/YYYY', field: $('#IssueDate')[0] });
    if ($("#docId").val() == 0) {
        IssueDate.setDate(new Date())
    }

    ExpireDate = new Pikaday({ format: 'DD/MM/YYYY', field: $('#ExpireDate')[0] });
    if ($("#docId").val() == 0) {
        ExpireDate.setDate(new Date())
    }

    if ($("#docId").val() > 0) {
        $("#DocumentsType").prop("disabled", true);
    } else {
        $("#DocumentsType").prop("disabled", false);
    }

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
    $("#SaveDocuments").click(function () {
        SaveDocuments();
    })
    console.log(Documenttype);
    $("#DocumentsType").val(Documenttype);
    $(".deleteDoc").click(function () {
        OpenConfirmationMessageNew("Confirm?", "Are you sure you want to delete this file?", function () {
            $(".Upload_Doc").removeClass('hidden');
            //$(".LoadPreviewDocument").addClass('hidden');
            //$(".LoadPreviewDocument1").addClass('hidden');
            $("#UploadCustomerFileBtn").attr('src', '/Content/Icons/blank_thumb_file.png');
            $(".chooseFilebtn").removeClass("hidden");
            $(".changeFilebtn").addClass("hidden");
            $(".deleteDoc").addClass("hidden");
            $("#Preview_Doc").attr('src', "");
            $("#Frame_Doc").attr('src', "");
            $("#UploadSuccessMessage").hide();
            $("#description").val("");
            $("#UploadedPath").val('');
            $(".fileborder").addClass('border_none');
            $("#UploadCustomerFileBtn").removeClass('otherfileposition');
        });
    })
    $("#UploadCustomerFileBtn").click(function () {
        console.log("sdfdsf");
        if ($("#DocumentsType").val() != null && $("#DocumentsType").val() != "" && $("#DocumentsType").val() != "-1") {
            $("#UploadedFile").click();
            $("#UploadSuccessMessage").addClass('hidden');
        }
        else {
            OpenErrorMessageNew("Error!", "Select Document Type First", function () {
            })
        }
    });
    $(".change-picture-logo").click(function () {
        if ($("#DocumentsType").val() != null && $("#DocumentsType").val() != "" && $("#DocumentsType").val() != "-1") {
            $("#UploadedFile").click();
            $("#UploadSuccessMessage").addClass('hidden');
        }
        else {
            OpenErrorMessageNew("Error!", "Select Document Type First", function () {
            })
        }
    });
    $('#UploadedFile').fileupload({
        pasteZone: null,
        url: '/File/UploadVehicleDocuments/?type=' + $("#DocumentsType").val(),
        dataType: 'json',
        add: function (e, data) {
            var ext = data.files[0].name.split(".");
            if ($("#description").val() == "") {
                var filename = data.fileInput[0].value.split('\\').pop();
                $("#description").val(filename);
            }
            if (ext[1] == 'doc' || ext[1] == 'docx' || ext[1] == 'xls' || ext[1] == 'xlsx' || ext[1] == 'jpeg' || ext[1] == 'jpg' || ext[1] == 'gif' || ext[1] == 'png' || ext[1] == 'rtf' || ext[1] == 'pdf' || ext[1] == 'txt' || ext[1] == 'mp4' || ext[1] == 'mov') {

                if (data.files[0].size <= 50000000) {
                    UserFileUploadjqXHRData = data;
                }
                else {
                    OpenErrorMessageNew("Error!", "File size is more then 50 mb.", function () {
                        $(".close").click();
                    })
                }

            }
            else {
                OpenErrorMessageNew("Error!", "File formet not valid.", function () {
                    $(".close").click();
                })
            }

        },
        progress: function (e, data) {
            var percentVal = parseInt(data.loaded / data.total * 100, 10);
            $(".file-progress").show();
            $(".file-progress .progress-bar").animate({
                width: percentVal + "%"
            }, 40);
            $(".file-progress .progress-bar span").text(percentVal + '%');
        },
        done: function (event, data) {
            console.log("dfdf");
            setTimeout(function () {
                $(".file-progress").hide();
                $(".file-progress .progress-bar").animate({
                    width: 0 + "%"
                }, 0);
                $(".file-progress .progress-bar span").text(0 + '%');
            }, 500);

            if ((typeof (data.result.isUploaded) != "undefined") && data.result.isUploaded == true) {
                $("#UploadSuccessMessage").removeClass('hidden');
                //$("#UploadCustomerFileBtn").addClass('hidden');
                $("#UploadedPath").val(data.result.filePath);
                var spfile = data.result.FullFilePath.split('.');
                //if (spfile[1] == "png" || spfile[1] == "jpg" || spfile[1] == "jpeg") {
                //    $(".Upload_Doc").addClass('hidden');

                //    $(".LoadPreviewDocument").removeClass('hidden');
                //    $("#Preview_Doc").attr('src', data.result.FullFilePath);
                //}
                $(".fileborder").removeClass('red-border');
                $("#uploadfileerror").addClass("hidden");

                var index = spfile.length - 1;
                if (spfile[index] == "png" || spfile[index] == "PNG" || spfile[index] == "jpg" || spfile[index] == "JPG" || spfile[index] == "jpeg" || spfile[index] == "JPEG") {
                    //$(".Upload_Doc").addClass('hidden');
                    //$(".LoadPreviewDocument").removeClass('hidden');
                    //$("#Preview_Doc").attr('src', data.result.FullFilePath);
                    $("#UploadCustomerFileBtn").attr('src', data.result.FullFilePath)
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
                else {
                    $(".chooseFilebtn").addClass("hidden");
                    $(".changeFilebtn").removeClass("hidden");
                    $(".deleteDoc").removeClass("hidden");
                    $("#UploadCustomerFileBtn").attr('src', '/Content/Icons/docx.png');
                    $("#UploadCustomerFileBtn").addClass('otherfileposition');
                    $("#UploadCustomerFileBtn").removeClass('custom-file');
                    $(".fileborder").removeClass('border_none');
                }
                //else {
                //    $(".Upload_Doc").addClass('hidden');
                //    $(".LoadPreviewDocument").addClass('hidden');
                //    $(".LoadPreviewDocument1").removeClass('hidden');
                //    $("#Frame_Doc").attr('src', data.result.FullFilePath);
                //}
            }
        },
        fail: function (event, data) {
            //if (data.files[0].error) {
            //    //alert(data.files[0].error);
            //}
        }
    });
    $("#UploadedFile").on("change", function () {
        if ($("#DocumentsType").val() != null && $("#DocumentsType").val() != "") {
            if (UserFileUploadjqXHRData) {
                UserFileUploadjqXHRData.submit();
            }
        }
        else {
            OpenErrorMessageNew("Error!", "Select Document Type First", function () {
                $(".close").click();
            })
        }
        return false;
    });
});