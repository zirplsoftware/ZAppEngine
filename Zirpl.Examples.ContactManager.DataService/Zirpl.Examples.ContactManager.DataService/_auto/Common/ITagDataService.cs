using System;
using Zirpl.AppEngine.DataService;
using Zirpl.AppEngine.Service;

namespace Zirpl.Examples.ContactManager.DataService.Common
{
    public partial interface ITagDataService : IDataService<Zirpl.Examples.ContactManager.Model.Common.Tag, long>, ISupports

        , ISupportsGetById<Zirpl.Examples.ContactManager.Model.Common.Tag, long>
        , ISupportsGetAll<Zirpl.Examples.ContactManager.Model.Common.Tag>
        , ISupportsExists<long>
        , ISupportsGetTotalCount
        , ISupportsSearch<Zirpl.Examples.ContactManager.Model.Common.Tag>
        , ISupportsSearchUnique<Zirpl.Examples.ContactManager.Model.Common.Tag>
        , ISupportsQueryable<Zirpl.Examples.ContactManager.Model.Common.Tag>
        , ISupportsReload<Zirpl.Examples.ContactManager.Model.Common.Tag>

        , ISupportsDelete<Zirpl.Examples.ContactManager.Model.Common.Tag>
        , ISupportsDeleteById<long>
        , ISupportsDeleteList<Zirpl.Examples.ContactManager.Model.Common.Tag>
        , ISupportsDeleteListByIds<long>
        , ISupportsDeleteSearch
        , ISupportsCreate<Zirpl.Examples.ContactManager.Model.Common.Tag>
        , ISupportsInsert<Zirpl.Examples.ContactManager.Model.Common.Tag>
        , ISupportsInsertList<Zirpl.Examples.ContactManager.Model.Common.Tag>
        , ISupportsUpdate<Zirpl.Examples.ContactManager.Model.Common.Tag>
        , ISupportsUpdateList<Zirpl.Examples.ContactManager.Model.Common.Tag>
        , ISupportsSave<Zirpl.Examples.ContactManager.Model.Common.Tag>
        , ISupportsSaveList<Zirpl.Examples.ContactManager.Model.Common.Tag>
    {

    }
}
