
namespace Mvc.Breadcrumb.Models
{
    /// <summary>
    /// Simple breadcrumb UI data model
    /// </summary>
    public class MenuItem
    {
        #region Constructors 

        public MenuItem()
        {

        }

        public MenuItem(string displayText, string url)
        {
            DisplayText = displayText;
            Url = url;
        }

        public MenuItem(string displayText, string url, bool active)
        {
            DisplayText = displayText;
            Url = url;
            Active = active;
        }

        #endregion

        /// <summary>
        /// Display text of the breadcrumb item
        /// </summary>
        public string DisplayText { get; set; }

        /// <summary>
        /// URL link to the breadcrumb item
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Is the item active?
        /// </summary>
        public bool Active { get; set; }
    }
}
