$(document).ready(function() {

    $('button#scenario1-execute').click(function () {
        var url = $('button#scenario1-execute').data('url');
        $.getJSON(url, function(data) {

            $('#tests-target').html(data);
        });

    });
});