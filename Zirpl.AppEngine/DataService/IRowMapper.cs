using System.Data;

namespace Zirpl.AppEngine.DataService
{
    public interface IRowMapper<TEntity>
    {
        TEntity MapRow(IDataReader reader, int rowNum);
    }
}
