$(document).ready(function () {
    var result = null;
    $("#execute").click(function () {

        var url = $('button#execute').data('url');
        $("#execute").attr('disabled', 'disabled');
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
                $("#execute").removeAttr('disabled');
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
