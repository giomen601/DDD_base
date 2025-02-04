using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easypos.domain.ValueObjects;
public partial record Address
{
  public string Country { get; init; }
  public string LineA { get; init; }
  public string LineB { get; init; }
  public string City { get; init; }
  public string State { get; init; }
  public string ZipCode { get; init; }

  public Address(string country, string lineA, string lineB, string city, string state, string zipCode)
  {
    Country = country;
    LineA = lineA;
    LineB = lineB;
    City = city;
    State = state;
    ZipCode = zipCode;
  }

  public static Address? Create(string country, string lineA, string lineB, string city, string state, string zipCode)
  {
    if(
      string.IsNullOrEmpty(country) ||
      string.IsNullOrEmpty(lineA) ||
      string.IsNullOrEmpty(lineB) ||
      string.IsNullOrEmpty(city) ||
      string.IsNullOrEmpty(state) ||
      string.IsNullOrEmpty(zipCode)
    )
    {
      return null;
    }

    return new Address(country, lineA, lineB, city, state, zipCode);
  }
}