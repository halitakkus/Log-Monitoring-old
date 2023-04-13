using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Core.Entities.Concrete;
using Application.Core.Utilities.DataTransferObjects.User;
using Application.DataAccess.Services.HttpClientService;

namespace Application.DataAccess.Services.Api;

public class UserSevice : IUserService
{
    private readonly IHttpService _httpService;
    
    public UserSevice(IHttpService httpService)
    {
        _httpService = httpService;
    }
    
    public async Task<User> GetUserByTokenAsync(string token)
    {
        return await _httpService.PostAsync<User>("profilbilgilerinigetir",new UserRequest(null), token);
    }

    public async Task<User> GetOtherUserAsync(string token, int userId)
    {
        var user = await _httpService.PostAsync<User>("profilbilgilerinigetir", new UserRequest(userId), token);

        return user;
    }

    public async Task<List<SearcUserResponse>> GetSearchUsersAsync(string token, string searchText)
    {
        var searchModel = new SearchUserRequest(searchText);

        var searchUsers = await _httpService.PostAsync<List<SearcUserResponse>>("personelArama", searchModel, token);

        return searchUsers;
    }
}