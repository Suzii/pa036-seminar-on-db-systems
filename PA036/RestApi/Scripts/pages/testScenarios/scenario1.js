$(document).ready(function () {
    var result = null;
    $("#scenario1-execute").click(function () {

        var url = $('#scenario1-execute').data('url');
        $("#scenario1-execute").attr('disabled', 'disabled');
        $.ajax({
            url: url,
            success: function (result) {
                var data = [];
                data.push({ name: 'Not cached', data: result.notCached });
                data.push({ name: 'cached', data: result.cached });
                var xAxis = [];
                for (var i = result.xAxis[0]; i < result.xAxis[1]; i += result.xAxis[2]) {
                    xAxis.push(i);
                }
                graph(data, xAxis);
                $("#scenario1-execute").removeAttr('disabled');
            }
        });
    });
});

function graph(data, xAxisSettings) {
    new Highcharts.Chart({
        chart: {
            type: 'line',
            renderTo: 'container'
        },
        title: {
            text: 'Result',
            x: -20 //center
        },
        xAxis: {
            title: {
                text: 'Number of Objects(°C)'
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