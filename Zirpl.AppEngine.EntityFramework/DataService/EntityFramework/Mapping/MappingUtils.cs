using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq.Expressions;

namespace Zirpl.AppEngine.DataService.EntityFramework.Mapping
{
    public enum CascadeOnDeleteOption : byte
    {
        No = 0,
        Yes = 1
    }

    public static class MappingUtils
    {
        public static StringPropertyConfiguration IsRequired(this StringPropertyConfiguration config, bool required)
        {
            config = required ? config.IsRequired() : config.IsOptional();
            return config;
        }

        public static DateTimePropertyConfiguration IsRequired(this DateTimePropertyConfiguration config, bool required)
        {
            config = required ? config.IsRequired() : config.IsOptional();
            return config;
        }

        public static PrimitivePropertyConfiguration IsRequired(this PrimitivePropertyConfiguration config, bool required)
        {
            config = required ? config.IsRequired() : config.IsOptional();
            return config;
        }

        public static DecimalPropertyConfiguration IsRequired(this DecimalPropertyConfiguration config, bool required)
        {
            config = required ? config.IsRequired() : config.IsOptional();
            return config;
        }

        public static DecimalPropertyConfiguration IsCurrency(this DecimalPropertyConfiguration config)
        {
            config = config.HasPrecision(18,4);
            return config;
        }

        public static DateTimePropertyConfiguration IsDateTime(this DateTimePropertyConfiguration config)
        {
            config = config.HasColumnType("datetime2");
            return config;
        }

        public static StringPropertyConfiguration HasMaxLength(this StringPropertyConfiguration config, int maxLength, bool isMaxLength = false)
        {
            config = isMaxLength ? config.IsMaxLength() : config.HasMaxLength(maxLength);
            return config;
        }

        public static CascadableNavigationPropertyConfiguration HasNavigationProperty<T, TTargetEntity, TForeignKey>(this EntityTypeConfiguration<T> configuration, 
            Expression<Func<T, TTargetEntity>> navigationPropertyExpression, 
            Expression<Func<T, TForeignKey>> foreignKeyPropertyExpression, 
            bool isNavigationPropertyRequired,
            CascadeOnDeleteOption cascadeOnDelete,
            Expression<Func<TTargetEntity, ICollection<T>>> collectionNavigationPropertyExpression = null)
            where T : class
            where TTargetEntity : class
        {
            CascadableNavigationPropertyConfiguration returnValue = null;

            DependentNavigationPropertyConfiguration<T> collectionConfig = null;
            if (isNavigationPropertyRequired)
            {
                var requiredConfig = configuration.HasRequired(navigationPropertyExpression);
                collectionConfig = collectionNavigationPropertyExpression != null
                                       ? requiredConfig.WithMany(collectionNavigationPropertyExpression)
                                       : requiredConfig.WithMany();
            }
            else
            {
                var optionalConfig = configuration.HasOptional(navigationPropertyExpression);
                collectionConfig = collectionNavigationPropertyExpression != null
                                       ? optionalConfig.WithMany(collectionNavigationPropertyExpression)
                                       : optionalConfig.WithMany();
            }

            returnValue = collectionConfig.HasForeignKey(foreignKeyPropertyExpression);
            returnValue.WillCascadeOnDelete(cascadeOnDelete == CascadeOnDeleteOption.Yes);

            return returnValue;
        }
    }
}
