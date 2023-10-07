var ctx;

window.onload = function () {
    loadDataForChart();
    test();
};


function qweqwe() {
    console.log(1111);

}

function loadDataForChart() {
    ctx = document.getElementById('myChart');
    $.ajax({
        async: true,
        type: "POST",
        url: "Home/GetChartData",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            console.log(response);
            fillChart(response);
        },
        failure: function (response) {
            alert(response);
        },
        error: function (xhr, status, error) {
            console.log(error);
        }
    });    
}

function fillChart(data) {
    if (!data) return;
    new Chart(ctx, {
        type: 'bar',
        data: {
            datasets: [{
                data: data.table,
                borderWidth: 1
            }]
        },
        options: {
            scales: {
                y: {
                    beginAtZero: true
                }
            }
        }
    });
}

function test() {
    $.ajax({
        async: true,
        type: "GET",
        url: "Home/Ping",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            //alert(response);
            //var a = JSON.parse(response);
            console.log(response);

        },
        failure: function (response) {
            alert(response);
        },
        error: function (xhr, status, error) {
            console.log(error);
        }
    });
}