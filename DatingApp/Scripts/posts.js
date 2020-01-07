(function () {

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
        const userId = $('#user-id').val();

        const post = {
            
            ReceiverId: userId,
            DateTime: timestamp,
            Content: newPost
        };

        $.post('/api/postapi/send', post)
            .then((answer) => {
                if (answer === "Ok") {
                    $('#new-post').val('');
                    //updateWall();
                } else {
                    alert("Something went wrong!");
                }
            });
    }
})();

