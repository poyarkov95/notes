using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLogic.Model;
using BusinessLogic.Service.Interface;
using Postgres.Filter;
using Postgres.Repository.Interface;

namespace BusinessLogic.Service.Implementation
{
    public class NoteService : INoteService
    {
        private readonly INoteRepository _noteRepository;
        private readonly IMapper _mapper;
        
        public NoteService(INoteRepository noteRepository, IMapper mapper)
        {
            _noteRepository = noteRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<NoteModel>> GetNotes(NotesFilterModel filter)
        {
            var queryResult = await _noteRepository.GetNotes(filter);
            return _mapper.Map<IEnumerable<NoteModel>>(queryResult);
        }
    }
}