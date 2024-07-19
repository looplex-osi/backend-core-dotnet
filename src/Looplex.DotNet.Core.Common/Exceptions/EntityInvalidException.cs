using System.ComponentModel.DataAnnotations;

namespace Looplex.DotNet.Core.Common.Exceptions
{
    public class EntityInvalidExcepion(IList<string> errorMessages)
        : ValidationException("One or more validation errors occurred.")
    {
        public IList<string> ErrorMessages { get; } = errorMessages;

        public override string ToString()
        {
            return string.Join(Environment.NewLine, ErrorMessages);
        }
    }
}