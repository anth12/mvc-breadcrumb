using System;
using Mvc.Breadcrumb.Utilities;

namespace Mvc.Breadcrumb.Attributes
{
    /// <summary>
    /// Defines the Parent Action
    /// </summary>
    public class ParentAttribute : Attribute
    {
        #region Constructors

        public ParentAttribute(
            string area = null,
            string controller = null,
            string action = null
            )
        {
            Area = area;
            Controller = controller;
            Action = action;
        }

        #endregion

        public string Area { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }

        public string Key
        {
            get
            {
                var key = "";

                if (Area.IsNotBlank())
                    key += Area + ".";

                if (Controller.IsNotBlank())
                    key += Controller + ".";

                return key + Action;
            }
        }
    }
}
