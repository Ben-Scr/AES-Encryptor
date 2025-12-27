using BenScr.Cryptography;
using System.Text;

public static class Program
{
    public static void Main(string[] args)
    {
        while (true)
        {
            Console.Clear();
            string text = Console.ReadLine();
            string password = Console.ReadLine();

            string input = Console.ReadLine();
            string encryptedText = input == "" ? Encryptor.EncryptString(text, password, KeySize.Bits256) : input;

            Console.WriteLine("\nEncrypted:");
            Console.WriteLine(encryptedText);

            Console.WriteLine("\nDecrypted:");
            password = Console.ReadLine();
            string decryptedText = Encryptor.DecryptString(encryptedText, password);
            Console.WriteLine(decryptedText);
            Console.ReadLine();
        }
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