using System.Collections.Generic;
using Application.Core.AspectOrientedProgramming.AOP.Interceptor;
using Application.Core.Utilities.DependencyServiceTool;
using Castle.DynamicProxy;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Core.AspectOrientedProgramming.AOP.Aspects.Secure;

public class SecuredOperationAspect : MethodInterceptor
{
    private string[] _roles;
    private IHttpContextAccessor _httpContextAccessor;

    public SecuredOperationAspect(string roles)
    {
        _roles = roles.Split(',');
        _httpContextAccessor =  DependencyServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
    }

    public override void OnBefore(IInvocation invocation)
    {
        //TODO: Buraya kullanıcı rolleri eklenecek.
        //List<string> roleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles(); gibi
        List<string> roleClaims = new List<string>();
        
        foreach (var role in _roles)
        {
            if (roleClaims.Contains(role))
            {
                return;
            }
        }
        throw new System.Exception("Erişim Engellendi.");
    }
}