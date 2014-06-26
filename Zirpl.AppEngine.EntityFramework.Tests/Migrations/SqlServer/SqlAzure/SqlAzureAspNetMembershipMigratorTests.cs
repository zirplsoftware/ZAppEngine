using System.IO;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Zirpl.AppEngine.DataService.EntityFramework.Migrations.SqlServer.SqlAzure;

namespace Zirpl.AppEngine.EntityFramework.Tests.Migrations.SqlServer.SqlAzure
{
    [TestFixture]
    public class SqlAzureAspNetMembershipMigratorTests
    {
        private SqlAzureAspNetMembershipMigrator migrator;

        [SetUp]
        public void SetUp()
        {
            this.migrator = new SqlAzureAspNetMembershipMigrator(null, null);
        }

        [Test]
        public void TestExtractAspNetRegSqlFilesForSqlAzureToDisk()
        {
            var fileInfos =this.migrator.ExtractFiles();
            fileInfos.Should().NotBeNull();
            fileInfos.Count().Should().BeGreaterOrEqualTo(1);

            foreach (var fileInfo in fileInfos)
            {
                File.Exists(fileInfo.FullName).Should().BeTrue();
            }
        }
    }
}
