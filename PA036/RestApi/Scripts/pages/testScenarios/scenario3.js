$(document).ready(function() {
    var result = null;
    $("#execute").click(function () {
        var url = $('button#execute').data('url');
        $("#execute").attr('disabled', 'disabled');
        $.ajax({
            url: url,
            success: function(result) {
                $('#before').append(result.beforeUpdate);
                $('#after').append(result.afterUpdate);
                $("#execute").removeAttr('disabled');
                return;
            }
        });
    });
})
