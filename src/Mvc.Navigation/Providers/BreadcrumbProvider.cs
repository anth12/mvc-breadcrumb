using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Mvc.Navigation.Helpers;
using Mvc.Navigation.Models;

namespace Mvc.Navigation.Providers
{
    internal class BreadcrumbProvider : IBreadcrumbProvider
    {
        private readonly TreeIndex _index;
        private readonly ActivePathHelper _pathHelper;
        private readonly IHttpContextAccessor _httpContext;

        public BreadcrumbProvider(TreeIndex index, ActivePathHelper pathHelper, IHttpContextAccessor httpContext)
        {
            _index = index;
            _pathHelper = pathHelper;
            _httpContext = httpContext;
        }

        public BreadcrumbViewModel GetNavigationModel()
        {
            var requestPath = _httpContext.HttpContext.Request.Path.Value.ToLower().Trim('/');

            TrailViewModel MapViewModel(TreeElement element)
            {               
                return new TrailViewModel
                {
                    Name = element.Name,
                    Path = element.Path,
                    CssClass = element.CssClass,

                    IsActive = _pathHelper.IsActive(element.Path, requestPath)
                };
                
            }

            var trail = new List<TrailViewModel>();

            if (_index.FlatItems.ContainsKey(requestPath))
            {
                var currentPage = _index.FlatItems[requestPath];

                trail.Add(MapViewModel(currentPage));

                var parent = currentPage.Parent;

                while (parent != null)
                {
                    trail.Insert(0, MapViewModel(parent));
                    parent = parent.Parent;
                }
            }

            return new BreadcrumbViewModel()
            {
                Trail = trail
            };
        }
        
    }
}
