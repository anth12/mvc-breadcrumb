using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Routing;
using Mvc.Breadcrumb.Attributes;
using Mvc.Breadcrumb.Models;
using Mvc.Breadcrumb.Utilities;

namespace Mvc.Breadcrumb
{
    public class BreadcrumbProvider
    {
        private static List<BreadcrumbAction> Items { get; } = new List<BreadcrumbAction>();

        public void CreateRegistry()
        {
            //Find all Controller implementations
            var controllerType = typeof(Controller);
            var controllerImplementations = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => controllerType.IsAssignableFrom(p));

            foreach (var controller in controllerImplementations)
            {
                var areaAttribute = controller.GetAttribute<AreaAttribute>();
                var areaName = areaAttribute?.RouteValue;

                var controllerName = controller.Name.Replace("Controller", "");

                var actionResultType = typeof(IActionResult);

                //TODO change first of group to find HTTPGet then first?
                foreach (var controllerAction in controller.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)
                    .Where(m => actionResultType.IsAssignableFrom(m.ReturnParameter?.ParameterType))
                    .GroupBy(m => m.Name)
                    .Select(g => g.First()))
                {
                    var key = GetRouteKey(areaName, controllerName, controllerAction.Name);

                    var routeAttribute = controllerAction.GetCustomAttribute<RouteAttribute>();
                    var parentAttribute = controllerAction.GetCustomAttribute<ParentAttribute>();

                    var parentKey = parentAttribute?.Key;

                    if (parentKey.IsNotBlank())
                    {
                        //If only the action has been specified, add the current controller to the key
                        if (parentKey.Split('.').Count() == 1)
                        {
                            parentKey = $"{controllerName}.{parentKey}";
                        }

                        //If the area has not been specified, use the current area- if defined
                        if (parentKey.Split('.').Count() == 2 && areaName.IsNotBlank())
                        {
                            parentKey = $"{areaName}.{parentKey}";
                        }
                    }

                    var breadcrumbAction = new BreadcrumbAction
                    {
                        Key = key,
                        Route = routeAttribute?.Template,
                        Title = GetPageTitle(controllerAction, controller),
                        ParentKey = parentKey
                    };

                    Items.Add(breadcrumbAction);
                }
            }

        }

        public BreadcrumbModel CreateBreadcrumb(Controller currentController, ActionExecutingContext filterContext)
        {
            var result = new BreadcrumbModel();

            var controllerType = currentController.GetType();
            var actionName = filterContext.RouteData.Values["action"].ToString();

            var routeKey = GetRouteKey(controllerType, actionName);

            //Add the current route
            var currrentAction = GetItemByKey(routeKey);

            if (currrentAction == null)
                return result;

            result.Items.Add(BreadcrumbActionToMenuItem(currrentAction, filterContext.RouteData, true));

            while (currrentAction?.ParentKey.IsNotBlank() ?? false)
            {
                currrentAction = GetItemByKey(currrentAction.ParentKey);

                //TODO improve, there is no need to go to the next loop
                if (currrentAction == null)
                    continue;

                var ancestorMenuItem = BreadcrumbActionToMenuItem(currrentAction, filterContext.RouteData, false);

                result.Items.Insert(0, ancestorMenuItem);
            }

            return result;
        }

        #region Private helper methods

        private MenuItem BreadcrumbActionToMenuItem(BreadcrumbAction action, RouteData routeData, bool active)
        {
            var displayText = action.Title;
            var url = action.Route;

            foreach (var key in routeData.Values.Keys)
            {
                var keyRegex = new Regex("{" + key + "(.*?)}");
                var value = routeData.Values[key].ToString();

                displayText = keyRegex.Replace(displayText, value);
                url = keyRegex.Replace(url, value);
            }

            return new MenuItem
            {
                Active = active,
                Url = "/" + url,
                DisplayText = displayText
            };
        }

        private BreadcrumbAction GetItemByKey(string key)
        {
            return Items.FirstOrDefault(i => i.Key == key);
        }

        private string GetRouteKey(Type controllerType, string actionName)
        {
            var controllerName = controllerType.Name.Replace("Controller", "");

            var routeKey = $"{controllerName}.{actionName}";

            var areaAttribute = controllerType.GetCustomAttribute<AreaAttribute>();
            if (areaAttribute != null)
                routeKey = $"{areaAttribute.RouteValue}.{routeKey}";

            return routeKey;
        }

        private string GetRouteKey(string areaName, string controllerName, string actionName)
        {
            var key = "";

            if (areaName.IsNotBlank())
                key += areaName + ".";
            if (controllerName.IsNotBlank())
                key += controllerName + ".";

            return key + actionName;
        }

        private string GetPageTitle(MethodInfo method, Type controller)
        {
            var titleAttribute = method.GetCustomAttribute<TitleAttribute>();

            if (titleAttribute != null)
                return titleAttribute.PageTitle;

            //If there is no title attribute, use the action name (if its not "Index")

            var actionName = method.Name;

            if (actionName != "Index")
                return actionName.ToDisplayText();

            //Rather than Index, use the controller name e.g. Account from AccountController rather than just Index
            return controller.Name.Replace("Controller", "").ToDisplayText();
        }

        public string GetPageTitle(Controller controller, ActionExecutingContext filterContext)
        {
            //Default Page title
            var pageTitle = filterContext.ActionDescriptor.Name.ToDisplayText();

            var actionValue = filterContext.RouteData.Values["action"];

            //Try to get the action name, default to Index
            var actionName = actionValue != null && actionValue.ToString().IsNotBlank()
                ? actionValue.ToString()
                : "Index";

            var controllerType = controller.GetType();
            var actionMember = controllerType.GetMethods().FirstOrDefault(m => m.Name == actionName);

            //Find any custom `TitleAttribute` values
            var titleAttributes = actionMember?.GetCustomAttributes(typeof(TitleAttribute), true);

            if (titleAttributes?.Any() ?? false)
            {
                var titleAttribute = (TitleAttribute)titleAttributes.First();
                if (titleAttribute != null)
                    pageTitle = titleAttribute.PageTitle;
            }
            else if (actionName == "Index")
            {
                //If there is no attribute and the default Index action 
                //is in use, use the Controller name as the page title
                pageTitle = controller.GetType().Name.Replace("Controller", "").ToDisplayText();
            }

            //Inject any route data into the page title
            foreach (var routeData in filterContext.RouteData.Values)
            {
                pageTitle = pageTitle.Replace("{" + routeData.Key + "}", routeData.Value.ToString());
            }

            return pageTitle;
        }

        #endregion

    }
}
