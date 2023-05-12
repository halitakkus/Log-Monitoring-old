using Application.Business.Abstract;
using Application.Core.Utilities.DataTransferObjects_DTO_;
using Microsoft.AspNetCore.Mvc;

namespace AppController.MVC.Controllers;

public class AppController: Controller
{
    private readonly IAppManager _appManager;
    
    public AppController(IAppManager appManager)
    {
        _appManager = appManager;
    }
    
    [HttpGet("{id}")]
    public IActionResult GetById(Guid id)
    {
        var response = _appManager.GetById(id);

        if (response is null || !response.IsSuccess)
        {
            return View("Error");
        }
        
        return Json(response);
    }
    
    [HttpGet("apps")]
    public IActionResult GetList()
    {
        var response = _appManager.GetList();

        if (response is null || !response.IsSuccess)
        {
            return View("Error");
        }
        
        return Json(response);
    }
    
    [HttpDelete("{id}")]
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