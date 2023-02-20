using System;
using System.Linq;

namespace Entities.Shared.Utilities
{
    public static class PropertyChecker
    {
        public static bool CheckIfPropertyExists<T>(string propertyName)
        {
            var type = typeof(T);
            var properties = type.GetProperties();
            return properties.Any(c => c.Name.Equals(propertyName, StringComparison.OrdinalIgnoreCase));
        }
    }
}
