using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using warehouse.Exceptions;

namespace warehouse
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (NotFound ex)
            {
                Console.WriteLine(ex + "  " + ex.Message);
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex + "  " + ex.Message);
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("something goes wrong.");
            }
        }
    }
}
