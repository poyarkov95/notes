using System.Threading.Tasks;
using BusinessLogic.Model;
using BusinessLogic.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Notes.WebApi.Controllers
{
    [Microsoft.AspNetCore.Components.Route("api/[controller]")]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        
        [HttpPost("register")]
        public async Task Register(UserRegisterModel filter)
        {
            await _userService.Register(filter);
        }
        
        [HttpPost("login")]
        public async Task<TokenPair> Login(UserRegisterModel filter)
        {
            return await _userService.Login(filter);
        }
    }
}