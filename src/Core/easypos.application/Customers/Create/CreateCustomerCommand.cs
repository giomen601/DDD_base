using ErrorOr;
using MediatR;

namespace easypos.application.Customers.Create;
public record CreateCustomerCommand
(
  string FirstName,
  string LastName,
  string Email,
  string PhoneNumber,
  //Address address,
  string Country,
  string LineA,
  string LineB,
  string City,
  string State,
  string ZipCode
)
: IRequest<ErrorOr<Unit>>;