using Application.Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace LogMonitoring.MVC.Controllers;

public class StatisticController : Controller
{
    private readonly ILogManager _logManager;

    public StatisticController(ILogManager logManager)
    {
        _logManager = logManager;
    }
    
    [HttpGet("column-chart-statistics/{id}")]
    public IActionResult GetColumnChartStatisticsByAppId(Guid id)
    {
        var response = _logManager.StatisticColumnChart(id);

        if (response is null || !response.IsSuccess)
        {
            return View("Error");
        }
        
        return Json(response);
    }
}