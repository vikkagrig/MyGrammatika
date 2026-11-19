using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace LR1
{
    public class SyntaxAnalyzer
    {
        bool flag = true;
        public bool Flag
        {
            get => flag;
            set { flag = value; }
        }
        public SyntaxAnalyzer(string text)
        {
            string[] myWords = text.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            if(myWords.Length == 2)
            {
                flag = D(myWords[0]) && Z(myWords[1]);
            }
            else
            {
                flag = false;
            }
        }
        private bool D(string word)
        {
            if (word.Length >= 3 && word.Substring(0, 3) == "int")
            {
                return V(word.Substring(3));
            }
            else
            {
                return false;
            }
        }
        private bool V(string word)
        {
            bool res = I(word);
            if(!res)
            {
                for(int i = 1; i < word.Length; i++)
                {
                    res = I(word.Substring(0, i)) && X(word.Substring(i));
                    if (res)
                        break;
                }
            }
            return res;
        }
        private bool I(string word)
        {
            LexicalAnalyzer analyzer = new LexicalAnalyzer();
            if(word.IndexOf(":=") == -1)
            {
                analyzer.AnalyzWordBool(word);
                
            }
            string[] letters;
            if (analyzer.ClassWord != "Second word")  
            {
                try
                {
                    letters = word.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                    if (letters.Length != 2 || letters[1][0] != '=')
                    {
                        return false;
                    }
                    else
                    {
                        analyzer.AnalyzWordBool(letters[0]);
                        if (analyzer.ClassWord != "Second word")
                        {
                            return false;
                        }

                        return A(letters[1].Substring(1));
                    }
                }
                catch
                {
                    return false;
                }
            }
            return true;
        }
        private bool X(string word)
        {
            if (word[0] != ',')
            {
                return false;
            }
            if(!I(word.Substring(1)))
            {
                bool res = false;
                for (int i = 2; i < word.Length; i++)
                {
                    res = I(word.Substring(1, i - 1)) && X(word.Substring(i));
                    if (res)
                        break;
                }
                return res;
            }
            return true;
        }
        private bool Z(string word)
        {
            string[] letters;
            try
            {
                letters = word.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                if (letters.Length != 3 || letters[1] != "=") {
                    return false;
                }
            }
            catch
            {
                return false;
            }
            return A(letters[0]) && A(letters[2]);
        }
        private bool A(string word)
        {
            bool res = M(word);
            if (!res)
            {
                for (int i = 1; i < word.Length; i++)
                {
                    res = M(word.Substring(0, i)) && Y(word.Substring(i));
                    if (res)
                        break;
                }
            }
            return res;
        }
        private bool M(string word)
        {
            bool res = T(word);
            if (!res)
            {
                for (int i = 1; i < word.Length; i++)
                {
                    res = T(word.Substring(0, i)) && N(word.Substring(i));
                    if (res)
                        break;
                }
            }
            return res;
        }
        private bool N(string word)
        {
            if (word[0] != '*')
            {
                return false;
            }
            if (!T(word.Substring(1)))
            {
                bool res = false;
                for (int i = 2; i < word.Length; i++)
                {
                    res = T(word.Substring(1, i - 1)) && N(word.Substring(i));
                    if (res)
                        break;
                }
                return res;
            }
            return true;

        }
        private bool Y(string word)
        {
            if (word[0] != '+')
            {
                return false;
            }
            if (!M(word.Substring(1)))
            {
                bool res = false;
                for (int i = 2; i < word.Length; i++)
                {
                    res = M(word.Substring(1, i - 1)) && Y(word.Substring(i));
                    if (res)
                        break;
                }
                return res;
            }
            return true;
        }
        private bool T(string  word)
        {
            LexicalAnalyzer analyzer = new LexicalAnalyzer();
            analyzer.AnalyzWordBool(word);
            if (analyzer.ClassWord != "First word" && analyzer.ClassWord != "Second word")
            {
                try
                {
                    if (word[0] != '(' && word[word.Length - 1] != ')')
                    {
                        return false;
                    }
                    return A(word.Substring(1, word.Length - 2));
                }
                catch
                {
                    return false;
                }
            }
            return true;
        }

        public SyntaxTree Tree { get; private set; }
        public bool BuildTree(string text)
        {
            Tree = null;

            if (!Flag || string.IsNullOrWhiteSpace(text))
                return false;

            int separator = text.IndexOf(';');

            if (separator < 0) return false;
            string dec = text.Substring(0, separator);
            string comp = text.Substring(separator + 1);
            Node d = BuildD(dec);
            Node z = BuildZ(comp);
            if (d == null || z == null) return false;
            Tree = new SyntaxTree(new Node("S", "", d, z));
            return true;
        }
        private Node BuildD(string text)
        {
            if (!text.StartsWith("int"))
                return null;
            string variables = text.Substring(3);
            string[] parts = variables.Split(',');
            Node d = new Node("D", "int");
            Node v = new Node("V");
            foreach (string part in parts)
            {
                Node variable = BuildI(part);

                if (variable == null)
                    return null;

                v.Children.Add(variable);
            }
            d.Children.Add(v);
            return d;
        }
        private Node BuildI(string text)
        {
            int index = text.IndexOf(":=");
            string name = index >= 0 ? text.Substring(0, index) : text;
            LexicalAnalyzer lexer = new LexicalAnalyzer();
            if (!lexer.AnalyzWordBool(name) || lexer.ClassWord != "Second word")
                return null;
            Node i = new Node("I");
            i.Children.Add(new Node("Идентификатор", name));

            if (index >= 0)
            {
                string expression = text.Substring(index + 2);
                Node a = BuildA(expression);

                if (a == null)
                    return null;

                i.Children.Add(new Node(":="));
                i.Children.Add(a);
            }

            return i;
        }
        private Node BuildZ(string text)
        {
            int index = FindSkob(text, ":=:");

            if (index < 0)
                return null;

            Node left = BuildA(text.Substring(0, index));
            Node right = BuildA(text.Substring(index + 3));

            if (left == null || right == null)
                return null;

            return new Node("Z", ":=:", left, right);
        }
        private int FindSkob(string text, string operation)
        {
            int depth = 0;
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == '(')
                    depth++;
                else if (text[i] == ')')
                    depth--;
                if (depth < 0)
                    return -1;
                if (depth == 0 &&
                    i + operation.Length <= text.Length &&
                    text.Substring(i, operation.Length) == operation)
                    return i;
            }
            return -1;
        }

        private Node BuildA(string text)
        {
            if (string.IsNullOrEmpty(text))
                return null;

            int index = FindSkob(text, "+");

            if (index >= 0)
            {
                Node left = BuildM(text.Substring(0, index));
                Node right = BuildA(text.Substring(index + 1));

                if (left == null || right == null)
                    return null;

                return new Node("A", "+", left, right);
            }

            Node m = BuildM(text);

            if (m == null)
                return null;

            return new Node("A", "", m);
        }

        private Node BuildM(string text)
        {
            if (string.IsNullOrEmpty(text))
                return null;

            int index = FindSkob(text, "*");

            if (index >= 0)
            {
                Node left = BuildT(text.Substring(0, index));
                Node right = BuildM(text.Substring(index + 1));

                if (left == null || right == null)
                    return null;

                return new Node("M", "*", left, right);
            }

            Node t = BuildT(text);

            if (t == null)
                return null;

            return new Node("M", "", t);
        }
        private Node BuildT(string text)
        {
            if (string.IsNullOrEmpty(text))
                return null;

            LexicalAnalyzer lexer = new LexicalAnalyzer();

            if (lexer.AnalyzWordBool(text))
            {
                if (lexer.ClassWord == "First word")
                    return new Node("T", "",
                        new Node("Число", text));

                if (lexer.ClassWord == "Second word")
                    return new Node("T", "",
                        new Node("Идентификатор", text));
            }

            if (text.Length >= 2 &&
                text[0] == '(' &&
                text[text.Length - 1] == ')')
            {
                Node expression = BuildA(
                    text.Substring(1, text.Length - 2));

                if (expression == null)
                    return null;

                return new Node("T", "()", expression);
            }

            return null;
        }
        public List<string> DeclaredVariables { get; private set; }

        public bool CheckContext()
        {
            DeclaredVariables = new List<string>();

            if (Tree == null || Tree.Root == null)
            {
                return false;
            }

            HashSet<string> declared = new HashSet<string>();
            Node root = Tree.Root;
            if (root.Kind != "S" || root.Children.Count != 2)
            {
                return false;
            }

            Node d = root.Children[0];
            Node z = root.Children[1];

            if (d.Kind != "D" || z.Kind != "Z")
            {
                return false;
            }

            if (!CheckVars(d, declared) || !CheckExp(z, declared))
                return false;

            return true;
        }

        private bool CheckVars(Node d, HashSet<string> declared)
        {
            foreach (Node v in d.Children)
            {
                foreach (Node i in v.Children)
                {
                    if (i.Kind != "I" || i.Children.Count == 0 || i.Children[0].Kind != "Идентификатор")
                    {
                        return false;
                    }

                    string name = i.Children[0].Value;
                    if (declared.Add(name))
                        DeclaredVariables.Add(name);
                    foreach (Node child in i.Children)
                    {
                        if (child.Kind == "A")
                        {
                            if (!CheckExp(child, declared))
                                return false;
                        }
                    }
                }
            }
            return true;
        }

        private bool CheckExp(
            Node node, HashSet<string> declared)
        {
            if (node.Kind == "Идентификатор")
            {
                if (!declared.Contains(node.Value))
                {
                    return false;
                }

                return true;
            }

            if (node.Kind == "Число")
                return true;

            foreach (Node child in node.Children)
            {
                if (!CheckExp(child, declared))
                    return false;
            }

            return true;
        }


        private StringBuilder asm;
        private int tempCounter;

        public string GenAsm()
        {
            if (Tree == null || Tree.Root == null) return null;
            asm = new StringBuilder();
            tempCounter = 0;
            Node root = Tree.Root;

            if (root.Kind != "S" || root.Children.Count != 2)
                return null;

            Node d = root.Children[0];
            Node z = root.Children[1];

            if (z.Kind != "Z" || z.Children.Count != 2)
                return null;

            if (!GenVars(d)) return null;
            string left = GenExp(z.Children[0]);
            if (left == null)
                return null;
            string right = GenExp(z.Children[1]);
            if (right == null)
                return null;

            string newVar = NewTemp();
            asm.AppendLine($"MOV {newVar}, {left}");
            asm.AppendLine($"SUB {newVar}, {right}");
            asm.AppendLine($"JZ {newVar}, M1");
            asm.AppendLine("JMP M2");
            asm.AppendLine("M1:");
            asm.AppendLine("    JMP M3");
            asm.AppendLine("M2:");
            asm.AppendLine("M3:");
            return asm.ToString();
        }

        private bool GenVars(Node d)
        {
            if (d.Kind != "D") return false;

            foreach (Node v in d.Children)
            {
                foreach (Node i in v.Children)
                {
                    if (i.Kind != "I" || i.Children.Count == 0) return false;
                    string name = i.Children[0].Value;
                    foreach (Node child in i.Children)
                    {
                        if (child.Kind == "A")
                        {
                            string value = GenExp(child);
                            if (value == null)return false;
                            asm.AppendLine($"MOV {name}, {value}");
                        }
                    }
                }
            }
            return true;
        }
        private string GenExp(Node node)
        {
            if (node == null) return null;

            if (node.Kind == "Число" || node.Kind == "Идентификатор")
            {
                return node.Value;
            }

            if ((node.Kind == "A" && node.Value == "+") || (node.Kind == "M" && node.Value == "*"))
            {
                if (node.Children.Count != 2) return null;
                string left = GenExp(node.Children[0]);
                if (left == null) return null;
                string right = GenExp(node.Children[1]);
                if (right == null) return null;
                string result = NewTemp();
                asm.AppendLine($"MOV {result}, {left}");
                asm.AppendLine(node.Value == "+" ? $"ADD {result}, {right}" : $"MUL {result}, {right}");
                return result;
            }
            if (node.Children.Count == 1) return GenExp(node.Children[0]);

            return null;
        }

        private string NewTemp()
        {
            tempCounter++;
            return "R" + tempCounter;
        }

    }
}
