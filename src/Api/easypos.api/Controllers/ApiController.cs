using easypos.api.Commons.Http;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace easypos.api.Controllers;
[ApiController]
public class ApiController : ControllerBase
{
  protected IActionResult Problem(List<Error> errors)
  {
    if (errors.Count is 0)
      return Problem();

    if (errors.All(err => err.Type == ErrorType.Validation))
      return ValidationProblem(errors);

    HttpContext.Items[HttpContextItemKeys.Error] = errors;

    return Problem(errors[0]);
  }

  private IActionResult Problem(Error error)
  {
    var statusCode = error.Type switch
    {
      ErrorType.Conflict => StatusCodes.Status409Conflict,
      ErrorType.Validation => StatusCodes.Status400BadRequest,
      _ => StatusCodes.Status500InternalServerError
    };

    return Problem(statusCode: statusCode, title: error.Description);
  }

  private IActionResult ValidationProblem(List<Error> errors)
  {
    ModelStateDictionary modelStateDictionary = new();

    foreach (var error in errors)
    {
      modelStateDictionary.AddModelError(error.Code, error.Description);
    }

    return ValidationProblem(modelStateDictionary);
  }
}