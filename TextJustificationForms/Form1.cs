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
            string justifiedText = TextJustifier.JustifyString(InputText.Text,(int)WidthBox.Value,maxLastLineSpacePad);
            OutputText.Text = justifiedText;
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
