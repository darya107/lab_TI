using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab3
{
    public partial class MainAlgorithm : Form
    {
        
        string inputFile = "";
        string outputFile = "";
        public MainAlgorithm()
        {
            InitializeComponent();
        }

        void PrintPreview(List<long> data)
        {
            const int LIMIT = 50;
            textBoxResult.Clear();

            int n = data.Count;

            if (n <= LIMIT * 2)
            {
                foreach (var x in data)
                    textBoxResult.AppendText(x + " ");
                return;
            }

            for (int i = 0; i < LIMIT; i++)
                textBoxResult.AppendText(data[i] + " ");

            textBoxResult.AppendText("... ");

            for (int i = n - LIMIT; i < n; i++)
                textBoxResult.AppendText(data[i] + " ");
        }


        //быстрое возведение в ст
        long ModPow(long a, long e, long mod)
        {
            long res = 1;
            a %= mod;

            while (e > 0)
            {
                if ((e & 1) == 1)
                    res = (res * a) % mod;

                a = (a * a) % mod;
                e >>= 1;
            }

            return res;
        }

        //расширенный евклида
        long ModInverse(long a, long n)
        {
            long t = 0, newt = 1;
            long r = n, newr = a;

            while (newr != 0)
            {
                long q = r / newr;
                (t, newt) = (newt, t - q * newt);
                (r, newr) = (newr, r - q * newr);
            }

            if (t < 0) t += n;
            return t;
        }


       //проверка на простоту
        bool IsPrime(int x)
        {
            if (x < 2) return false;
            for (int i = 2; i * i <= x; i++)
                if (x % i == 0) return false;
            return true;
        }

        void Encrypt(string input, string output, long p, long q, long b)
        {
            long n = p * q;
            byte[] data = File.ReadAllBytes(input);

            List<long> encrypted = new List<long>();

            using (BinaryWriter writer = new BinaryWriter(File.Open(output, FileMode.Create)))
            {
                foreach (byte m in data)
                {
                    //шифрование каждого байта
                    long c = (m * (m + b)) % n;

                    writer.Write(c); // 8 байт
                    encrypted.Add(c);
                }
            }

            PrintPreview(encrypted);
            MessageBox.Show("Шифрование завершено!");
        }

        //расшифровка 1 числа
        byte DecryptBlock(long c, long p, long q, long b)
        {
            long n = p * q;

            long D = (b * b + 4 * c) % n;

            long mp = ModPow(D, (p + 1) / 4, p);
            long mq = ModPow(D, (q + 1) / 4, q);

            long yp = ModInverse(q, p);
            long yq = ModInverse(p, q);

            long r1 = (yp * q * mp + yq * p * mq) % n;
            long r2 = n - r1;
            long r3 = (yp * q * mp - yq * p * mq) % n;
            long r4 = n - r3;

            long inv2 = ModInverse(2, n);

            long[] roots = { r1, r2, r3, r4 };

            foreach (var r in roots)
            {
                long m = ((r - b) * inv2) % n;
                if (m < 0) m += n;
                //выбор верного
                if (m >= 0 && m < 256)
                    return (byte)m;   
            }

            throw new Exception("Корень не найден");
        }


        void Decrypt(string input, string output, long p, long q, long b)
        {
            List<long> encrypted = new List<long>();
            List<byte> result = new List<byte>();

            using (BinaryReader reader = new BinaryReader(File.Open(input, FileMode.Open)))
            {
                while (reader.BaseStream.Position < reader.BaseStream.Length)
                {
                    long c = reader.ReadInt64();
                    encrypted.Add(c);

                    result.Add(DecryptBlock(c, p, q, b));
                }
            }

            File.WriteAllBytes(output, result.ToArray());

            PrintPreview(result.Select(x => (long)x).ToList());
            MessageBox.Show("Дешифрование завершено!");
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                inputFile = openFileDialog1.FileName;
                MessageBox.Show("Файл выбран!");
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                outputFile = saveFileDialog1.FileName;
            }

        }

        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(textBoxP.Text) ||
                    string.IsNullOrEmpty(textBoxQ.Text) ||
                    string.IsNullOrEmpty(textBoxB.Text))
                {
                    MessageBox.Show("Введите p, q, b");
                    return;
                }

                long p = long.Parse(textBoxP.Text);
                long q = long.Parse(textBoxQ.Text);
                long b = long.Parse(textBoxB.Text);

                if (!IsPrime((int)p) || !IsPrime((int)q))
                {
                    MessageBox.Show("p и q должны быть простыми");
                    return;
                }

                if (p % 4 != 3 || q % 4 != 3)
                {
                    MessageBox.Show("p и q должны ≡ 3 mod 4");
                    return;
                }

                long n = p * q;

                if (b <= 0 || b >= n)
                {
                    MessageBox.Show("0 < b < n");
                    return;
                }

                // входной файл
                OpenFileDialog ofd = new OpenFileDialog();
                if (ofd.ShowDialog() != DialogResult.OK) return;

                string input = ofd.FileName;

                // файл выхода
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Encrypted file (*.enc)|*.enc";
                if (sfd.ShowDialog() != DialogResult.OK) return;

                string output = sfd.FileName;

                Encrypt(input, output, p, q, b);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(textBoxP.Text) ||
                    string.IsNullOrEmpty(textBoxQ.Text) ||
                    string.IsNullOrEmpty(textBoxB.Text))
                {
                    MessageBox.Show("Введите p, q, b");
                    return;
                }

                long p = long.Parse(textBoxP.Text);
                long q = long.Parse(textBoxQ.Text);
                long b = long.Parse(textBoxB.Text);

                // файл с шифротекстом
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "Encrypted file (*.enc)|*.enc";
                if (ofd.ShowDialog() != DialogResult.OK) return;

                string input = ofd.FileName;

                // куда сохранить результат
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Image (*.png)|*.png|All files|*.*";
                if (sfd.ShowDialog() != DialogResult.OK) return;

                string output = sfd.FileName;

                Decrypt(input, output, p, q, b);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBoxResult.Clear();
            textBoxP.Clear();
            textBoxQ.Clear();
            textBoxB.Clear();

        }

        private void textBoxP_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
