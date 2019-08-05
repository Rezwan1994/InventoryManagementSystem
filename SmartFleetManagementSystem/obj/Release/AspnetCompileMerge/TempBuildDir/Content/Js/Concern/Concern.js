EditConcern = function(item)
{
    OpenRightToLeftModal("/Concern/AddConcern?Id="+item);
}

$(document).ready(function () {
    $(".ListContents").load("/Concern/LoadConcernList");
    $("#AddConcern").click(function () {
        OpenRightToLeftModal("/Concern/AddConcern");
    })



    $('.SearchConcern').click(function () {
        NavigatePageListing(1, null);
    })
});