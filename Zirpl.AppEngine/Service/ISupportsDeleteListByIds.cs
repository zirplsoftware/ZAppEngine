using System.Collections.Generic;

namespace Zirpl.AppEngine.Service
{
    public interface ISupportsDeleteListByIds<in TId> :ISupports
    {
        void DeleteById(IEnumerable<TId> ids);
    }
}
