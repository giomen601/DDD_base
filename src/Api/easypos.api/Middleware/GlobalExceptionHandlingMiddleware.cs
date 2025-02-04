using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace easypos.api.Middleware;
public class GlobalExceptionHandlingMiddleware(ILogger<GlobalExceptionHandlingMiddleware> logger)
: IMiddleware
{
  public async Task InvokeAsync(HttpContext context, RequestDelegate next)
  {
    try
    {
      await next(context);
    }catch(Exception ex)
    {
      logger.LogError(ex, ex.Message);
      context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

      ProblemDetails problem = new()
      {
        Status = (int)HttpStatusCode.InternalServerError,
        Type = "Server Error",
        Title = "Server Error",
        Detail = "An internal server error has ocurred"
      };

      string json = JsonSerializer.Serialize(problem);

      context.Response.ContentType = "application/json";
      await context.Response.WriteAsync(json);
    }
  }
}