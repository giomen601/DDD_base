using FluentValidation;

namespace easypos.application.Customers.Create;
public class CreateCustomerCommandValidator
: AbstractValidator<CreateCustomerCommand>
{
  public CreateCustomerCommandValidator()
  {
    RuleFor(x => x.FirstName)
      .NotEmpty()
      .MaximumLength(50);

    RuleFor(x => x.LastName)
      .NotEmpty()
      .MaximumLength(50);

    RuleFor(x => x.Email)
      .NotEmpty()
      .MaximumLength(255);

    RuleFor(x => x.PhoneNumber)
      .NotEmpty()
      .MaximumLength(9)
      .WithName("Phone Number");

    RuleFor(x => x.Country)
      .NotEmpty()
      .MaximumLength(3);

    RuleFor(x => x.LineA)
      .NotEmpty()
      .MaximumLength(20);

    RuleFor(x => x.LineB)
      .NotEmpty()
      .MaximumLength(20);

    RuleFor(x => x.City)
      .NotEmpty()
      .MaximumLength(40);

    RuleFor(x => x.State)
      .NotEmpty()
      .MaximumLength(40);

    RuleFor(x => x.ZipCode)
      .NotEmpty()
      .MaximumLength(10);
  }
}