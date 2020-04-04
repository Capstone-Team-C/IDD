using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.IO;
using System;
using Microsoft.WindowsAzure.Storage;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using Microsoft.WindowsAzure.Storage.Blob;

namespace ImageUpload.Tests
{
    [TestClass()]
    public class ImageUploadTest
    {
        [TestMethod()]
        [DeploymentItem(@"Appserver/ImageUpload/pineapple.jpg")]
        public async Task TestMethod1()
        {


            //////////////////////////////////////////////////////////////////////////////
            // Initial Setup
            //////////////////////////////////////////////////////////////////////////////
            
            var storageAccount = CloudStorageAccount.Parse(
                "UseDevelopmentStorage=true;"
            );

            var blobStorage = storageAccount.CreateCloudBlobClient();

            //Create a unique name for the container
            string containerName = "testing";

            // Create the container and return a container client object
            var container = blobStorage.GetContainerReference(containerName);
            await container.CreateIfNotExistsAsync();
            await container.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Container });


            // Hard code an image file name here instead
            string fileName = "Appserver/ImageUpload/pineapple.jpg";
            string localPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName;

            Debug.WriteLine(localPath);
            string localFilePath = Path.Combine(localPath, fileName);

            // Get a reference to a blob
            var blobClient = container.GetBlockBlobReference(fileName);

            ////////////////////////////////////////////////////////////////////////////////
            //// Function to Test
            ////////////////////////////////////////////////////////////////////////////////

            await IDD.ImageUpload.UploadImage(blobClient, fileName, localFilePath);

            ////////////////////////////////////////////////////////////////////////////////
            //// Assert it worked
            ////////////////////////////////////////////////////////////////////////////////

            /////////////////////////////
            //// List the blobs in a container
            /////////////////////////////
            //Console.WriteLine("Listing blobs...");

            //// Check item is in Blob
            var blobitem = container.GetBlockBlobReference(fileName);

            var itemexists = blobitem.ExistsAsync();
            await itemexists;
            Assert.IsTrue(itemexists.Result);


            /////////////////////////////
            //// Download Blob
            /////////////////////////////

            //// Download the blob to a local file
            //// Append the string "DOWNLOAD" before the .txt extension 
            //// so you can compare the files in the data directory

            //// Download to same image file+.Download

            //// Download the blob's contents and save it to a file


            ////////////////////////////////////////////////////////////////////////////////
            //// Teardown test
            ////////////////////////////////////////////////////////////////////////////////

        }
    }
}
