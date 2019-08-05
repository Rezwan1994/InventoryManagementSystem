

var EditUsers = function (id) {
    OpenTopToBottomModal("/Driver/AddDriver?id=" + id);

}

var NavigatePageListing = function (pagenumber, order) {
    console.log("fff")
    var searchText = $(".srch-term").val();
    $(".ListContents").load("/Driver/LoadDriversList", { PageNumber: pagenumber, SearchText: searchText, Order: order });
}

$(document).ready(function () {
    $(".ListContents").load("/Driver/LoadDriversList");
    $("#AddDriver").click(function () {
        OpenTopToBottomModal("/Driver/AddDriver");
    })

    $('.SearchDrivers').click(function () {
        NavigatePageListing(1, null);
    })
});