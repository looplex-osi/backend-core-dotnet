namespace Looplex.DotNet.Core.Common.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string entity, string id) : base($"The {entity} with id {id} was not found.") { }
    }
}