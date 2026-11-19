using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LR1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label4.Text = "";
            SourceText text = new SourceText();
            LexicalAnalyzer analyzer = new LexicalAnalyzer();
            text.Text = richTextBox1.Text;
            if (text.Check())
            {
                label1.Text = "Текст верен";
                string[] strings = richTextBox1.Text.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                int n = 1;
                foreach(string w in strings)
                {
                    SyntaxAnalyzer syntaxAnalyzer = new SyntaxAnalyzer(w);
                    if(syntaxAnalyzer.Flag)
                    {
                        label4.Text += $"Строка {n} верна. ";
                    }
                    else
                    {
                        label4.Text += $"Ошибка синтаксиса в строке {n}. ";
                    }
                    n++;
                }
                //text.SplitText();
                //foreach (string s in text.Split)
                //{
                //    label4.Text += analyzer.AnalyzWord(s);
                //}
                //foreach (char c in text.Text)
                //{
                //    label4.Text += analyzer.AnalyzLetter(c);
                //}
            }
            else
                label1.Text = "Символ не принадлежит алфавиту";
        }
    }
}
