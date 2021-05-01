$(function () {
    $("#pageTitle").on("click", function () {
        if ($("#titleIcon").hasClass("fa-toggle-down")) {
            $("#titleIcon").removeClass("fa-toggle-down").addClass("fa-toggle-up");
            $("#searchDiv").show(300);
        }
        else {
            $("#titleIcon").removeClass("fa-toggle-up").addClass("fa-toggle-down");
            $("#searchDiv").hide(300);
        }
    })
}
);
