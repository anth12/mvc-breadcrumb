
namespace Mvc.Navigation.Models
{
    public class TreeElementViewModel
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public bool IsActive { get; set; }
        public bool IsChildActive { get; set; }

        public TreeElementViewModel[] Children { get; set; }
    }
}
