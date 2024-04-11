using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace BusinessLogic.Service.Interface
{
    public interface ICustomAuthenticationService
    {
        Task<AuthenticateResult> Authenticate();
    }
}