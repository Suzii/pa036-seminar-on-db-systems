$(document).ready(function () {
    var firstExecution = true;
    $("#execute").click(function (event) {
        event.preventDefault();
        var url = $('button#execute').data('url');
        $("#execute").attr('disabled', 'disabled');
        addNewEmptyRow(firstExecution, 'placeholderMsg', 'msg');
        $.ajax({
            url: url,
            success: function (result) {
                firstExecution = false;
                $('.msg:last').append(result.message);
                $("#execute").removeAttr('disabled');
            }
        });
    });
});
var addNewEmptyRow = function (isFirstExecution, placeholder, beforeRow) {
    var placeholderRows = $('#' + placeholder);
    if (placeholderRows.length > 0 && !isFirstExecution) {
        console.log('Appending line for next test execution');
        var row = placeholderRows.first();
        var tableBody = row.parent();
        var newRow = row.clone();
        newRow.find(beforeRow).empty();
        tableBody.append(newRow);
    }
};