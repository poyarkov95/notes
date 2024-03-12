using System;

namespace BusinessLogic.Model
{
    public class NoteModel
    {
        public Guid UserId { get; set; }

        public string Title { get; set; }
        
        public string Details { get; set; }

        public DateTime CreationDate { get; set; }
        
        public DateTime? EditDate { get; set; }
    }
}