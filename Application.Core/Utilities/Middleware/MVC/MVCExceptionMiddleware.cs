﻿using Application.Core.Utilities.Exceptions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Primitives;

namespace Application.Core.Utilities.Middleware.MVC
{
    public class MVCExceptionMiddleware
    {
        private string _errorPageUrl;

        private RequestDelegate _next;

        public MVCExceptionMiddleware(RequestDelegate next, string errorPageUrl)
        {
            _next = next;
            _errorPageUrl = errorPageUrl;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                if (context.Request.Headers.TryGetValue("X-Forwarded-Host", out StringValues host))
                {
                    context.Request.Host = new HostString(host);
                }
                
                await _next(context);
            }
            catch (Exception e)
            {
                context.Response.Redirect(_errorPageUrl);
            }
        }
    }
}
