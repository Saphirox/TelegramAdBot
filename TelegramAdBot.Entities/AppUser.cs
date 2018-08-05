using TelegramAdBot.Entities.Enums;

namespace TelegramAdBot.Entities
{
    [CollectionName("Users")]
    public class AppUser : Entity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string LanguageCode { get; set; }
        
        public bool IsBot { get; set; }

        public UserRole UserRole { get; set; }

        public int TelegramId { get; set; }
        
        public string UserName { get; set; }
    }
}