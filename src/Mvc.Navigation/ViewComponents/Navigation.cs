using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Mvc.Navigation.ViewComponents
{
    [ViewComponent]
    public class Navigation : ViewComponent
    {
        private INavigationProvider provider;

        public Navigation(INavigationProvider provider)
        {
            this.provider = provider;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = provider.GetNavigationModel();

            return View("Bootstrap" ,model);
        }
    }
}
