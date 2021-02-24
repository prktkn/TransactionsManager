using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace TransactionsManager
{
    public class AuthOptions
    {
        public const string ISSUER = "MyAuthServer"; 
        public const string AUDIENCE = "MyAuthClient";
        const string KEY = "thisIsNotSecretAttAll!";
        public const int LIFETIME = 360;
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
