using System;
using Zirpl.AppEngine.DataService;
using Zirpl.AppEngine.Service;

namespace Zirpl.Examples.ContactManager.DataService
{
    public partial interface IProjectUrlDataService : IDataService<Zirpl.Examples.ContactManager.Model.ProjectUrl, long>, ISupports

        , ISupportsGetById<Zirpl.Examples.ContactManager.Model.ProjectUrl, long>
        , ISupportsGetAll<Zirpl.Examples.ContactManager.Model.ProjectUrl>
        , ISupportsExists<long>
        , ISupportsGetTotalCount
        , ISupportsSearch<Zirpl.Examples.ContactManager.Model.ProjectUrl>
        , ISupportsSearchUnique<Zirpl.Examples.ContactManager.Model.ProjectUrl>
        , ISupportsQueryable<Zirpl.Examples.ContactManager.Model.ProjectUrl>
        , ISupportsReload<Zirpl.Examples.ContactManager.Model.ProjectUrl>

        , ISupportsDelete<Zirpl.Examples.ContactManager.Model.ProjectUrl>
        , ISupportsDeleteById<long>
        , ISupportsDeleteList<Zirpl.Examples.ContactManager.Model.ProjectUrl>
        , ISupportsDeleteListByIds<long>
        , ISupportsDeleteSearch
        , ISupportsCreate<Zirpl.Examples.ContactManager.Model.ProjectUrl>
        , ISupportsInsert<Zirpl.Examples.ContactManager.Model.ProjectUrl>
        , ISupportsInsertList<Zirpl.Examples.ContactManager.Model.ProjectUrl>
        , ISupportsUpdate<Zirpl.Examples.ContactManager.Model.ProjectUrl>
        , ISupportsUpdateList<Zirpl.Examples.ContactManager.Model.ProjectUrl>
        , ISupportsSave<Zirpl.Examples.ContactManager.Model.ProjectUrl>
        , ISupportsSaveList<Zirpl.Examples.ContactManager.Model.ProjectUrl>
    {

    }
}
