using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace EmirhanAvci.WebApi.Middleware
{
    #region ErrorHandlerMiddlewareDesc
    /*
     * Global Error Handler Middleware - tüm istisnaları yakalayan ve işleyen ve istisna türüne göre hangi HTTP yanıt kodunun döndürüleceğini belirleyen özel ara yazılım.
     */
    #endregion
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";          //For Other languages, its global language type.

                switch (error)
                {
                    case AppException e:
                        // custom application error
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case KeyNotFoundException e:
                        // not found error
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    default:
                        // unhandled error
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }
                var result = JsonSerializer.Serialize(new { message = error?.Message });
                await response.WriteAsync(result);
            }
        }
    }

    #region AppExceptionDesc
    /*Özel Uygulama İstisnası - 400 yanıt veren işlenen istisnalar ile 500 yanıt veren işlenmeyen istisnalar arasında ayrım yapmak için kullanılan özel bir istisna sınıfı.*/
    #endregion
    public class AppException : Exception
    {
        public AppException() : base() { }

        public AppException(string message) : base(message) { }

        public AppException(string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
