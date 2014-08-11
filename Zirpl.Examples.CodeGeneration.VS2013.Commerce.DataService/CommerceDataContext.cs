using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService
{
    public partial class CommerceDataContext
    {
        protected override bool IsModifiable(object obj)
        {
            return true;
        }
    }
}
