using Mvc.Navigation.Models;

namespace Mvc.Navigation
{
    public interface ISitemapProvider
    {
        SitemapViewModel GetSitemapModel();
    }
}