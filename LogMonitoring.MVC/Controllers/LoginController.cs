using LogMonitoring.MVC.Services.Api.User;
using Microsoft.AspNetCore.Mvc;

namespace LogMonitoring.MVC.Controllers;

public class LoginController : Controller
{
    private readonly IUserService _userService;

    public LoginController(IUserService userService)
    {
        _userService = userService;
    }
    public IActionResult Index(string token)
    {
        if (string.IsNullOrEmpty(token))
        {
            return View("Error");
        }

        var response = _userService.GetUserByTokenAsync(token).GetAwaiter().GetResult();

        return View();
    }
}