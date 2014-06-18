using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Web.Security;
using Zirpl.AppEngine.DataService.EntityFramework.Migrations.SqlServer;
using Zirpl.AppEngine.DataService.EntityFramework.Migrations.SqlServer.SqlAzure;

namespace Zirpl.AppEngine.DataService.EntityFramework.Migrations
{
    public static class MigrationUtils
    {
        public static void CreateRole(Action<String, bool, Object> sqlExecutionAction, String roleName, String owner = "dbo")
        {
            sqlExecutionAction(String.Format("CREATE ROLE {0} AUTHORIZATION {1}", roleName, owner), false, null);
        }

        public static void DropRole(Action<String, bool, Object> sqlExecutionAction, String roleName)
        {
            sqlExecutionAction(String.Format("DROP ROLE {0}", roleName), false, null);
        }

        public static void GrantExecutePermission(Action<String, bool, Object> sqlExecutionAction, String securedObject, String roleOrUser)
        {
            sqlExecutionAction(String.Format("GRANT EXECUTE ON OBJECT::{0} TO {1};", securedObject, roleOrUser), false, null);
        }

        public static void DenyExecutePermission(Action<String, bool, Object> sqlExecutionAction, String securedObject, String roleOrUser)
        {
            sqlExecutionAction(String.Format("DENY EXECUTE ON OBJECT::{0} TO {1};", securedObject, roleOrUser), false, null);
        }

        public static void ExecuteEmbeddedResourceAsSqlScript(Action<string, bool, object> sqlExecutionAction, Assembly assembly, string sqlScriptResourcePath, params object[] formatArgs)
        {
            String sql = null;
            using (var stream = assembly.GetManifestResourceStream(sqlScriptResourcePath))
            {
                if (stream == null)
                {
                    throw new ArgumentException("Could not find embedded resource " + sqlScriptResourcePath);
                }
                using (var reader = new StreamReader(stream))
                {
                    sql = reader.ReadToEnd();
                }
            }
            sql = String.Format(sql, formatArgs);

            String[] sqlTokens = sql.Split(new String[] { "\r\nGO;" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (String token in sqlTokens)
            {
                sqlExecutionAction(token, false, null);
            }
        }

        public static void ExecuteEmbeddedResourceAsSqlScript(Action<String, bool, Object> sqlExecutionAction, String assemblyName, String sqlScriptResourcePath, bool suppressTransaction = false, Object anonymousArguments = null, params object[] formatArgs)
        {
            String sql = null;
            var assembly = Assembly.Load(new AssemblyName(assemblyName));
            ExecuteEmbeddedResourceAsSqlScript(sqlExecutionAction, assembly, sqlScriptResourcePath, formatArgs);
        }

        public static void CreateTableBasedSequence(Action<String, bool, Object> sqlExecutionAction, String sequenceName, int seed, int increment, params String[] rolesOrUsersWithExecutePermission)
        {
            ExecuteEmbeddedResourceAsSqlScript(sqlExecutionAction,
                Assembly.GetExecutingAssembly(),
                "Zirpl.AppFramework.DataService.EntityFramework.Migrations.SqlServer.Scripts.CreateTableBasedSequenceObjects1.sql", sequenceName, seed, increment);
            ExecuteEmbeddedResourceAsSqlScript(sqlExecutionAction,
                Assembly.GetExecutingAssembly(),
                "Zirpl.AppFramework.DataService.EntityFramework.Migrations.SqlServer.Scripts.CreateTableBasedSequenceObjects2.sql", sequenceName);
            ExecuteEmbeddedResourceAsSqlScript(sqlExecutionAction,
                Assembly.GetExecutingAssembly(),
                "Zirpl.AppFramework.DataService.EntityFramework.Migrations.SqlServer.Scripts.CreateTableBasedSequenceObjects3.sql", sequenceName);
            if (rolesOrUsersWithExecutePermission != null)
            {
                foreach (var roleOrUser in rolesOrUsersWithExecutePermission)
                {
                    GrantExecutePermission(sqlExecutionAction, "dbo.usp_GetNext" + sequenceName, roleOrUser);
                    GrantExecutePermission(sqlExecutionAction, "dbo.usp_GetCurrent" + sequenceName, roleOrUser);
                }
            }
        }

        public static void DropTableBasedSequence(Action<String, bool, Object> sqlExecutionAction, String sequenceName)
        {
            ExecuteEmbeddedResourceAsSqlScript(sqlExecutionAction,
                   Assembly.GetExecutingAssembly(),
                   "Zirpl.AppFramework.DataService.EntityFramework.Migrations.SqlServer.Scripts.DropTableBasedSequenceObjects.sql", "PromoCodeSequence");
        }

        public static void CreateAspNetMembershipSchema(Action<String, bool, Object> sqlExecutionAction, String connectionStringName, bool azure)
        {
            var aspnetMigrator = azure
                                ? (IAspNetMembershipMigrator)new SqlAzureAspNetMembershipMigrator(sqlExecutionAction, connectionStringName)
                                : new SqlServerAspNetMembershipMigrator(sqlExecutionAction, connectionStringName);
            aspnetMigrator.Create();
        }

        public static void DropAspNetMembershipSchema(Action<String, bool, Object> sqlExecutionAction, String connectionStringName, bool azure)
        {
            var aspnetMigrator = azure
                                ? (IAspNetMembershipMigrator)new SqlAzureAspNetMembershipMigrator(sqlExecutionAction, connectionStringName)
                                : new SqlServerAspNetMembershipMigrator(sqlExecutionAction, connectionStringName);
            aspnetMigrator.Drop();
        }

        public static void AddAspNetMembershipRoles(IEnumerable<String> roleNames)
        {
            foreach (var roleName in roleNames)
            {
                if (!Roles.RoleExists(roleName))
                {
                    Roles.CreateRole(roleName);
                }
            }
        }
    }
}
