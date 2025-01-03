using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using finanzauto_Back.Helpers;
using finanzauto_Back.Models;

public class JwtBearerConfiguration : IJwtBearerConfiguration
{
    private readonly IConfiguration _config;
    public JwtBearerConfiguration(IConfiguration configuration)
    {
        _config = configuration;
    }
    public string CreateToken(UserLogged user)
    {
        try
        {
            var keyStr = _config["Jwt:Key"]!;

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyStr));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var issuer = _config["Jwt:Issuer"];
            var audience = _config["Jwt:Audience"];

            var claims = new ClaimsIdentity(new[]
            {
                new Claim(JwtRegisteredClaimNames.Sid, Convert.ToString(user.Id)),
                new Claim(JwtRegisteredClaimNames.Name, user.Nombres),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            });


            // Create the token
            var expiration = DateTime.UtcNow.AddMinutes(Double.Parse(_config["Defaults:TokenTimeInMinutes"] ?? "1440"));

            //var token = new JwtSecurityToken(
            //    //issuer: issuer,
            //    //audience: audience,
            //    claims: claims,
            //    expires: expiration,
            //    signingCredentials: creds);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Issuer = issuer,
                Audience = audience,
                Expires = expiration,
                SigningCredentials = creds
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);
            return jwtToken;
        }
        catch
        {
            throw;
        }
    }
}