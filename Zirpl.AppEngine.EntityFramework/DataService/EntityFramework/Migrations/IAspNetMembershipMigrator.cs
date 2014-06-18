using System;

namespace Zirpl.AppEngine.DataService.EntityFramework.Migrations
{
    public interface IAspNetMembershipMigrator
    {
        void Create();
        void Drop();
        String ExtractedFilesDirectory { get; set; }
        String ConnectionString { get; set; }
    }
}
