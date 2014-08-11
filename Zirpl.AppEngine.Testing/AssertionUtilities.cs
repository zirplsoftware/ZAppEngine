using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Zirpl.AppEngine.Model;

namespace Zirpl.AppEngine.Testing
{
    public static class AssertionUtils
    {
        public static void AssertNavigationPropertyMatchesAndIsNotNull<TEntity, TNavigationEntity, TForeignKey>(
            this TEntity entityFromDb,
            TEntity entity,
            Func<TEntity, TNavigationEntity> navigationPropertyFunction,
            Func<TEntity, TForeignKey> foreignKeyPropertyFunction)
            where TNavigationEntity : IPersistable
        {
            navigationPropertyFunction(entityFromDb).Should().NotBeNull();
            navigationPropertyFunction(entityFromDb).Should().Be(navigationPropertyFunction(entity));
            // no need to check the properties further since they are the EXACT same object
            foreignKeyPropertyFunction(entityFromDb).Should().Be(navigationPropertyFunction(entityFromDb).GetId());
            foreignKeyPropertyFunction(entityFromDb).Should().Be(foreignKeyPropertyFunction(entity));

            navigationPropertyFunction(entity).IsPersisted.Should().BeTrue();
            navigationPropertyFunction(entityFromDb).IsPersisted.Should().BeTrue();
        }
        public static void AssertNavigationPropertyIsNull<TEntity, TNavigationEntity, TForeignKey>(
            this TEntity entityFromDb,
            TEntity entity,
            Func<TEntity, TNavigationEntity> navigationPropertyFunction,
            Func<TEntity, TForeignKey> foreignKeyPropertyFunction,
            TForeignKey expectedValuefOForeignKey)
            where TNavigationEntity : IPersistable
        {
            navigationPropertyFunction(entity).Should().BeNull();
            navigationPropertyFunction(entityFromDb).Should().BeNull();

            foreignKeyPropertyFunction(entity).Should().Be(expectedValuefOForeignKey);
            foreignKeyPropertyFunction(entityFromDb).Should().Be(expectedValuefOForeignKey);
        }
    }
}
