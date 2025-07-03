using MediatR;

namespace Shared.DDD
{
    public class IDomainEvent : INotification
    {
        Guid EventId => Guid.NewGuid();

        public DateTime OccurredOn => DateTime.UtcNow;

        public string EventType => GetType().AssemblyQualifiedName!;
    }
}
