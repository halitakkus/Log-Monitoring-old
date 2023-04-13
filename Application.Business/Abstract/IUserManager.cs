using System.Threading.Tasks;
using Application.Core.Utilities.Result;
using Application.DataAccess.Entities;

namespace Application.Business.Abstract
{
    public interface IUserManager
    {
        Task<IDataResult<User>> GetUserByTokenAsync(string token);
        Task<IDataResult<User>> GetOtherUserAsync(string token, int userId);
        Task<IDataResult<User>> GetLoginUser();
    }
}
