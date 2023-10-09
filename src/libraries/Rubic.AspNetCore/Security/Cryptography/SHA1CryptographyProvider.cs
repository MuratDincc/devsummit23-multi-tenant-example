using System.Security.Cryptography;
using System.Text;

namespace Rubic.AspNetCore.Security.Cryptography;

public class SHA1CryptographyProvider : ICryptographyProvider
{
    private readonly string _encryptionKey;

    public SHA1CryptographyProvider(string encryptionKey)
    {
        _encryptionKey = encryptionKey;
    }
    
    public virtual string Encrypt(string data)
    {
        if (string.IsNullOrEmpty(data))
            throw new ArgumentNullException(nameof(data));

        var tDESalg = TripleDES.Create();

        tDESalg.Key = new ASCIIEncoding().GetBytes(_encryptionKey.Substring(0, 16));
        tDESalg.IV = new ASCIIEncoding().GetBytes(_encryptionKey.Substring(8, 8));

        byte[] encryptedBinary = EncryptTextToMemory(data, tDESalg.Key, tDESalg.IV);
        return Convert.ToBase64String(encryptedBinary);
    }

    public virtual string Decrypt(string data)
    {
        if (string.IsNullOrEmpty(data))
            throw new ArgumentNullException(nameof(data));

        var tDESalg = TripleDES.Create();

        tDESalg.Key = new ASCIIEncoding().GetBytes(_encryptionKey.Substring(0, 16));
        tDESalg.IV = new ASCIIEncoding().GetBytes(_encryptionKey.Substring(8, 8));

        byte[] buffer = Convert.FromBase64String(data);
        return DecryptTextFromMemory(buffer, tDESalg.Key, tDESalg.IV);
    }

    private byte[] EncryptTextToMemory(string data, byte[] key, byte[] iv)
    {
        var tDESalg = TripleDES.Create();

        using var ms = new MemoryStream();
        using var cs = new CryptoStream(ms, tDESalg.CreateEncryptor(key, iv),
            CryptoStreamMode.Write);
        byte[] toEncrypt = new UnicodeEncoding().GetBytes(data);
        cs.Write(toEncrypt, 0, toEncrypt.Length);
        cs.FlushFinalBlock();

        return ms.ToArray();
    }

    private string DecryptTextFromMemory(byte[] data, byte[] key, byte[] iv)
    {
        var tDESalg = TripleDES.Create();

        using var ms = new MemoryStream(data);
        using var cs = new CryptoStream(ms, tDESalg.CreateDecryptor(key, iv),
            CryptoStreamMode.Read);
        var sr = new StreamReader(cs, new UnicodeEncoding());
        return sr.ReadLine();
    }
}