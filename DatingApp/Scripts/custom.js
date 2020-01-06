$(document).ready(function() {
    updateRequests();
    setInterval(updateRequests, 10000);
});


function updateRequests() {
    $.ajax({
        type: "GET",
        url: "/Contact/GetPendingRequests",
        dataType: "JSON",
        success: function(data) {
            $("#contactsText").text("View contacts (" + data.number + ")");
        },
        error: () => {
            console.log("Error, failed to fetch pending contact requests data");
        }
    });
}