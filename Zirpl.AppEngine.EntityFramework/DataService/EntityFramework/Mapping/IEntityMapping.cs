using System.Data.Entity;

namespace Zirpl.AppEngine.DataService.EntityFramework.Mapping
{
    public interface IEntityMapping
    {
        void OnModelCreating(DbModelBuilder modelBuilder);
    }
}
