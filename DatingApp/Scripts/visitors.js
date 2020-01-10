﻿window.addEventListener('load', () => {

    getVisitors();

});

function getVisitors() {

    $.ajax({
        type: "GET",
        url: "/api/visitorapi/getvisitors",
        success: function(result) {
            result.forEach((visitor) => {
                $('#visitor-list').append(
                    '<li>' + visitor.VisitorName + '</li>'
                );

            })
        }
    })
}