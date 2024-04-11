using System.Threading.Tasks;
using BusinessLogic.Model;

namespace BusinessLogic.Service.Interface
{
    public interface IUserService
    {
        /// <summary>
        /// Метод регистрации пользователя
        /// </summary>
        /// <param name="user"></param>
        Task Register(UserRegisterModel user);

        /// <summary>
        /// Аутентификация пользователя
        /// </summary>
        /// <param name="user"></param>
        /// <returns>token</returns>
        Task<TokenPair> Login(UserRegisterModel user);
    }
}