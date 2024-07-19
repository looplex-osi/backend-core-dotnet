using MediatR;

namespace Looplex.DotNet.Core.Application.Abstractions.Commands
{
    public interface ICommandHandler<TRequest> : IRequestHandler<TRequest> where TRequest : IRequest
    {
    }
}