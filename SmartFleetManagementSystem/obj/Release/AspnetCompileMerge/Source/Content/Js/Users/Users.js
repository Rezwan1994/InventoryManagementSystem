

var EditUsers = function (id) {
    OpenTopToBottomModal("/Users/AddUser?id=" + id);

}

var NavigatePageListing = function (pagenumber, order) {
    console.log("fff")
    var searchText = $(".srch-term").val();
    $(".ListContents").load("/Users/LoadUsersList", { PageNumber: pagenumber, SearchText: searchText, Order: order });
}

$(document).ready(function () {
    $(".ListContents").load("/Users/LoadUsersList");
    $("#AddUser").click(function () {
        OpenRightToLeftModal("/Users/AddUser");

    })

    $('.SearchUsers').click(function () {
        NavigatePageListing(1, null);
    })
});