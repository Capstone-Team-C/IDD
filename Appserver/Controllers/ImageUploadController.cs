using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Data.SqlClient;
using Amazon.Textract;


namespace mvc_trial.Controllers
{
    public class ImageUploadController : Controller
    {
        // Path to upload submitted documents while waiting for
        // Azure/AWS infrastructure to get setup. 
        private string image_upload_path = Path.GetFullPath(".") + "/uploaded_docs/";


        // POST: /home/timesheet/
        [HttpPost("ImageUpload")]
        public IActionResult PostImage(List<IFormFile> files)
        {
            // Total upload size
            long size = files.Sum(f => f.Length);

            // MIME types for image processing
            var image_types = new List<string>();
            image_types.Add("image/jpeg");
            image_types.Add("image/png");
            image_types.Add("image/tiff");

            //List of file upload times
            List<String> stats = new List<string>();

            // Iterate over list of submitted documents
            foreach (var file in files)
            {
                // Is the file non-empty?
                if(file.Length > 0)
                {
                    // Process image file upload
                    if (image_types.Contains(file.ContentType))
                    {
                        //Time how long it takes to upload image
                        Stopwatch stopwatch = new Stopwatch();
                        stopwatch.Start();

                        process_image_upload(file);

                        stopwatch.Stop();
                        TimeSpan ts = stopwatch.Elapsed;
                        string s = String.Format("{0:00}:{1:00}", ts.Minutes, ts.Seconds);
                        stopwatch.Reset();
                        stats.Add(file.FileName + " :: " + s);
                    }
                    // Process PDF upload
                    else if ("application/pdf".Equals(file.ContentType))
                    {
                        //Time how long it takes to upload pdf
                        Stopwatch stopwatch = new Stopwatch();
                        stopwatch.Start();
                        process_pdf_upload(file);

                        stopwatch.Stop();
                        TimeSpan ts = stopwatch.Elapsed;
                        string s = String.Format("{0:00}:{1:00}", ts.Minutes, ts.Seconds);
                        stopwatch.Reset();
                        stats.Add(file.FileName + " :: " + s);
                    }
                    // Skip unhandled MIME types
                    else
                    {
                        var s = "Skipping MIME Type " + file.ContentType;
                        s += " having file name " + file.FileName;
                        Debug.WriteLine(s);
                    }
                }
            }

            return Json(new { count = files.Count, total_size = size, upload_times=stats});
        }

        // Method to generate the connection string to remote SQL Server
        // TODO: Move db creds to something like a config file
        private string generate_conn_str()
        {
            string hostname = "clown-db-instance.cjq672cxkqss.us-west-2.rds.amazonaws.com";
            string dbname = "";
            string username = "countess_bathory";
            string password = "clown-von-password";
            return "Data Source=" + hostname + ";Initial Catalog=" + dbname + ";User ID=" + username + ";Password=" + password + ";";
        }


        // Returns a connection object to a remote SQL Server
        // NOTE add some try/catch goodness
        // TODO: move this and all db related methods to their own files.
        private SqlConnection connect_to_db(string connection_string)
        {
            SqlConnection dbcon = new SqlConnection(connection_string);
            return dbcon;
        }


        // TODO make this work
        /*
        private void TextractPOC(byte[] doc_as_bytes)
        {
            // Convert bytes to base64
            string b64 = Convert.ToBase64String(doc_as_bytes);
            // Create request JSON
            var jsondoc = Json(new { Blob = doc_as_bytes });
            var features = new List<string>();
            features.Add("TABLES");
            features.Add("FORMS");
            var request_json = Json(new { Document = jsondoc, FeatureTypes = features });


            Amazon.Textract.Model.Document document = new Amazon.Textract.Model.Document();
            AmazonTextractClient client = new AmazonTextractClient();
        }*/

        // FIXME: In the current implementation, this only works when the
        // INBOUND port rules for the applicable security group are set to
        // allow for connections from any/all clients (*). Currently, this
        // just verfies that we're able to create a connection with the remote
        // SQL Server, currently hosted in AWS as a RDS.
        // FIXME: Inbound port rules are not left open by default.
        // Method to send uploaded documents to remote SQL storage
        private void documents_to_db()
        {
            string cstr = generate_conn_str();
            SqlConnection conn = connect_to_db(cstr);
            conn.Open();
            conn.Close();
        }

        // Method to handle image uploads. Takes as an argument an
        // IFormFile, which represents a file sent with an HTTP Request.
        private void process_image_upload(IFormFile file)
        {
            // Create a path for the uploaded image to be saved to. In this case
            // it's just a project root level folder called 'uploaded_docs'
            string sPath = Path.GetFullPath(image_upload_path + file.FileName);
            using (FileStream sFile = System.IO.File.Create(@"" + sPath))
            {
                // save file
                file.CopyTo(sFile);

                // convert to base64 string
                string b64;
                using (MemoryStream stream = new MemoryStream())
                {
                    file.CopyTo(stream);
                    byte[] fileAsBytes = stream.ToArray();
                    b64 = Convert.ToBase64String(fileAsBytes);

                    //FIXME pass this to textract when figured out
                    // Document model that can be passed to textract
                    Amazon.Textract.Model.Document document = new Amazon.Textract.Model.Document();
                    document.Bytes = stream;

                    Debug.WriteLine("Base64string created from " + file.FileName);
                }
            }
            // TODO should this return something?
            return;
        }


        // Method to handle PDF uploads. Pages from PDF uploads
        // needs to be turned into bytes to send to Textract.
        // We could do this by page in the PDF, but how would we know
        // what type of page we're sending? Milage, hours, etc.?
        // Method argument is file sent with an HTTP Request (IFormFile)
        private void process_pdf_upload(IFormFile file)
        {
            Debug.WriteLine("Would have processed a PDF");
            return;
        }


        // Method to combine multiple submissions into a single object
        private void CombineSubmissions() { }

        // Method to generate BLOB from document
        private void GenerateBlob() { }

        // Method to store submission in SQL Server
        private void StoreSubmission() { }

        // Method to process AWS Textract results
        private void ParseTextract() { }

        // Method to send document to AWS Textract
        private void SendToTextract() { }

        // Method to get submission from SQL Server
        private void GetSubmission() { }

    }
}
