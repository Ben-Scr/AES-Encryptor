using System.Security.Cryptography;
using System.Text;
using BenScr.Cryptography;

public static class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Enter a text for encryption:");
        string text = Console.ReadLine();

        // Info: Key used for encrypting the text
        byte[] key = AES.GenerateKey(KeySize.Bits256);

        // Info: IV -> Initialization Vector
        byte[] iv = AES.GenerateIV();

        byte[] data = Encoding.UTF8.GetBytes(text);

        byte[] cipheredData = AES.EncryptBytes(data, key, iv);

        string cipherText = Encoding.UTF8.GetString(cipheredData);
        Console.WriteLine("Cipher Text: " + cipherText);
        byte[] decryptedData = AES.DecryptBytes(cipheredData, key, iv);
        Console.WriteLine("Decrypted Text: " + Encoding.UTF8.GetString(decryptedData));
    }

    public static string ArrayToString<T>(T[] arr)
    {
        string text = "[";
        for (int i = 0; i < arr.Length; i++)
        {
            if (i == 0)
            {
                text += arr[i];
            }
            else
            {
                text += ", " + arr[i];
            }
        }

        return text + "]";
    }
}