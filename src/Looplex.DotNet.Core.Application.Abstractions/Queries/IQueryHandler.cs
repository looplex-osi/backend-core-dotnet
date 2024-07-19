using MediatR;

namespace Looplex.DotNet.Core.Application.Abstractions.Queries
{
    public interface IQueryHandler<TRequest, TResult> : IRequestHandler<TRequest, TResult> where TRequest : IRequest<TResult>
    {
    }
}