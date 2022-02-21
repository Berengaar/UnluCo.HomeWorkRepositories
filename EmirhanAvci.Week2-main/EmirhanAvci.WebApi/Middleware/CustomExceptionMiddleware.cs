using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace EmirhanAvci.WebApi.Middleware
{
    public class CustomExceptionMiddleware
    {

        private readonly RequestDelegate _next;
        public CustomExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            // Time Passing
            var watch = Stopwatch.StartNew();
            string message = "[Request] HTTP " + context.Request.Method + " - " + context.Request.Path;
            Console.WriteLine(message);

            await _next(context);
            watch.Stop();
            message = "[Response] HTTP " + context.Request.Method + " - " + context.Request.Path + "responded "+context.Response.StatusCode+ " in "+watch.Elapsed.TotalMilliseconds +" ms";

            Console.WriteLine(message);
        }
    }

    
}
