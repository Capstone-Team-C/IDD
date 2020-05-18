using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Amazon;
using Amazon.S3;
using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;
using Amazon.Runtime.Internal;
using Amazon.Textract;
using Amazon.Textract.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Amazon.S3.Transfer;
using Amazon.S3.Model;
using System.Diagnostics;

namespace Appserver.Controllers
{
    public class TextractHandler
    {
		private AmazonTextractClient textractClient;
		private AmazonS3Client s3Client;

		public TextractHandler()
		{
			this.textractClient = new AmazonTextractClient();
			this.s3Client = new AmazonS3Client();
		}

		public AnalyzeDocumentResponse HandleAsyncJob(IFormFile file, string type)
		{
            if(type.ToLower() == "image")
            {
			    var job = StartDocumentAnalysis(file.OpenReadStream(), new List<string> { "TABLES", "FORMS" });
				try
				{
					job.Wait();
				}
				catch (System.AggregateException e)
				{
					Console.WriteLine(e.Message);
					throw;
				}
				var result = job.Result;

				return result;
			}

            if(type.ToLower() == "pdf")
            {
				var job = StartDocumentPDFAnalysis(file, new List<string> { "TABLES", "FORMS" });
				try
				{
					job.Wait();
				}
				catch (System.AggregateException e)
				{
					Console.WriteLine(e.Message);
					throw;
				}
				var result = job.Result;

				return result;// new AnalyzeDocumentResponse();//result;
			}

			throw new System.Exception("Invalid file type " + type);
		}

		private async Task<AnalyzeDocumentResponse> StartDocumentAnalysis(Stream file, List<string> featureTypes)
		{
			var request = new AnalyzeDocumentRequest();
			var memoryStream = new MemoryStream();
			file.CopyTo(memoryStream);

			request.Document = new Document
			{
				Bytes = memoryStream
			};

			request.FeatureTypes = featureTypes;
			var response = await this.textractClient.AnalyzeDocumentAsync(request);
			return response;
		}

		// In order for textract to process PDFs, they must come from an S3 bucket
		// that is in the same region that textract is in. The file is no longer
		// needed in S3 after being processed by textract as the file is still
		// being stored in azure blob.
		//private async Task<StartDocumentAnalysisResponse> StartDocumentPDFAnalysis(IFormFile file, List<string> featureTypes)
		private async Task<AnalyzeDocumentResponse> StartDocumentPDFAnalysis(IFormFile file, List<string> featureTypes)

		{
			// Upload PDF to S3, with guid as file key
			var k = Guid.NewGuid();
			PDFtoS3Bucket(file, k.ToString()).Wait();

            // Create S3 obj to hand to Textract
            var s3 = new Amazon.Textract.Model.S3Object();
            s3.Bucket = "multcapstone";
            s3.Name = k.ToString();
            var r = new StartDocumentAnalysisRequest();

            // Set document for request to S3 obj
            r.DocumentLocation = new DocumentLocation
            {
                S3Object = s3
            };
            r.FeatureTypes = featureTypes;
            var response = await this.textractClient.StartDocumentAnalysisAsync(r);


            //var s2 = new Amazon.Textract.Model.S3Object();
            //s2.Bucket = "multcapstone";
            //s2.Name = k.ToString();
            //var request = new AnalyzeDocumentRequest();
            //request.Document = new Document {
            //	S3Object = s2
            //         };
            //request.FeatureTypes = featureTypes;
            //var response = await this.textractClient.AnalyzeDocumentAsync(request);

            // Remove PDF from S3
            RemoveFromS3Bucket(k.ToString()).Wait();
			return response;
		}


        // Saves PDF to S3 bucket
        private async Task PDFtoS3Bucket(IFormFile file, string filekey)
        {
			var fileTransferUtil = new TransferUtility(s3Client);
			var req = new TransferUtilityUploadRequest();
			var mem = new MemoryStream();

            try
            {
                // Set Params for upload request
				file.CopyTo(mem);
				req.BucketName = "multcapstone";
				req.InputStream = mem;
				req.Key = filekey;

				await fileTransferUtil.UploadAsync(req);
            }
            catch (Exception)
            {
				return;
            }
        }

        // Removes object from bucket with matching key
        private async Task RemoveFromS3Bucket(string key)
        {
			var deleteObj = new DeleteObjectRequest
			{
				BucketName = "multcapstone",
				Key = key
			};

            try
            {
				await s3Client.DeleteObjectAsync(deleteObj);
            }
            catch (Exception)
            {
				return;
            }
        }
	}
}
