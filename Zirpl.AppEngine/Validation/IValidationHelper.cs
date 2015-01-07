using System;
using System.Collections.Generic;

namespace Zirpl.AppEngine.Validation
{
    public interface IValidationHelper
    {
        ICollection<ValidationError> Validate(params Object[] objs);
        void AssertValid(params Object[] objs);
        bool IsValidatable(Object obj);
    }
}
