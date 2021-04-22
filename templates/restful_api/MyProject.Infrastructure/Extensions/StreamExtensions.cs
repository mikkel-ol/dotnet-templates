using System.IO;
using System.Threading.Tasks;

namespace MyProject.Infrastructure.Extensions
{
    public static class StreamExtensions
    {
        public static async Task<byte[]> ToByteArrayAsync(this Stream stream)
        {
            using (var ms = new MemoryStream())
            {
                await stream.CopyToAsync(ms);
                return ms.ToArray();
            }
        }
    }
}
