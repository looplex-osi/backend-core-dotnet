namespace Looplex.DotNet.Core.Domain.ServicesTest
{
    internal class EntityForDeltaTests
    {
        public int Id { get; set; }
        public string? Property1 { get; set; }
        public int Property2 { get; set; }
        private string? PrivateProperty { get; set; }
        internal string? InternalProperty { get; set; }
        protected string? ProtectedProperty { get; set; }
        public const string? ConstProperty = "Constant Value";
        public static string? StaticProperty { get; set; }
    }
}
