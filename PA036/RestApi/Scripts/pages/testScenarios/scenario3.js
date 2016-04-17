$(document).ready(function() {
    var firstExecution = true;
    $("#execute").click(function() {
        var url = $('button#execute').data('url');
        $("#execute").attr('disabled', 'disabled');

        addNewEmptyRow(firstExecution);

        $.ajax({
            url: url,
            success: function(result) {
                $('.before:last').append(result.beforeUpdate);
                $('.after:last').append(result.afterUpdate);
                $("#execute").removeAttr('disabled');
                firstExecution = false;
                return;
            }
        });
    });
});

var addNewEmptyRow = function(isFirstExecution) {
    var placeholderRows = $('.placeholder-row');
    if (placeholderRows.length > 0 && !isFirstExecution) {
        console.log('Appending line for next test execution');
        var row = placeholderRows.first();
        var tableBody = row.parent();
        var newRow = row.clone();
        newRow.find('.before').empty();
        newRow.find('.after').empty();
        tableBody.append(newRow);
    }
};