using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLogic.Model;
using Postgres.Filter;

namespace BusinessLogic.Service.Interface
{
    public interface INoteService
    {
        /// <summary>
        /// Получить список зписей
        /// </summary>
        Task<IEnumerable<NoteModel>> GetNotes(NotesFilterModel filter);
        
        /// <summary>
        /// Создать запись
        /// </summary>
        Task<Guid> CreateNote(NoteModel note);
    }
}