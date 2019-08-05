

var EditVehicle = function (id) {
    OpenTopToBottomModal("/Car/AddVehicle?id=" + id);

}

var NavigatePageListing = function (pagenumber, order) {
    console.log("fff")
    var searchText = $(".srch-term").val();
    $(".ListContents").load("/Car/LoadCarList", { PageNumber: pagenumber, SearchText: searchText, Order: order });
}

$(document).ready(function () {
    $(".ListContents").load("/Car/LoadCarList");
    $("#AddVehicle").click(function () {
        OpenTopToBottomModal("/Car/AddVehicle");

    })


    $("#EditVehicle").click(function () {

    })
    $('.SearchVehicles').click(function () {
   
        NavigatePageListing(pagenumber);
    })
});