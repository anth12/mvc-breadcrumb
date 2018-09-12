using System;
using System.Collections.Generic;

namespace Mvc.Navigation
{
    public class TreeElement
    {
        internal Guid Id { get; } = Guid.NewGuid();

        public string Name { get; set; }
        public string Path { get; set; }
        public string CssClass { get; set; }

        public string[] RequiredRoles { get; set; }
        public bool AnonymousOnly { get; set; }
        public bool AuthenticatedOnly { get; set; }

        public TreeElement Parent { get; set; }
        public TreeElement[] Children { get; set; }

        public Dictionary<string, string> Properties { get; set; } = new Dictionary<string, string>();
    }
}
