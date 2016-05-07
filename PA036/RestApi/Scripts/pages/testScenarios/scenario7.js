$(document).ready(function () {
    var firstExecution = true;
    $("#execute").click(function (event) {
        event.preventDefault();



        var url = $('button#execute').data('url');
        $("#execute").attr('disabled', 'disabled');
        addNewEmptyRow(firstExecution, 'placeholderDiff', '.azure', '.local');
        $.ajax({
            url: url,
            success: function (result) {
                var azureObject = JSON.stringify(result.objectFromAzure);
                var localObject = JSON.stringify(result.objectFromLocal);
                $('.azure:last').append(azureObject);
                $('.local:last').append(localObject);
                $("#execute").removeAttr('disabled');
            }
        });
    });
});
var addNewEmptyRow = function(isFirstExecution, placeholder, beforeRow, afterRow) {
    var placeholderRows = $('#' + placeholder);
    if (placeholderRows.length > 0 && !isFirstExecution) {
        console.log('Appending line for next test execution');
        var row = placeholderRows.first();
        var tableBody = row.parent();
        var newRow = row.clone();
        newRow.find(beforeRow).empty();
        newRow.find(afterRow).empty();
        tableBody.append(newRow);
    }
};