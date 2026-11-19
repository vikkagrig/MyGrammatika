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
    public partial class LR4 : Form
    {
        public LR4()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label1.Text = "";
            SourceText text = new SourceText();
            LexicalAnalyzer analyzer = new LexicalAnalyzer();
            text.Text = richTextBox1.Text;
            if (text.Check())
            {
                label1.Text = "Текст верен. ";
                string[] strings = richTextBox1.Text.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                int n = 1;
                foreach (string w in strings)
                {
                    SyntaxAnalyzer syntaxAnalyzer = new SyntaxAnalyzer(w);
                    if (syntaxAnalyzer.Flag)
                    {
                        TabularMethods tabular = new TabularMethods(w);
                        int i = tabular.getSymbol();
                        while (i != 0)
                        {
                            bool flag = true;
                            if(i == 1)
                            {
                                foreach(var item in listBox1.Items)
                                {
                                    if(item.ToString() == tabular.CurrentWord)
                                        flag = false;
                                }
                                if(flag) listBox1.Items.Add(tabular.CurrentWord);
                            }
                            else if(i == 2)
                            {
                                foreach (var item in listBox2.Items)
                                {
                                    if (item.ToString() == tabular.CurrentWord)
                                        flag = false;
                                }
                                if (flag) listBox2.Items.Add(tabular.CurrentWord);
                            }
                            else if(i == 3)
                            {
                                foreach (var item in listBox3.Items)
                                {
                                    if (item.ToString() == tabular.CurrentWord)
                                        flag = false;
                                }
                                if (flag) listBox3.Items.Add(tabular.CurrentWord);
                            }
                            i = tabular.getSymbol();
                        }
                    }
                    else
                    {
                        label1.Text += $"Ошибка синтаксиса в строке {n}. ";
                    }
                    n++;
                }
            }
            else
                label1.Text = "Символ не принадлежит алфавиту";
        }
    }
}
