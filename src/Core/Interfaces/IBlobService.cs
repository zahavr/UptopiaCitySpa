using System;
using System.Threading.Tasks;

namespace Core.Interfaces
{
	public interface IBlobService
    {
		Task<bool> UploadPhotoAsync(string fileName, byte[] photoBytes, string fileType);
		Uri GetPhoto(string fileName);
	}
}
