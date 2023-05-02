using LogMonitoring.MVC.Constant;
using LogMonitoring.MVC.Services.Session;
using Microsoft.AspNetCore.Mvc;

namespace LogMonitoring.MVC.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ISessionService _sessionService;

    public HomeController(ILogger<HomeController> logger, ISessionService sessionService)
    {
        _logger = logger;
        _sessionService = sessionService;
    }

    public IActionResult Index()
    {
        if(!_sessionService.Any(SessionKey.CurrentUser)) return View("Error");
        
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View();
    }
}