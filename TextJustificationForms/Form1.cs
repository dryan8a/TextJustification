using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TextJustificationForms
{
    public partial class Form1 : Form
    {
        const int maxLastLineSpacePad = 2;
        public Form1()
        {
            InitializeComponent();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OutputText.Clear();
            string justifiedText = JustifyText(InputText.Text,(int)WidthBox.Value);
            OutputText.Text = justifiedText;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            OutputText.Clear();

        }
        static string JustifyText(string text, int width)
        {
            StringBuilder outputBuilder = new StringBuilder();
            List<string> lines = new List<string> { text };
            for(int lineIndex = 0;lineIndex<lines.Count;lineIndex++)
            {
                var words = lines[lineIndex].Split(new []{' ','\t'},StringSplitOptions.RemoveEmptyEntries).ToList();
                while(lines[lineIndex].Length >= width)
                {
                    if(words[words.Count-1].Contains('\n') && words[words.Count - 1][0] != '\n') //splits word at newline character, preserving the newline character in the rightmost word
                    {
                        string newLinedWord = words[words.Count - 1].Substring(words[words.Count - 1].LastIndexOf('\n'));
                        words[words.Count - 1] = words[words.Count - 1].Remove(words[words.Count - 1].LastIndexOf(newLinedWord));
                        words.Add(newLinedWord);
                        continue;
                    }
                    else if(lineIndex == lines.Count - 1)
                    {
                        lines.Add(words[words.Count - 1]);
                    }
                    else if(lines[lineIndex+1][0] == '\n')
                    {
                        lines.Insert(lineIndex + 1, words[words.Count - 1]);
                    }
                    else
                    { 
                        lines[lineIndex+1] = lines[lineIndex + 1].Insert(0,$"{words[words.Count - 1]} ");
                    }
                    lines[lineIndex] = lines[lineIndex].Remove(lines[lineIndex].LastIndexOf(words[words.Count - 1]));
                    words.RemoveAt(words.Count - 1);
                }

                int spacesToAdd = width - lines[lineIndex].Replace(" ", "").Length; //# of spaces that need to be inserted between words
                for (int wordIndex = 0; wordIndex < words.Count-1; wordIndex++)
                {
                    int spacesAfterWord = (int)Math.Ceiling((double)spacesToAdd / (words.Count - wordIndex - 1)); //how many spaces needed after the current word
                    spacesAfterWord = spacesAfterWord > maxLastLineSpacePad && lineIndex == lines.Count-1 ? maxLastLineSpacePad : spacesAfterWord; //removes gross over-spacing on the last line
                    outputBuilder.Append(words[wordIndex].PadRight(words[wordIndex].Length + spacesAfterWord)); //adds spaces after word and appends to string builder
                    spacesToAdd -= spacesAfterWord;
                }
                outputBuilder.Append($"{words[words.Count - 1]}\n"); //appends final word and newline character
            }
            
            return outputBuilder.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //temporary default values so I don't have to type it in every time
            InputText.Text = "I felt as if it was necessary to write actual sentences to test the justification, as filling the input text with various amounts of 'a' did not seem like the best way to test that.\nThis way I can also see that the code isn't purging parts of the input.";
            WidthBox.Value = 50;
            OutputText.Font = new Font("consolas", OutputText.Font.Size);
        }

       
    }
}
