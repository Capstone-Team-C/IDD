using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System.IO;
using System;

using BlobQS;

namespace ImageUpload
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        [DeploymentItem(@"ImageUpload\pineapple.jpg")]
        public async Task TestMethod1()
        {
            

            //////////////////////////////////////////////////////////////////////////////
            // Initial Setup
            //////////////////////////////////////////////////////////////////////////////

            // Get our secret key from local environment
            string connectionString = Environment.GetEnvironmentVariable("AZURE_STORAGE_CONNECTION_STRING");

            // Create a BlobServiceClient object which will be used to create a container client
            BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);

            //Create a unique name for the container
            string containerName = "quickstartblobs" + Guid.NewGuid().ToString();

            // Create the container and return a container client object
            BlobContainerClient containerClient = await blobServiceClient.CreateBlobContainerAsync(containerName);

            // Hard code an image file name here instead
            string fileName = "pineapple.jpg";
            string localPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            string localFilePath = Path.Combine(localPath, fileName);

            // Get a reference to a blob
            BlobClient blobClient = containerClient.GetBlobClient(fileName);

            //////////////////////////////////////////////////////////////////////////////
            // Function to Test
            //////////////////////////////////////////////////////////////////////////////

            await Program.uploadImage(blobClient, fileName, localFilePath);
            
            //////////////////////////////////////////////////////////////////////////////
            // Assert it worked
            //////////////////////////////////////////////////////////////////////////////

            ///////////////////////////
            // List the blobs in a container
            ///////////////////////////
            Console.WriteLine("Listing blobs...");

            // List all blobs in the container
            await foreach (BlobItem blobItem in containerClient.GetBlobsAsync())
            {
                Console.WriteLine("\t" + blobItem.Name);
            }
            
            Console.WriteLine("Listing blobs...");

            // List all blobs in the container
            Boolean fileExists = false;
            await foreach (BlobItem blobItem in containerClient.GetBlobsAsync())
            {
                Console.WriteLine("\t" + blobItem.Name);
                if(blobItem.Name == fileName) fileExists = true;
            }
            Assert.IsTrue(fileExists);

            
            ///////////////////////////
            // Download Blob
            ///////////////////////////

            // Download the blob to a local file
            // Append the string "DOWNLOAD" before the .txt extension 
            // so you can compare the files in the data directory
            
            // Download to same image file+.Download
            string downloadFilePath = localFilePath.Replace(".jpg", "DOWNLOAD.jpg");

            Console.WriteLine("\nDownloading blob to\n\t{0}\n", downloadFilePath);

            // Download the blob's contents and save it to a file
            BlobDownloadInfo download = await blobClient.DownloadAsync();

            using FileStream downloadFileStream = File.OpenWrite(downloadFilePath);
            await download.Content.CopyToAsync(downloadFileStream);
            downloadFileStream.Close();

            //////////////////////////////////////////////////////////////////////////////
            // Teardown test
            //////////////////////////////////////////////////////////////////////////////

            // Assert.IsTrue(false);
        }
    }
}
