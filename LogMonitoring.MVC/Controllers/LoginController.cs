using Microsoft.AspNetCore.Mvc;

namespace LogMonitoring.MVC.Controllers;

public class LoginController : Controller
{
    public IActionResult Index(string token)
    {
        if (string.IsNullOrEmpty(token))
        {
            return View("Error");
        }
        
        return View();
    }
}