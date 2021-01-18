
namespace TextJustificationForms
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.InputText = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.WidthBox = new System.Windows.Forms.NumericUpDown();
            this.OutputText = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.WidthBox)).BeginInit();
            this.SuspendLayout();
            // 
            // InputText
            // 
            this.InputText.Location = new System.Drawing.Point(3, 12);
            this.InputText.Name = "InputText";
            this.InputText.Size = new System.Drawing.Size(387, 401);
            this.InputText.TabIndex = 1;
            this.InputText.Text = "";
            this.InputText.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(713, 419);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Justify";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // WidthBox
            // 
            this.WidthBox.Location = new System.Drawing.Point(12, 419);
            this.WidthBox.Name = "WidthBox";
            this.WidthBox.Size = new System.Drawing.Size(120, 20);
            this.WidthBox.TabIndex = 3;
            // 
            // OutputText
            // 
            this.OutputText.Location = new System.Drawing.Point(421, 12);
            this.OutputText.Name = "OutputText";
            this.OutputText.Size = new System.Drawing.Size(367, 401);
            this.OutputText.TabIndex = 4;
            this.OutputText.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.OutputText);
            this.Controls.Add(this.WidthBox);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.InputText);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.WidthBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.RichTextBox InputText;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RichTextBox OutputText;
        public System.Windows.Forms.NumericUpDown WidthBox;
    }
}

