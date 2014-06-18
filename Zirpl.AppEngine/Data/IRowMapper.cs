using System.Data;

namespace Zirpl.AppEngine.Data
{
    public interface IRowMapper<TEntity>
    {
        TEntity MapRow(IDataReader reader, int rowNum);
    }
}
