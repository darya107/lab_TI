using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1
{
    public static class Cipher2
    {

        const string RU = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";

        public static bool IsRussianLetter(char c)
        {
            return RU.IndexOf(c) != -1;
        }

        public static string CheckKeyRU(string key)
        {
            var sb = new System.Text.StringBuilder();

            for (int i = 0; i < key.Length; i++)
            {
                char c = char.ToUpper(key[i]);

                if (Cipher2.IsRussianLetter(c))
                    sb.Append(c);
            }

            return sb.ToString();
        }

      
        public static string VigenereAutoKeyEncryptRU(string text, string key)
        {
            text = text.ToUpper();
            key = CheckKeyRU(key); 

            var result = new StringBuilder();
            var autoKey = new StringBuilder(key);

            int keyPos = 0;

            foreach (char p in text)
            {
                if (!IsRussianLetter(p))
                {
                    result.Append(p);  
                    continue;        
                }

                char k = autoKey[keyPos];

                int pIndex = RU.IndexOf(p);
                int kIndex = RU.IndexOf(k);

                int cIndex = (pIndex + kIndex) % RU.Length;
                char cipher = RU[cIndex];

                result.Append(cipher);

                autoKey.Append(p); 
                keyPos++;
            }

            return result.ToString();
        }

        public static string VigenereAutoKeyDecryptRU(string cipherText, string key)
        {
            cipherText = cipherText.ToUpper();
            key = CheckKeyRU(key);

            var result = new StringBuilder();
            var autoKey = new StringBuilder(key);

            int keyPos = 0;

            foreach (char c in cipherText)
            {
                if (!IsRussianLetter(c))
                {
                    result.Append(c);   
                    continue;          
                }

                char k = autoKey[keyPos];

                int cIndex = RU.IndexOf(c);
                int kIndex = RU.IndexOf(k);

                int pIndex = (cIndex - kIndex + RU.Length) % RU.Length;
                char plain = RU[pIndex];

                result.Append(plain);

                autoKey.Append(plain); 
                keyPos++;
            }

            return result.ToString();
        }





    }
}
