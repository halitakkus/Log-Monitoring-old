using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;

namespace LogMonitoring.MVC.Services.HttpClientService;

public class HttpService : IHttpService
{
    private readonly string? _baseAddress;
    private readonly IConfiguration _configuration;
    
    public HttpService(IConfiguration configuration)
    {
        _configuration = configuration;
        _baseAddress = _configuration.GetSection("http-base-address").Value;
    }
    public async Task<T> GetAsync<T>(string url, string token)
    {
        var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri(_baseAddress);

        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        
        var response = await httpClient.GetAsync(url);

        var responseValueAsString = await response.Content.ReadAsStringAsync();

        return JsonConvert.DeserializeObject<T>(responseValueAsString);
    }

    public async Task<T> PostAsync<T>(string url, object data, string token)
    {
        var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri(_baseAddress);
        
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var requestDataAsJson = JsonConvert.SerializeObject(data);
        var stringContent = new StringContent(requestDataAsJson, Encoding.UTF8, "application/json");

        var response = await httpClient.PostAsync(url, stringContent);

        var responseValueAsString = await response.Content.ReadAsStringAsync();

        return JsonConvert.DeserializeObject<T>(responseValueAsString);
    }
}