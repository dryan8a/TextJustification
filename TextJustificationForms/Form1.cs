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
            string justifiedText = JustifyText(OutputText.Lines.ToList(),(int)WidthBox.Value);
            OutputText.Text = justifiedText;
        }
        static string JustifyText(List<string> text, int width)
        {
            int textStartIndex = 0;
            StringBuilder outputBuilder = new StringBuilder();
            foreach(string line in text)
            {
                
            }
            
            return outputBuilder.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }
    }
}
