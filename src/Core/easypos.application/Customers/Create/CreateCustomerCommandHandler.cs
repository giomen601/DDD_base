using easypos.domain.Customers;
using easypos.domain.Primitives;
using easypos.domain.ValueObjects;
using ErrorOr;
using MediatR;

namespace easypos.application.Customers.Create;
public class CreateCustomerCommandHandler
: IRequestHandler<CreateCustomerCommand, ErrorOr<Unit>>
{
  private readonly ICustomerRepository customerRepository;
  private readonly IUnitOfWork unitOfWork;

  public CreateCustomerCommandHandler(ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
  {
    this.customerRepository = customerRepository ?? throw new ArgumentException(nameof(customerRepository));
    this.unitOfWork = unitOfWork ?? throw new ArgumentException(nameof(customerRepository));
  }
  public async Task<ErrorOr<Unit>> Handle(CreateCustomerCommand command, CancellationToken cancellationToken)
  {
    if (PhoneNumber.Create(command.PhoneNumber) is not PhoneNumber phoneNumber)
      return Error.Validation("Customer.Phonenumber", "Phone number has not valid format");

    if (Address.Create(command.Country, command.LineA, command.LineB, command.City, command.State, command.ZipCode) is not Address address)
      return Error.Validation("Customer.Address", "Address it's not valid");

    Customer customer = new(
      new CustomerId(Guid.NewGuid()),
      command.FirstName,
      command.LastName,
      command.Email,
      phoneNumber,
      address,
      true
    );

    await customerRepository.Add(customer);

    await unitOfWork.SaveChangesAsync(cancellationToken);

    return Unit.Value;
  }
}