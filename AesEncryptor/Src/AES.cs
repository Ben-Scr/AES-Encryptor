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

        public static byte[] EncryptBytes(byte[] bytes, byte[] key, byte[] iv)
        {
            using var aes = Aes.Create();
            aes.Key = key;
            aes.IV = iv;

            using var encryptor = aes.CreateEncryptor();
            using var ms = new MemoryStream();
            using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
            {
                cs.Write(bytes, 0, bytes.Length);
                cs.FlushFinalBlock();
            }
            return ms.ToArray();
        }

        public static byte[] DecryptBytes(byte[] bytes, byte[] key, byte[] iv)
        {
            using var aes = Aes.Create();
            aes.Key = key;
            aes.IV = iv;

            using var decryptor = aes.CreateDecryptor();
            using var input = new MemoryStream(bytes);
            using var cs = new CryptoStream(input, decryptor, CryptoStreamMode.Read);
            using var output = new MemoryStream();

            cs.CopyTo(output);
            return output.ToArray();
        }
    }
}
