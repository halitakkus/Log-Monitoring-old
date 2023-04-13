using System.Diagnostics;
using Application.Core.Utilities.DependencyServiceTool;
using Application.Packages.AOP.Interceptor;
using Castle.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;


namespace Application.Packages.AOP.Aspects.Performance;

public class PerformanceAspect : MethodInterceptor
{
    private int _interval;
    private Stopwatch _stopwatch;

    public PerformanceAspect(int interval)
    {
        _interval = interval;
        _stopwatch = DependencyServiceTool.ServiceProvider.GetService<Stopwatch>();
    }
    public override void OnBefore(IInvocation invocation)
    {
        _stopwatch.Start();
    }

    public override void OnAfter(IInvocation invocation)
    {
        if (_stopwatch.Elapsed.TotalSeconds>_interval)
        {
            Debug.WriteLine($"Performance : {invocation.Method.DeclaringType.FullName}.{invocation.Method.Name}-->{_stopwatch.Elapsed.TotalSeconds}");
        }
        _stopwatch.Reset();
    }
}