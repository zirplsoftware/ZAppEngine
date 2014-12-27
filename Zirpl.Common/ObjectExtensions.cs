using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace Zirpl
{
    /// <summary>
    /// Extension methods for Objects
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Evaluates if <paramref name="obj" /> is null. If it is not, it is returned. If
        /// if it null, <paramref name="replacementExpression" /> is run and the result returned
        /// </summary>
        /// <typeparam name="T">The type of object</typeparam>
        /// <param name="obj">The object to be evaluated for null</param>
        /// <param name="replacementExpression">An expression that returns a T instance if <paramref name="obj"/> is null</param>
        /// <returns><paramref name="obj"/> if not null, otherwise, the result of the <paramref name="replacementExpression"/></returns>
        [SuppressMessage("ReSharper", "ConvertConditionalTernaryToNullCoalescing")]
        public static T Or<T>(this T obj, Expression<Func<T>> replacementExpression)
        {
            return obj != null ? obj : replacementExpression.Compile().Invoke();
        }

        [SuppressMessage("ReSharper", "ConvertConditionalTernaryToNullCoalescing")]
        public static T Or<T>(this T obj, T replacement)
        {
            return obj != null ? obj : replacement;
        }
    }
}
