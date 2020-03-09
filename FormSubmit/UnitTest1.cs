using Microsoft.VisualStudio.TestTools.UnitTesting;
using FormSubmit;
using System;
using System.Diagnostics;

namespace FormSubmit
{
    [TestClass]
    public class UnitTest1
    {
    
        //[TestMethod]
        //public void FromJSON()
        //{
        //    Assert.IsFalse(true);
        //}
        [TestMethod]
        public void testStoringImagetoAzure()
        {
            // First grab a file from filesystem
            // Setup bucket in azure

            // Call my code(file)
            // check my code

            // remove things from azure
            // clean up
        }


        [TestMethod]
        public void ToJSON()
        {
            string k = "{\"json\":\"Something Here\"}";
            TimesheetForm obj;
            obj = new TimesheetForm();

            for( int i =0; i < 10; ++i){
               obj.addTimeRow(i, 100, 200, true);
            }
            
            string j = obj.ToJSON();
            System.Console.WriteLine(j);
            System.Console.WriteLine(k);
            
            Assert.IsTrue(false);
            Assert.IsTrue(String.Equals(j,k));
        }
    }
}
