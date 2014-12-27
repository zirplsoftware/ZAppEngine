using System;
using FluentValidation;
using Zirpl.AppEngine.Model;

namespace Zirpl.AppEngine.Validation
{
    public static class AbstractValidatorExtensions
    {
        public static void ForeignEntityNotNullAndIdMatches<TEntity, TForeignEntity, TForeignEntityId>(
            this AbstractValidator<TEntity> validator,
            Func<TEntity, TForeignEntity> foreignEntityExpression,
            Func<TEntity, TForeignEntityId> foreignEntityIdExpression,
            String foreignEntityPropertyName,
            String foreignEntityIdPropertyName)
            where TForeignEntity : class, IPersistable<TForeignEntityId>
        {
            validator.RuleFor(o => foreignEntityExpression(o)).NotEmpty().WithName(foreignEntityPropertyName).OverridePropertyName(foreignEntityIdPropertyName).WithMessage(foreignEntityPropertyName + " required");
            validator.When(o => foreignEntityExpression(o) != null
                && foreignEntityExpression(o).IsPersisted, () =>
                {
                    validator.RuleFor(o => foreignEntityIdExpression(o)).NotEmpty().WithName(foreignEntityIdPropertyName).WithMessage(foreignEntityIdPropertyName + " required");
                });
            validator.When(o => foreignEntityExpression(o) != null
                                && foreignEntityIdExpression(o) != null, () =>
                                {

                                    validator.RuleFor(o => foreignEntityIdExpression(o))
                                                .Must((entity, foreignEntityId) =>
                                                {
                                                    TForeignEntity foreignEntity = foreignEntityExpression(entity);
                                                    return foreignEntity != null
                                                            && foreignEntityId != null
                                                           && foreignEntity.Id.Equals(foreignEntityId);
                                                })
                                                .WithName(foreignEntityIdPropertyName)
                                                .WithMessage(foreignEntityPropertyName + ".Id does not match " + foreignEntityIdPropertyName);
                                });
        }

        public static void ForeignEntityNotNullAndIdMatches<TEntity, TForeignEntity, TForeignEntityId>(
            this AbstractValidator<TEntity> validator,
            Func<TEntity, TForeignEntity> foreignEntityExpression,
            Func<TEntity, TForeignEntityId?> foreignEntityIdExpression,
            String foreignEntityPropertyName,
            String foreignEntityIdPropertyName)
            where TForeignEntity : class, IPersistable<TForeignEntityId>
            where TForeignEntityId : struct
        {
            validator.RuleFor(o => foreignEntityExpression(o)).NotEmpty().WithName(foreignEntityPropertyName).OverridePropertyName(foreignEntityIdPropertyName).WithMessage(foreignEntityPropertyName + " required");
            validator.When(o => foreignEntityExpression(o) != null
                && foreignEntityExpression(o).IsPersisted, () =>
                {
                    validator.RuleFor(o => foreignEntityIdExpression(o)).NotEmpty().WithName(foreignEntityIdPropertyName).WithMessage(foreignEntityIdPropertyName + " required");
                });
            validator.When(o => foreignEntityExpression(o) != null
                                && foreignEntityIdExpression(o) != null, () =>

                                    validator.RuleFor(o => foreignEntityIdExpression(o))
                                                .Must((entity, foreignEntityId) =>
                                                {
                                                    TForeignEntity foreignEntity = foreignEntityExpression(entity);
                                                    return foreignEntity != null
                                                            && foreignEntityId.HasValue
                                                           && foreignEntity.Id.Equals(foreignEntityId.Value);
                                                })
                                                .WithName(foreignEntityIdPropertyName)
                                                .WithMessage(foreignEntityPropertyName + ".Id does not match " + foreignEntityIdPropertyName)
                                );
        }

        public static void ForeignEntityAndIdMatchIfNotNull<TEntity, TForeignEntity, TForeignEntityId>(
            this AbstractValidator<TEntity> validator,
            Func<TEntity, TForeignEntity> foreignEntityExpression,
            Func<TEntity, TForeignEntityId> foreignEntityIdExpression,
            String foreignEntityPropertyName,
            String foreignEntityIdPropertyName)
            where TForeignEntity : class, IPersistable<TForeignEntityId>
        {
            validator.When(o => foreignEntityExpression(o) != null
                                || foreignEntityIdExpression(o) != null, () =>

                                    validator.RuleFor(o => foreignEntityIdExpression(o))
                                                .Must((entity, foreignEntityId) =>
                                                {
                                                    TForeignEntity foreignEntity = foreignEntityExpression(entity);
                                                    return (foreignEntity == null && foreignEntityId.IsNullId())
                                                            || (foreignEntity != null
                                                                && foreignEntityId != null
                                                                && foreignEntity.Id.Equals(foreignEntityId));
                                                })
                                                .WithName(foreignEntityIdPropertyName)
                                                .WithMessage(foreignEntityPropertyName + ".Id does not match " + foreignEntityIdPropertyName)
                                );
        }

        public static void ForeignEntityAndIdMatchIfNotNull<TEntity, TForeignEntity, TForeignEntityId>(
           this AbstractValidator<TEntity> validator,
           Func<TEntity, TForeignEntity> foreignEntityExpression,
           Func<TEntity, TForeignEntityId?> foreignEntityIdExpression,
           String foreignEntityPropertyName,
           String foreignEntityIdPropertyName)
            where TForeignEntity : class, IPersistable<TForeignEntityId>
            where TForeignEntityId : struct
        {
            validator.When(o => foreignEntityExpression(o) != null
                                || foreignEntityIdExpression(o) != null, () =>

                                    validator.RuleFor(o => foreignEntityIdExpression(o))
                                                .Must((entity, foreignEntityId) =>
                                                {
                                                    TForeignEntity foreignEntity = foreignEntityExpression(entity);
                                                    return (foreignEntity == null
                                                                && foreignEntityId == null)
                                                            || (foreignEntity != null
                                                                && foreignEntityId != null
                                                                && foreignEntity.Id.Equals(foreignEntityId));
                                                })
                                                .WithName(foreignEntityIdPropertyName)
                                                .WithMessage(foreignEntityPropertyName + ".Id does not match " + foreignEntityIdPropertyName)
                                );
        }


    }
}
