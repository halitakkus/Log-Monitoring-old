using Application.Business.Abstract;
using Application.Business.Extensions;
using Application.Core.Configuration.Context;
using Application.Core.Configuration.Environment;
using Application.Core.Extensions;
using Application.Packages.Hashing.MD5.Extensions;
using Application.Packages.JWT.Entities;
using Application.Packages.JWT.Extensions;
using LogMonitoring.MVC.Services.Session;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

IApplicationConfigurationContext ConfigurationContext = new ApplicationConfigurationContext(new EnvironmentService());
// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

// Register core module. 🎉
builder.Services.AddCoreModule();
builder.Services.AddSession();
// Register business module. 🎉
builder.Services.AddBusinessModule(ConfigurationContext);

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Central Log Monitorning", Version = "v1" });
});


builder.Services.AddMD5();
builder.Services.AddSingleton<ISessionService, SessionService>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddJWT(configuration =>
{
    configuration.SecretKey = ConfigurationContext.JWTKey;
    configuration.Issuer = ConfigurationContext.JWTIssuer;
    configuration.Audience = ConfigurationContext.JWTAudience;
    configuration.ExpiryHour = ConfigurationContext.JWTExpiryHour;
    configuration.TokenSecurityAlgorithms = EnumTokenSecurityAlgorithms.HmacSha256;
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Central Log Monitorning");
});
app.UseRouting();
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();