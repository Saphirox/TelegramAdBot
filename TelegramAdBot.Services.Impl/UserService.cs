using System.Threading.Tasks;
using TelegramAdBot.DataAccess;

namespace TelegramAdBot.Services.Impl
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<bool> ExistsByTelegramIdAsync(int telegramId)
        {
            return _userRepository.ExistsByTelegramIdAsync(telegramId);
        }
    }
}