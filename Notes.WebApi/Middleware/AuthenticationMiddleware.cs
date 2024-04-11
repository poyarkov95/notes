using System.Text.Encodings.Web;
using System.Threading.Tasks;
using BusinessLogic.Service.Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Notes.WebApi.Middleware
{
    public class AuthenticationMiddleware : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly ICustomAuthenticationService _authenticationService;
        
        public AuthenticationMiddleware(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock, ICustomAuthenticationService authenticationService)
            : base(options, logger, encoder, clock)
        {
            _authenticationService = authenticationService;
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            return _authenticationService.Authenticate();
        }
    }
}