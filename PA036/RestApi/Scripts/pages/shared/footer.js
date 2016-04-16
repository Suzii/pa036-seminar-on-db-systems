    // function to set the height on fly
    function autoHeight() {
        $('#wrap').css('min-height', 0);
        $('#wrap').css('min-height', ($(document).height() - $('footer').height() - $('header').height()));
    }

    // onDocumentReady function bind
    $(document).ready(function () {
        autoHeight();
    });

    // onResize bind of the function
    $(window).resize(function () {
        autoHeight();
    });
