using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLogic.Model;
using Postgres.Filter;

namespace BusinessLogic.Service.Interface
{
    public interface INoteService
    {
        Task<IEnumerable<NoteModel>> GetNotes(NotesFilterModel filter);
    }
}