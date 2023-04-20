using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Core.Utilities.DataTransferObjects.User;
using Application.DataAccess.Entities;

namespace Application.DataAccess.Services.Api;

public interface IUserService
{
     Task<UserResponse> GetUserByTokenAsync(string token);
}