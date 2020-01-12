window.addEventListener('load', () => {
  
    document.getElementById("percentage-label").style.display = "none";
});


function displayMatchPercentage(targetId) {

    $.ajax({
        type: "GET",
        async: false,
        url: "/profile/GetMatchPercentage",
        data: { targetId: targetId },
        success: function (matchPercentage) {

            document.getElementById("percentage-label").innerHTML = matchPercentage + "%";

        }
    });

    document.getElementById("percentage-label").style.display = "block";

}