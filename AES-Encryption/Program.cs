using System.Security.Cryptography;
using System.Text;
using BenScr.Cryptography;

public static class Program
{
    public static void Main(string[] args)
    {
        string text = File.ReadAllText("");
        string encryptedText = Encryptor.EncryptString(text, "123", KeySize.Bits256);
        Console.WriteLine(encryptedText);
        Console.WriteLine(Encryptor.DecryptString(encryptedText, "123", KeySize.Bits256));
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