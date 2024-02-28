using System.Security.Cryptography;
using System.Text;

namespace App.Application.Features.Common;

public class PasswordEncryptor
{
    public static string Encrypt(string password)
    {
        var md5 = MD5.Create();
        byte[] inputBytes = Encoding.ASCII.GetBytes(password);
        byte[] hashBytes = md5.ComputeHash(inputBytes);
        return Convert.ToHexString(hashBytes);
    }
}
