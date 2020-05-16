using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Amazon;
using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;
using Amazon.Runtime.Internal;
using Amazon.Textract;
using Amazon.Textract.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;


namespace Appserver.Controllers
{
    public class TextractHandler
    {
		private AmazonTextractClient textractClient;
		public TextractHandler()
		{
			this.textractClient = new AmazonTextractClient();
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
				var job = StartDocumentPDFAnalysis(file.OpenReadStream(), new List<string> { "TABLES", "FORMS" });
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

		private async Task<AnalyzeDocumentResponse> StartDocumentPDFAnalysis(Stream file, List<string> featureTypes)
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
	}
}
