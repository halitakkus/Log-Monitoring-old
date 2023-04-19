using System.Threading.Tasks;
using Application.Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace RW.WebAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class UserController: ControllerBase
{
    private readonly IUserManager _userManager;

    public UserController(IUserManager userManager)
    {
        _userManager = userManager;
    }

    [HttpGet]
    [Route("login-user")]
   public async Task<IActionResult> GetLoginUser()
    {
        var result = await _userManager.GetLoginUser();

        if (!result.IsSuccess)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }
}