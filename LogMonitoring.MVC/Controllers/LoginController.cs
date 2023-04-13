using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LogMonitoring.MVC.Models;

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