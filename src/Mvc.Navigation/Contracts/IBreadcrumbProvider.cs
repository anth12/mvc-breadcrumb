using Mvc.Navigation.Models;

namespace Mvc.Navigation
{
    public interface IBreadcrumbProvider
    {
        BreadcrumbViewModel GetNavigationModel();
    }
}