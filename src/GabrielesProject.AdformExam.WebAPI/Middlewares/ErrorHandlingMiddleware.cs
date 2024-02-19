using GabrielesProject.AdformExam.Domain.Exceptions;
using System.Net;
using System.Text.Json;

namespace GabrielesProject.AdformExam.WebAPI.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
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
                response.ContentType = "application/json";

                switch (error)
                {
                    case NotFoundException:
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;

                    case StatusException:
                        response.StatusCode = (int)HttpStatusCode.BadRequest; 
                        break;

                    case NotCreatedException:
                        response.StatusCode= (int)HttpStatusCode.BadRequest;
                        break;

                    case CleanupException:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;

                    default: response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }
                var result = JsonSerializer.Serialize(new { message = error?.Message });
                await response.WriteAsync(result);
            }
        }
    }
}

