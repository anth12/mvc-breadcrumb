using System;

namespace Mvc.Navigation.Helpers
{
    internal class ActivePathHelper
    {
        internal bool IsActive(string itemPath, string currentPath)
        {

            return currentPath.Equals(Sanitise(itemPath), StringComparison.OrdinalIgnoreCase);
        }

        internal string Sanitise(string path)
        {
            if (path.StartsWith("/"))
                path = path.Substring(1, path.Length - 1);

            if (path.EndsWith("/"))
                path = path.Substring(0, path.Length - 1);

            return path.ToLower();
        }
    }
}
