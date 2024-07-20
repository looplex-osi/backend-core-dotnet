using Looplex.DotNet.Core.Application.ExtensionMethods;
using System.Dynamic;
using Looplex.OpenForExtension.Abstractions.Contexts;
using NSubstitute;

namespace Looplex.DotNet.Core.Application.UnitTests.ExtensionMethods
{
    [TestClass]
    public class ServiceExtensionMethodsTests
    {
        private IContext _context = null!;

        [TestInitialize]
        public void Setup()
        {
            _context = Substitute.For<IContext>();
            var state = new ExpandoObject();
            _context.State.Returns(state);
            _context.State.prop1 = new ExpandoObject();
            _context.State.prop1.prop2 = "Hello, World!";
            _context.State.prop1.prop3 = 123;
            _context.State.prop1.prop4 = new Dictionary<string, object>();
            _context.State.prop1.prop4.Add("Name", "Test Object");
            _context.State.prop1.prop8 = (int?)123;
            _context.State.prop1.prop9 = default(int?);
        }

        [TestMethod]
        [DataRow("prop1.prop2", "Hello, World!")]
        [DataRow("prop1.prop3", 123)]
        [DataRow("prop1.prop4", null)] // For object, we cannot assert equality directly
        public void GetRequiredValue_ValidPath_ReturnsValue(string path, object expected)
        {
            var result = _context.GetRequiredValue<object>(path);

            if (expected != null)
            {
                Assert.AreEqual(expected, result);
            }
            else
            {
                Assert.IsNotNull(result);
            }
        }

        [TestMethod]
        [DataRow("prop1.prop5")]
        [DataRow("prop2")]
        public void GetRequiredValue_InvalidPath_ThrowsArgumentNullException(string path)
        {
            Assert.ThrowsException<ArgumentNullException>(() => _context.GetRequiredValue<object>(path));
        }

        [TestMethod]
        [DataRow("prop1.prop2", "Hello, World!")]
        [DataRow("prop1.prop3", 123)]
        [DataRow("prop1.prop5", null)]
        [DataRow("prop2", null)]
        public void GetValue_ValidOrInvalidPath_ReturnsExpectedValue(string path, object expected)
        {
            var result = _context.GetValue<object>(path);

            if (expected != null)
            {
                Assert.AreEqual(expected, result);
            }
            else
            {
                Assert.IsNull(result);
            }
        }

        [TestMethod]
        [DataRow("prop1.prop3", 123)]
        [DataRow("prop1.prop6", null)]
        public void GetValue_Int_ValidOrInvalidPath_ReturnsExpectedValue(string path, int? expected)
        {
            var result = _context.GetValue<int?>(path);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [DataRow("prop1.prop2", "Hello, World!")]
        [DataRow("prop1.prop6", null)]
        public void GetValue_String_ValidOrInvalidPath_ReturnsExpectedValue(string path, string expected)
        {
            var result = _context.GetValue<string>(path);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [DataRow("prop1", typeof(ExpandoObject))]
        [DataRow("prop1.prop4", typeof(IDictionary<string, object>))]
        [DataRow("prop1.prop6", null)]
        public void GetValue_Object_ValidOrInvalidPath_ReturnsExpectedValue(string path, Type expectedType)
        {
            var result = _context.GetValue<object>(path);

            if (expectedType != null)
            {
                Assert.IsNotNull(result);
                Assert.IsInstanceOfType(result, expectedType);
            }
            else
            {
                Assert.IsNull(result);
            }
        }

        [TestMethod]
        [DataRow("prop1.prop8", 123)]
        [DataRow("prop1.prop9", default)]
        public void GetValue_NullableInt_ValidOrInvalidPath_ReturnsExpectedValue(string path, int? expected)
        {
            var result = _context.GetValue<int?>(path);
            Assert.AreEqual(expected, result);
        }
    }
}