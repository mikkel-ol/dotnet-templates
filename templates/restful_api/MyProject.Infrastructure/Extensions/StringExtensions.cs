using System.Linq;

namespace MyProject.Infrastructure.Extensions
{
    public static class StringExtensions
    {
        public static string TrimDashes(this string str)
        {
            return str.Replace("-", "");
        }

        public static string GetFileName(this string s)
        {
            return s.Split('/').Last();
        }
    }
}
