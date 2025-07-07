using MediatR;

namespace Shared.CQRS
{
    public interface ICommand : IRequest<Unit>
    {
        // This interface is intentionally left empty. It serves as a marker interface for commands in the CQRS pattern.
    }

    public interface ICommand<out TResponse> : IRequest<TResponse>
    {
        // This interface is intentionally left empty. It serves as a marker interface for commands in the CQRS pattern.
    }
}
