using System;
using Amazon.CognitoSync.Model;
using Amazon.S3.Model;

namespace AdminUI.Models
{
    public class Timesheet
    {
        public int ID { get; set; }
        public string ClientName { get; set; }
        public string ProviderName { get; set; }
        public int Prime { get; set; }
        public double Hours { get; set; }
        public DateTime Submitted { get; set; }
    }
}