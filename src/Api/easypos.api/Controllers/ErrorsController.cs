using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace easypos.api.Controllers;
public class ErrorsController : ControllerBase
{
  [ApiExplorerSettings(IgnoreApi = true)]
  [Route("/error")] //es la misma ruta que está en program (app.UseExceptionHandler("/error");)
  public IActionResult Error()
  {
    Exception? exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
    return Problem();
  }
}