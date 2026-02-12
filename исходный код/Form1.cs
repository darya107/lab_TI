using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using static System.Net.Mime.MediaTypeNames;

namespace lab1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        public void proccesText()
        {
            string result;

            if (radioButton1.Checked)
            {
                string input = new string(
                textBox3.Text
                .Where(c => c >= 'A' && c <= 'Z' || c >= 'a' && c <= 'z')
                .Select(char.ToUpper)
                .ToArray()
                );

                string key = Cipher1.checkKey(textBox1.Text); 
                string key2 = Cipher1.checkKey(textBox4.Text);
                result = Cipher1.DoubleColumnEncrypt(input, key, key2);
                textBox2.Text = result;
            }

            if (radioButton2.Checked)
            {
                string input = new string(
                textBox3.Text
                .ToUpper()
                .Where(c => Cipher2.IsRussianLetter(c) || c == ' ')
                .ToArray()
                );


                string keyRu = Cipher2.CheckKeyRU(textBox1.Text);
                result = Cipher2.VigenereAutoKeyEncryptRU(input, keyRu);
                textBox2.Text = result;

            }

        }

        public void proccesText1()
        {
            string result;

            if (radioButton1.Checked)
            {
                string input = new string(
                textBox3.Text
                .Where(c => c >= 'A' && c <= 'Z' || c >= 'a' && c <= 'z')
                .Select(char.ToUpper)
                .ToArray()
                 );


                string key = Cipher1.checkKey(textBox1.Text);

                string key2 = Cipher1.checkKey(textBox4.Text);
                result = Cipher1.DoubleColumnDecrypt(input, key, key2);
                textBox2.Text = result;

            }

            if (radioButton2.Checked)
            {
              
                string input = new string(
                textBox3.Text
                .ToUpper()
                .Where(c => Cipher2.IsRussianLetter(c) || c == ' ')
                .ToArray()
                );

                string keyRu = Cipher2.CheckKeyRU(textBox1.Text);
                result = Cipher2.VigenereAutoKeyDecryptRU(input, keyRu);
                textBox2.Text = result;

            }

        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string path = openFileDialog1.FileName;
                string content = File.ReadAllText(path, Encoding.UTF8);
          
                textBox3.Text = content;

            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            foreach (Control c in this.Controls)
            {
                if (c is TextBox)
                    c.Text = "";
            }
        }

        private void buttonCode_Click(object sender, EventArgs e)
        {
            proccesText();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                textBox4.Visible = true;  
                label1.Visible = true;
                
            }
            else
            {
                textBox4.Visible = false;  
                label1.Visible = false;
            }
        }

        private void buttonDec_Click(object sender, EventArgs e)
        {
            proccesText1();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllText(
                        saveFileDialog1.FileName,
                        textBox2.Text,
                        Encoding.UTF8
                    );

                    MessageBox.Show("Файл сохранён.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка сохранения: " + ex.Message);
            }
        }
    }
}
