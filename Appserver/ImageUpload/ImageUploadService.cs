using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.IO;
using System.Threading.Tasks;

namespace IDD
{
    public class ImageUpload
    {
        public static async Task UploadImage(CloudBlockBlob blobClient, string fileName, string localFilePath)
        {
            ///////////////////////////
            // Upload Blob to container
            ///////////////////////////

            Console.WriteLine("Uploading to Blob storage as blob:\n\t {0}\n", blobClient.Uri);

            // Open the file and upload its data
            using FileStream uploadFileStream = File.OpenRead(localFilePath);
            await blobClient.UploadFromStreamAsync(uploadFileStream);
            uploadFileStream.Close();
        }

        public static async Task UploadImage64(CloudBlockBlob blobClient, string fileName, string base64)
        {
            using MemoryStream memoryStream = new MemoryStream( Convert.FromBase64String(base64) );

            var imageBlob = blobClient.Container.GetBlockBlobReference(fileName);
            await imageBlob.UploadFromStreamAsync(memoryStream);
        }
    }
}
