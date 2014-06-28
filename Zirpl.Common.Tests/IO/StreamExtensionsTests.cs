using System;
using System.IO;
using System.Reflection;
using NUnit.Framework;
using Zirpl.IO;

namespace Zirpl.Common.Tests.IO
{
    [TestFixture]
    public class StreamExtensionsTests
    {
        private String assemblyDirectory;
        private String exampleContentInputFilePath;
        private String exampleContentOutputFilePath;

        [SetUp]
        public void SetUp()
        {
            var path = Assembly.GetExecutingAssembly().CodeBase;
            assemblyDirectory = Path.GetDirectoryName(path.Replace(@"file:///", null));
            exampleContentInputFilePath = Path.Combine(assemblyDirectory, @"IO\example_content.txt");
            exampleContentOutputFilePath = Path.Combine(assemblyDirectory, @"IO\example_content.txt.output");
        }

        [TearDown]
        public void TearDown()
        {
            if (File.Exists(exampleContentOutputFilePath))
            {
                File.Delete(exampleContentOutputFilePath);
            }
        }

        [Test]
        public void TestCopyStream_File()
        {
            using (var input = new System.IO.FileStream(exampleContentInputFilePath, FileMode.Open))
            {
                using (var output = new System.IO.FileStream(exampleContentOutputFilePath, FileMode.CreateNew))
                {
                    input.CopyStream(output);
                }
            }

            Assert.IsTrue(File.Exists(exampleContentOutputFilePath));
            Assert.AreEqual("example_content Testing123", File.ReadAllText(exampleContentOutputFilePath));
        }
    }
}
