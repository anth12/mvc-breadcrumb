using Mvc.Navigation.Models;

namespace Mvc.Navigation
{
    public interface INavigationProvider
    {
        NavigationViewModel GetNavigationModel();
    }
}