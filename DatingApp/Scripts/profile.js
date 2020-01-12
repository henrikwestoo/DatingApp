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

            document.getElementById("percentage-label").innerHTML ="Ni har en matchings-procent på " + matchPercentage + "%";

        }
    });

    document.getElementById("match-button").style.display = "none";
    document.getElementById("percentage-label").style.display = "block";

}