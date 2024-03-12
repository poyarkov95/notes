using Postgres.Entity;

namespace BusinessLogic.Model.Profile
{
    public class NotesModelProfile : AutoMapper.Profile
    {
        public NotesModelProfile()
        {
            CreateMap<Note, NoteModel>();
        }
    }
}