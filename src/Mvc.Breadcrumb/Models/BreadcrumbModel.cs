using System.Collections;
using System.Collections.Generic;

namespace Mvc.Breadcrumb.Models
{
    public class BreadcrumbModel : IEnumerable<MenuItem>
    {
        public List<MenuItem> Items { get; set; } = new List<MenuItem>();

        #region IEnumerable Implementation

        public IEnumerator<MenuItem> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

    }
}
