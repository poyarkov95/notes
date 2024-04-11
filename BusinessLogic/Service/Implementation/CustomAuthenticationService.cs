using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BusinessLogic.Service.Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Postgres.Repository.Interface;

namespace BusinessLogic.Service.Implementation
{
    public class CustomAuthenticationService : ICustomAuthenticationService
    {
        private readonly IHttpContextAccessor _httpContext;
        private readonly IUserRepository _userRepository;

        public CustomAuthenticationService(IUserRepository userRepository, IHttpContextAccessor httpContext)
        {
            _userRepository = userRepository;
            _httpContext = httpContext;
        }

        public async Task<AuthenticateResult> Authenticate()
        {
            var base64Token = _httpContext.HttpContext.Request.Headers["access_token"];
            var jwtToken = new JwtSecurityTokenHandler().ReadToken(base64Token) as JwtSecurityToken;

            if (jwtToken == null)
            {
                return AuthenticateResult.Fail(new Exception("Invalid token"));
            }

            if (jwtToken.ValidTo < DateTime.UtcNow)
            {
                return AuthenticateResult.Fail(new Exception("Token expired"));
            }
            
            var claims = jwtToken.Claims.ToList();
            
            var identity = new ClaimsIdentity(claims, "BasicSchema");
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, "BasicSchema");
            return AuthenticateResult.Success(ticket);
        }
    }
}