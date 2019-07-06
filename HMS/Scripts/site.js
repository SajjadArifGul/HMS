$(".changeAccomodationType").click(function () {
    var accomodationTypeID = $(this).attr("data-id");

    $(".accomodationTypesRow").hide();
    $("div.accomodationTypesRow[data-id=" + accomodationTypeID + "]").show();
});