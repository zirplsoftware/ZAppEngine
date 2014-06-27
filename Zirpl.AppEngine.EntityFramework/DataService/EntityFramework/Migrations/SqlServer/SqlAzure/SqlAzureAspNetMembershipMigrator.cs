using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Reflection;

namespace Zirpl.AppEngine.DataService.EntityFramework.Migrations.SqlServer.SqlAzure
{
    public class SqlAzureAspNetMembershipMigrator :AspNetMembershipMigratorBase
    {
        public SqlAzureAspNetMembershipMigrator(Action<String, bool, Object> sqlExecutionAction, String connectionStringName)
            : base(sqlExecutionAction, connectionStringName)
        {
        }

        protected override string DefaultExtractedFilesParentFolderName
        {
            get { return "AspNetToolsForSqlAzure"; }
        }

        protected override void OnCreate()
        {
            var builder = new SqlConnectionStringBuilder(this.ConnectionString);

            var arguments = string.Format(@" -S {0} -D {1} -U {2} -P {3} -A mrp", builder.DataSource, builder.InitialCatalog, builder.UserID, builder.Password);

            var processStartInfo = new ProcessStartInfo
            {
                WorkingDirectory = this.ExtractedFilesDirectory,
                FileName = "aspnet_regsqlazure.exe",
                Arguments = arguments,
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Normal
            };

            using (var process = Process.Start(processStartInfo))
            {
                process.WaitForExit();
            }
        }

        protected override void OnDrop()
        {
            MigrationUtils.ExecuteEmbeddedResourceAsSqlScript(this.SqlExecutionAction, Assembly.GetExecutingAssembly(), "Zirpl.AppEngine.DataService.EntityFramework.Migrations.SqlServer.SqlAzure.Scripts.PrepareRunAspNetRegSqlUnregister.sql");
            MigrationUtils.ExecuteEmbeddedResourceAsSqlScript(this.SqlExecutionAction, Assembly.GetExecutingAssembly(), "Zirpl.AppEngine.DataService.EntityFramework.Migrations.SqlServer.SqlAzure.Scripts.FinalizeRunAspNetRegSqlUnregister.sql");
        }

        protected override Dictionary<ManifestResourceInfo, String> ResourcesToExtract
        {
            get
            {
                var dictionary = new Dictionary<ManifestResourceInfo, String>();
                var assembly = Assembly.GetExecutingAssembly();

                dictionary.Add(new ManifestResourceInfo(assembly, "Zirpl.AppEngine.DataService.EntityFramework.Migrations.SqlServer.SqlAzure.Tools.InstallCommon.sql", ResourceLocation.Embedded), "InstallCommon.sql");


                dictionary.Add(new ManifestResourceInfo(assembly, "Zirpl.AppEngine.DataService.EntityFramework.Migrations.SqlServer.SqlAzure.Tools.InstallMembership.sql", ResourceLocation.Embedded), "InstallMembership.sql");
                dictionary.Add(new ManifestResourceInfo(assembly, "Zirpl.AppEngine.DataService.EntityFramework.Migrations.SqlServer.SqlAzure.Tools.InstallPersistSqlState.sql", ResourceLocation.Embedded), "InstallPersistSqlState.sql");
                dictionary.Add(new ManifestResourceInfo(assembly, "Zirpl.AppEngine.DataService.EntityFramework.Migrations.SqlServer.SqlAzure.Tools.InstallPersonalization.sql", ResourceLocation.Embedded), "InstallPersonalization.sql");
                dictionary.Add(new ManifestResourceInfo(assembly, "Zirpl.AppEngine.DataService.EntityFramework.Migrations.SqlServer.SqlAzure.Tools.InstallProfile.SQL", ResourceLocation.Embedded), "InstallProfile.sql");
                dictionary.Add(new ManifestResourceInfo(assembly, "Zirpl.AppEngine.DataService.EntityFramework.Migrations.SqlServer.SqlAzure.Tools.InstallRoles.sql", ResourceLocation.Embedded), "InstallRoles.SQL");
                dictionary.Add(new ManifestResourceInfo(assembly, "Zirpl.AppEngine.DataService.EntityFramework.Migrations.SqlServer.SqlAzure.Tools.InstallSqlState.sql", ResourceLocation.Embedded), "InstallSqlState.sql");
                dictionary.Add(new ManifestResourceInfo(assembly, "Zirpl.AppEngine.DataService.EntityFramework.Migrations.SqlServer.SqlAzure.Tools.InstallSqlStateTemplate.sql", ResourceLocation.Embedded), "InstallSqlStateTemplate.sql");
                dictionary.Add(new ManifestResourceInfo(assembly, "Zirpl.AppEngine.DataService.EntityFramework.Migrations.SqlServer.SqlAzure.Tools.InstallWebEventSqlProvider.sql", ResourceLocation.Embedded), "InstallWebEventSqlProvider.sql");
                dictionary.Add(new ManifestResourceInfo(assembly, "Zirpl.AppEngine.DataService.EntityFramework.Migrations.SqlServer.SqlAzure.Tools.app.config", ResourceLocation.Embedded), "app.config");
                dictionary.Add(new ManifestResourceInfo(assembly, "Zirpl.AppEngine.DataService.EntityFramework.Migrations.SqlServer.SqlAzure.Tools.aspnet_regsqlazure.exe", ResourceLocation.Embedded), "aspnet_regsqlazure.exe");
                return dictionary;
            }
        }
    }
}
