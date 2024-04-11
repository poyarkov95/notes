using System;
using System.Threading.Tasks;
using Postgres.Entity;

namespace Postgres.Repository.Interface
{
    public interface IUserRepository
    {
        /// <summary>
        /// Добавить пользователя в базу данных
        /// </summary>
        Task<Guid> InsertUser(User user);

        /// <summary>
        /// Получить пользователя по логину
        /// </summary>
        /// <param name="login"></param>
        Task<User> GetUserByLogin(string login);

        /// <summary>
        /// Обновить пользователя в базе данных
        /// </summary>
        Task UpdateUser(User user);
    }
}