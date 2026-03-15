using System.Text;


namespace labSecond
{
    public partial class Form1 : Form
    {
        private byte[] originalBytes;
        private const int RegisterSize = 34;
        private readonly int[] taps = new int[] { 0, 13, 14, 33 };
        private byte[] processedBytes;

        public Form1()
        {
            InitializeComponent();
        }


        public bool[] InitLfsrState(string bits)
        {
            bool[] state = new bool[RegisterSize];
            for (int i = 0; i < RegisterSize; i++)
            {
                state[RegisterSize - 1 - i] = (bits[i] == '1');
            }
            return state;
        }

        private int LfsrNextBit(bool[] state)
        {
            // return bit
            bool outputBit = state[33];

            bool feedback = state[33] ^ state[14] ^ state[13] ^ state[0];

            // shift register
            for (int i = 33; i > 0; i--)
            {
                state[i] = state[i - 1];
            }
            state[0] = feedback;

            return outputBit ? 1 : 0;
        }


        private byte[] GenerateKeyStream(bool[] state, int byteCount)
        {
            byte[] key = new byte[byteCount];

            for (int i = 0; i < byteCount; i++)
            {
                byte b = 0;
                for (int bit = 0; bit < 8; bit++)
                {
                    int kbit = LfsrNextBit(state);
                    b = (byte)((b << 1) | kbit);
                }
                key[i] = b;
            }

            return key;
        }

        private byte[] ProcessBytes(byte[] data, string registerBits, out string keyBits)
        {
            bool[] state = InitLfsrState(registerBits);

            byte[] key = GenerateKeyStream(state, data.Length);
            byte[] result = new byte[data.Length];

            for (int i = 0; i < data.Length; i++)
                result[i] = (byte)(data[i] ^ key[i]);

            keyBits = BytesToBitString(key);
            return result;
        }


        private string BytesToBitString(byte[] data)
        {
            StringBuilder sb = new StringBuilder(data.Length * 8);

            foreach (byte b in data)
                sb.Append(Convert.ToString(b, 2).PadLeft(8, '0'));

            return sb.ToString();
        }


        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '0' && e.KeyChar != '1' && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            label5.Text = textBox1.Text.Length + "/34";
        }


        private void buttonOpenF_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                originalBytes = File.ReadAllBytes(dialog.FileName);
                textBoxInput.Text = BytesToBitStringPreview(originalBytes);

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string bits = textBox1.Text;

            if (!bits.Contains('1'))
            {
                MessageBox.Show("Начальное состояние регистра не может быть нулевым.");
                return;
            }

            if (originalBytes == null)
            {
                MessageBox.Show("Сначала загрузите файл");
                return;
            }

            if (bits.Length != 34)
            {
                MessageBox.Show("Введите 34 бита регистра");
                return;
            }

            string keyBits;

            processedBytes = ProcessBytes(originalBytes, bits, out keyBits);

            textBoxKey.Text = BitStringPreview(keyBits);

            textBoxOutput.Text = BytesToBitStringPreview(processedBytes);
        }

        private string BitStringPreview(string bits, int firstBits = 100, int lastBits = 50)
        {
            if (bits.Length <= firstBits + lastBits)
                return bits;

            return bits.Substring(0, firstBits) + " ... " +
                   bits.Substring(bits.Length - lastBits);
        }

        
        private string BytesToBitStringPreview(byte[] data, int firstBits = 100, int lastBits = 50)
        {
            StringBuilder sb = new StringBuilder();

            int totalBits = data.Length * 8;

            if (totalBits <= firstBits + lastBits)
            {
                foreach (byte b in data)
                    sb.Append(Convert.ToString(b, 2).PadLeft(8, '0'));

                return sb.ToString();
            }

            int firstBytes = (firstBits + 7) / 8;

            for (int i = 0; i < firstBytes; i++)
                sb.Append(Convert.ToString(data[i], 2).PadLeft(8, '0'));

            string start = sb.ToString().Substring(0, firstBits);

            sb.Clear();

            // last bits
            int lastBytes = (lastBits + 7) / 8;

            for (int i = data.Length - lastBytes; i < data.Length; i++)
                sb.Append(Convert.ToString(data[i], 2).PadLeft(8, '0'));

            string end = sb.ToString().Substring(sb.Length - lastBits);

            return start + " ... " + end;
        }

        private void buttonWriteInF_Click(object sender, EventArgs e)
        {
            if (processedBytes == null)
            {
                MessageBox.Show("Нет данных для сохранения");
                return;
            }

            SaveFileDialog dialog = new SaveFileDialog();

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllBytes(dialog.FileName, processedBytes);
            }
        }

        private void textBoxInput_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBoxInput.Clear();
            textBoxOutput.Clear();
            textBoxKey.Clear();
        }
    }
}
