window.addEventListener('load', () => {

    updateWall();

    $("#post-wall-button").click(function () {
        postToWall()
    });
});

function postToWall() {

    const newPost = $('#new-post').val();
    const dateTime = new Date().toISOString();
    const recieverId = $('#user-id').val();
    //var validCharacters = new RegExp('^ [\w][^\W_][\w]$');

    if (newPost !== '') {

        const post = {

            ReceiverId: recieverId,
            DateTime: dateTime,
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

    else {

        alert("Your post is empty");

    }
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
                var dateArray = post.DateTime.split("T");
                var clock = dateArray[1].substring(0, 8);
                var formattedDate = dateArray[0] + " " + clock;

                if (getCurrentProfileId() == +$('#user-id').val()) {

                    //Vi använder post.id för att hålla koll på vilken post elementen tillhör
                    $('#posts-div').append(
                        '<div class="panel panel-default" id="post-div-' + post.Id + '"><div class="panel-heading panel-header-wide"><div class="col-md-8">' + post.CreatorName + '</div><div class="col-md-4">' + formattedDate + '</div></div><div class="panel-body text-left" style="word-wrap: break-word !important;">' + post.Content + '</div><div class="panel-footer"><div class="row"><div class="col-md-4" id="remove-post-' + post.Id + '"><input type="button" onclick="displayRemovePostButtons(' + post.Id + ')" value = "Remove post"class= "btn btn-danger"/></div><div class="col-md-2" style="display: none;" id="confirm-remove-' + post.Id + '"><input type="button" onclick="removePost(' + post.Id + ')" value = "Confirm"class= "btn btn-success"/></div><div class="col-md-2" style="display: none;" id="cancel-remove-' + post.Id + '"><input type="button" onclick="cancelRemovePost(' + post.Id + ')" value = "Cancel"class= "btn btn-danger"/></div></div></div>'
                    );
                }

                else {
                    $('#posts-div').append(
                        '<div class="panel panel-default"><div class="panel-heading panel-header-wide"><div class="col-md-8">' + post.CreatorName + '</div><div class="col-md-4">' + formattedDate + '</div></div><div class="panel-body text-left" style="word-wrap: break-word !important;">' + post.Content + '</div></div>'
                    );
                }
            });
        },
        error: function () {
            alert("Failed to display posts");
        }
    });
}

function getCurrentProfileId() {

    var number = 0;

    $.ajax({
        type: "GET",
        async: false,
        url: "/profile/getcurrentprofileid",
        success: function (id) {
            number = id;
        }
    });

    return number;

}

//När man bekräftat att man vill ta bort posten
function removePost(postId) {

    $("#post-div-" + postId + "").fadeOut();

    $.ajax({
        type: "POST",
        url: "/post/delete",
        data: { postId: postId },
    });

}
//När man inte bekräftar borttagningen
function cancelRemovePost(postId) {

    document.getElementById("remove-post-" + postId + "").style.display = "block";
    document.getElementById("confirm-remove-" + postId + "").style.display = "none";
    document.getElementById("cancel-remove-" + postId + "").style.display = "none";

}
//När man klickat på remove
function displayRemovePostButtons(postId) {

    document.getElementById("remove-post-" + postId + "").style.display = "none";
    document.getElementById("confirm-remove-" + postId + "").style.display = "block";
    document.getElementById("cancel-remove-" + postId + "").style.display = "block";

}