using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;


namespace Gama.Intranet.Management
{
    public class JWT
    {

        public static string create_token(string user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            //var secretKey = _secretKey;
            var key = Encoding.ASCII.GetBytes("asdwda1d8a4sd8w4das8d*w8d*asd@#");
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("user", user),
                    new Claim("fechaIngreso", DateTime.UtcNow.ToString("MM-dd-yyyy"))
                }),
                Expires = DateTime.UtcNow.AddHours(4),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }



        // se modifico la validacion para inyectarlo en el Startup
        #region
        //public JWTRequest validate_token(JWTRequest jwtr)
        //{
        //    JWTRequest jwtToken = new JWTRequest();
        //    if (jwtr.token == null)
        //    {
        //        return jwtToken;
        //    }
        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var key = Encoding.ASCII.GetBytes("secretsecretsecret");

        //    try
        //    {
        //        tokenHandler.ValidateToken(jwtr.token, new TokenValidationParameters
        //        {
        //            ValidateIssuerSigningKey = true,
        //            IssuerSigningKey = new SymmetricSecurityKey(key),
        //            ValidateIssuer = false,
        //            ValidateAudience = false,
        //            ClockSkew = TimeSpan.Zero

        //        }, out SecurityToken validatedToken);

        //        var jwtTokenV = (JwtSecurityToken)validatedToken;
        //        jwtToken.user = jwtTokenV.Claims.First(x => x.Type == "user").Value.ToString();
        //        jwtToken.IP = jwtTokenV.Claims.First(x => x.Type == "IP").Value.ToString();
        //        jwtToken.loc = jwtTokenV.Claims.First(x => x.Type == "loc").Value.ToString();
        //        jwtToken.FechaIngreso = jwtTokenV.Claims.First(x => x.Type == "fechaIngreso").Value.ToString();
        //        jwtToken.alg = jwtTokenV.Claims.First(x => x.Type == "alg").Value.ToString();

        //        return jwtToken;

        //    }
        //    catch (SecurityTokenException)
        //    {
        //        return jwtToken;
        //    }
        //    catch (Exception ex)
        //    {
        //        return jwtToken;
        //    }


        //}
        #endregion
    }
}
