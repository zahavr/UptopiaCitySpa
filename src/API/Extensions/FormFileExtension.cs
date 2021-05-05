using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace API.Extensions
{
	public static class FormFileExtension
    {
        public static async Task<byte[]> GetBytesAsync(this IFormFile formFile)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                await formFile.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}
