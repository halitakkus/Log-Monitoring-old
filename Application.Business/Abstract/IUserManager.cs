
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Core.Entities.Concrete;
using Application.Core.Utilities.DataTransferObjects.User;
using Application.Core.Utilities.Result;

namespace Application.Business.Abstract
{
    public interface IUserManager
    {
        Task<IDataResult<User>> GetUserByTokenAsync(string token);
        Task<IDataResult<User>> GetOtherUserAsync(string token, int userId);
        Task<IDataResult<User>> GetLoginUser();
    }
}
