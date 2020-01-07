(function () {

    window.addEventListener('load', () => {
        //updateWall();

        $('#post-wall-button').click(alert('Test'));
    });

    function postToWall() {

        const newPost = $('#new-post').val();
        const timestamp = new Date().toISOString();
        const userId = $('#user-id').val();

        const post = {
            CreatorId: userId,
            ReceiverId: 10,
            DateTime: timestamp,
            Content: newPost
        };

        $.post('/api/postapi/send', post)
            .then((answer) => {
                if (answer === "Ok") {
                    $('new-post').val('');
                    //updateWall();
                } else {
                    alert("Something went wrong!");
                }
            });
    }
})();

