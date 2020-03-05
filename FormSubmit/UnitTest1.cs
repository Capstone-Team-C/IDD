using Microsoft.VisualStudio.TestTools.UnitTesting;
using FormSubmit;
using System;

namespace FormSubmit
{
    [TestClass]
    public class UnitTest1
    {
        

        [TestMethod]
        public void TestMethod1()
        {
            AbstractFormObject obj;
            obj = new TimesheetForm();
            obj.print();
            System.Console.Write("First Run");
            Assert.IsFalse(false);
        }

        [TestMethod]
        public void FromJSON()
        {
            Assert.IsFalse(true);
        }

        [TestMethod]
        public void ToJSON()
        {
            string k = "{\"json\":\"Something Here\"}";
            AbstractFormObject obj;
            obj = new TimesheetForm();

            string j = obj.ToJSON();
            System.Console.Write(j);
            
            Assert.IsTrue(String.Equals(j,k));
        }
    }
}
