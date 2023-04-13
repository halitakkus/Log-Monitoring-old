using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Application.DataAccess.Abstract;
using Application.DataAccess.Concrete.EntityFramework;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Application.Packages.HttpClientService;

public class HttpService : IHttpService
{
 
    private readonly ISettingDal _settingDal;

    public HttpService(IConfiguration configuration, ISettingDal settingDal)
    {
        _settingDal = settingDal;
    }
    public async Task<T> GetAsync<T>(string url, string token)
    {
        var bilgemBaseAddress = _settingDal.Find(f => f.Key == "BilgemAppBaseUrl");

        using var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri(bilgemBaseAddress?.Value!);
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        
        var response = await httpClient.GetAsync(url);
        var responseValueAsString = await response.Content.ReadAsStringAsync();

        return JsonConvert.DeserializeObject<T>(responseValueAsString)!;
    }

    public async Task<T> PostAsync<T>(string url, object data, string token)
    {
        var bilgemBaseAddress = _settingDal.Find(f => f.Key == "BilgemAppBaseUrl");

        using var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri(bilgemBaseAddress?.Value!);
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var requestDataAsJson = JsonConvert.SerializeObject(data);
        var stringContent = new StringContent(requestDataAsJson, Encoding.UTF8, "application/json");
        var response = await httpClient.PostAsync(url, stringContent);
        var responseValueAsString = await response.Content.ReadAsStringAsync();

        return JsonConvert.DeserializeObject<T>(responseValueAsString)!;
    }
}