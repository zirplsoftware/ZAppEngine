using System;

namespace Zirpl.AppEngine.Validation
{
    public class EntityValidationError :ValidationError
    {
        public EntityValidationError()
        {
            
        }

        public EntityValidationError(String propertyName, String errorMessage, Object entity)
            :base(propertyName, errorMessage)
        {
            this.Entity = entity;
        }

        public Object Entity { get; set; }
    }
}
