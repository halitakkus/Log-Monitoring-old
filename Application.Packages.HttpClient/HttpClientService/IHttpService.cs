using System.Threading.Tasks;

namespace Application.Packages.HttpClientService;

public interface IHttpService
{
    Task<T> GetAsync<T>(string url, string token);
    Task<T> PostAsync<T>(string url, object data, string token);
}