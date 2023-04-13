using System.Linq;
using System.Threading.Tasks;
using Application.Business.Abstract;
using Application.Core.Constants.Messages;
using Application.Core.Utilities.Result;
using Application.DataAccess.Entities;
using Application.DataAccess.Services.Api;
using FluentValidation;
using Application.Packages.Hashing.Core.Service;
using Application.Packages.JWT.Service;
using Microsoft.AspNetCore.Http;

namespace Application.Business.Concrete
{
    public class UserManager : IUserManager
    {
        private readonly IUserService _userService;
        private readonly IHashService _hashService;
        private readonly ITokenService _tokenService;
        private readonly IValidator<User> _userValidator;
        private IHttpContextAccessor _httpContextAccessor;
        public UserManager(IValidator<User> userValidator, IHashService hashService, ITokenService tokenService, IUserService userService, IHttpContextAccessor contextAccessor)
        {
            _hashService = hashService;
            _tokenService = tokenService;
            _userValidator = userValidator;
            _userService = userService;
            _httpContextAccessor = contextAccessor;
        }

        public async Task<IDataResult<User>> GetUserByTokenAsync(string token)
        {
            if(string.IsNullOrEmpty(token)) return new ErrorDataResult<User>(ResultMessages.EmptyOrNullContent, ResultMessages.EmptyOrNullContent);
            
            var user = await _userService.GetUserByTokenAsync(token);

            if (user is null) return new ErrorDataResult<User>(ResultMessages.NotFound, ResultMessages.NotFound);

            return new SuccessDataResult<User>(user);
        }

        public async Task<IDataResult<User>> GetOtherUserAsync(string token, int userId)
        {
            if (userId == default(int))
                return new ErrorDataResult<User>(ResultMessages.EmptyOrNullContent, ResultMessages.EmptyOrNullContent);
            
            if(string.IsNullOrEmpty(token)) return new ErrorDataResult<User>(ResultMessages.EmptyOrNullContent, ResultMessages.EmptyOrNullContent);
            
            var user = await _userService.GetOtherUserAsync(token, userId);

            if (user is null) return new ErrorDataResult<User>(ResultMessages.NotFound, ResultMessages.NotFound);

            return new SuccessDataResult<User>(user);
        }
       
        public async Task<IDataResult<User>> GetLoginUser()
        {
            var token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString();
            var user = await _userService.GetUserByTokenAsync(token.Split(" ").LastOrDefault());
            
            return new SuccessDataResult<User>(user);
        }
    }
}
