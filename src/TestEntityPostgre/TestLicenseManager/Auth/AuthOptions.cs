using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace TestLicenseManager.Auth;

public class AuthOptions
{
    private const string KEY = "mysupersecret_secretkey!12345678910"; // ключ для шифрации

    public const string ISSUER   = "MyAuthServer"; // издатель токена
    public const string AUDIENCE = "MyAuthClient"; // потребитель токена
    public const int    LIFETIME = 1;              // время жизни токена - 1 минута

    public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
        new(Encoding.ASCII.GetBytes(KEY));
}