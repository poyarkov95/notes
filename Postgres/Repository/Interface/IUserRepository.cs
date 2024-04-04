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
    }
}