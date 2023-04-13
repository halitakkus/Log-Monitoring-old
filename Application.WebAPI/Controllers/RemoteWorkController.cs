using Application.Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using Application.Packages.JWT.Filters;


namespace RW.WebAPI.Controllers
{
    [Route("")]
    [ApiController]
    [TypeFilter(typeof(ApplicationTokenAuthFilter))]
    public class RemoteworkController : ControllerBase
    {
        private readonly IRemoteWorkManager _remoteworkManager;
       
        private readonly IUserManager _userManager;
        public RemoteworkController(IRemoteWorkManager remoteworkManager, IUserManager userManager)
        {
            _remoteworkManager = remoteworkManager;
            _userManager = userManager;
        }
      
    }
}
