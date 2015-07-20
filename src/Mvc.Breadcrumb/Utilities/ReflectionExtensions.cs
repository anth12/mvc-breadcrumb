using System;
using System.Linq;

namespace Mvc.Breadcrumb.Utilities
{
    internal static class ReflectionExtensions
    {
        internal static TAttribute GetAttribute<TAttribute>(this Type source) where TAttribute : class
        {
            var attributes = source.GetCustomAttributes(typeof(TAttribute), true);

            if (attributes.Any())
            {
                return attributes.First() as TAttribute;
            }

            return null;
        }
        
    }
}
