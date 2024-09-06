namespace Looplex.DotNet.Core.Domain.Traits
{
    public interface IHasChangedPropertyNotificationTrait 
    {
        IChangedPropertyNotificationTrait ChangedPropertyNotification { get; }
    }
}