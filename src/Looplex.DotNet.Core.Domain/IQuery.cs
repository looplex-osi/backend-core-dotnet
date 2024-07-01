using MediatR;

namespace Looplex.DotNet.Core.Domain
{
    public interface IQuery<T> : IRequest<T>
    {
    }
}
