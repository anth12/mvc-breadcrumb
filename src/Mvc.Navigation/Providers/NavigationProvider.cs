using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Mvc.Navigation.Helpers;
using Mvc.Navigation.Models;

namespace Mvc.Navigation.Providers
{
    internal class NavigationProvider : INavigationProvider
    {
        private readonly TreeIndex _index;
        private readonly ActivePathHelper _pathHelper;
        private readonly IEnumerable<INavigationFilter> _filters;
        private readonly IHttpContextAccessor _httpContext;

        public NavigationProvider(TreeIndex index, ActivePathHelper pathHelper, IEnumerable<INavigationFilter> filters, IHttpContextAccessor httpContext)
        {
            _index = index;
            _pathHelper = pathHelper;
            _filters = filters;
            _httpContext = httpContext;
        }

        public NavigationViewModel GetNavigationModel()
        {
            var requestPath = _pathHelper.Sanitise(_httpContext.HttpContext.Request.Path.Value);
            
            IEnumerable<TreeElementViewModel> MapViewModel(IEnumerable<TreeElement> elements)
            {
                var filteredElements = elements.Where(e => _filters.All(f => !f.IsNodeHidden(e)));

                foreach (var element in filteredElements)
                {
                    
                    var children = element.Children != null
                        ? MapViewModel(element.Children).ToArray()
                        : new TreeElementViewModel[0];

                    yield return new TreeElementViewModel
                    {
                        Name = element.Name,
                        Path = element.Path,
                        CssClass = element.CssClass,

                        IsActive = _pathHelper.IsActive(element.Path, requestPath),
                        IsChildActive = children.Any(c=> c.IsActive || c.IsChildActive),

                        Children = children
                    };
                }
            }

            var rootElements = MapViewModel(_index.RootElements);

            return new NavigationViewModel
            {
                Root = rootElements.ToList()
            };
        }

    }
}
