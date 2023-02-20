using System;
using System.Linq.Expressions;

namespace Entities.Shared.Utilities
{
    public static class EntityExtensions
    {
        public static Func<TIn, TOut> CreatePropertyAccessor<TIn, TOut>(string propertyName)
        {
            var param = Expression.Parameter(typeof(TIn));
            var body = Expression.PropertyOrField(param, propertyName);
            return Expression.Lambda<Func<TIn, TOut>>(body, param).Compile();
        }
    }
}
