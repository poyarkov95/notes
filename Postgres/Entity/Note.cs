using System;
using Postgres.Attributes;

namespace Postgres.Entity
{
    public class Note
    {
        [PrimaryKey]
        [ColumnName("id")]
        public Guid Id { get; set; }
        
        [ColumnName("user_id")]
        public Guid UserId { get; set; }

        [ColumnName("title")]
        public string Title { get; set; }
        
        [ColumnName("details")]
        public string Details { get; set; }
        
        [ColumnName("creation_date")]
        public DateTime CreationDate { get; set; }
        
        [ColumnName("edit_date")]
        public DateTime? EditDate { get; set; }
    }
}