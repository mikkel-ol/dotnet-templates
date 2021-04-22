using System;

namespace MyProject.Infrastructure.Extensions
{
    public static class TypeExtensions
    {
        public static string NestedClassName(this Type type)
        {
            // type.FullName = "YourNameSpace.OuterClass+InnerClass"
            var fullName = type.FullName;
            var lastDotPos = fullName.LastIndexOf('.');

            // "OuterClass+InnerClass"
            var outerInnerName = fullName.Substring(lastDotPos + 1);

            return outerInnerName;
        }
    }
}
