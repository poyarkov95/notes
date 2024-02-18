using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLogic.Filter;
using Postgres.Entity;

namespace Postgres.Repository.Interface
{
    public interface INoteRepository
    {
        Task<IEnumerable<Note>> GetNotes(NotesFilterModel filterModel);
    }
}