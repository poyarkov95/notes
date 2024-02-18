using System.Collections.Generic;
using Dapper;

namespace Postgres.Filter
{
    public class QueryTermsModel
    {
        public List<string> Where { get; set; }

        public DynamicParameters Parameters { get; set; }
    }
}