using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LR1
{
    public class SourceText
    {
        private string text;
        private string[] splitText;
        private char[] alphabet = { '0', '1', 'a', 'b', 'c', 'd', ' ', '\n', '#' ,
            ';', ',', ':', '=', '+', '*', '(', ')', 'i', 'n', 't'};
        public string Text
        {
            get => text;
            set { this.text = value; }
        }
        public string[] Split
        {
            get => splitText;
            set { this.splitText = value; }
        }
        public bool Check()
        {
            for (int i = 0; i < text.Length; i++)
            {
                if (!CheckSymbol(text[i]))
                    return false;
            }
            return true;
        }
        private bool CheckSymbol(char cur)
        {
            foreach (char c in alphabet)
            {
                if (c == cur)
                {
                    return true;
                }
            }
            return false;
        }

        public void SplitText()
        {
            char[] finalText = new char[text.Length];
            int j = 0;
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == '#')
                {
                    while (text[i] != '\n' && i < text.Length)
                        i++;
                }
                finalText[j] = text[i];
                j++;
            }
            string result = new string(finalText);
            string[] myWords = result.Split(new char[] { ' ', '\n', '\0'}, StringSplitOptions.RemoveEmptyEntries);
            splitText = myWords;
            text = result;
        }
    }
}