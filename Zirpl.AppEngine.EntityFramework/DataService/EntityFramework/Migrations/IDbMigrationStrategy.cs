namespace Zirpl.AppEngine.DataService.EntityFramework.Migrations
{
    public interface IDbMigrationStrategy
    {
        void OnPostUp();
        void OnPreDown();
    }
}
