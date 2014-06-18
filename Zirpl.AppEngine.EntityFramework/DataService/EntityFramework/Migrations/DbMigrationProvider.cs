using System;

namespace Zirpl.AppEngine.DataService.EntityFramework.Migrations
{
    public class DbMigrationProvider
    {
        public Action<String, bool, Object> Sql { get; set; }
    }
}
