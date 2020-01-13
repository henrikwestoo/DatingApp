window.addEventListener('load', () => {
  
    document.getElementById("percentage-label").style.display = "none";
    document.getElementById("percentage-description").style.display = "none";
});


function displayMatchPercentage(targetId) {

    $.ajax({
        type: "GET",
        async: false,
        url: "/profile/GetMatchPercentage",
        data: { targetId: targetId },
        success: function (matchPercentage) {

            var increaser = 0;

            document.getElementById("match-button").style.display = "none";
            document.getElementById("percentage-description").style.display = "block";
            document.getElementById("percentage-label").style.display = "block";

            document.getElementById("percentage-label").innerHTML = increaser + "%";

            // "Animerar" matchingsnumret tills den träffar ett tag
            var timer = setInterval(function () {

                document.getElementById("percentage-label").innerHTML = increaser + "%";

                if (increaser == matchPercentage) {
                    clearInterval(timer);

                    if (increaser < 50) {
                        document.getElementById("percentage-label").style.color = "red";
                    } else {
                        document.getElementById("percentage-label").style.color = "green";
                    }
                }

                increaser++;

            }, 2); // Varannan millisekund
        }
    });
}