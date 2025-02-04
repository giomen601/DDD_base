using easypos.application.Customers.Create;
using easypos.domain.Customers;
using easypos.domain.Primitives;
using easypos.domain.DomainErrors;
using ErrorOr;
using FluentAssertions;
using Moq;
using System.Reflection.Metadata;

namespace Application.Customer.UnitTest
{
  public class CreateCustomerCommandHandlerTest
  {
    private readonly Mock<ICustomerRepository> customerRepository;
    private readonly Mock<IUnitOfWork> unitOfWork;
    private readonly CreateCustomerCommandHandler handler;
    public CreateCustomerCommandHandlerTest()
    {
      customerRepository = new Mock<ICustomerRepository>();
      unitOfWork = new Mock<IUnitOfWork>();
      handler = new CreateCustomerCommandHandler(customerRepository.Object, unitOfWork.Object);
    }


    [Fact]
    public async void HandleCreateCustomer_WhenInvalidPhoneNumber_ShouldReturnValildationError()
    {
      //Arrange
      CreateCustomerCommand command = new CreateCustomerCommand("Fernando", "Ventura", "fe939@mc.com", "33049439443",
        "", "", "", "", "", "");

      //Act
      var result = await handler.Handle(command, default);

      //Assert
      result.IsError.Should().BeTrue();
      result.FirstError.Type.Should().Be(ErrorType.Validation);
      result.FirstError.Code.Should().Be(Errors.Customer.PhoneNumberWithBadFormat.Code);
      result.FirstError.Description.Should().Be(Errors.Customer.PhoneNumberWithBadFormat.Description);
    }
  }
}