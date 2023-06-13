using Application.Business.Abstract;
using Application.Core.Utilities.DataTransferObjects.User;
using LogMonitoring.MVC.Constant;
using LogMonitoring.MVC.Services.Session;
using Microsoft.AspNetCore.Mvc;

namespace LogMonitoring.MVC.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ISessionService _sessionService;
    private readonly IAppManager _appManager;

    public HomeController(ILogger<HomeController> logger, ISessionService sessionService, IAppManager appManager)
    {
        _logger = logger;
        _sessionService = sessionService;
        _appManager = appManager;
    }

    public IActionResult Index()
    {
        if (!_sessionService.Any(SessionKey.CurrentUser))
            _sessionService.Add<UserResponse>(SessionKey.CurrentUser, new UserResponse(SessionKey.AnonimUser, SessionKey.AnonimUserPicture));
        
        var apps = _appManager.GetList();

        if (!apps.IsSuccess)
        {
            return View("Error");
        }

        if (apps.Data.Count() is 0)
        {
            return View("Error");
        }

        TempData["apps"] = apps.Data;
        
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View();
    }
}