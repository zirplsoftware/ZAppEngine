using System;
using Zirpl.AppEngine.DataService;
using Zirpl.AppEngine.Service;

namespace Zirpl.Examples.ContactManager.DataService
{
    public partial interface IProjectImageDataService : IDataService<Zirpl.Examples.ContactManager.Model.ProjectImage, long>, ISupports

        , ISupportsGetById<Zirpl.Examples.ContactManager.Model.ProjectImage, long>
        , ISupportsGetAll<Zirpl.Examples.ContactManager.Model.ProjectImage>
        , ISupportsExists<long>
        , ISupportsGetTotalCount
        , ISupportsSearch<Zirpl.Examples.ContactManager.Model.ProjectImage>
        , ISupportsSearchUnique<Zirpl.Examples.ContactManager.Model.ProjectImage>
        , ISupportsQueryable<Zirpl.Examples.ContactManager.Model.ProjectImage>
        , ISupportsReload<Zirpl.Examples.ContactManager.Model.ProjectImage>

        , ISupportsDelete<Zirpl.Examples.ContactManager.Model.ProjectImage>
        , ISupportsDeleteById<long>
        , ISupportsDeleteList<Zirpl.Examples.ContactManager.Model.ProjectImage>
        , ISupportsDeleteListByIds<long>
        , ISupportsDeleteSearch
        , ISupportsCreate<Zirpl.Examples.ContactManager.Model.ProjectImage>
        , ISupportsInsert<Zirpl.Examples.ContactManager.Model.ProjectImage>
        , ISupportsInsertList<Zirpl.Examples.ContactManager.Model.ProjectImage>
        , ISupportsUpdate<Zirpl.Examples.ContactManager.Model.ProjectImage>
        , ISupportsUpdateList<Zirpl.Examples.ContactManager.Model.ProjectImage>
        , ISupportsSave<Zirpl.Examples.ContactManager.Model.ProjectImage>
        , ISupportsSaveList<Zirpl.Examples.ContactManager.Model.ProjectImage>
    {

    }
}
