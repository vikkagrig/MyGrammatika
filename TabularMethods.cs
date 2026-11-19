using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LR1
{
    public class TabularMethods
    {
        private string text;
        private string currentWord;
        public string Text
        {
            get => text;
            set { text = value; }
        }
        public string CurrentWord
        {
            get => currentWord;
            set { currentWord = value; }
        }
        public TabularMethods(string s) { 
            text = s;
        }
        public int getSymbol()
        {
            if (string.IsNullOrEmpty(text))
                return 0;

            char first = text[0];
            LexicalAnalyzer lexicalAnalyzer = new LexicalAnalyzer();
            lexicalAnalyzer.AnalyzLetter(first);

            string classSymbol = lexicalAnalyzer.ClassSymbol;

            if (classSymbol != "Letter" && classSymbol != "Number")
            {
                currentWord = first.ToString();
                text = text.Substring(1);
                return 3;
            }

            int i = 1;
            while (i < text.Length)
            {
                lexicalAnalyzer.AnalyzLetter(text[i]);
                if (lexicalAnalyzer.ClassSymbol != classSymbol)
                    break;
                i++;
            }

            currentWord = text.Substring(0, i);
            text = text.Substring(i);

            return classSymbol == "Letter" ? 2 : 1;
        }
    }
}
