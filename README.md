# Advanded Encryption Standard Library
A `Net 10.0` C# AES Library

## Usage
- Encryption and Decryption of byte data

## How to use
```csharp
using BenScr.Cryptography;
```

```csharp
string text = "Example123";
byte[] bytes = Encoding.UTF8.GetBytes(text);

byte[] key = AES.GenerateKey(KeySize.Bits256);
byte[] iv = AES.GenerateBytes(16);

byte[] encryptedBytes = AES.EncryptBytes(bytes, key, iv);
string encryptedText = Encoding.UTF8.GetString(encryptedBytes);

byte[] decryptedBytes = AES.DecryptBytes(encryptedBytes, key, iv);
string decryptedText = Encoding.UTF8.GetString(decryptedBytes);
```
