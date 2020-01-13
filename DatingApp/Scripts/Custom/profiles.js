window.addEventListener('load', () => {

    loadProgressBars();
});

// Animerar kunskapsbars:en
function loadProgressBars() {

    var cSharpValue = $('#progress-bar-csharp').data('value') * 10;

    $('#progress-bar-csharp').animate({ width: cSharpValue + "%" }, 100);
    $('#progress-csharp').delay(1000).fadeOut(500);

    var javascriptValue = $('#progress-bar-javascript').data('value') * 10;

    $('#progress-bar-javascript').animate({ width: javascriptValue + "%" }, 100);
    $('#progress-javascript').delay(1000).fadeOut(500);

    var stackOverflowValue = $('#progress-bar-stackoverflow').data('value') * 10;

    $('#progress-bar-stackoverflow').animate({ width: stackOverflowValue + "%" }, 100);
    $('#progress-stackoverflow').delay(1000).fadeOut(500);
}