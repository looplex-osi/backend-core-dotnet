using MediatR;

namespace Looplex.DotNet.Core.Application.Abstractions.Queries
{
    public interface IQueryHandler<T, U> : IRequestHandler<T, U> where T : IRequest<U>
    {
    }
}