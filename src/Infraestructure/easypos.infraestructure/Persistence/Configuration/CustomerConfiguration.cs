using easypos.domain.Customers;
using easypos.domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace easypos.infraestructure.Persistence.Configuration;
public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
  public void Configure(EntityTypeBuilder<Customer> builder)
  {
    builder.HasKey(x => x.Id);

    builder.Property(x => x.Id)
      .HasConversion(x => x.value, value => new CustomerId(value));

    builder.Property(x => x.FirstName)
      .HasMaxLength(50);

    builder.Property(x => x.LastName)
      .HasMaxLength(50);

    builder.Ignore(x => x.FullName);

    builder.Property(x => x.Email)
      .HasMaxLength(255);

    builder.HasIndex(x => x.Email).IsUnique();

    //convertion valueobjects
    builder.Property(x => x.PhoneNumber)
      .HasConversion(phoneNumber => phoneNumber.Value,
      value => PhoneNumber.Create(value)!)
      .HasMaxLength(9);

    builder.OwnsOne(x => x.Address, addressBuilder =>
    {
      addressBuilder.Property(a => a.Country).HasMaxLength(3);
      addressBuilder.Property(a => a.LineA).HasMaxLength(20);
      addressBuilder.Property(a => a.LineB).HasMaxLength(20).IsRequired(false);
      addressBuilder.Property(a => a.City).HasMaxLength(40);
      addressBuilder.Property(a => a.State).HasMaxLength(40);
      addressBuilder.Property(a => a.ZipCode).HasMaxLength(10).IsRequired(false);
    });

    builder.Property(x => x.Active);
  }
}