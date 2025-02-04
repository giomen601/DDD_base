using easypos.application.Customers.Create;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace easypos.api.Controllers;
[Route("api/[controller]")]
public class CustomerController : ApiController
{
  private readonly ISender sender;

  public CustomerController
  (
    ISender sender
  )
  {
    this.sender = sender ?? throw new ArgumentNullException(nameof(sender));
  }

  [HttpPost]
  public async Task<IActionResult> Create([FromBody] CreateCustomerCommand command)
  {
    var result = await sender.Send(command);

    return result.Match(
      customer => Ok(),
      errors => Problem(errors)
      );
  }
}