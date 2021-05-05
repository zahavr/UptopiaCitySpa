using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Core.Interfaces;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
	public class BlobService : IBlobService
	{
		private const string CONTAINER_NAME = "user-photo";
		private const string DEFAULT_PHOTO_URL = "https://utopiacity.blob.core.windows.net/user-photo/mainDefault.png";

		private readonly BlobServiceClient _blobServiceClient;

		public BlobService(BlobServiceClient blobServiceClient)
		{
			_blobServiceClient = blobServiceClient;
		}

		public Uri GetPhoto(string fileName)
		{
			BlobContainerClient blobContainerClient = _blobServiceClient.GetBlobContainerClient(CONTAINER_NAME);
			BlobClient blobClient = blobContainerClient.GetBlobClient("main" + fileName);

			if (!blobClient.Exists())
				return new Uri(DEFAULT_PHOTO_URL);

			return blobClient.Uri;
		}

		public async Task<bool> UploadPhotoAsync(string fileName, byte[] photoBytes, string fileType)
		{
			BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(CONTAINER_NAME);
			BlobClient blobClient = containerClient.GetBlobClient("main" + fileName);

			Response<BlobContentInfo> response;

			using (MemoryStream memoryStream = new MemoryStream(photoBytes, false))
			{
				response = await blobClient.UploadAsync(memoryStream, new BlobHttpHeaders { ContentType = fileType });
			}

			return response.GetRawResponse().Status == 201 ? true : false;
		}

	}
}
