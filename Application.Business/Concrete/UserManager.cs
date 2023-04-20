using System.Linq;
using System.Threading.Tasks;
using Application.Business.Abstract;
using Application.Core.Constants.Messages;
using Application.Core.Utilities.DataTransferObjects.User;
using Application.Core.Utilities.Result;
using Application.DataAccess.Entities;
using Application.DataAccess.Services.Api;
using AutoMapper;
using Microsoft.AspNetCore.Http;

namespace Application.Business.Concrete
{
    public class UserManager : IUserManager
    {
        private readonly IUserService _userService;
        private IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        public UserManager(IUserService userService, IHttpContextAccessor contextAccessor, IMapper mapper)
        {
            _userService = userService;
            _httpContextAccessor = contextAccessor;
            _mapper = mapper;
        }
       
        public async Task<IDataResult<UserResponse>> GetUserByTokenAsync(string token)
        {
            if(string.IsNullOrEmpty(token)) return new ErrorDataResult<UserResponse>(ResultMessages.EmptyOrNullContent, ResultMessages.EmptyOrNullContent);
            
            var user = await _userService.GetUserByTokenAsync(token);

            if (user is null) return new ErrorDataResult<UserResponse>(ResultMessages.NotFound, ResultMessages.NotFound);

            return new SuccessDataResult<UserResponse>(user);
        }

        public async Task<IDataResult<UserResponse>> GetLoginUser()
        {
            var token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString();
            var user = await _userService.GetUserByTokenAsync(token.Split(" ").LastOrDefault());
            
            return new SuccessDataResult<UserResponse>(user);
        }
    }
}
