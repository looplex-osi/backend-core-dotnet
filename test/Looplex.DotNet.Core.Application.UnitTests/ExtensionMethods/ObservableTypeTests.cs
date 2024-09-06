using System.Collections.ObjectModel;
using FluentAssertions;
using Looplex.DotNet.Core.Application.ExtensionMethods;
using Looplex.DotNet.Core.Domain.Traits;

namespace Looplex.DotNet.Core.Application.UnitTests.ExtensionMethods;

[TestClass]
public class ObservableTypeTests
{
    [TestMethod]
    public void PropertyChange_ShouldBeTracked()
    {
        // Arrange
        var model = SampleModel.Mock();

        // Act
        model.Name = "Test Name";
        model.Age = 30;

        // Assert
        Assert.IsTrue(model.ChangedPropertyNotification.ChangedProperties.Contains("Name"));
        Assert.IsTrue(model.ChangedPropertyNotification.ChangedProperties.Contains("Age"));
        Assert.AreEqual(2, model.ChangedPropertyNotification.ChangedProperties.Count);
        
        model.ChangedPropertyNotification.AddedItems.Should().BeEmpty();
        model.ChangedPropertyNotification.RemovedItems.Should().BeEmpty();
    }

    [TestMethod]
    public void CollectionChange_ShouldBeTracked()
    {
        // Arrange
        var model = SampleModel.Mock();
        var item1 = "Item 1";
        var item2 = "Item 2";

        // Act
        model.Items.Add(item1);
        model.Items.Add(item2);
        model.Items.Remove(item1);

        // Assert
        Assert.IsTrue(model.ChangedPropertyNotification.AddedItems.ContainsKey("Items"));
        Assert.IsTrue(model.ChangedPropertyNotification.AddedItems["Items"].Contains(item1));
        Assert.IsTrue(model.ChangedPropertyNotification.AddedItems["Items"].Contains(item2));
        Assert.AreEqual(2, model.ChangedPropertyNotification.AddedItems["Items"].Count);

        Assert.IsTrue(model.ChangedPropertyNotification.RemovedItems.ContainsKey("Items"));
        Assert.IsTrue(model.ChangedPropertyNotification.RemovedItems["Items"].Contains(item1));
        Assert.AreEqual(1, model.ChangedPropertyNotification.RemovedItems["Items"].Count);
        
        model.ChangedPropertyNotification.ChangedProperties.Should().BeEmpty();
    }

    [TestMethod]
    public void MultiplePropertyChanges_ShouldTrackAllChanges()
    {
        // Arrange
        var model = SampleModel.Mock();

        // Act
        model.Name = "First Name";
        model.Age = 25;
        model.Name = "Second Name"; // Change Name again

        // Assert
        Assert.AreEqual(2, model.ChangedPropertyNotification.ChangedProperties.Count);
        Assert.IsTrue(model.ChangedPropertyNotification.ChangedProperties.Contains("Name"));
        Assert.IsTrue(model.ChangedPropertyNotification.ChangedProperties.Contains("Age"));
        
        model.ChangedPropertyNotification.AddedItems.Should().BeEmpty();
        model.ChangedPropertyNotification.RemovedItems.Should().BeEmpty();
    }

    [TestMethod]
    public void CollectionChanged_ShouldTrackAddAndRemove()
    {
        // Arrange
        var model = SampleModel.Mock();

        // Act
        model.Items.Add("Item 1");
        model.Items.Remove("Item 1");

        // Assert
        Assert.IsTrue(model.ChangedPropertyNotification.AddedItems.ContainsKey("Items"));
        Assert.IsTrue(model.ChangedPropertyNotification.AddedItems["Items"].Contains("Item 1"));
        Assert.AreEqual(1, model.ChangedPropertyNotification.AddedItems["Items"].Count);

        Assert.IsTrue(model.ChangedPropertyNotification.RemovedItems.ContainsKey("Items"));
        Assert.IsTrue(model.ChangedPropertyNotification.RemovedItems["Items"].Contains("Item 1"));
        Assert.AreEqual(1, model.ChangedPropertyNotification.RemovedItems["Items"].Count);
        
        model.ChangedPropertyNotification.ChangedProperties.Should().BeEmpty();
    }
    
    [TestMethod]
    public void NewCollectionChanged_ShouldTrackAddAndRemove()
    {
        // Arrange
        var model = SampleModel.Mock();

        // Act
        model.Items = new ObservableCollection<string>();
        model.Items.Add("Item 1");
        model.Items.Remove("Item 1");

        // Assert
        Assert.IsTrue(model.ChangedPropertyNotification.AddedItems.ContainsKey("Items"));
        Assert.IsTrue(model.ChangedPropertyNotification.AddedItems["Items"].Contains("Item 1"));
        Assert.AreEqual(1, model.ChangedPropertyNotification.AddedItems["Items"].Count);

        Assert.IsTrue(model.ChangedPropertyNotification.RemovedItems.ContainsKey("Items"));
        Assert.IsTrue(model.ChangedPropertyNotification.RemovedItems["Items"].Contains("Item 1"));
        Assert.AreEqual(1, model.ChangedPropertyNotification.RemovedItems["Items"].Count);
        
        model.ChangedPropertyNotification.ChangedProperties.Should().BeEmpty();
    }
}

public class SampleModel : IHasChangedPropertyNotificationTrait
{
    public IChangedPropertyNotificationTrait ChangedPropertyNotification { get; } = new ChangedPropertyNotificationTrait();
    
    public static SampleModel Mock()
    {
        return new SampleModel()
        {
            Age = 1,
            Name = "Name Init"
        }.WithObservableProxy();
    }
    public virtual string? Name { get; set; }

    public virtual int Age { get; set; }

    public virtual IList<string> Items { get; set; } = new ObservableCollection<string>();
}