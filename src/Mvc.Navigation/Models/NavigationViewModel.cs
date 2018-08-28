using System.Collections.Generic;

namespace Mvc.Navigation.Models
{
    public class NavigationViewModel
    {
        public List<TreeElementViewModel> Root { get; set; } = new List<TreeElementViewModel>();
    }
}
