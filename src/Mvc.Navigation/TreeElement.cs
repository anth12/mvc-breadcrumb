using System;

namespace Mvc.Navigation
{
    public class TreeElement
    {
        internal Guid Id { get; } = Guid.NewGuid();

        public string Name { get; set; }
        public string Path { get; set; }

        public string[] RequiredRoles { get; set; }

        public TreeElement Parent { get; set; }
        public TreeElement[] Children { get; set; }
    }
}
