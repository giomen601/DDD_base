namespace easypos.domain.Primitives;
public abstract class AggregateRoot
{
  //listado de eventos de dominio
  private readonly List<DomainEvent> _domainEvents = new();

  public ICollection<DomainEvent> GetDomainEvents() => _domainEvents;

  protected void RaiseEvent(DomainEvent domainEvent)
  {
    _domainEvents.Add(domainEvent);
  }
}