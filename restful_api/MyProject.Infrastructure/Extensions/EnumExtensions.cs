using System;
using System.ComponentModel;
using System.Linq;

namespace MyProject.Infrastructure.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDescription<T>(this T value) where T : IConvertible
        {
            var result = value.ToString();

            var fieldInfo = value.GetType().GetField(value.ToString());

            if (fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false)
                    is DescriptionAttribute[] attributes &&
                attributes.Any())
                result = attributes.First().Description;

            return result;
        }
    }
}
