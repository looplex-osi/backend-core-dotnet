using System.Runtime.CompilerServices;

namespace Looplex.DotNet.Core.Common.Utils;

public static class ObjectUtils
{
    public static string GetCallerName(this object input, [CallerMemberName] string memberName = "")
    {
        return memberName;
    }
}