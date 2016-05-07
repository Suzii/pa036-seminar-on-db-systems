$(document).ready(function() {
    var firstExecution = true;
    var firstExecutionStores = true;

    $("#executeProducts").click(function() {
        var url = $('button#executeProducts').data('url');
        disableButtons();

        var before = '.beforeProducts';
        var after = '.afterProducts';
        addNewEmptyRow(firstExecution, 'placeholderProducts', before, after);

        var formData = $('form#options-products').serialize();

        $.ajax({
            url: url,
            type: 'POST',
            data: formData,
            success: function (result) {
                successFunction(result, before, after);
                firstExecution = false;
                return;
            }
        });
    });

    $("#executeStores").click(function () {
        var url = $('button#executeStores').data('url');
        disableButtons();

        var before = '.beforeStores';
        var after = '.afterStores';
        addNewEmptyRow(firstExecutionStores, 'placeholderStores', before, after);

        var formData = $('form#options-stores').serialize();

        $.ajax({
            url: url,
            type: 'GET',
            data: formData,
            success: function (result) {
                successFunction(result, before, after);
                firstExecutionStores = false;
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
    $("#executeStores").removeAttr('disabled');
    $("#executeProducts").removeAttr('disabled');
}

function disableButtons() {
    $("#executeStores").attr('disabled', 'disabled');
    $("#executeProducts").attr('disabled', 'disabled');
}