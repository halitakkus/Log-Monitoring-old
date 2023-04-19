using LogMonitoring.MVC.Models;
using LogMonitoring.MVC.Services.HttpClientService;

namespace LogMonitoring.MVC.Services.Api.User;

public class UserService : IUserService
{
    private readonly IHttpService _httpService;
    
    public UserService(IHttpService httpService)
    {
        _httpService = httpService;
    }
    
    public async Task<LoginUser> GetUserByTokenAsync(string token)
    {
        return await _httpService.GetAsync<LoginUser>("login-user", token);
    }
}