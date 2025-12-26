using System.Security.Cryptography;
using System.Text;
using System.Text.Unicode;

public static class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Enter a text for encryption:");
        string text = Console.ReadLine();

        // Info: Key used for encrypting the text
        byte[] key = new byte[16];

        // Info: IV -> Initialization Vector
        byte[] iv = new byte[16];

        using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(key);
            rng.GetBytes(iv);
        }

        byte[] data = Encoding.UTF8.GetBytes(text);

        byte[] cipheredData = Encrypt(data, key, iv);

        string cipherText = Encoding.UTF8.GetString(cipheredData);
        Console.WriteLine("Cipher Text: " + cipherText);
        byte[] decryptedData = Decrypt(cipheredData, key, iv);
        Console.WriteLine("Decrypted Text: " + Encoding.UTF8.GetString(decryptedData));
    }

    public static byte[] Encrypt(byte[] data, byte[] key, byte[] iv)
    {
        byte[] cipheredData;

        using (Aes aes = Aes.Create())
        {
            aes.Key = key;
            aes.IV = iv;

            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cryptoStream = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(data, 0, data.Length);
                    cryptoStream.FlushFinalBlock();
                }

                cipheredData = ms.ToArray();
            }
        }

        return cipheredData;
    }

    public static byte[] Decrypt(byte[] cipheredData, byte[] key, byte[] iv)
    {
        byte[] decryptedData;

        using (Aes aes = Aes.Create())
        {
            aes.Key = key;
            aes.IV = iv;

            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            using (MemoryStream ms = new MemoryStream(cipheredData))
            {
               MemoryStream output = new MemoryStream();

                using (CryptoStream cryptoStream = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                {
                    cryptoStream.CopyTo(output);
                }

                decryptedData = output.ToArray();
            }
        }

        return decryptedData;
    }
}