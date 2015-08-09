using System.Collections.Generic;

namespace GasWebMap.Services.Dtos
{
    public class TreeNode
    {
        public TreeNode()
        {
            children = new List<TreeNode>();
        }

        public int Order { get; set; }

        public object Data { get; set; }
        public string id { get; set; }
        public string text { get; set; }

        public bool @checked { get; set; }
        public IList<TreeNode> children { get; set; }
    }
}