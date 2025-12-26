using System.Security.Cryptography;

namespace BenScr.Cryptography
{
    public enum KeySize
    {
        Bits128 = 128,
        Bits192 = 192,
        Bits256 = 256
    }

    public static class AES
    {
        public static byte[] GenerateKey(KeySize keySize)
        {
            return RandomNumberGenerator.GetBytes((int)keySize / 8);
        }
        public static byte[] GenerateBytes(int length)
        {
            return RandomNumberGenerator.GetBytes(length);
        }

        public static byte[] EncryptBytes(byte[] bytes, byte[] key, byte[] iv)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(bytes, 0, bytes.Length);
                        cryptoStream.FlushFinalBlock();
                    }

                    return ms.ToArray();
                }
            }
        }

        public static byte[] DecryptBytes(byte[] bytes, byte[] key, byte[] iv)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;

                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream ms = new MemoryStream(bytes))
                {
                    MemoryStream output = new MemoryStream();

                    using (CryptoStream cryptoStream = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        cryptoStream.CopyTo(output);
                    }

                    return output.ToArray();
                }
            }
        }
    }
}
