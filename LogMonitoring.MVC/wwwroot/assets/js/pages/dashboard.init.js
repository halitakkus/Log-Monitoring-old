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
let index = 1
var interval = undefined
function processArray() {
    const appId = apps[index].id;
    
    index++;
    let length = apps.length;

    appLogs(appId)
    chartColumnStatistics(appId)
    changeDisplayButton(appId)
    
    if(index == length)
    {
        index = 0;
    }
}

function changeDisplayButton(appId)
{
    apps.forEach(app => {
        
        document.getElementById(app.id).style.backgroundColor = "transparent"
        document.getElementById(app.id).style.color = "gray"
    })

    document.getElementById(appId).style.backgroundColor = "#3c4b8e"
    document.getElementById(appId).style.color = "#fff"
}
function showColumnStatistics()
{
    getAjaxRequest("/App/GetList/")
        .then(result => {
            apps = result.data;
            let firstAppId = apps[0].id
            chartColumnStatistics(firstAppId)
            changeDisplayButton(firstAppId)
            appLogs(firstAppId)

            setInterval(processArray,4000)

        }).catch(error => {
        alert(error)
    })
}

onload = (event) => {
        showColumnStatistics()
};


function appLogs(appId)
{
    getAjaxRequestById("/App/GetAppLogs", appId)
        .then(result => {
            
            document.getElementById("log-monitor-content-list").innerHTML = ""
            result.data.forEach(log => {
                var element = ``
                
                if(log.isItFixed != true)
                {
                    element = `<li class="event-list active">    
                                <div class="event-timeline-dot">
                            <i class="bx bxs-right-arrow-circle font-size-18 bx-fade-right"></i>
                        </div>`
                }else{
                    element = `<li class="event-list">`
                }
                
                let content = `${element}
                        <div class="d-flex">
                            <div class="flex-shrink-0 me-3">
                                <h5 class="font-size-14"> ${new Date(log.logDate).toLocaleDateString("tr-TR", { month: "long", day: "numeric" })} <i class="bx bx-right-arrow-alt font-size-16 text-primary align-middle ms-2"></i></h5>
                            </div>
                            <div class="flex-grow-1">
                                <div>
                                    <a href="javascript: void(0);" onclick="logDetailForModal('${log.name}', '${log.logId}', '${log.appName}', '${log.content}', '${log.serverName}', '${log.serverIp}', '${log.logDate}', '${log.level}', '${log.userId}')" data-bs-toggle="offcanvas" data-bs-target="#offcanvasExample" aria-controls="offcanvasExample">${log.name}</a> · ${log.appName} 
                                </div>
                            </div>
                        </div>
                    </li>`
                
                document.getElementById("log-monitor-content-list").innerHTML += content
            })
        })
        .catch(error => {
            alert(error)
        })
}

function changeTrimmerContent(trimmedContent)
{
    let content = `<div style="overflow-wrap: break-word; word-wrap: break-word;">${trimmedContent}</div>`

    let otherContent = `<div class="dropdown mt-3">
                                               <button class="btn btn-primary" type="button" id="dropdownMenuButton" data-bs-toggle="dropdown">
                                                      Dropdown button <i class="mdi mdi-chevron-down"></i>
                                                </button>
                                                  <ul class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                                                 <li><a class="dropdown-item" href="#">Action</a></li>
                                                  <li><a class="dropdown-item" href="#">Another action</a></li>
                                                     <li><a class="dropdown-item" href="#">Something else here</a></li>
                                                 </ul>
                                         </div>`
    
    document.getElementById("offcanvasExampleContent").innerHTML = `${content} ${otherContent}`
}

function logDetailForModal(logName, logId, appName,logContent, serverName, serverIp, logDate, logLevel, userId)
{
    let labelText = `${logLevel} · ${appName} · ${logName}`
   document.getElementById("offcanvasExampleLabel").innerHTML = labelText

    let maxContentWorld = 940
    var isLogContentBigger = logContent.length > maxContentWorld
    var trimmedContent = isLogContentBigger ? logContent.substring(0, maxContentWorld) : logContent;

    if(isLogContentBigger)
    {
        trimmedContent += `.. <a href="javascript: void(0);" onclick="changeTrimmerContent('${logContent}')"> daha fazlası </a>` 
    }
    
    changeTrimmerContent(trimmedContent)
}
function chartColumnStatistics(appId)
{
    series = []
    info.data = [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0]
    warning.data = [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0]
    error.data = [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0]
    
    getAjaxRequestById("/Statistic/GetColumnChartStatisticsByAppId", appId)
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
        alert(error)
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