using System;
using Application.Core.AspectOrientedProgramming.AOP.Interceptor;
using Castle.DynamicProxy;

namespace Application.Core.AspectOrientedProgramming.Aspects.Exception
{
    /// <summary>
    /// ExceptionAspect works when method is throwing exception
    /// </summary>
    public class ExceptionAspect : MethodInterceptor
    {
        public override void OnException(IInvocation invocation, System.Exception exception)
        {
            Console.WriteLine($"[AppExceptionAspect] {exception}");
        }
    }
}
