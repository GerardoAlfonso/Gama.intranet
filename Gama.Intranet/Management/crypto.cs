using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Gama.Intranet.Management
{
    public class Crypto
    {
        // encrypt string symmetrically
        public static string Encrypt(string _stringToEncript)
        {
            try
            {
                byte[] encrypted = System.Text.Encoding.UTF8.GetBytes(_stringToEncript);
                return Convert.ToBase64String(encrypted);
            }
            catch (Exception)
            {
                return _stringToEncript;
            }
        }

        // decrypt string symmetrically
        public static string Decrypt(string _stringToDecrypt)
        {
            try
            {
                byte[] decrypted = Convert.FromBase64String(_stringToDecrypt);
                return System.Text.Encoding.Unicode.GetString(decrypted);
            }
            catch (Exception)
            {
                return _stringToDecrypt;
            }
        }

        // encrypt single key password in sha256  
        public static string GetSHA256(string str)
        {
            SHA256 sha256 = SHA256Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha256.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }


        // generate random string
        public static string RandomString(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                byte[] uintBuffer = new byte[sizeof(uint)];

                while (length-- > 0)
                {
                    rng.GetBytes(uintBuffer);
                    uint num = BitConverter.ToUInt32(uintBuffer, 0);
                    res.Append(valid[(int)(num % (uint)valid.Length)]);
                }
            }

            return res.ToString();
        }

        //
        public static string CreateJWT(string user, int role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            //var secretKey = _secretKey;
            var key = Encoding.ASCII.GetBytes("asdwda1d8a4sd8w4das8d*w8d*asd@#");
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("user", user),
                    new Claim(ClaimTypes.Role, Convert.ToString(role)),
                    new Claim("fechaIngreso", DateTime.UtcNow.ToString("MM-dd-yyyy"))
                }),
                Expires = DateTime.UtcNow.AddHours(4),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }

    }
}
