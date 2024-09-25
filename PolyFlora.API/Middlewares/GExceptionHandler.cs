using Newtonsoft.Json;
using System.Net;

namespace PolyFlora.API.Middlewares
{
    public class GExceptionHandler
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GExceptionHandler> _logger;

        public GExceptionHandler(RequestDelegate next, ILogger<GExceptionHandler> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {               
                await _next(context);
            }
            catch (Exception ex)
            {               
                _logger.LogError(ex, "An error occurred");
                
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {            
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";
           
            var response = new
            {
                message = "An error occurred while executing the task",
                detail = exception.Message // DEV
            };
           
            return context.Response.WriteAsync(JsonConvert.SerializeObject(response));
        }
    }
}
