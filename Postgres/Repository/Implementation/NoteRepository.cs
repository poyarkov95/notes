using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLogic.Filter;
using Postgres.Entity;
using Postgres.Filter;
using Postgres.Repository.Interface;

namespace Postgres.Repository.Implementation
{
    public class NoteRepository : BaseRepository, INoteRepository
    {
        private const string TableName = "portal.note";
        
        public NoteRepository(DapperContext context) : base(context)
        {
        }
        
        public async Task<IEnumerable<Note>> GetNotes(NotesFilterModel filterModel)
        {
            return await ExecuteSelect<Note>(TableName, filterModel);
        }
    }
}