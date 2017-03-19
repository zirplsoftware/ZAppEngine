using System;
using Zirpl.AppEngine.DataService;
using Zirpl.AppEngine.Service;

namespace Zirpl.Examples.ContactManager.DataService
{
    public partial interface IProjectDataService : IDataService<Zirpl.Examples.ContactManager.Model.Project, long>, ISupports

        , ISupportsGetById<Zirpl.Examples.ContactManager.Model.Project, long>
        , ISupportsGetAll<Zirpl.Examples.ContactManager.Model.Project>
        , ISupportsExists<long>
        , ISupportsGetTotalCount
        , ISupportsSearch<Zirpl.Examples.ContactManager.Model.Project>
        , ISupportsSearchUnique<Zirpl.Examples.ContactManager.Model.Project>
        , ISupportsQueryable<Zirpl.Examples.ContactManager.Model.Project>
        , ISupportsReload<Zirpl.Examples.ContactManager.Model.Project>

        , ISupportsDelete<Zirpl.Examples.ContactManager.Model.Project>
        , ISupportsDeleteById<long>
        , ISupportsDeleteList<Zirpl.Examples.ContactManager.Model.Project>
        , ISupportsDeleteListByIds<long>
        , ISupportsDeleteSearch
    {

    }
}
