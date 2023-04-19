using LogMonitoring.MVC.Models;

namespace LogMonitoring.MVC.Services.Api.User;

public interface IUserService
{
    Task<LoginUser> GetUserByTokenAsync(string token);
}