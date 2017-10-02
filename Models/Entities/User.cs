using System;

namespace Models.Entities
{
    public class User
    {
        public int UserId { get; set; }
        
        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public string PasswordSalt { get; set; }
        
        public DateTime DateCreated { get; set; }
        
        public string PasswordResetToken { get; set; }

        public static User Create(
            string email,
            string hashString,
            string saltString)
        {
            //TODO: ensure all not null
            
            var user = new User
            {
                Email = email,
                PasswordHash = hashString,
                PasswordSalt = saltString,
                DateCreated = DateTime.UtcNow
            };

            return user;
        }
    }
}