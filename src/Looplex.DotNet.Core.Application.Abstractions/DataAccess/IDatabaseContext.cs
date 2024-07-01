namespace Looplex.DotNet.Core.Application.Abstractions.DataAccess
{
    public interface IDatabaseContext
    {
        IDatabaseConnection CreateConnection();
    }
}
