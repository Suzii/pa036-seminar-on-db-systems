$(document).ready(function() {
    var firstExecution = true;
    var firstExecutionDelete = true;
    var firstExecutionPost = true;
    $("#executeUpdate").click(function() {
        var url = $('button#executeUpdate').data('url');
        disableButtons();

        var before = '.beforeUpdate';
        var after = '.afterUpdate';
        addNewEmptyRow(firstExecution, 'placeholderUpdate', before, after);

        var formData = $('form#options-update').serialize();

        $.ajax({
            url: url,
            type: 'GET',
            data: formData,
            success: function (result) {
                successFunction(result, before, after);
                firstExecution = false;
                return;
            }
        });
    });

    $("#executePost").click(function() {
        var url = $('button#executePost').data('url');
        disableButtons();

        var before = '.beforePost';
        var after = '.afterPost';
        addNewEmptyRow(firstExecutionPost, 'placeholderPost', before, after);

        var formData = $('form#options-post').serialize();

        $.ajax({
            url: url,
            type: 'POST',
            data: formData,
            success: function (result) {
                successFunction(result, before, after);
                firstExecutionPost = false;
                return;
            }
        });
    });

    $("#executeDelete").click(function () {
        var url = $('button#executeDelete').data('url');
        disableButtons();

        var before = '.beforeDelete';
        var after = '.afterDelete';
        addNewEmptyRow(firstExecutionDelete, 'placeholderDelete', before, after);

        var formData = $('form#options-delete').serialize();

        $.ajax({
            url: url,
            type: 'DELETE',
            data: formData,
            success: function (result) {
                successFunction(result, before, after);
                firstExecutionDelete = false;
                return;
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

function successFunction(result, before, after) {
    $(before + ':last').append(result.beforeAction);
    $(after + ':last').append(result.afterAction);
    $("#executeDelete").removeAttr('disabled');
    $("#executeUpdate").removeAttr('disabled');
    $("#executePost").removeAttr('disabled');
}

function disableButtons() {
    $("#executeDelete").attr('disabled', 'disabled');
    $("#executeUpdate").attr('disabled', 'disabled');
    $("#executePost").attr('disabled', 'disabled');
}