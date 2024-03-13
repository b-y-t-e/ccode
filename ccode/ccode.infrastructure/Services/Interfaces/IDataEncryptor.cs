namespace ccode.infrastructure.Services.Interfaces;

public interface IDataEncryptor
{
    byte[] Encrypt(
        string password,
        string data);

    string Decrypt(
        string password,
        byte[] bytes);
}
