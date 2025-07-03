﻿namespace Shared.DDD
{
    public interface IAggregate<T> : IEntity<T>, IAggregate
        where T : notnull
    {
    }

    public interface IAggregate : IEntity
    {
        IReadOnlyList<IDomainEvent> DomainEvents { get; }

        IDomainEvent[] ClearDomainEvents();
    }
}
