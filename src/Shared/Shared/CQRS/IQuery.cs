using MediatR;

namespace Shared.CQRS
{
    public interface IQuery<out T> : IRequest<T> where T : notnull
    {
        // This interface is intentionally left empty. It serves as a marker interface for queries in the CQRS pattern.
    }
}
