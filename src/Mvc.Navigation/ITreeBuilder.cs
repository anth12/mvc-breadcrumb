using System.Collections.Generic;

namespace Mvc.Navigation
{
    public interface ITreeBuilder
    {
        IEnumerable<TreeElement> GetElements();
    }
}
