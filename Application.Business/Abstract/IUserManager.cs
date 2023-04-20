using System.Threading.Tasks;
using Application.Core.Utilities.DataTransferObjects.User;
using Application.Core.Utilities.Result;

namespace Application.Business.Abstract
{
    public interface IUserManager
    {
        Task<IDataResult<UserResponse>> GetLoginUser();
        Task<IDataResult<UserResponse>> GetUserByTokenAsync(string token);
    }
}
