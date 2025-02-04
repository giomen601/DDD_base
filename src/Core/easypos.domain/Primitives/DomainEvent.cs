using MediatR;

namespace easypos.domain.Primitives;
public record DomainEvent(Guid Id) : INotification;