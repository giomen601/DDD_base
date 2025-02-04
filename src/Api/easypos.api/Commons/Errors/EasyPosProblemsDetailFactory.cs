using easypos.api.Commons.Http;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Diagnostics;

namespace easypos.api.Commons.Errors;
public class EasyPosProblemsDetailFactory : ProblemDetailsFactory
{
  private readonly ApiBehaviorOptions _options;

  public EasyPosProblemsDetailFactory(ApiBehaviorOptions options)
  {
    _options = options ?? throw new ArgumentNullException(nameof(options));
  }

  public override ProblemDetails CreateProblemDetails
  (
    HttpContext httpContext,
    int? statusCode = null,
    string? title = null,
    string? type = null,
    string? detail = null,
    string? instance = null
  )
  {
    statusCode ??= 500;
    var problemDetail = new ProblemDetails
    {
      Status = statusCode,
      Title = title,
      Type = type,
      Detail = detail,
      Instance = instance
    };

    ApplyProblemDetailsDefaults(httpContext, problemDetail, statusCode.Value);

    return problemDetail;
  }

  public override ValidationProblemDetails CreateValidationProblemDetails
  (
    HttpContext httpContext,
    ModelStateDictionary modelStateDictionary,
    int? statusCode = null,
    string? title = null,
    string? type = null,
    string? detail = null,
    string? instance = null
  )
  {
    if (modelStateDictionary == null)
      throw new ArgumentNullException(nameof(modelStateDictionary));

    statusCode ??= 400;

    ValidationProblemDetails problemDetails = new(modelStateDictionary)
    {
      Status = statusCode,
      Type = type,
      Detail = detail,
      Instance = instance
    };

    if (title is not null)
      problemDetails.Title = title;

    ApplyProblemDetailsDefaults(httpContext, problemDetails, statusCode.Value);

    return problemDetails;
  }

  private void ApplyProblemDetailsDefaults(HttpContext httpContext, ProblemDetails problemDetails, int statusCode)
  {
    problemDetails.Status ??= statusCode;
    if(_options.ClientErrorMapping.TryGetValue(statusCode, out var clientErrorData))
    {
      problemDetails.Title ??= clientErrorData.Title;
      problemDetails.Type ??= clientErrorData.Link;
    }

    string traceId = Activity.Current?.Id ?? httpContext.TraceIdentifier;

    if (traceId != null)
      problemDetails.Extensions["traceId"] = traceId;

    List<Error> errors = httpContext?.Items[HttpContextItemKeys.Error] as List<Error>;

    if (errors is not null)
      problemDetails.Extensions.Add("ErrorCodes", errors.Select(e => e.Code));
  }
}