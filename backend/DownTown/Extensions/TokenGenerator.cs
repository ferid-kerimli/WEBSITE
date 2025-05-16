using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DownTown.Dto;
using DownTown.Response;
using Microsoft.IdentityModel.Tokens;

namespace DownTown.Extensions;

public static class TokenGenerator
{
    public static async Task<JwtTokenResponse> GenerateToken(TokenRequest tokenRequest, IConfiguration configuration, IList<string> roles)
    {
        try
        {
            var symmetricSecurityKey =
                new SymmetricSecurityKey(
                    Encoding.ASCII.GetBytes(configuration["Jwt:Secret"] ?? throw new InvalidOperationException()));

            var dateTimeNow = DateTime.Now;
            var expireMinute = int.Parse(configuration["Jwt:Expire"] ?? throw new InvalidOperationException());

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, tokenRequest.Email)
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var jwt = new JwtSecurityToken(
                issuer: configuration["Jwt:ValidIssuer"],
                audience: configuration["Jwt:ValidAudience"],
                claims: claims,
                notBefore: dateTimeNow,
                expires: dateTimeNow.AddMinutes(expireMinute),
                signingCredentials: new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256)
            );

            var token = new JwtSecurityTokenHandler().WriteToken(jwt);
            var expireDate = dateTimeNow.AddMinutes(expireMinute);


            return await Task.FromResult(new JwtTokenResponse()
            {
                Token = token,
                ExpireDate = expireDate
            });
        }
        catch
        {
            throw new InvalidOperationException();
        }
    }
}