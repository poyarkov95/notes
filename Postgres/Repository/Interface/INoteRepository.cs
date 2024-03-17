using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Postgres.Entity;
using Postgres.Filter;

namespace Postgres.Repository.Interface
{
    public interface INoteRepository
    {
        /// <summary>
        /// Получить список записей
        /// </summary>
        Task<IEnumerable<Note>> GetNotes(NotesFilterModel filterModel);

        /// <summary>
        /// Создать запись
        /// </summary>
        Task<Guid> CreateNote(Note note);
    }
}