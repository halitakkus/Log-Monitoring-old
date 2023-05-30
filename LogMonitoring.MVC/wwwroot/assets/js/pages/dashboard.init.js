var series = []
var info = {
    name: "Info",
    data: [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0]
}

let warning = {
    name: "Warning",
    data: [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0]
}

let error = {
    name: "Error",
    data: [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0]
}

let apps = []
let index = 0
var interval = undefined
function processArray() {
    const appId = apps[index].id;
    
    index++;
    let length = apps.length;

    console.log(appId)
    chartColumnStatistics(appId)
    
    if(index == length)
    {
        index = 0;
    }
}
function showColumnStatistics()
{
    getApps("/App/GetList/")
        .then(result => {
            apps = result.data;

            chartColumnStatistics(apps[0].id)

            setInterval(processArray,1500)

        }).catch(error => {
        alert("error!")
    })
}

onload = (event) => {
        showColumnStatistics()

    /*
    * setTimeout(function (){
        chartColumnStatistics("aca9546a-195b-45e9-b48f-649fc2dabd11")
    },1000)

    setTimeout(function (){
        chartColumnStatistics("6e7aaa7f-ceb2-4c6e-bcb4-0ff9f33b3232")
    },2500)

    setTimeout(function (){
      
        chartColumnStatistics("c2f94534-9184-4562-8758-53c03e5dfde4")
    },4500)
    * */
};

function chartColumnStatistics(appId)
{
    series = []
    info.data = [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0]
    warning.data = [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0]
    error.data = [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0]
    
    getColumnChartStatisticsByAppIdViaAjaxRequest("/Statistic/GetColumnChartStatisticsByAppId", appId)
        .then(result => {
            if(result.data != null)
            {
              result.data.columnChartModels.forEach(i=> {//Info için

                let dateObject = new Date(i.dateMon);
                let dateMonth = dateObject.getMonth() + 1

                const foundEntry = i.statistics.find(entry => entry.key === info.name);

                if(foundEntry)
                {
                    const foundValue = foundEntry.value
                    info.data[dateMonth - 1] = foundValue
                }
            })

            result.data.columnChartModels.forEach(i=> {// Warning için
                let dateObject = new Date(i.dateMon);
                let dateMonth = dateObject.getMonth() + 1

                const foundEntry = i.statistics.find(entry => entry.key === warning.name);

                if(foundEntry)
                {
                    const foundValue = foundEntry.value
                    warning.data[dateMonth - 1] = foundValue
                }
            })

            result.data.columnChartModels.forEach(i=> {//Error için
                let dateObject = new Date(i.dateMon);
                let dateMonth = dateObject.getMonth() + 1

                const foundEntry = i.statistics.find(entry => entry.key === error.name);

                if (foundEntry) {
                    const foundValue = foundEntry.value
                    error.data[dateMonth - 1] = foundValue
                }
            })
            }
            
            series.push(info)
            series.push(warning)
            series.push(error)

            chartColumnInstall()

        }).catch(error => {
        alert("error!")
    })
}

var linechartBasicColors = getChartColorsArray("stacked-column-chart");
linechartBasicColors[2] = "#b80046";
function chartColumnInstall()
{
    document.getElementById("stacked-column-chart").innerHTML = ""
    
    linechartBasicColors && (options = {
        chart: {
            height: 360,
            type: "bar",
            stacked: !0,
            toolbar: {
                show: !1
            },
            zoom: {
                enabled: !0
            }
        },
        plotOptions: {
            bar: {
                horizontal: !1,
                columnWidth: "15%",
                endingShape: "rounded"
            }
        },
        dataLabels: {
            enabled: !1
        },
        series: series,
        xaxis: {
            categories: ["Ocak", "Şubat", "Mart", "Nisan", "Mayıs", "Hazian", "Temmuz", "Ağus.", "Eyl.", "Ekim", "Kas.", "Aralık"]
        },
        colors: linechartBasicColors,
        legend: {
            position: "bottom"
        },
        fill: {
            opacity: 1
        }
    }, (chart = new ApexCharts(document.querySelector("#stacked-column-chart"), options)).render());
}

var options, chart, radialbarColors = getChartColorsArray("radialBar-chart");
radialbarColors && (options = {
    chart: {
        height: 200,
        type: "radialBar",
        offsetY: -10
    },
    plotOptions: {
        radialBar: {
            startAngle: -135,
            endAngle: 135,
            dataLabels: {
                name: {
                    fontSize: "13px",
                    color: void 0,
                    offsetY: 60
                },
                value: {
                    offsetY: 22,
                    fontSize: "16px",
                    color: void 0,
                    formatter: function(e) {
                        return e + "%"
                    }
                }
            }
        }
    },
    colors: radialbarColors,
    fill: {
        type: "gradient",
        gradient: {
            shade: "dark",
            shadeIntensity: .15,
            inverseColors: !1,
            opacityFrom: 1,
            opacityTo: 1,
            stops: [0, 50, 65, 91]
        }
    },
    stroke: {
        dashArray: 4
    },
    series: [67],
    labels: ["Series A"]
}, (chart = new ApexCharts(document.querySelector("#radialBar-chart"), options)).render());
function getChartColorsArray(e) {
    if (null !== document.getElementById(e)) {
        var t = document.getElementById(e).getAttribute("data-colors");
        if (t) return (t = JSON.parse(t)).map(function(e) {
            var t = e.replace(" ", "");
            if (-1 === t.indexOf(",")) {
                var r = getComputedStyle(document.documentElement).getPropertyValue(t);
                return r || t
            }
            var a = e.split(",");
            return 2 != a.length ? t : "rgba(" + getComputedStyle(document.documentElement).getPropertyValue(a[0]) + "," + a[1] + ")"
        })
    }
}
setTimeout(function() {
    $("#subscribeModal").modal("show")
}, 2e3);