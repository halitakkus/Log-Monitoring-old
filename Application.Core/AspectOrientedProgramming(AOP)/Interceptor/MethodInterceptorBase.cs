using System;
using Castle.DynamicProxy;

namespace Application.Core.AspectOrientedProgramming.Interceptor;

    public abstract class MethodInterceptorBase : Attribute, IInterceptor
    {
        /// <summary>
        /// Working priority
        /// </summary>
        public int Priority { get; set; }
        /// <summary>
        /// Intercept method
        /// </summary>
        /// <param name="invocation"></param>
        public abstract void Intercept(IInvocation invocation);
    }

