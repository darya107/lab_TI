namespace labSecond
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            textBox1 = new TextBox();
            label1 = new Label();
            textBoxInput = new TextBox();
            textBoxOutput = new TextBox();
            label2 = new Label();
            label3 = new Label();
            button1 = new Button();
            textBoxKey = new TextBox();
            label4 = new Label();
            label5 = new Label();
            buttonOpenF = new Button();
            buttonWriteInF = new Button();
            button2 = new Button();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.Location = new Point(27, 40);
            textBox1.MaxLength = 34;
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(428, 27);
            textBox1.TabIndex = 0;
            textBox1.TextChanged += textBox1_TextChanged;
            textBox1.KeyPress += textBox1_KeyPress;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(27, 0);
            label1.Name = "label1";
            label1.Size = new Size(140, 37);
            label1.TabIndex = 1;
            label1.Text = "ключ(34)";
            // 
            // textBoxInput
            // 
            textBoxInput.Location = new Point(12, 136);
            textBoxInput.Multiline = true;
            textBoxInput.Name = "textBoxInput";
            textBoxInput.ScrollBars = ScrollBars.Both;
            textBoxInput.Size = new Size(305, 477);
            textBoxInput.TabIndex = 2;
            textBoxInput.TextChanged += textBoxInput_TextChanged;
            // 
            // textBoxOutput
            // 
            textBoxOutput.Location = new Point(730, 136);
            textBoxOutput.Multiline = true;
            textBoxOutput.Name = "textBoxOutput";
            textBoxOutput.ScrollBars = ScrollBars.Both;
            textBoxOutput.Size = new Size(310, 477);
            textBoxOutput.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(12, 102);
            label2.Name = "label2";
            label2.Size = new Size(189, 31);
            label2.TabIndex = 4;
            label2.Text = "исходный текст";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(730, 102);
            label3.Name = "label3";
            label3.Size = new Size(265, 31);
            label3.TabIndex = 5;
            label3.Text = "зашифрованный текст";
            // 
            // button1
            // 
            button1.BackColor = Color.HotPink;
            button1.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button1.Location = new Point(526, 22);
            button1.Name = "button1";
            button1.Size = new Size(183, 52);
            button1.TabIndex = 6;
            button1.Text = "шифровать";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // textBoxKey
            // 
            textBoxKey.Location = new Point(376, 136);
            textBoxKey.Multiline = true;
            textBoxKey.Name = "textBoxKey";
            textBoxKey.ScrollBars = ScrollBars.Both;
            textBoxKey.Size = new Size(313, 477);
            textBoxKey.TabIndex = 7;
            textBoxKey.TextChanged += textBoxKey_TextChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(376, 102);
            label4.Name = "label4";
            label4.Size = new Size(256, 31);
            label4.TabIndex = 8;
            label4.Text = "сгенерируемый ключ";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(470, 43);
            label5.Name = "label5";
            label5.Size = new Size(31, 20);
            label5.TabIndex = 9;
            label5.Text = "0/0";
            // 
            // buttonOpenF
            // 
            buttonOpenF.Location = new Point(715, 22);
            buttonOpenF.Name = "buttonOpenF";
            buttonOpenF.Size = new Size(148, 52);
            buttonOpenF.TabIndex = 10;
            buttonOpenF.Text = "открыть файл";
            buttonOpenF.UseVisualStyleBackColor = true;
            buttonOpenF.Click += buttonOpenF_Click;
            // 
            // buttonWriteInF
            // 
            buttonWriteInF.Location = new Point(869, 22);
            buttonWriteInF.Name = "buttonWriteInF";
            buttonWriteInF.Size = new Size(148, 52);
            buttonWriteInF.TabIndex = 11;
            buttonWriteInF.Text = "запись в файл";
            buttonWriteInF.UseVisualStyleBackColor = true;
            buttonWriteInF.Click += buttonWriteInF_Click;
            // 
            // button2
            // 
            button2.Location = new Point(1023, 22);
            button2.Name = "button2";
            button2.Size = new Size(132, 52);
            button2.TabIndex = 12;
            button2.Text = "очистить";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Plum;
            ClientSize = new Size(1158, 625);
            Controls.Add(button2);
            Controls.Add(buttonWriteInF);
            Controls.Add(buttonOpenF);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(textBoxKey);
            Controls.Add(button1);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(textBoxOutput);
            Controls.Add(textBoxInput);
            Controls.Add(label1);
            Controls.Add(textBox1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MainForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBox1;
        private Label label1;
        private TextBox textBoxInput;
        private TextBox textBoxOutput;
        private Label label2;
        private Label label3;
        private Button button1;
        private TextBox textBoxKey;
        private Label label4;
        private Label label5;
        private Button buttonOpenF;
        private Button buttonWriteInF;
        private Button button2;
    }
}
