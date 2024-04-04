using System;
using System.Threading.Tasks;
using Postgres.Entity;
using Postgres.Repository.Interface;

namespace Postgres.Repository.Implementation
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        private const string TableName = "portal.user";
        
        public UserRepository(DapperContext context) : base(context)
        {
        }

        public Task<Guid> InsertUser(User user)
        {
            return ExecuteInsert<User, Guid>(TableName, user);
        }
    }
}