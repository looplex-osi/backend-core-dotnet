namespace Looplex.DotNet.Core.Domain.Entities
{
    public interface IEntity
    {
        /// <summary>
        ///     Sequencial id for an entity.
        /// </summary>
        int? Id { get; set; }
    }
}