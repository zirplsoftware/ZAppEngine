﻿using System;
using System.Linq;
using Zirpl.AppEngine.DataService;
using Zirpl.AppEngine.DataService.EntityFramework;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Settings;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Settings
{
    public partial class TaxRuleDataService : DbContextDataServiceBase<CommerceDataContext, TaxRule, int>, ITaxRuleDataService
    {
    }
}
