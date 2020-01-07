window.addEventListener('load', () => {
    //updateWall();

    //$('#post-wall-button').click(alert('Test'));

    $("#post-wall-button").click(function () {
        postToWall()
    });
});

function postToWall() {

    const newPost = $('#new-post').val();
    const timestamp = new Date().toISOString();
    const recieverId = $('#user-id').val();

    const post = {

        ReceiverId: recieverId,
        DateTime: timestamp,
        Content: newPost
    };

    $.post('/api/postapi/send', post)
        .then((answer) => {
            if (answer === "Ok") {
                $('#new-post').val('');
                updateWall();
            } else {
                alert("Something went wrong!");
            }
        });
}

function updateWall() {

    var receiverId = $('#user-id').val();

    $.ajax({
        type: "POST",
        url: "/api/postapi/display",
        contentType: "application/json",
        data: JSON.stringify(receiverId),
        dataType: "json",
        success: function (result) {
            $('#posts-div').html('');
            result.forEach((post) => {
               $('#posts-div').append(
                 '<div class="panel panel-default"><div class="panel-heading panel-header-wide"><div class="col-md-8">Placeholder name</div><div class="col-md-4">' + post.DateTime + '</div></div><div class="panel-body text-left">' + post.Content + '</div></div>'
                );
            });
        }
    });
}

