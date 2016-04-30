$(document).ready(function () {

    $('form#options input#doNotCacheItems').change(function (event) {
        var doNotCacheItems = event.target.checked;
        var disableInvalidateCache = (doNotCacheItems) ? 'disabled' : null;

        console.log('doNotCacheItems changed', doNotCacheItems, disableInvalidateCache);

        $('form#options input#invalidateCache').attr('disabled', disableInvalidateCache);
    });

    $("#execute").click(function (event) {
        event.preventDefault();

        var url = $('button#execute').data('url');
        var formData = $('form#options').serialize();
        $("#execute").attr('disabled', 'disabled');
        $.ajax({
            url: url,
            data: formData,
            success: function (result) {

                renderExecutionTimesGraph(result);

                renderCacheSizeGraph(result);

                $("#execute").removeAttr('disabled');
            }
        });
    });
});

function renderExecutionTimesGraph(result) {
    var data = [];
    data.push({ name: 'First query', data: result.notCachedQueriesTimes });
    data.push({ name: 'Second query', data: result.cachedQueriesTimes });
    var xAxis = [];
    for (var i = result.xAxis[0]; i < result.xAxis[1]; i += result.xAxis[2]) {
        xAxis.push(i);
    }

    //console.log('renderExecutionTimesGraph : So far so good');

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
                text: 'k : Number of products requested'
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


function renderCacheSizeGraph(result) {
    //console.log('renderCacheSizeGraph start', result);
    var data = [];
    data['before'] = {
        label: 'Before first query',
        data: result.cacheSizeComparison.map(function(item) { return item.beforeQueryExecution; })
    };

    data['after'] = {
        label: 'After first query',
        data: result.cacheSizeComparison.map(function(item) { return item.afterQueryExecution; })
    };

    data['number'] = {
        label: 'No. of items requested',
        data: result.cacheSizeComparison.map(function(item) { return item.noOfObjectsReturnedInQuery; })
    };

    var xAxis = [];
    for (var i = result.xAxis[0]; i < result.xAxis[1]; i += result.xAxis[2]) {
        var iteration = i / result.xAxis[0];
        xAxis.push((iteration).toString());
    }

    console.log('renderCacheSizeGraph data ok', data);


    cacheSizeGraph(data, xAxis);
}

function cacheSizeGraph(data, xAxisSettings) {
    $('#cache-sizes').highcharts({
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
                name: data.before.label,
                data: data.before.data

            }, {
                name: data.after.label,
                data: data.after.data

            }, {
                name: data.number.label,
                data: data.number.data

            }
        ]
    });
};