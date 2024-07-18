using System.ComponentModel.DataAnnotations;

namespace Looplex.DotNet.Core.Common.Exceptions
{
    public class EntityInvalidExcepion(List<string> errorMessages)
        : ValidationException("One or more validation errors occurred.")
    {
        public List<string> ErrorMessages { get; } = errorMessages;

        public override string ToString()
        {
            return string.Join(Environment.NewLine, ErrorMessages);
        }
    }
}