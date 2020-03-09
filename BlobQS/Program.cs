using System;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System.IO;
using System.Threading.Tasks;

namespace BlobQS
{
    public class Program
    {
        static void Main()
        {
        }

        public static async Task uploadImage(BlobClient blobClient, string fileName, string localFilePath)
        {
            ///////////////////////////
            // Upload Blob to container
            ///////////////////////////

            Console.WriteLine("Uploading to Blob storage as blob:\n\t {0}\n", blobClient.Uri);

            // Open the file and upload its data
            using FileStream uploadFileStream = File.OpenRead(localFilePath);
            await blobClient.UploadAsync(uploadFileStream, true);
            uploadFileStream.Close();

        }
    }
}
