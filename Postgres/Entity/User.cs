using System;
using Postgres.Attributes;

namespace Postgres.Entity
{
    public class User
    {
        [PrimaryKey]
        [ColumnName("user_id")]
        public Guid UserId { get; set; }

        [ColumnName("login")]
        public string Login { get; set; }
        
        [ColumnName("salt")]
        public string Salt { get; set; }
        
        [ColumnName("password_hash")]
        public string PasswordHash { get; set; }
        
        [ColumnName("access_token")]
        public string AccessToken { get; set; }
        
        [ColumnName("refresh_token")]
        public string RefreshToken { get; set; }
        
        [ColumnName("registration_date")]
        public DateTime RegistrationDate { get; set; }
    }
}