using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Core.Entities.Concrete;
using Application.Core.Utilities.DataTransferObjects.User;

namespace Application.DataAccess.Services.Api;

public interface IUserService
{
     //Kullanıcı bilgisini döner.
     Task<User> GetUserByTokenAsync(string token);
     //id'si verilen bir kullanıcıyı getirir.
     Task<User> GetOtherUserAsync(string token, int userId);
     //Kullanıcı listesinde arama yapar.
     Task<List<SearcUserResponse>> GetSearchUsersAsync(string token, string searchKey);
}