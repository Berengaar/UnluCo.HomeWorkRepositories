using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmirhanAvci.WebApi.Filters
{
    public class GlobalHeaderResultFilter : IActionFilter
    {

        public void OnActionExecuting(ActionExecutingContext context)
        {
            context.HttpContext.Response.Headers.Add("Request", DateTime.Now.ToString("MM/dd/yyyy h:mm tt"));
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            context.HttpContext.Response.Headers.Add("Response", DateTime.Now.ToString("MM/dd/yyyy h:mm tt"));
        }


    }
}
