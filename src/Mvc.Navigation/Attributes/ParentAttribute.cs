using System;
using System.Collections.Generic;
using System.Text;

namespace Mvc.Navigation.Attributes
{
    public class ParentAttribute : Attribute
    {
        public ParentAttribute(string action, string controller = null, string area = null)
        {
            Action = action;
            Controller = controller;
            Area = area;
        }

        public string Action { get; }
        public string Controller { get; }
        public string Area { get; }
    }
}
