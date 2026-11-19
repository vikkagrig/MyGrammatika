using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Forms;

namespace LR1
{
    public class LexicalAnalyzer
    {
        string classSymbol;
        char symbol;
        string word;
        private string classWord;
        public string ClassWord
        {
            get => classWord;
            set { classWord = value; }
        }
        public string ClassSymbol
        {
            get => classSymbol;
            set { classSymbol = value; }
        }
        public string AnalyzLetter(char c)
        {
            symbol = c;
            if (symbol == 'a' || symbol == 'b' || symbol == 'c' || symbol == 'd')
            {
                classSymbol = "Letter";
            }
            else if (symbol == '0' || symbol == '1')
            {
                classSymbol = "Number";
            }
            else if (symbol == ' ' || symbol == '\n')
            {
                classSymbol = "Whitespace character";
                if (symbol == '\n') symbol = ' ';
            }
            else if (symbol == '#')
            {
                classSymbol = "Comment";
            }
            else if (symbol == ';' || symbol == ',' || symbol == ':' || symbol == '=' ||
                symbol == '+' || symbol == '*' || symbol == '(' || symbol == ')')
            {
                classSymbol = "Special character";
            }
            else
                classSymbol = "Not recognized";
            return $"({classSymbol} {symbol}) ";
        }

        public bool AnalyzWordBool(string word)
        {
            char c1, c2, c3;
            int takt = 1;
            try
            {
                if (word[0] == '0' || word[0] == '1')
                    classWord = "Number";

                else if (word[0] == 'a' && word[1] == 'c') classWord = "Letter";
                else
                {
                    return false;
                }
                foreach (char n in word)
                {
                    AnalyzLetter(n);
                    if (classSymbol != classWord)
                    {
                        return false;
                    }
                    if (classSymbol == "Number" && word.Length % 3 != 0)
                    {
                        return false;
                    }
                }
                if (classSymbol == "Number")
                {
                    for (int i = 0; i < word.Length; i += 3)
                    {
                        c1 = word[i];
                        c2 = word[i + 1];
                        c3 = word[i + 2];
                        if (takt == 1)
                        {
                            if (c1 == c2 && c2 == c3 && c1 == '0')
                            {
                                takt = 1;
                            }
                            else if (c1 == '1' && c2 == c3 && c2 == '0')
                            {
                                takt = 2;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        if (takt == 2)
                        {
                            if (c1 == '1' && c2 == c3 && c2 == '0')
                            {
                                takt++;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else if (takt == 3)
                        {
                            if (!(c1 == '0' && c2 == '1' && c3 == '0'))
                            {
                                return false;
                            }
                        }
                    }
                    if (takt == 1)
                    {
                        return false;
                    }
                }
                if (classWord == "Number") classWord = "First word";
                if (classWord == "Letter") classWord = "Second word";
                return true;
            }
            catch
            {
                return false;
            }
        }

        public string AnalyzWord(string word)
        {
            char c1, c2, c3;
            int takt = 1;
            if (word[0] == '0' || word[0] == '1')
                classWord = "Number";

            else if (word[0] == 'a' && word[1] == 'c') classWord = "Letter";
            else
            {
                MessageBox.Show($"Ошибка. Слово {word} введено неверно");
                return "";
            }
            foreach (char n in word)
            {
                AnalyzLetter(n);
                if(classSymbol != classWord)
                {
                    MessageBox.Show($"Ошибка. Слово {word} введено неверно");
                    return "";
                }
                if(classSymbol == "Number" && word.Length % 3 != 0)
                {
                    MessageBox.Show($"Ошибка. Слово {word} введено неверно");
                    return "";
                }
            }
            if(classSymbol == "Number") { 
                for (int i = 0; i < word.Length; i += 3)
                {
                    c1 = word[i];
                    c2 = word[i + 1];
                    c3 = word[i + 2];
                    if (takt == 1)
                    {
                        if (c1 == c2 && c2 == c3 && c1 == '0')
                        {
                            takt = 1;
                        }
                        else if (c1 == '1' && c2 == c3 && c2 == '0')
                        {
                            takt = 2;
                        }
                        else
                        {
                            MessageBox.Show($"Ошибка. Слово {word} введено неверно");
                            return "";
                        }
                    }
                    if (takt == 2)
                    {
                        if (c1 == '1' && c2 == c3 && c2 == '0')
                        {
                            takt++;
                        }
                        else
                        {
                            MessageBox.Show($"Ошибка. Слово {word} введено неверно");
                            return "";
                        }
                    }
                    else if (takt == 3)
                    {
                        if (!(c1 == '0' && c2 == '1' && c3 == '0'))
                        {
                            MessageBox.Show($"Ошибка. Слово {word} введено неверно");
                            return "";
                        }
                    }
                }
                if (takt == 1)
                {
                    MessageBox.Show($"Ошибка. Слово {word} введено неверно");
                    return "";
                }
            }
            if (classWord == "Number") classWord = "First word";
            if (classWord == "Letter") classWord = "Second word";
            return $"({classWord} {word}) ";
        }
    }
}
