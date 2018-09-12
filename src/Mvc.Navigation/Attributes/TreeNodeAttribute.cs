using System;
using System.Collections.Generic;
using System.Linq;

namespace Mvc.Navigation.Attributes
{
    public class TreeNodeAttribute : Attribute
    {
        public TreeNodeAttribute(string name, string parentAction = null, string parentController = null, string parentArea = null)
        {
            Name = name;
            ParentAction = parentAction;
            ParentController = parentController;
            ParentArea = parentArea;
        }

        public TreeNodeAttribute(string name, string parentAction = null, string parentController = null, string parentArea = null, params (string key, string value)[] properties)
        {
            Name = name;
            ParentAction = parentAction;
            ParentController = parentController;
            ParentArea = parentArea;
            Properties = properties?.ToDictionary(x => x.key, x => x.value);
        }

        public string Name { get; }
        public string ParentAction { get; }
        public string ParentController { get; }
        public string ParentArea { get; }

        public Dictionary<string, string> Properties { get; set; }
    }
}
