using Microsoft.VisualStudio.TestTools.UnitTesting;
using FormSubmit;
using System;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;

namespace FormSubmit
{
    [TestClass]
    public class FormSubmitTest
    {
        [TestMethod]
        public void EmptyTimesheet()
        {
            string localPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            string path = localPath + @"\FormSubmit\emptyTimesheet.json";
            System.Console.WriteLine(path);

            if (!File.Exists(path))
            {
                Assert.IsTrue(false);
            }

            string k = File.ReadAllText(path);
            TimesheetForm obj;
            obj = new TimesheetForm();

            // Due to Windows adding \r for newlines we remove these, and for whatever reason Windows
            // Also adds a newline at the end of the file even if it doesn't exist, so we add that.
            string j = JsonConvert.SerializeObject(obj, Formatting.Indented).Replace("\r","") + "\n";
            
            Assert.IsTrue(String.Equals(j, k));
        }

        [TestMethod]
        public void TenRowTimesheet()
        {
            string localPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            string path = localPath + @"\FormSubmit\TenRowTimesheet.json";
            System.Console.WriteLine(path);

            if (!File.Exists(path))
            {
                Assert.IsTrue(false);
            }

            string k = File.ReadAllText(path);
            TimesheetForm obj = new TimesheetForm();

            obj.clientName   = "Donald Duck";
            obj.prime        = "123456";
            obj.providerName = "Daughy Duck";
            obj.providerNum  = "654321";
            obj.brokerage    = "Not sure";
            obj.scpaName     = "SC/PA";
            obj.serviceAuthorized = "All";
            obj.units = 20;
            obj.type = "Feeding";
            obj.frequency = "Daily";

            obj.addTimeRow("2020-03-20", "9:00", "10:00", 1, 2);
            obj.addTimeRow("2020-03-21", "9:00", "10:00", 1, 2);
            obj.addTimeRow("2020-03-22", "9:00", "10:00", 1, 2);
            obj.addTimeRow("2020-03-23", "9:00", "10:00", 1, 2);
            obj.addTimeRow("2020-03-24", "9:00", "10:00", 1, 2);
            obj.addTimeRow("2020-03-25", "9:00", "10:00", 1, 2);
            obj.addTimeRow("2020-03-26", "9:00", "10:00", 1, 2);
            obj.addTimeRow("2020-03-27", "9:00", "10:00", 1, 2);
            obj.addTimeRow("2020-03-28", "9:00", "10:00", 1, 2);
            obj.addTimeRow("2020-03-29", "9:00", "10:00", 1, 2);

            obj.serviceGoal = "Feed them";
            obj.progressNotes = "Eating more fish";
            obj.employerSignature = true;
            obj.employerSignatureDate = "2020-04-01";
            obj.authorization = true;
            obj.approval = true;
            obj.brokerageSignature = true;
            obj.brokerageSignatureDate = "2020-04-01";

            // Due to Windows adding \r for newlines we remove these, and for whatever reason Windows
            // Also adds a newline at the end of the file even if it doesn't exist, so we add that.
            string j = JsonConvert.SerializeObject(obj, Formatting.Indented).Replace("\r", "") + "\n";

            Assert.IsTrue(String.Equals(j, k));
        }
    }
}
