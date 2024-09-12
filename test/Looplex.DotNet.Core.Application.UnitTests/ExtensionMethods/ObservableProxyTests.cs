using System.Collections.ObjectModel;
using FluentAssertions;
using Looplex.DotNet.Core.Application.ExtensionMethods;
using Looplex.DotNet.Core.Domain.Traits;

namespace Looplex.DotNet.Core.Application.UnitTests.ExtensionMethods;

[TestClass]
public class ObservableProxyTests
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
    public void ChildPropertyPropertyChange_ShouldBeTracked()
    {
        // Arrange
        var model = SampleModel.Mock();

        // Act
        model.Child!.Title = "Test Title";

        // Assert
        Assert.IsTrue(model.Child.ChangedPropertyNotification.ChangedProperties.Contains("Title"));
        Assert.AreEqual(1, model.Child.ChangedPropertyNotification.ChangedProperties.Count);
        
        model.Child.ChangedPropertyNotification.AddedItems.Should().BeEmpty();
        model.Child.ChangedPropertyNotification.RemovedItems.Should().BeEmpty();
    }
    
    [TestMethod]
    public void ChildPropertyChange_ShouldBeTracked()
    {
        // Arrange
        var model = SampleModel.Mock();

        // Act
        model.Child = new SampleChildModel();
        
        // Assert
        Assert.IsTrue(model.ChangedPropertyNotification.ChangedProperties.Contains("Child"));
        Assert.AreEqual(1, model.ChangedPropertyNotification.ChangedProperties.Count);

        model.ChangedPropertyNotification.AddedItems.Should().BeEmpty();
        model.ChangedPropertyNotification.RemovedItems.Should().BeEmpty();
        
        model.Child.ChangedPropertyNotification.ChangedProperties.Should().BeEmpty();
        
        model.Child.ChangedPropertyNotification.AddedItems.Should().BeEmpty();
        model.Child.ChangedPropertyNotification.RemovedItems.Should().BeEmpty();
    }

    [TestMethod]
    public void ChildPropertyChangeAndChildPropertyPropertyChange_ShouldBeTracked()
    {
        // Arrange
        var model = SampleModel.Mock();

        // Act
        model.Child = new SampleChildModel();
        model.Child!.Title = "Test Title";
        
        // Assert
        Assert.IsTrue(model.ChangedPropertyNotification.ChangedProperties.Contains("Child"));
        Assert.AreEqual(1, model.ChangedPropertyNotification.ChangedProperties.Count);

        model.ChangedPropertyNotification.AddedItems.Should().BeEmpty();
        model.ChangedPropertyNotification.RemovedItems.Should().BeEmpty();
        
        Assert.IsTrue(model.Child.ChangedPropertyNotification.ChangedProperties.Contains("Title"));
        Assert.AreEqual(1, model.Child.ChangedPropertyNotification.ChangedProperties.Count);
        
        model.Child.ChangedPropertyNotification.AddedItems.Should().BeEmpty();
        model.Child.ChangedPropertyNotification.RemovedItems.Should().BeEmpty();
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
    
    [TestMethod]
    public void ObservableItemsChanged_ShouldTrackAddAndRemove()
    {
        // Arrange
        var model = SampleModel.Mock();

        // Act
        model.ObservableItems.Add(new SampleChildModel { Title = "Item 1" });
        model.ObservableItems.RemoveAt(0);
        model.ObservableItems.Add(new SampleChildModel { Title = "Item 2" });

        // Assert
        Assert.IsTrue(model.ChangedPropertyNotification.AddedItems.ContainsKey("ObservableItems"));
        model.ChangedPropertyNotification.AddedItems["ObservableItems"].Should()
            .Contain(s => ((SampleChildModel)s).Title == "Item 1");
        model.ChangedPropertyNotification.AddedItems["ObservableItems"].Should()
            .Contain(s => ((SampleChildModel)s).Title == "Item 2");
        Assert.AreEqual(2, model.ChangedPropertyNotification.AddedItems["ObservableItems"].Count);

        Assert.IsTrue(model.ChangedPropertyNotification.RemovedItems.ContainsKey("ObservableItems"));
        model.ChangedPropertyNotification.AddedItems["ObservableItems"].Should()
            .Contain(s => ((SampleChildModel)s).Title == "Item 1");
        Assert.AreEqual(1, model.ChangedPropertyNotification.RemovedItems["ObservableItems"].Count);
        
        model.ChangedPropertyNotification.ChangedProperties.Should().BeEmpty();
    }
    
    [TestMethod]
    public void InitialObservableItemsChanged_ChangesShouldBeTracked()
    {
        // Arrange
        var model = new SampleModel
        {
            Age = 1,
            Name = "Name Init",
            ObservableItems = new ObservableCollection<SampleChildModel>()
            {
                new() { Title = "Item 1" }
            }
        }.WithObservableProxy();

        // Act
        model.ObservableItems.First().Title = "New Item";
        
        // Assert
        model.ObservableItems.First().ChangedPropertyNotification.ChangedProperties.Should().Contain("Title");
        model.ObservableItems.First().ChangedPropertyNotification.AddedItems.Should().BeEmpty();   
        model.ObservableItems.First().ChangedPropertyNotification.RemovedItems.Should().BeEmpty();
    }
    
    [TestMethod]
    public void ReplaceObservableItems_ChangesShouldBeTracked()
    {
        // Arrange
        var model = SampleModel.Mock();
        model.ObservableItems = new ObservableCollection<SampleChildModel>()
        {
            new() { Title = "Item 1" }
        };
        model.ObservableItems.Add(new() { Title = "Item 2" });
        
        // Act
        model.ObservableItems.First().Title = "New Item 1";
        model.ObservableItems.Last().Title = "New Item 2";

        // Assert
        model.ObservableItems.First().ChangedPropertyNotification.ChangedProperties.Should().Contain("Title");
        model.ObservableItems.First().ChangedPropertyNotification.AddedItems.Should().BeEmpty();   
        model.ObservableItems.First().ChangedPropertyNotification.RemovedItems.Should().BeEmpty();
        
        model.ObservableItems.Last().ChangedPropertyNotification.ChangedProperties.Should().Contain("Title");
        model.ObservableItems.Last().ChangedPropertyNotification.AddedItems.Should().BeEmpty();   
        model.ObservableItems.Last().ChangedPropertyNotification.RemovedItems.Should().BeEmpty();
        
        Assert.IsTrue(model.ChangedPropertyNotification.AddedItems.ContainsKey("ObservableItems"));
        model.ChangedPropertyNotification.AddedItems["ObservableItems"].Should()
            .Contain(s => ((SampleChildModel)s).Title == "New Item 2");
        model.ChangedPropertyNotification.AddedItems["ObservableItems"].Should()
            .Contain(s => ((SampleChildModel)s).Title == "New Item 2");
        Assert.AreEqual(1, model.ChangedPropertyNotification.AddedItems["ObservableItems"].Count);
    }
    
    [TestMethod]
    public void ObservableItemsChanged_AddedItemsChangesShouldBeTracked()
    {
        // Arrange
        var model = SampleModel.Mock();
        model.ObservableItems.Add(new SampleChildModel { Title = "Item 1" });

        // Act
        model.ObservableItems.First().Title = "New Item";
        
        // Assert
        model.ObservableItems.First().ChangedPropertyNotification.ChangedProperties.Should().Contain("Title");
        model.ObservableItems.First().ChangedPropertyNotification.AddedItems.Should().BeEmpty();   
        model.ObservableItems.First().ChangedPropertyNotification.RemovedItems.Should().BeEmpty();
    }
    
    [TestMethod]
    public void TwoProxiesEquality_TargetAreEqual()
    {
        // Arrange
        var model = new SampleModel
        {
            Age = 1,
            Name = "Name Init",
            Child = new ()
            {
                Title = "Title Init",
            },
        };
        var proxy1 = model.WithObservableProxy();
        var proxy2 = model.WithObservableProxy();

        // Act
        var equal = proxy1.Equals(proxy2);
        
        // Assert
        Assert.IsTrue(equal);
    }
}

public class SampleModel : IHasChangedPropertyNotificationTrait
{
    public IChangedPropertyNotificationTrait ChangedPropertyNotification { get; } = new ChangedPropertyNotificationTrait();
    
    public static SampleModel Mock()
    {
        return new SampleModel
        {
            Age = 1,
            Name = "Name Init",
            Child = new ()
            {
                Title = "Title Init",
            },
        }.WithObservableProxy();
    }
    public virtual string? Name { get; set; }

    public virtual int Age { get; set; }

    public virtual SampleChildModel? Child { get; set; }

    public virtual IList<string> Items { get; set; } = new ObservableCollection<string>();
    
    public virtual IList<SampleChildModel> ObservableItems { get; set; } = new ObservableCollection<SampleChildModel>();

}

public class SampleChildModel : IHasChangedPropertyNotificationTrait
{
    public IChangedPropertyNotificationTrait ChangedPropertyNotification { get; } = new ChangedPropertyNotificationTrait();

    public virtual string? Title { get; set; }
}