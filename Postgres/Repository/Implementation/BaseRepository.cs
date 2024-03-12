using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Postgres.Attributes;
using Postgres.Filter;

namespace Postgres.Repository.Implementation
{
    public class BaseRepository
    {
        private readonly DapperContext _context;

        public BaseRepository(DapperContext context)
        {
            _context = context;
        }

        protected async Task<IEnumerable<T>> ExecuteSelect<T>( string tableName, BaseFilterModel filter)
        {
            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryAsync<T>(PrepareSelect<T>(tableName, filter));
            }
        }

        protected string PrepareSkip(BaseFilterModel filter)
        {
            return $"offset {filter.Skip} fetch next {filter.Take} rows only";
        }

        private string PrepareSelect<T>(string tableName, BaseFilterModel filter)
        {
            var props = typeof(T)
                .GetProperties()
                .Where(s => Attribute.IsDefined(s, typeof(ColumnName)));
            
            var columnNames = new List<string>();

            foreach (var property in props)
            {
                foreach (var attribute in property.GetCustomAttributes(true))
                {
                    var columnNameAttribute = attribute as ColumnName;
                    if (columnNameAttribute != null)
                    {
                        columnNames.Add(columnNameAttribute.Name);
                    }
                }
            }

            var query = $@"select
                      {string.Join(" , ", columnNames)}
                      from {tableName} 
                      {PrepareSkip(filter)}";

            return query;
        }
    }
}