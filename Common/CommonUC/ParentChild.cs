using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SOV.Common
{
    public class ParentChild
    {
        public static List<TreeNode> GetTreeNodes(List<IParent> items)
        {
            List<TreeNode> ret = new List<TreeNode>();
            //foreach (var item in items.Where(x => !x.GetParentId().HasValue))
            List<IParent> roots = items.Where(x => !items.Exists(y => y.GetId() == x.GetParentId())).OrderBy(x => x.GetName()).ToList();
            foreach (var item in roots)
            {
                TreeNode node = new TreeNode(item.GetName());
                node.Tag = item;
                AddChilds(node, items);
                ret.Add(node);
            }
            return ret;
        }

        private static void AddChilds(TreeNode nodeRoot, List<IParent> items)
        {
            foreach (var item in items.Where(x => x.GetParentId() == ((IParent)nodeRoot.Tag).GetId()).OrderBy(x=>x.GetName()))
            {
                TreeNode node = new TreeNode(item.GetName());
                node.Tag = item;
                AddChilds(node, items);

                nodeRoot.Nodes.Add(node);
            }
        }
    }
}
