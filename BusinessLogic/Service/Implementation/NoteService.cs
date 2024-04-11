using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLogic.Model;
using BusinessLogic.Service.Interface;
using FluentValidation;
using Postgres.Entity;
using Postgres.Filter;
using Postgres.Repository.Interface;

namespace BusinessLogic.Service.Implementation
{
    public class NoteService : INoteService
    {
        private readonly INoteRepository _noteRepository;
        private readonly IValidator<NoteModel> _validator;
        private readonly IMapper _mapper;
        
        public NoteService(INoteRepository noteRepository,
            IMapper mapper,
            IValidator<NoteModel> validator)
        {
            _noteRepository = noteRepository;
            _mapper = mapper;
            _validator = validator;
        }
        
        public async Task<IEnumerable<NoteModel>> GetNotes(NotesFilterModel filter)
        {
            var queryResult = await _noteRepository.GetNotes(filter);
            return _mapper.Map<IEnumerable<NoteModel>>(queryResult);
        }

        public async Task<Guid> CreateNote(NoteModel note)
        {
            var validationResult = await _validator.ValidateAsync(note);
            
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var noteEntity = _mapper.Map<Note>(note);
            noteEntity.CreationDate = DateTime.UtcNow;
            return await _noteRepository.CreateNote(noteEntity);
        }
    }
}