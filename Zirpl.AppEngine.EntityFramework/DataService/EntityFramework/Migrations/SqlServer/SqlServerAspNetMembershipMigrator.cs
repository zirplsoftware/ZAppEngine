using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace Zirpl.AppEngine.DataService.EntityFramework.Migrations.SqlServer
{
    public class SqlServerAspNetMembershipMigrator : AspNetMembershipMigratorBase
    {
        public SqlServerAspNetMembershipMigrator(Action<String, bool, Object> sqlExecutionAction,
                                                 String connectionStringName)
            : base(sqlExecutionAction, connectionStringName)
        {
        }

        protected override string DefaultExtractedFilesParentFolderName
        {
            get { return "AspNetToolsForSqlServer"; }
        }

        protected override void OnCreate()
        {
            this.RunAspNetRegSqlExecutable(false);
        }

        protected override void OnDrop()
        {
            MigrationUtils.ExecuteEmbeddedResourceAsSqlScript(this.SqlExecutionAction, Assembly.GetExecutingAssembly(), "Zirpl.AppFramework.DataService.EntityFramework.Migrations.SqlServer.Scripts.PrepareRunAspNetRegSqlUnregister.sql");

            this.RunAspNetRegSqlExecutable(true);

            // NOTE: for some reason the remove does not actually remove all the object. This is a workaround for now.
            MigrationUtils.ExecuteEmbeddedResourceAsSqlScript(this.SqlExecutionAction, Assembly.GetExecutingAssembly(), "Zirpl.AppFramework.DataService.EntityFramework.Migrations.SqlServer.Scripts.FinalizeRunAspNetRegSqlUnregister.sql");
        }

        protected override Dictionary<ManifestResourceInfo, String> ResourcesToExtract
        {
            get
            {
                var list = new Dictionary<ManifestResourceInfo, String>();
                var assembly = Assembly.GetExecutingAssembly();
                list.Add(new ManifestResourceInfo(assembly, "Zirpl.AppFramework.DataService.EntityFramework.Migrations.SqlServer.Tools.aspnet_regsql.exe", ResourceLocation.Embedded), "aspnet_reqsql.exe");
                return list;
            }
        }

        private void RunAspNetRegSqlExecutable(bool down)
        {
            var action = down ? "R" : "A";
            var arguments = string.Format(@"-Q -C ""{0}"" -{1} mrp", this.ConnectionString, action);

            var processStartInfo = new ProcessStartInfo
            {
                WorkingDirectory = this.ExtractedFilesDirectory,
                FileName = "aspnet_regsql.exe",
                Arguments = arguments,
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Normal
            };

            using (var process = Process.Start(processStartInfo))
            {
                process.WaitForExit();
            }
        }
    }
}
