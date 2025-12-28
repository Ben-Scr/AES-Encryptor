using BenScr.Cryptography;
using System.Text;

public static class Program
{
    public static void Main(string[] args)
    {
        while (true)
        {
            Console.Clear();
            AesEncryptorTest();
        }
    }

    public static void EncryptorTest()
    {
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

    static byte[] key = AesHelper.GenerateKey(KeySize.Bits256);

    public static void AesHelperTest()
    {
        string text = Console.ReadLine();
       

        byte[] encryptedBytes = AesHelper.EncryptBytes(Encoding.UTF8.GetBytes(text), key);

        Console.WriteLine("\nEncrypted:");
        Console.WriteLine(Encoding.UTF8.GetString(encryptedBytes));
        Console.WriteLine(ArrayToString(encryptedBytes));

        Console.WriteLine("\nDecrypted:");
        byte[] decryptedBytes = AesHelper.DecryptBytes(encryptedBytes, key);
        Console.WriteLine(Encoding.UTF8.GetString(decryptedBytes));
        Console.ReadLine();
    }

    public static void AesEncryptorTest()
    {
        var encryptor = new AesEncryptor(AesHelper.GenerateKey(KeySize.Bits128));
        string text = Console.ReadLine();


        byte[] encryptedBytes = encryptor.Encrypt(Encoding.UTF8.GetBytes(text));

        Console.WriteLine("Encryptor Key");
        Console.WriteLine(ArrayToString(encryptor.GetKey()));

        Console.WriteLine("\nEncrypted String:");
        Console.WriteLine(Encoding.UTF8.GetString(encryptedBytes));
        Console.WriteLine("Raw Bytes:");
        Console.WriteLine(ArrayToString(encryptedBytes));

        Console.WriteLine("\nDecrypted:");
        byte[] decryptedBytes = encryptor.Decrypt(encryptedBytes);
        Console.WriteLine(Encoding.UTF8.GetString(decryptedBytes));
        Console.ReadLine();
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