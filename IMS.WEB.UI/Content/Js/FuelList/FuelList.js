
var EditFuel= function (id) {
    OpenTopToBottomModal("/Fuel/AddDailyFuel?id=" + id);

}

$(document).ready(function () {
    $(".ListDailyFuelSystem").load("/Fuel/LoadFuelSystemList");
    //$("#AddVehicle").click(function () {
    //    OpenTopToBottomModal("/Car/AddVehicle");

    //})
    $("#AddFuel").click(function () {
        OpenTopToBottomModal("/Fuel/AddDailyFuel");

    })

    //$("#EditVehicle").click(function () {

    //})
    $('.SearchFuels').click(function () {
        NavigatePageListing(pagenumber);
    })
});
