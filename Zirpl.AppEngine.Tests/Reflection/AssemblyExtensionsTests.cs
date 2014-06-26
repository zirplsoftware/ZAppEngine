using System;
using System.IO;
using System.Reflection;
using NUnit.Framework;
using Zirpl.AppEngine.Reflection;

namespace Zirpl.AppEngine.Tests.Reflection
{
    [TestFixture]
    public class AssemblyExtensionsTests
    {
        private String assemblyDirectory;
        private String exampleResourceInputFilePath;
        private String exampleResourceOutputFilePath;

        [SetUp]
        public void SetUp()
        {
            var path = Assembly.GetExecutingAssembly().CodeBase;
            assemblyDirectory = Path.GetDirectoryName(path.Replace(@"file:///", null));
            exampleResourceOutputFilePath = Path.Combine(assemblyDirectory, @"IO\example_resource.txt.output");
        }

        [TearDown]
        public void TearDown()
        {
            if (File.Exists(exampleResourceOutputFilePath))
            {
                File.Delete(exampleResourceOutputFilePath);
            }
        }

        [Test]
        public void TestWriteEmbeddedResourceToFile()
        {
            Assembly.GetExecutingAssembly().WriteEmbeddedResourceToFile("Zirpl.AppEngine.Tests.IO.example_embedded.txt", exampleResourceOutputFilePath);

            Assert.IsTrue(File.Exists(exampleResourceOutputFilePath));
            Assert.AreEqual("example_embedded Testing123", File.ReadAllText(exampleResourceOutputFilePath));
        }
    }
}
