using easypos.domain.Customers;
using Microsoft.EntityFrameworkCore;

namespace easypos.infraestructure.Persistence.Repositories;
public class CustomerRepository : ICustomerRepository
{
  private readonly ApplicationDbContext dbContext;

  public CustomerRepository(ApplicationDbContext dbContext)
  {
    this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
  }

  public async Task Add(Customer customer)
    => await dbContext.Customers.AddAsync(customer);

  public async Task<Customer?> GetByIdAsync(CustomerId id)
    => await dbContext.Customers.SingleOrDefaultAsync(x => x.Id == id);
}