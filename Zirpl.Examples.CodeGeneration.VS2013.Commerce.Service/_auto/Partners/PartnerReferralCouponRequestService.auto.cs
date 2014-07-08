using System;
using Zirpl.AppEngine.Service;
using Zirpl.AppEngine.Service.EntityFramework;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Partners;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Partners;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Partners
{
    public partial class PartnerReferralCouponRequestService  : DbContextServiceBase<CommerceDataContext, PartnerReferralCouponRequest, int>, IPartnerReferralCouponRequestService
    {
    }
}
