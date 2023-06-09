using Application.Business.Abstract;
using Application.Core.Utilities.DataTransferObjects_DTO_;
using Application.Core.Utilities.DataTransferObjects_DTO_.App;
using Application.Core.Utilities.DataTransferObjects_DTO_.Log;
using Microsoft.AspNetCore.Mvc;

namespace AppController.MVC.Controllers;

public class AppController: Controller
{
    private readonly IAppManager _appManager;
    private readonly ILogManager _logManager;

    public AppController(IAppManager appManager, ILogManager logManager)
    {
        _appManager = appManager;
        _logManager = logManager;
    }
    
    public IActionResult GetById(Guid id)
    {
        var response = _appManager.GetById(id);

        if (response is null || !response.IsSuccess)
        {
            return View("Error");
        }
        
        return Json(response);
    }
    
    public IActionResult InsertApp(AppRequest request)
    {
        var response = _appManager.Insert(request);

        if (response is null || !response.IsSuccess)
        {
            return View("Error");
        }
        
        return Json(response);
    }
    
    public IActionResult InsertLog(LogRequest request)
    {
        var response = _appManager.InsertLog(request);

        if (response is null || !response.IsSuccess)
        {
            return View("Error");
        }
        
        return Json(response);
    }
    
    public IActionResult GetList()
    {
        var response = _appManager.GetList();

        if (response is null || !response.IsSuccess)
        {
            return View("Error");
        }
        
        return Json(response);
    }
    
    public IActionResult GetAppLogs(Guid id)
    {
        var response = _logManager.GetLogs(id);

        if (response is null || !response.IsSuccess)
        {
            return View("Error");
        }
        
        return Json(response);
    }
    
    public IActionResult Remove(RemoveEntityDTO removeEntityDto)
    {
        var response = _appManager.Remove(removeEntityDto);

        if (response is null || !response.IsSuccess)
        {
            return View("Error");
        }
        
        return Json(response);
    }
}