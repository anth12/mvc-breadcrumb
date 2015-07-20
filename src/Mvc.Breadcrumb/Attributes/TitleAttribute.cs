using System;

namespace Mvc.Breadcrumb.Attributes
{
    /// <summary>
    /// Defines the current Action's Page Title
    /// </summary>
    public class TitleAttribute : Attribute
    {
        public TitleAttribute(string pageTitle)
        {
            PageTitle = pageTitle;
        }

        public string PageTitle { get; set; }
    }
}
