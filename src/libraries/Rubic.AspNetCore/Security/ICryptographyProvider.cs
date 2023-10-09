namespace Rubic.AspNetCore.Security;

public interface ICryptographyProvider
{
    string Encrypt(string data);
    string Decrypt(string data);
}