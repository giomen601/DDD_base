using easypos.domain.Primitives;
using easypos.domain.ValueObjects;

namespace easypos.domain.Customers;
public sealed class Customer : AggregateRoot
{
  public Customer(CustomerId id, string firstName, string lastName, string email, PhoneNumber phoneNumber, Address address, bool active)
  {
    Id = id;
    FirstName = firstName;
    LastName = lastName;
    Email = email;
    PhoneNumber = phoneNumber;
    Address = address;
    Active = active;
  }

  public Customer()
  {
    
  }

  public CustomerId Id { get; private set; }
  public string FirstName { get; private set; } = string.Empty;
  public string LastName { get; private set; } = string.Empty;
  public string FullName => $"{FirstName} {LastName}";
  public string Email { get; private set; } = string.Empty;
  public PhoneNumber PhoneNumber { get; private set; }
  public Address Address { get; private set; }
  public bool Active { get; set; }
}