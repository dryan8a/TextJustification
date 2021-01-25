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
            string justifiedText = JustifyText(InputText.Lines.ToList(),(int)WidthBox.Value);
            OutputText.Text = justifiedText;
        }
        static string JustifyText(List<string> lines, int width)
        {
            StringBuilder outputBuilder = new StringBuilder();
            for(int i = 0;i<lines.Count;i++)
            {
                var words = lines[i].Split(new []{' '},StringSplitOptions.RemoveEmptyEntries).ToList();
                while(lines[i].Length >= width)
                {
                    if(i == lines.Count - 1)
                    {
                        lines.Add(words[words.Count - 1]);
                    }
                    else
                    {
                        lines[i+1] = lines[i + 1].Insert(0,words[words.Count - 1] + " ");
                    }
                    lines[i] = lines[i].Remove(lines[i].LastIndexOf(words[words.Count - 1]));
                    words.RemoveAt(words.Count - 1);
                }
                int spacesToAdd = width - lines[i].Replace(" ", "").Length;
                
                foreach (string word in words)
                {
                    outputBuilder.Append(word);
                    outputBuilder.Append(" ");
                }
                outputBuilder.Append("\n");
            }
            
            return outputBuilder.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //temporary default values so I don't have to type it in every time
            InputText.Text = "aa aaa aaa aaa aa aa aaa aa aa a aa a aa aa aa a aaaaaaa aaaa aa aaa aaa aaa aa aaa aa a aa aa aaa aaa aaa aa aa aaa aa aa a aa a aa aa aa a aaaaaaa aaaa aa aaa aaa aaa aa aaa aa a aa aa aaa aaa aaa aa aa aaa aa aa a aa a aa aa aa a aaaaaaa aaaa aa aaa aaa aaa aa aaa aa a aa aa aaa aaa aaa aa aa aaa aa aa a aa a aa aa aa a aaaaaaa aaaa aa aaa aaa aaa aa aaa aa a aa";
            WidthBox.Value = 50;
        }
    }
}
