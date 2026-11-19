using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LR1
{
    public class Node
    {
        public string Kind { get; set; }
        public string Value { get; set; }
        public List<Node> Children { get; set; }
        public Node(string kind, string value = "", params Node[] children)
        {
            Kind = kind; Value = value; Children = new List<Node>(children);
        }
        public override string ToString() { return Kind + (Value.Length > 0 ? ": " + Value : ""); }
    }
    public class SyntaxTree
    {
        public Node Root { get; set; }
        public SyntaxTree(Node root) { Root = root; }
        public string Print()
        {
            StringBuilder result = new StringBuilder();
            PrintNode(Root, result, 0);
            return result.ToString();
        }
        private void PrintNode(Node node, StringBuilder result, int level)
        {
            result.Append(new string(' ', level * 4));
            result.AppendLine(node.ToString());

            foreach (Node child in node.Children)
            {
                PrintNode(child, result, level + 1);
            }
        }
    }
}
