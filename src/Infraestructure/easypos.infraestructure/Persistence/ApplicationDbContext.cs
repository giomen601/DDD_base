using easypos.application.Data;
using easypos.domain.Customers;
using easypos.domain.Primitives;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace easypos.infraestructure.Persistence;
public class ApplicationDbContext : DbContext, IApplicationDbContext, IUnitOfWork
{
  private readonly IPublisher publisher;

  public ApplicationDbContext(DbContextOptions options, IPublisher publisher) : base(options)
  {
    this.publisher = publisher ?? throw new ArgumentNullException(nameof(publisher));
  }
  public DbSet<Customer> Customers { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
  }

  public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
  {
    var domainEvents = ChangeTracker.Entries<AggregateRoot>()
      .Select(e => e.Entity)
      .Where(e => e.GetDomainEvents().Any())
      .SelectMany(e => e.GetDomainEvents());

    var result = await base.SaveChangesAsync(cancellationToken);

    foreach (var item in domainEvents)
    {
      //publish pipeline mediatr
      await publisher.Publish(item, cancellationToken);
    }

    return result;
  }
}