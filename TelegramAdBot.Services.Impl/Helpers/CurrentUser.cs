using TelegramAdBot.Entities;

namespace TelegramAdBot.Services.Impl.Helpers
{
    public static class CurrentUser 
    {
        public static AppUser Instance { get; private set; }

        public static void SetInstance(AppUser user)
        {
            Instance = user;
        }

        public static bool Exists()
        {
            return Instance != null;
        }
    }
}