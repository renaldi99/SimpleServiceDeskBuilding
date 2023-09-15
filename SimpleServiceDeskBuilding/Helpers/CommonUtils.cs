using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace SimpleServiceDeskBuilding.Helpers
{
    public class CommonUtils
    {
        public static string EncryptPassword(string password)
        {
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(password);
            byte[] keyBytes = new Rfc2898DeriveBytes("key", Encoding.UTF8.GetBytes("salt")).GetBytes(16);
            using var aes = Aes.Create();
            aes.Key = keyBytes;
            aes.GenerateIV();
            using var memoryStream = new MemoryStream();
            memoryStream.Write(aes.IV, 0, 16);
            using var cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write);
            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
            cryptoStream.FlushFinalBlock();
            byte[] cipherTextBytes = memoryStream.ToArray();
            return Convert.ToBase64String(cipherTextBytes);
        }

        public static string DecryptPassword(string encrypted)
        {
            byte[] cipherTextBytes = Convert.FromBase64String(encrypted);
            byte[] keyBytes = new Rfc2898DeriveBytes("key", Encoding.UTF8.GetBytes("salt")).GetBytes(16);
            using var aes = Aes.Create();
            aes.Key = keyBytes;
            byte[] iv = new byte[16];
            Array.Copy(cipherTextBytes, 0, iv, 0, 16);
            aes.IV = iv;
            using var memoryStream = new MemoryStream();
            using var cryptoStream = new CryptoStream(memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Write);
            cryptoStream.Write(cipherTextBytes, 16, cipherTextBytes.Length - 16);
            cryptoStream.FlushFinalBlock();
            byte[] plainTextBytes = memoryStream.ToArray();
            return Encoding.UTF8.GetString(plainTextBytes);
        }

        public static string? ValidateJwtToken(string? token, IConfiguration configuration)
        {
            if (token == "")
            {
                return "";
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
                }, out SecurityToken validateToken);

                var jwtToken = (JwtSecurityToken)validateToken;
                var username = jwtToken.Claims.First(x => x.Type == ClaimTypes.Name).ToString();
                return username;


            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
