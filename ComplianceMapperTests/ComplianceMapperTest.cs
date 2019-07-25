using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ComplianceDemo;
using System.Text;

namespace ComplianceMapperTests
{
    [TestClass]
    public class ComplianceMapperTest
    {
        [TestInitialize]
        public void InitializeTest()
        {
            StreamWriter standardOut =
                new StreamWriter(Console.OpenStandardOutput());
            standardOut.AutoFlush = true;
            Console.SetOut(standardOut);
        }

        [TestMethod]
        public void When_MoreRowsEnteredThanSpecified_ErrorIsReturned()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                using (StringReader sr = new StringReader(string.Format("2 3{0}.*.{0}...{0}...{0}", Environment.NewLine)))
                {
                    Console.SetIn(sr);

                    ComplianceMapper.Main(new string[] { });

                    string expected = "Invalid data entry detected";

                    Assert.IsTrue(sw.ToString().Contains(expected));
                }
            }
        }

        [TestMethod]
        public void When_FewerRowsEnteredThanSpecified_ErrorIsReturned()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                using (StringReader sr = new StringReader(string.Format("2 3{0}.*.{0}0 0{0}", Environment.NewLine)))
                {
                    Console.SetIn(sr);

                    ComplianceMapper.Main(new string[] { });

                    string expected = "Invalid data entry detected";

                    Assert.IsTrue(sw.ToString().Contains(expected));
                }
            }

        }

        [TestMethod]
        public void When_InconsistentColumnsEntered_ErrorIsReturned()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                using (StringReader sr = new StringReader(string.Format("2 3{0}.*.{0}....{0}0 0{0}", Environment.NewLine)))
                {
                    Console.SetIn(sr);

                    ComplianceMapper.Main(new string[] { });

                    string expected = "Invalid data entry detected";

                    Assert.IsTrue(sw.ToString().Contains(expected));
                }
            }
        }

        [TestMethod]
        public void When_AnythingBesidesANumberOrAsteriskOrDotIsEntered_ErrorIsReturned()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                using (StringReader sr = new StringReader(string.Format("H 3{0}.*.{0}....{0}0 0{0}", Environment.NewLine)))
                {
                    Console.SetIn(sr);

                    ComplianceMapper.Main(new string[] { });

                    string expected = "Invalid data entry detected";

                    Assert.IsTrue(sw.ToString().Contains(expected));
                }
            }
        }

        [TestMethod]
        public void When_NothingIsEntered_ErrorIsReturned()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                using (StringReader sr = new StringReader(string.Format("0 0{0}", Environment.NewLine)))
                {
                    Console.SetIn(sr);

                    ComplianceMapper.Main(new string[] { });

                    string expected = "Invalid data entry detected";

                    Assert.IsTrue(sw.ToString().Contains(expected));
                }
            }
        }

        [TestMethod]
        public void When_ValidDataIsEntered_ValidMapIsReturned()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                using (StringReader sr = new StringReader(string.Format("2 3{0}.*.{0}.*.{0}0 0{0}", Environment.NewLine)))
                {
                    Console.SetIn(sr);

                    ComplianceMapper.Main(new string[] { });

                    string expected = string.Format("Set #1:{0}2*2{0}2*2{0}{0}",Environment.NewLine);
                    string unexpected = "Invalid data entry";

                    Assert.IsTrue(sw.ToString().Contains(expected));
                    Assert.IsTrue(!sw.ToString().Contains(unexpected));
                }
            }
        }
    }
}
