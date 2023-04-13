using System.Linq;
using Application.Core.AspectOrientedProgramming.AOP.Interceptor;
using Application.Core.Utilities.DependencyServiceTool;
using Application.Packages.Caching.Core.Service;
using Castle.DynamicProxy;
using Newtonsoft.Json;

namespace Application.Core.AspectOrientedProgramming.Aspects.Caching;

public class CacheAspect : MethodInterceptor
{
    private int _timeout;
    private readonly ICacheService _cacheService;

    public CacheAspect(int timeout = 60)
    {
        _timeout = timeout;
        var service = DependencyServiceTool.ServiceProvider.GetService(typeof(ICacheService));

        _cacheService = (ICacheService)service;
    }

    public override void Intercept(IInvocation invocation)
    {
        var fullName = $"{invocation.Method.ReflectedType.FullName}.{invocation.Method.Name}";

        var arguments = invocation.Arguments.ToArray();

        var key = $"{fullName}({JsonConvert.SerializeObject(arguments)})";
        
        if (_cacheService.Any(key))
        {
            invocation.ReturnValue = _cacheService.Get<string>(key);
        }
        else
        {
            invocation.Proceed();

            _cacheService.Add(key, invocation.ReturnValue, _timeout);
        }
    }
}