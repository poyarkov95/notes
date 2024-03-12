using System.Collections.Generic;
using System.Threading.Tasks;
using Postgres.Entity;
using Postgres.Filter;

namespace Postgres.Repository.Interface
{
    public interface INoteRepository
    {
        Task<IEnumerable<Note>> GetNotes(NotesFilterModel filterModel);
    }
}