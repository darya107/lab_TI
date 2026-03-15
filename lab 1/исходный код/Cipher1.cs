using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1
{
    public static class Cipher1
    {

        public static int[] GetColumnRanks(string key)
        {
            var sorted = key
                .Select((ch, index) => new { ch, index })
                .OrderBy(x => x.ch)
                .ThenBy(x => x.index)
                .ToList();

            int[] ranks = new int[key.Length];

            for (int i = 0; i < sorted.Count; i++)
            {
                ranks[sorted[i].index] = i + 1;
            }

            return ranks;
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

        
        private static List<List<char>> BuildTable(string text, int[] ranks)
        {
            var table = new List<List<char>>();
            int k = 0;
            int row = 0;
            int cols = ranks.Length;

            while (k < text.Length)
            {
                int currentRank = (row % cols) + 1;

                // ищу столбец с таким рангом
                int colIndex = Array.IndexOf(ranks, currentRank);

                int len = colIndex + 1;

                var currentRow = new List<char>();

                for (int i = 0; i < len && k < text.Length; i++)
                {
                    currentRow.Add(text[k++]);
                }

                table.Add(currentRow);
                row++;
            }

            return table;
        }


        public static string ColumnEncryptImproved(string text, string key)
        {
            key = checkKey(key);
            text = new string(text.Where(char.IsLetter).Select(char.ToUpper).ToArray());

            int[] ranks = GetColumnRanks(key);

            var table = BuildTable(text, ranks);

            var order = ranks
                .Select((rank, idx) => new { rank, idx })
                .OrderBy(x => x.rank)
                .Select(x => x.idx)
                .ToArray();

            StringBuilder result = new StringBuilder();

            foreach (int col in order)
            {
                for (int i = 0; i < table.Count; i++)
                {
                    if (col < table[i].Count)
                        result.Append(table[i][col]);
                }
            }

            return result.ToString();
        }


        public static string ColumnDecryptImproved(string cipher, string key)
        {
            key = checkKey(key);
            cipher = new string(cipher.Where(char.IsLetter).Select(char.ToUpper).ToArray());

            int[] ranks = GetColumnRanks(key);
            int cols = key.Length;

            // Строится форма таблицы
            var shapeTable = BuildTable(new string('X', cipher.Length), ranks);

            int rows = shapeTable.Count;

            // порядок столбцов
            var order = ranks
                .Select((rank, idx) => new { rank, idx })
                .OrderBy(x => x.rank)
                .Select(x => x.idx)
                .ToArray();

            char[][] table = new char[rows][];
            for (int i = 0; i < rows; i++)
                table[i] = new char[shapeTable[i].Count];

            int k = 0;

            // заполняю столбцы
            foreach (int col in order)
            {
                for (int i = 0; i < rows; i++)
                {
                    if (col < table[i].Length)
                    {
                        table[i][col] = cipher[k++];
                    }
                }
            }

            // читаю построчно
            StringBuilder result = new StringBuilder();

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < table[i].Length; j++)
                {
                    result.Append(table[i][j]);
                }
            }

            return result.ToString();
        }


    }



}
