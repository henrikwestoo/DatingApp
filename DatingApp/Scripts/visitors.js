window.addEventListener('load', () => {

    getVisitors();

});

function getVisitors() {

    $.ajax({
        type: "GET",
        url: "/api/visitorapi/getvisitors",
        success: function(result) {
            result.forEach((visitor) => {

                if (visitor.VisitorActive == true) {

                    $('#visitor-list').append(
                        '<li><a href=".?userId=' + visitor.VisitorProfileId + '">' + visitor.VisitorName + '</a></li>'
                    );
                } else {
                    $('#visitor-list').append(
                        '<li>' + visitor.VisitorName + ' (inactive) </li>'
                    );
                }
            })
        }
    })
}