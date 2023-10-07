using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using WebMaze.MyExceptions;

namespace WebApplication4.Services
{
    public class GlobalErrorHandlerMidlleware
    {
        private readonly RequestDelegate _next;

        public GlobalErrorHandlerMidlleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (UserNotFoundException)
            {
                context.Response.Redirect("/User/UserNotFoundError");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
