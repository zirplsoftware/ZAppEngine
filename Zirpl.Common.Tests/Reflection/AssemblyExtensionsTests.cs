using System;
using System.IO;
using System.Reflection;
using NUnit.Framework;
using Zirpl.Reflection;

namespace Zirpl.Common.Tests.Reflection
{
    [TestFixture]
    public class AssemblyExtensionsTests
    {
        private String _exampleResourceOutputFilePath;

        [SetUp]
        public void SetUp()
        {
            var path = Assembly.GetExecutingAssembly().CodeBase;
            var assemblyDirectory = Path.GetDirectoryName(path.Replace(@"file:///", null));
            _exampleResourceOutputFilePath = Path.Combine(assemblyDirectory, @"IO\example_resource.txt.output");
        }

        [TearDown]
        public void TearDown()
        {
            if (File.Exists(_exampleResourceOutputFilePath))
            {
                File.Delete(_exampleResourceOutputFilePath);
            }
        }

        [Test]
        public void TestWriteEmbeddedResourceToFile()
        {
            Assembly.GetExecutingAssembly().WriteEmbeddedResourceToFile("Zirpl.Common.Tests.IO.example_embedded.txt", _exampleResourceOutputFilePath);

            Assert.IsTrue(File.Exists(_exampleResourceOutputFilePath));
            Assert.AreEqual("example_embedded Testing123", File.ReadAllText(_exampleResourceOutputFilePath));
        }
    }
}
