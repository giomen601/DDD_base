using easypos.domain.Customers;
using Microsoft.EntityFrameworkCore;

namespace easypos.application.Data;
public interface IApplicationDbContext
{
  DbSet<Customer> Customers { get; set; }
  Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}