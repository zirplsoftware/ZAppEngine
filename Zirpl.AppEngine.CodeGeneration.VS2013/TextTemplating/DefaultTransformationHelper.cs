using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TextTemplating;

namespace Zirpl.AppEngine.CodeGeneration.TextTemplating
{
    public class DefaultTransformationHelper :TransformationHelperBase
    {
        public DefaultTransformationHelper(TextTransformation callingTemplate)
            :base(callingTemplate)
        {
        }
    }
}
