using Looplex.DotNet.Core.Infra.Data;

namespace Looplex.DotNet.Core.Infra.UnitTests
{
    [TestClass]
    public class DeltaTests
    {
        [TestMethod]
        public void TrySetProperty_PropertyExistsAndWritable_ReturnsTrue()
        {
            // Arrange
            var delta = new Delta<EntityForDeltaTests>();
            string propertyName = "Property1";
            string propertyValue = "New Value 1";

            // Act
            bool result = delta.TrySetProperty(propertyName, propertyValue);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TrySetProperty_PropertyDoesNotExist_ReturnsFalse()
        {
            // Arrange
            var delta = new Delta<EntityForDeltaTests>();
            string propertyName = "NonExistentProperty";
            string propertyValue = "New Value";

            // Act
            bool result = delta.TrySetProperty(propertyName, propertyValue);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TrySetProperty_PropertyIsReadOnly_ReturnsFalse()
        {
            // Arrange
            var delta = new Delta<EntityForDeltaTests>();
            string propertyName = "ReadOnlyProperty";
            string propertyValue = "New Value";

            // Act
            bool result = delta.TrySetProperty(propertyName, propertyValue);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TryGetProperty_PropertyExists_ReturnsTrueAndChangedValue()
        {
            // Arrange
            var delta = new Delta<EntityForDeltaTests>();
            string propertyName = "Property1";
            string propertyValue = "New Value";
            delta.TrySetProperty(propertyName, propertyValue);

            // Act
            bool result = delta.TryGetProperty(propertyName, out object? changedValue);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(propertyValue, changedValue);
        }

        [TestMethod]
        public void TryGetProperty_PropertyDoesNotExist_ReturnsFalse()
        {
            // Arrange
            var delta = new Delta<EntityForDeltaTests>();
            string propertyName = "NonExistentProperty";

            // Act
            bool result = delta.TryGetProperty(propertyName, out object? changedValue);

            // Assert
            Assert.IsFalse(result);
            Assert.IsNull(changedValue);
        }

        [TestMethod]
        public void ApplyChanges_ChangesAppliedToEntity()
        {
            // Arrange
            var delta = new Delta<EntityForDeltaTests>();
            var entity = new EntityForDeltaTests();
            string propertyName = "Property1";
            string propertyValue = "New Value";
            delta.TrySetProperty(propertyName, propertyValue);

            // Act
            delta.ApplyChanges(entity);

            // Assert
            Assert.AreEqual(propertyValue, entity.Property1);
        }

        [TestMethod]
        public void GetChangedProperties_PropertiesChanged_ReturnsChangedProperties()
        {
            // Arrange
            var delta = new Delta<EntityForDeltaTests>();
            var entity = new EntityForDeltaTests();
            delta.TrySetProperty("Property1", "New Value 1");
            delta.TrySetProperty("Property2", 42);

            // Act
            var changedProperties = delta.GetChangedProperties();

            // Assert
            CollectionAssert.AreEqual(new[] { "Property1", "Property2" }, changedProperties.ToArray());
        }

        [TestMethod]
        public void TrySetProperty_PrivateProperty_ReturnsFalse()
        {
            // Arrange
            var delta = new Delta<EntityForDeltaTests>();
            string propertyName = "PrivateProperty";
            string propertyValue = "New Value";

            // Act
            bool result = delta.TrySetProperty(propertyName, propertyValue);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TrySetProperty_InternalProperty_ReturnsFalse()
        {
            // Arrange
            var delta = new Delta<EntityForDeltaTests>();
            string propertyName = "InternalProperty";
            string propertyValue = "New Value";

            // Act
            bool result = delta.TrySetProperty(propertyName, propertyValue);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TrySetProperty_ProtectedProperty_ReturnsFalse()
        {
            // Arrange
            var delta = new Delta<EntityForDeltaTests>();
            string propertyName = "ProtectedProperty";
            string propertyValue = "New Value";

            // Act
            bool result = delta.TrySetProperty(propertyName, propertyValue);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TrySetProperty_ConstProperty_ReturnsFalse()
        {
            // Arrange
            var delta = new Delta<EntityForDeltaTests>();
            string propertyName = "ConstProperty";
            string propertyValue = "New Value";

            // Act
            bool result = delta.TrySetProperty(propertyName, propertyValue);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TrySetProperty_StaticProperty_ReturnsFalse()
        {
            // Arrange
            var delta = new Delta<EntityForDeltaTests>();
            string propertyName = "StaticProperty";
            string propertyValue = "New Value";

            // Act
            bool result = delta.TrySetProperty(propertyName, propertyValue);

            // Assert
            Assert.IsTrue(result);
        }
    }
}