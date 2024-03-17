using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLogic.Model;
using BusinessLogic.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Postgres.Filter;

namespace Notes.WebApi.Controllers
{
    [Microsoft.AspNetCore.Components.Route("api/[controller]")]
    public class NoteController : BaseController
    {
        private readonly INoteService _service;

        public NoteController(INoteService service)
        {
            _service = service;
        }

        [HttpPost("list")]
        public async Task<IEnumerable<NoteModel>> GetAll(NotesFilterModel filter)
        {
            return await _service.GetNotes(filter);
        }

        [HttpPost("create")]
        public async Task<Guid> CreateNote(NoteModel note)
        {
            return await _service.CreateNote(note);
        }
    }
}