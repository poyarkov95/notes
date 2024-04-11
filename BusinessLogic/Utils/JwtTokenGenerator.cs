using System;
using System.IdentityModel.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Postgres.Entity;
using System.Security.Claims;
using BusinessLogic.Model;
using Claim = System.Security.Claims.Claim;

namespace BusinessLogic.Utils
{
    public static class JwtTokenGenerator
    {
        public static TokenPair GenerateToken(User user)
        {
            var claims = new[] {new Claim("userId", user.UserId.ToString())};
            
            var accessTokenSigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(TokenOptions.AccessTokenSecretKey)),
                SecurityAlgorithms.HmacSha256);
            
            var accessToken = new JwtSecurityToken(
                signingCredentials: accessTokenSigningCredentials,
                expires: DateTime.UtcNow.AddHours(TokenOptions.AccessTokenExpireHours),
                claims: claims);
            
            var refreshTokenSigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(TokenOptions.RefreshTokenSecretKey)),
                SecurityAlgorithms.HmacSha256);
            
            var refreshToken = new JwtSecurityToken(
                signingCredentials: refreshTokenSigningCredentials,
                expires: DateTime.UtcNow.AddHours(TokenOptions.RefreshTokenExpireHours),
                claims: claims);

            return new TokenPair
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(accessToken),
                RefreshToken = new JwtSecurityTokenHandler().WriteToken(refreshToken)
            };
        }
    }
}