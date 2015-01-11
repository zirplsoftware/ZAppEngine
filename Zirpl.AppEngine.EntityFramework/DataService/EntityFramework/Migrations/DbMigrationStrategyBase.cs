using System;
using System.IO;
using System.Reflection;

namespace Zirpl.AppEngine.DataService.EntityFramework.Migrations
{
    public abstract class DbMigrationStrategyBase :IDbMigrationStrategy
    {
        public DbMigrationProvider Provider { get; set; }

        protected DbMigrationStrategyBase(DbMigrationProvider provider)
        {
            this.Provider = provider;
        }

        public abstract void OnPostUp();
        public abstract void OnPreDown();
        
        protected void Sql(String sql, bool suppressTransaction = false, Object anonymousArguments = null)
        {
            this.Provider.Sql(sql, suppressTransaction, anonymousArguments);
        }

        protected void ExecuteEmbeddedResourceAsSqlScript(Assembly assembly, String sqlScriptResourcePath, bool suppressTransaction = false, Object anonymousArguments = null, params object[] formatArgs)
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
            this.Sql(sql, suppressTransaction, anonymousArguments);
        }

        protected void ExecuteEmbeddedResourceAsSqlScript(String assemblyName, String sqlScriptResourcePath, bool suppressTransaction = false, Object anonymousArguments = null, params object[] formatArgs)
        {
            var assembly = Assembly.Load(new AssemblyName(assemblyName));
            this.ExecuteEmbeddedResourceAsSqlScript(assembly, sqlScriptResourcePath, suppressTransaction, anonymousArguments, formatArgs);
        }
    }
}
