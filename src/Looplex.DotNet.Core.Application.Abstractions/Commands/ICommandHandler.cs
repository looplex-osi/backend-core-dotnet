using MediatR;

namespace Looplex.DotNet.Core.Application.Abstractions.Commands
{
    public interface ICommandHandler<T> : IRequestHandler<T> where T : IRequest
    {
    }
}