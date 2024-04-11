using System;
using System.Threading.Tasks;
using Dapper;
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

        public Task<User> GetUserByLogin(string login)
        {
            var query = $@"select * 
                                  from portal.user
                                  where login = :login";

            var parameters = new DynamicParameters();
            parameters.Add("login", login);
            
            return ExecuteSingle<User>(query, parameters);
        }

        public async Task UpdateUser(User user)
        {
            await ExecuteUpdate(TableName, user);
        }
    }
}