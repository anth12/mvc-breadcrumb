
namespace Mvc.Navigation
{
    public interface INavigationFilter
    {
        bool IsNodeHidden(TreeElement treeElement);
    }
}
