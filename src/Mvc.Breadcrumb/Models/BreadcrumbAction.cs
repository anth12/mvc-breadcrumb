
namespace Mvc.Breadcrumb.Models
{
    internal sealed class BreadcrumbAction
    {
        /// <summary>
        /// Key to the Controller Action e.g. {Area?}.{Controller}.{Action}
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// MVC Route
        /// </summary>
        public string Route { get; set; }

        /// <summary>
        /// Page title
        /// </summary>
        public string Title { get; set; }

        public string ParentKey { get; set; }
    }
}
