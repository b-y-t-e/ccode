namespace ccode.infrastructure.Services;

public interface IDataEncryptor
{
    byte[] Encrypt(
        string password,
        string data);

    string Decrypt(
        string password,
        byte[] bytes);
}