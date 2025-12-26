using BenScr.Cryptography;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace BenScr.Cryptography
{
    public static class Encryptor
    {
        public static string EncryptString(string input, string password)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(16);
            byte[] iv = RandomNumberGenerator.GetBytes(16);

            byte[] key = DeriveKeyFromPassword(password, salt, 32); // AES-256

            byte[] ciphertext = AES.EncryptBytes(Encoding.UTF8.GetBytes(input), key, iv);

            // pack: salt(16) || iv(16) || ciphertext
            byte[] packed = new byte[salt.Length + iv.Length + ciphertext.Length];
            Buffer.BlockCopy(salt, 0, packed, 0, salt.Length);
            Buffer.BlockCopy(iv, 0, packed, salt.Length, iv.Length);
            Buffer.BlockCopy(ciphertext, 0, packed, salt.Length + iv.Length, ciphertext.Length);

            return Convert.ToBase64String(packed);
        }

        public static string DecryptString(string encryptedBase64, string password)
        {
            byte[] packed = Convert.FromBase64String(encryptedBase64);

            if (packed.Length < 16 + 16 + 1)
                throw new CryptographicException("Ciphertext zu kurz oder ungültig.");

            byte[] salt = new byte[16];
            byte[] iv = new byte[16];
            byte[] ciphertext = new byte[packed.Length - 32];

            Buffer.BlockCopy(packed, 0, salt, 0, 16);
            Buffer.BlockCopy(packed, 16, iv, 0, 16);
            Buffer.BlockCopy(packed, 32, ciphertext, 0, ciphertext.Length);

            byte[] key = DeriveKeyFromPassword(password, salt, 32);

            byte[] plain = AES.DecryptBytes(ciphertext, key, iv);
            return Encoding.UTF8.GetString(plain);
        }

        private static byte[] DeriveKeyFromPassword(string password, byte[] salt, int keySizeBytes)
        {
            const int iterations = 100_000;
            var kdf = Rfc2898DeriveBytes.Pbkdf2(Encoding.UTF8.GetBytes(password), salt, iterations, HashAlgorithmName.SHA256, keySizeBytes);

            return kdf;
        }
    }
}
