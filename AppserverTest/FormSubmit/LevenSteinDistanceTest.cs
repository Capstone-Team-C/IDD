using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using AFO = AbstractFormObject;
namespace AppserverTest.FormSubmit
{
    [TestFixture]
    class LevenSteinDistanceTest
    {
        [TestCase(0, 0, 0, 0)]
        [TestCase(0, 1, 2, 0)]
        [TestCase(0, 1, 2, 0)]
        [TestCase(2, 1, 2, 1)]
        [TestCase(2, 1, 0, 0)]
        [TestCase(2, 1, -1, -1)]
        public void MinimumTest(int x, int y, int z, int min)
        {
            Assert.AreEqual(min, AFO.minimum(x,y,z));
        }

        [TestCase("SomeString")]
        [TestCase("short")]
        [TestCase("CAPS")]
        public void SameStringTest(string s)
        {
            Assert.AreEqual(0,AFO.LevenshteinDistance(s, s));
        }

        [TestCase("SomeString","SOMEstring")]
        public void OnlyCapsDifference(string s, string t)
        {
            Assert.AreEqual(s.ToLower(), t.ToLower());
            int diff = 0;
            for( int i = 0; i < s.Length; ++i)
            {
                if(s[i] != t[i])
                {
                    diff++;
                }
            }
            Assert.AreEqual(diff, AFO.LevenshteinDistance(s,t));
        }

        [TestCase("SomeString")]
        public void PrefixStrings(string s)
        {
            Assert.IsTrue(s.Length > 4);
            string t = s.Substring(0, 4);
            Assert.AreEqual(s.Length - 4,AFO.LevenshteinDistance(s, t));
        }

        [TestCase("sitting", "kitten", 3)]
        public void DifferentString( string s, string t, int diff)
        {
            Assert.AreEqual(diff, AFO.LevenshteinDistance(s, t));
        }
    }
}
