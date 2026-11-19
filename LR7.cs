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
    public partial class LR7 : Form
    {
        public LR7()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label4.Text = "";
            string text = richTextBox1.Text;

            if (string.IsNullOrWhiteSpace(text))
            {
                MessageBox.Show("Пустой текст");
                return;
            }
            SourceText source = new SourceText();
            source.Text = text;

            if (!source.Check())
            {
                MessageBox.Show("Текст содержит символы, не входящие в алфавит");
                return;
            }
            StringBuilder cleaned = new StringBuilder();
            foreach (string line in text.Split('\n'))
            {
                string part = line.Split('#')[0];
                foreach (char c in part)
                {
                    if (!char.IsWhiteSpace(c)) cleaned.Append(c);
                }
            }
            text = cleaned.ToString();
            if (text.Length == 0)
            {
                MessageBox.Show("Пустой текст");
                return;
            }
            SyntaxAnalyzer analyzer = new SyntaxAnalyzer(text);
            if (!analyzer.Flag || !analyzer.BuildTree(text))
            {
                MessageBox.Show("Ошибка синтаксиса");
                return;
            }
            if (!analyzer.CheckContext())
            {
                MessageBox.Show("Контекстная ошибка");
                return;
            }

            string result = analyzer.GenAsm();
            if (string.IsNullOrEmpty(result))
            {
                MessageBox.Show("Ошибка синтаксиса");
                return;
            }
            label4.Text = result;
        }
    }
}
