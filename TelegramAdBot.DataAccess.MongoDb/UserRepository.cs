
using System.Threading.Tasks;
using MongoDB.Driver;
using TelegramAdBot.Entities;

namespace TelegramAdBot.DataAccess.MongoDb
{
    public class UserRepository : MongoRepository<AppUser>, IUserRepository
    {
        public UserRepository(Util<AppUser> util, string connectionString) : base(util, connectionString)
        {
        }

        public UserRepository(string connectionString, string collectionName, Util<AppUser> util) : base(connectionString, collectionName, util)
        {
        }

        public UserRepository(MongoUrl url, Util<AppUser> util) : base(url, util)
        {
        }

        public UserRepository(MongoUrl url, Util<AppUser> util, string collectionName) : base(url, util, collectionName)
        {
        }

        public async Task<bool> ExistsByTelegramIdAsync(int telegramId)
        {
            return (await GetUserByTelegramIdAsync(telegramId)) != null;
        }

        public async Task<AppUser> GetUserByTelegramIdAsync(int telegramId)
        {
            return (await this.collection.FindAsync(c => c.TelegramId == telegramId)).SingleOrDefault();
        }
    }
}