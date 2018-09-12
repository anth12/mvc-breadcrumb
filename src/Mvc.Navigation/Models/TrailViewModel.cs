using System.Collections.Generic;

namespace Mvc.Navigation.Models
{
    public class TrailViewModel
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public bool IsActive { get; set; }
        public string CssClass { get; set; }

        public Dictionary<string, string> Properties { get; set; } = new Dictionary<string, string>();
    }
}
