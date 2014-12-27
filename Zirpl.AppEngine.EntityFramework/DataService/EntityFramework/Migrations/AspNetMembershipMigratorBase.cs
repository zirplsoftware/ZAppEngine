//extern alias ZirplCore;

using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Reflection;
using Zirpl.Reflection;

namespace Zirpl.AppEngine.DataService.EntityFramework.Migrations
{
    public abstract class AspNetMembershipMigratorBase :IAspNetMembershipMigrator
    {
        public AspNetMembershipMigratorBase(Action<String, bool, Object> sqlExecutionAction, String connectionStringName = null)
        {
            // default the ExtractedFilesDirectory
            //
            var assembly = Assembly.GetExecutingAssembly();
            var path = Path.GetDirectoryName(assembly.GetName().CodeBase).Replace(@"file:\", string.Empty);
            path = Path.Combine(path, this.DefaultExtractedFilesParentFolderName);
            this.ExtractedFilesDirectory = path;

            // default the connectionString
            //
            if (!String.IsNullOrWhiteSpace(connectionStringName))
            {
                var connectionstring = ConfigurationManager.ConnectionStrings[connectionStringName];
                if (connectionstring == null
                    || String.IsNullOrEmpty(connectionstring.ConnectionString))
                {
                    throw new InvalidOperationException("Connection string required");
                }
                this.ConnectionString = connectionstring.ConnectionString;
            }

            
            this.SqlExecutionAction = sqlExecutionAction;
        }
        
        public String ConnectionString { get; set; }
        public string ExtractedFilesDirectory { get; set; }
        protected abstract String DefaultExtractedFilesParentFolderName { get; }
        protected abstract Dictionary<ManifestResourceInfo, String> ResourcesToExtract { get; }
        protected Action<String, bool, Object> SqlExecutionAction { get; set; }

        public void Create()
        {
            this.ExtractFiles();
            this.OnCreate();

            MigrationUtils.ExecuteEmbeddedResourceAsSqlScript(this.SqlExecutionAction, Assembly.GetExecutingAssembly(), "Zirpl.AppEngine.DataService.EntityFramework.Migrations.SqlServer.Scripts.CreateAspNetMembershipHelperObjects.sql");
            MigrationUtils.GrantExecutePermission(this.SqlExecutionAction, "dbo.usp_ChangeUsername", "[aspnet_Membership_FullAccess]");
        }

        protected abstract void OnCreate();

        public void Drop()
        {
            this.ExtractFiles();

            MigrationUtils.ExecuteEmbeddedResourceAsSqlScript(this.SqlExecutionAction, Assembly.GetExecutingAssembly(), "Zirpl.AppEngine.DataService.EntityFramework.Migrations.SqlServer.Scripts.DropAspNetMembershipHelperObjects.sql");

            this.OnDrop();
        }

        protected abstract void OnDrop();
        
        public FileInfo[] ExtractFiles()
        {
            List<FileInfo> fileInfoList = new List<FileInfo>();
            foreach (var resourceInfo in this.ResourcesToExtract)
            {
                var resourcePath = resourceInfo.Key.FileName;
                var assembly = resourceInfo.Key.ReferencedAssembly;
                var fileName = resourceInfo.Value;
                var targetPath = Path.Combine(this.ExtractedFilesDirectory, fileName);
                fileInfoList.Add(assembly.WriteEmbeddedResourceToFile(resourcePath, targetPath));
            }
            return fileInfoList.ToArray();
        }
    }
}
