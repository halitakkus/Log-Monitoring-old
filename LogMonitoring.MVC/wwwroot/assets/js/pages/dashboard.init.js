var series = []
var logSeries = []
var performance = {
    name: "Performance",
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

            setInterval(processArray,14000)

        }).catch(error => {
        alert(error)
    })
}

onload = (event) => {
        showColumnStatistics()
};

function isWithinLastDay(dateString) {
    var logDate = new Date(dateString);
    var today = new Date();
    var oneDay = 24 * 60 * 60 * 1000;
    var difference = today - logDate;
    return difference <= oneDay;
}
function appLogs(appId)
{
    getAjaxRequestById("/App/GetAppLogs", appId)
        .then(result => {
            document.getElementById("error-div").style = ""
            document.getElementById("info-div").style = ""
            document.getElementById("warning-div").style = ""
            logChartInstall(result.data.totalLogCount, result.data.fixedTotalLogCount)
            document.getElementById("log-monitor-content-list").innerHTML = ""
            document.getElementById("total-log-count").innerHTML = result.data.totalLogCount

            var ratio = Math.floor((result.data.totalLogCount / result.data.totalAppsLogCount) * 100);
            document.getElementById("text-log-ratio").innerHTML = `${ratio}%`
            
            document.getElementById("info-card-label").innerHTML = `${result.data.totalInfoLogCount}`
            document.getElementById("warning-card-label").innerHTML = `${result.data.totalWarningLogCount}`
            document.getElementById("error-card-label").innerHTML = `${result.data.totalErrorLogCount}`
            
            result.data.logs.forEach(log => {

                let labelColor = `#e83e8c`

                if(log.level == "Warning")
                {
                    labelColor = "#f1b44c"
                }else if (log.level == "Performance")
                {
                    labelColor = "#556ee6"
                }

                let labelText = `<code class="highlighter-rouge" style="font-size:16px;color: ${labelColor}">${log.level}</code>`
                
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
                                     <a href="javascript: void(0);" onclick="logDetailForModal('${log.name}', '${log.logId}', '${log.appName}', '${log.content}', '${log.serverName}', '${log.serverIp}', '${log.logDate}', '${log.level}','${labelColor}', '${log.userId}', '${log.isItFixed}')" data-bs-toggle="offcanvas" data-bs-target="#offcanvasExample" aria-controls="offcanvasExample">${labelText}</a> · ${log.name} · ${log.appName} 
                                </div>
                            </div>
                        </div>
                    </li>`
                
                document.getElementById("log-monitor-content-list").innerHTML += content
            })
            if(result.data.logs.length == 0)
            {
                document.getElementById("log-monitor-content-list").innerHTML = "<span class='m-2'>Log kaydı bulunamadı.</span>"
            }
           

            var isErrorCount = false;
            var isInfoCount = false;
            var isWarningCount = false;
            
            console.log(result.data.logs)
            for (var key in result.data.logs) {
                if (result.data.logs[key].isItFixed === false && isWithinLastDay(result.data.logs[key].logDate) && result.data.logs[key].level === 'Error') {
                    isErrorCount = true;
                }
                if (result.data.logs[key].isItFixed === false && isWithinLastDay(result.data.logs[key].logDate) && result.data.logs[key].level === 'Warning') {
                    isWarningCount = true;
                }
                if (result.data.logs[key].isItFixed === false && isWithinLastDay(result.data.logs[key].logDate) && result.data.logs[key].level === 'Performance') {
                    isInfoCount = true;
                }
            }
            
            if(isErrorCount)
            {
                document.getElementById("error-div").style =  "border-style: solid;border-width: 8px;border-color:rgb(184, 0, 70)"
            } 
            
            if(isWarningCount)
            {
                document.getElementById("warning-div").style =  "border-style: solid;border-width: 2px;border-color:rgb(241, 180, 76)"
            } 
            
            if(isInfoCount)
            {
                document.getElementById("info-div").style =  "border-style: solid;border-width: 1px;border-color:rgb(85, 110, 230)"
            }
        })
        .catch(error => {
            alert(error)
        })
}

function changeTrimmerContent(trimmedContent)
{
    let content = `<strong>Hata Açıklaması:</strong> 
 <div style="overflow-wrap: break-word; word-wrap: break-word;">${trimmedContent}</div>`

    document.getElementById("offcanvasExampleContent").innerHTML = `${content}`
}
function logDetailForModal(logName, logId, appName,logContent, serverName, serverIp, logDate, logLevel,labelColor, userId, isItFixed)
{
    let labelText = `<code class="highlighter-rouge" style="font-size:16px;color: ${labelColor}">${logLevel}</code>`
    
    document.getElementById("offcanvasExampleLabel").innerHTML = `${labelText} · <cite title="Source Title">${appName}</cite>`

    let maxContentWorld = 940
    var isLogContentBigger = logContent.length > maxContentWorld
    var trimmedContent = isLogContentBigger ? logContent.substring(0, maxContentWorld) : logContent;
    
    if(isLogContentBigger)
    {
        trimmedContent += `.. <a href="javascript: void(0);" onclick="changeTrimmerContent('${logContent}')"> daha fazlası </a>` 
    }
    
    let doesTheErrorPersist = isItFixed == "false" ? "EVET" : "HAYIR"

    let otherContent =  `<div>
  <p><strong>Hata No:</strong> ${logId}</p>
  <p><strong>Hata Başlığı:</strong> ${logName}</p>
  <p><strong>Uygulama Adı:</strong> ${appName}</p>
  <p><strong>Sunucu Adı:</strong> ${serverName}</p>
  <p><strong>Sunucu Ip Numarası:</strong> ${serverIp}</p>
  <p><strong>Hata Tarihi:</strong> ${new Date(logDate).toLocaleDateString("tr-TR")}</p>
  <p><strong>Kullanıcı:</strong> ${userId}</p>
  <p><strong>Hata Seviyesi:</strong> ${logLevel}</p>
  <p><strong>Hata Devam Ediyor Mu?:</strong> ${doesTheErrorPersist}</p>
  </div>`

    document.getElementById("offcanvasExampleOtherContent").innerHTML = otherContent
    
    changeTrimmerContent(trimmedContent)
}
function chartColumnStatistics(appId)
{
    series = []
    logSeries = []
    
    performance.data = [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0]
    warning.data = [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0]
    error.data = [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0]
    
    getAjaxRequestById("/Statistic/GetColumnChartStatisticsByAppId", appId)
        .then(result => {
            if(result.data != null)
            {
              result.data.columnChartModels.forEach(i=> {//Performance için

                let dateObject = new Date(i.dateMon);
                let dateMonth = dateObject.getMonth() + 1

                const foundEntry = i.statistics.find(entry => entry.key === performance.name);

                if(foundEntry)
                {
                    const foundValue = foundEntry.value
                    performance.data[dateMonth - 1] = foundValue
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
            
            series.push(performance)
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
function logChartInstall(totalLogCount, fixedLogCount)
{
    if(totalLogCount == 0)
        totalLogCount = 1
    var achievement = Math.floor((fixedLogCount / totalLogCount) * 100);
   
    logSeries.push(achievement)
    document.getElementById("radialBar-chart").innerHTML = ""
    
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
        series: logSeries,
        labels: ["Düzeltilme Oranı"]
    }, (chart = new ApexCharts(document.querySelector("#radialBar-chart"), options)).render());
}


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