using System.Collections.Generic;

namespace Mvc.Navigation
{
    public class TreeIndex
    {
        public Dictionary<string, TreeElement> FlatItems { get; } = new Dictionary<string, TreeElement>();

        public HashSet<TreeElement> RootElements { get; } = new HashSet<TreeElement>();

        public void Build(IEnumerable<TreeElement> treeElements)
        {
            RootElements.Clear();

            void AddToIndex(TreeElement treeElement)
            {
                if (!string.IsNullOrEmpty(treeElement.Path))
                {
                    FlatItems[treeElement.Path.ToLower().Trim('/')] = treeElement;
                }


                if (treeElement.Children != null)
                {
                    foreach (var child in treeElement.Children)
                    {
                        child.Parent = treeElement;
                        AddToIndex(child);
                    }
                }
            }

            foreach (var treeElement in treeElements)
            {
                RootElements.Add(treeElement);
                AddToIndex(treeElement);
            }
        }

        //public TreeElement Search(TreePath path)
        //{
        //    foreach (var treePath in path.Paths)
        //    {
                
        //    }
        //}
    }

    internal struct TreePath
    {
        public int[] Paths { get; set; }
    }
}
