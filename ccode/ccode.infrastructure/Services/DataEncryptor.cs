using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using CCode.Infrastructure.Services.Interfaces;

namespace CCode.Infrastructure.Services;

public class DataEncryptor : IDataEncryptor
{
    private const int _saltSize = 16;

    public byte[] Encrypt(
        string password,
        string data)
    {
        using (var aes = Aes.Create())
        {
            var salt = GenerateSalt();
            var key = new Rfc2898DeriveBytes(password, salt);

            aes.Key = key.GetBytes(aes.KeySize / 8);
            aes.IV = key.GetBytes(aes.BlockSize / 8);

            using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
            using (var ms = new MemoryStream())
            {
                ms.Write(salt, 0, salt.Length);

                using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                using (var sw = new StreamWriter(cs))
                {
                    sw.Write(data);
                }

                return ms.ToArray();
            }
        }
    }

    public string Decrypt(
        string password,
        byte[] bytes)
    {
        using (var aes = Aes.Create())
        {
            var salt = new byte[_saltSize];
            Array.Copy(bytes, 0, salt, 0, _saltSize);

            var key = new Rfc2898DeriveBytes(password, salt);

            aes.Key = key.GetBytes(aes.KeySize / 8);
            aes.IV = key.GetBytes(aes.BlockSize / 8);

            using (var ms = new MemoryStream())
            {
                ms.Write(bytes, _saltSize, bytes.Length - _saltSize);

                ms.Position = 0; // Resetuj pozycję strumienia do początku

                using (var decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
                using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                using (var sr = new StreamReader(cs))
                {
                    return sr.ReadToEnd();
                }
            }
        }
    }

    private byte[] GenerateSalt()
    {
        using (var rng = new RNGCryptoServiceProvider())
        {
            var saltBytes = new byte[_saltSize];
            rng.GetBytes(saltBytes);

            return saltBytes;
        }
    }
}
