$(document).ready(function () {

    $("#execute").click(function (event) {
        event.preventDefault();

        var url = $('button#execute').data('url');
        var formData = $('form#options').serialize();
        $("#execute").attr('disabled', 'disabled');
        $("#execute-adjusted").attr('disabled', 'disabled');
        $.ajax({
            url: url,
            data: formData,
            success: function (result) {
                renderExecutionTimesGraph(result);
                renderRequestedDataGraph(result);
                renderCacheSizeGraph(result);

                $("#execute-adjusted").removeAttr('disabled');
                $("#execute").removeAttr('disabled');
            }
        });
    });

    $("#execute-adjusted").click(function (event) {
        event.preventDefault();

        var url = $('button#execute-adjusted').data('url');
        var formData = $('form#options').serialize();
        $("#execute").attr('disabled', 'disabled');
        $("#execute-adjusted").attr('disabled', 'disabled');
        $.ajax({
            url: url,
            data: formData,
            success: function (result) {
                renderExecutionTimesGraph(result);
                renderRequestedDataGraph(result);
                renderCacheSizeGraph(result);

                $("#execute").removeAttr('disabled');
                $("#execute-adjusted").removeAttr('disabled');
            }
        });
    });
});

function renderExecutionTimesGraph(result) {
    var data = [];
    data.push({ name: 'First query', data: result.originalCache });
    data.push({ name: 'overlapped query', data: result.overlappedCache });
    var xAxis = [];
    for (var i = 0; i < result.skipped.length; i += 1) {
        xAxis.push(result.skipped[i][0]);
    }

    executionTimesGraph(data, xAxis);
}

function executionTimesGraph(data, xAxisSettings) {
    new Highcharts.Chart({
        chart: {
            type: 'line',
            renderTo: 'execution-times'
        },
        title: {
            text: 'Result',
            x: -20 //center
        },
        xAxis: {
            title: {
                text: 'k : % of overlapped data'
            },
            categories: xAxisSettings
        },
        yAxis: {
            title: {
                text: 'Seconds'
            },
            plotLines: [
                {
                    value: 0,
                    width: 1,
                    color: '#808080'
                }
            ]
        },
        legend: {
            layout: 'vertical',
            align: 'right',
            verticalAlign: 'middle',
            borderWidth: 0
        },
        series: data
    });
};

function renderRequestedDataGraph(result){
    var rangeData = [];
    for (var i = 0; i < result.skipped.length; i++) {
        rangeData.push([result.skipped[i][1], result.skipped[i][1] + result.cacheSizeComparison[i].noOfObjectsReturnedInQuery]);
    }

    data['overlapped'] = {
        label: 'No. of items requested in second query',
        data: rangeData
    };

    rangeData = [];
    for (var i = 0; i < result.skipped.length; i++) {
        rangeData.push([0, result.cacheSizeComparison[i].noOfObjectsReturnedInQuery]);
    }

    data['first'] = {
        label: 'No. of items requested in first query',
        data: rangeData
    };

    var xAxis = [];
    for (var i = 0; i < result.skipped.length; i += 1) {
        xAxis.push(result.skipped[i][0]);
    }
    dataRequestedGraph(data, xAxis);
}

function renderCacheSizeGraph(result) {
    var data = [];

    data['first'] = {
        label: 'Cache after first query',
        data: result.cacheSizeComparison.map(function (item) { return item.beforeQueryExecution; })
    };

    data['overlapped'] = {
        label: 'Cache after overlapped query',
        data: result.cacheSizeComparison.map(function (item) { return item.afterQueryExecution; })
    };
    xAxis = [];
    for (var i = 0; i < result.skipped.length; i += 1) {
        xAxis.push(i);
    }
    cacheSizeGraph(data, xAxis);
}

function dataRequestedGraph(data, xAxisSettings) {
    $('#data').highcharts({
        chart: {
            type: 'columnrange'
        },
        title: {
            text: 'Number of requested data'
        },
        xAxis: {
            categories: 
                xAxisSettings
            ,
            title: {
                text: '% of overlapped data'
            },
            crosshair: true
        },
        yAxis: {
            min: 0,
            title: {
                text: 'Number of items'
            }
        },
        series: [
            {
                name: data.first.label,
                data: data.first.data

            }, {
                name: data.overlapped.label,
                data: data.overlapped.data

            }
        ]
    });
};

function cacheSizeGraph(data, xAxisSettings) {
    $('#cache').highcharts({
        chart: {
            type: 'column'
        },
        title: {
            text: 'Cache size comparison'
        },
        xAxis: {
            categories: 
                xAxisSettings,
            title: {
                text: 'Iteration'
            },
            crosshair: true
        },
        yAxis: {
            min: 0,
            title: {
                text: 'Number of items'
            }
        },
        series: [
            {
                name: data.first.label,
                data: data.first.data

            }, {
                name: data.overlapped.label,
                data: data.overlapped.data

            }
        ]
    });
};