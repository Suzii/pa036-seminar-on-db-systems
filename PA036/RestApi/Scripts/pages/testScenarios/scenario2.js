$(document).ready(function () {
    
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

    console.log('renderExecutionTimesGraph : So far so good');

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
    console.log('renderCacheSizeGraph start', result);
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
            categories: [
                xAxisSettings
            ],
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
        //chart: {
        //    type: 'column'
        //},
        //title: {
        //    text: 'Monthly Average Rainfall'
        //},
        ////xAxis: {
        ////    categories: [
        ////        'Jan',
        ////        'Feb',
        ////        'Mar',
        ////        'Apr',
        ////        'May',
        ////        'Jun',
        ////        'Jul',
        ////        'Aug',
        ////        'Sep',
        ////        'Oct',
        ////        'Nov',
        ////        'Dec'
        ////    ],
        ////    crosshair: true
        ////},
        //yAxis: {
        //    min: 0,
        //    title: {
        //        text: 'Rainfall (mm)'
        //    }
        //},

        //series: [{
        //    name: 'Tokyo',
        //    data: [49.9, 71.5, 106.4, 129.2, 144.0, 176.0, 135.6, 148.5, 216.4, 194.1, 95.6, 54.4]

        //}, {
        //    name: 'New York',
        //    data: [83.6, 78.8, 98.5, 93.4, 106.0, 84.5, 105.0, 104.3, 91.2, 83.5, 106.6, 92.3]

        //}, {
        //    name: 'London',
        //    data: [48.9, 38.8, 39.3, 41.4, 47.0, 48.3, 59.0, 59.6, 52.4, 65.2, 59.3, 51.2]

        //}, {
        //    name: 'Berlin',
        //    data: [42.4, 33.2, 34.5, 39.7, 52.6, 75.5, 57.4, 60.4, 47.6, 39.1, 46.8, 51.1]

        //}]
    });
};