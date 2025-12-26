using System.Security.Cryptography;
using System.Text;
using BenScr.Cryptography;

public static class Program
{
    public static void Main(string[] args)
    {
       // Console.WriteLine("Enter a text for encryption:");
       // string text = Console.ReadLine();

        string encryptedString = Encryptor.EncryptString("Hello", "123");
        Console.WriteLine(encryptedString);
        Console.WriteLine(Encryptor.DecryptString(encryptedString, "123"));
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