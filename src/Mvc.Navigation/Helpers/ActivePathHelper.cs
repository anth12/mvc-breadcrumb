using System;

namespace Mvc.Navigation.Helpers
{
    internal class ActivePathHelper
    {
        internal bool IsActive(string itemPath, string currentPath)
        {
            if (!itemPath.EndsWith("/"))
                itemPath += "/";
            if (!currentPath.EndsWith("/"))
                currentPath += "/";

            return currentPath.StartsWith(itemPath, StringComparison.OrdinalIgnoreCase);
        }
    }
}
