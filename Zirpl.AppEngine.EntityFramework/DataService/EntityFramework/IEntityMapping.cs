using System.Data.Entity;

namespace Zirpl.AppEngine.DataService.EntityFramework
{
    public interface IEntityMapping
    {
        void OnModelCreating(DbModelBuilder modelBuilder);
    }
}
