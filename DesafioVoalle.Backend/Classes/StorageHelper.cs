using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;

namespace DesafioVoalle.Backend.Classes
{
    public class StorageConfig
    {
        public string AccountName { get; set; }
        public string AccountKey { get; set; }
        public string QueueName { get; set; }
        public string ImageContainer { get; set; }
        public string ThumbnailContainer { get; set; }
    }

    public class StorageHelper
    {
		static readonly StorageConfig CONFIG = new StorageConfig()
		{
			AccountName = "storagemvc",
			AccountKey = "vjSJ9PJL/2XXHr05vlGGuv7tDK5LN8MeQVo9lnI+KFUiZEuZ7eyWOlTNIv7JQatVUH3j6wy/UTzksTx+dEEO+A==",
			ImageContainer = "temp",
		};

		private readonly StorageConfig _storageConfig = null;

        public StorageHelper(StorageConfig storageConfig = null)
        {
            _storageConfig = storageConfig;

			if(_storageConfig == null)
				_storageConfig = CONFIG;
		}

        public async Task<string> UploadAsync(Stream stream, string nameFile)
        {
			return await UploadFileToStorage(stream, nameFile, _storageConfig);
		}

        private static async Task<string> UploadFileToStorage(Stream fileStream, string fileName,
            StorageConfig storageConfig)
        {
            var storageCredentials = new StorageCredentials(storageConfig.AccountName, storageConfig.AccountKey);
            var storageAccount = new CloudStorageAccount(storageCredentials, true);
            var blobClient = storageAccount.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference(storageConfig.ImageContainer);
            var blockBlob = container.GetBlockBlobReference(fileName);

            await blockBlob.UploadFromStreamAsync(fileStream);

            return blockBlob.SnapshotQualifiedStorageUri.PrimaryUri.ToString();
        }

        public bool IsImage(string nameFile)
        {
            string[] formats = new string[] { ".jpg", ".png", ".gif", ".jpeg" };
            return formats.Any(item => nameFile.EndsWith(item, StringComparison.OrdinalIgnoreCase));
        }
    }
}
