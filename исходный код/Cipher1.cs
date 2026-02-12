using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1
{
    public static class Cipher1
    {

        public static int[] GetColumnOrder(string key)
        {
            return key
                .Select((ch, idx) => new
                {
                    LetterIndex = ch - 'A', 
                    idx
                })
                .OrderBy(x => x.LetterIndex)
                .ThenBy(x => x.idx)
                .Select(x => x.idx)
                .ToArray();
        }

        public static string checkKey(string key)
        {
            var sb = new System.Text.StringBuilder();

            for (int i = 0; i < key.Length; i++)
            {
                char c = key[i];

                if (c >= 'A' && c <= 'Z' || c >= 'a' && c <= 'z')
                    sb.Append(char.ToUpper(c));
            }

            return sb.ToString();
        }

        public static string ColumnEncrypt(string text, string key)
        {
            int cols = key.Length;
            int rows = (int)Math.Ceiling((double)text.Length / cols);

            char[,] table = new char[rows, cols];
            int k = 0;

            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)

                    table[i, j] = (k < text.Length) ? text[k++] : ' ';

            int[] order = Cipher1.GetColumnOrder(key);
            StringBuilder result = new StringBuilder();

            foreach (int col in order)
            {

                for (int i = 0; i < rows; i++)
                    result.Append(table[i, col]);

            }

            return result.ToString();
        }

       public static string DoubleColumnEncrypt(string text, string key1, string key2)
        {
            string first = ColumnEncrypt(text, key1);
            string result = first.Replace(" ", "");
            string second = ColumnEncrypt(result, key2);

            return second;
        }


        public static string ColumnDecrypt(string text, string key)
        {
            int cols = key.Length;
            int rows = (int)Math.Ceiling((double)text.Length / cols);
            
            int baseLen = text.Length / cols;
            int extra = text.Length % cols;

            char[,] table = new char[rows, cols];
            int[] order = GetColumnOrder(key);
            int k = 0;

            foreach (int col in order)
            {
                int currentColLen = baseLen;

                if ((col+1) <= extra)
                    currentColLen = baseLen + 1;

                for (int i = 0; i < currentColLen; i++)
                {
                    table[i, col] = text[k++];
                }
            }

            StringBuilder result = new StringBuilder();

            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    result.Append(table[i, j]);

            return result.ToString();
        }

        public static string DoubleColumnDecrypt(string text, string key1, string key2)
        {

            string first = ColumnDecrypt(text, key2);
            string result = first.TrimEnd('\0');
            string second = ColumnDecrypt(result, key1);
            return second;
        }





    }



}
