using Application.Business.Abstract;
using Application.Core.Utilities.DataTransferObjects.User;
using LogMonitoring.MVC.Constant;
using LogMonitoring.MVC.Services.Session;
using Microsoft.AspNetCore.Mvc;

namespace LogMonitoring.MVC.Controllers;

public class SessionController : Controller
{
    private readonly IUserManager _userManager;
    private readonly ISessionService _sessionService;
    public SessionController(IUserManager userManager, ISessionService sessionService)
    {
        _userManager = userManager;
        _sessionService = sessionService;
    }
    public IActionResult Index(string token)
    {
        if (string.IsNullOrEmpty(token))
        {
            return View("Error");
        }

        var response = _userManager.GetUserByTokenAsync(token).GetAwaiter().GetResult();

        if (response is null || !response.IsSuccess)
        {
            return View("Error");
        }
        
        _sessionService.Add<UserResponse>(SessionKey.CurrentUser, response.Data);
        
        return View();
    }
    
    public IActionResult Logout()
    {
        _sessionService.Remove(SessionKey.CurrentUser);
        return View();
    }
}