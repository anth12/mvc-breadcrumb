using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.AspNetCore.Http;
using Mvc.Navigation.Models;

namespace Mvc.Navigation.Providers
{
    public class NavigationProvider
    {
        private readonly TreeIndex _index;
        private readonly IHttpContextAccessor _httpContext;

        public NavigationProvider(TreeIndex index, IHttpContextAccessor httpContext)
        {
            _index = index;
            _httpContext = httpContext;
        }

        public NavigationViewModel GetNavigationModel()
        {
            var request = _httpContext.HttpContext.Request.Path.Value.ToLower();
            
            IEnumerable<TreeElementViewModel> MapViewModel(IEnumerable<TreeElement> elements)
            {
                foreach (var element in elements)
                {
                    if (element.RequiredRoles?.Length > 0)
                    {
                        if (element.RequiredRoles.All(r => !Thread.CurrentPrincipal.IsInRole(r)))
                            continue;
                    }

                    var children = element.Children != null
                        ? MapViewModel(element.Children).ToArray()
                        : new TreeElementViewModel[0];

                    yield return new TreeElementViewModel
                    {
                        Name = element.Name,
                        Path = element.Path,

                        IsActive = request.StartsWith(element.Path, StringComparison.OrdinalIgnoreCase),
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
