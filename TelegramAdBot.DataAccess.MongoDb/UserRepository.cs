
using System.Threading.Tasks;
using MongoDB.Driver;
using TelegramAdBot.Entities;

namespace TelegramAdBot.DataAccess.MongoDb
{
    public class UserRepository : MongoRepository<User>, IUserRepository
    {
        public UserRepository(Util<User> util, string connectionString) : base(util, connectionString)
        {
        }

        public UserRepository(string connectionString, string collectionName, Util<User> util) : base(connectionString, collectionName, util)
        {
        }

        public UserRepository(MongoUrl url, Util<User> util) : base(url, util)
        {
        }

        public UserRepository(MongoUrl url, Util<User> util, string collectionName) : base(url, util, collectionName)
        {
        }

        public async Task<bool> ExistsByTelegramIdAsync(int telegramId)
        {
            return (await this.collection.FindAsync(c => c.TelegramId == telegramId)).Any();
        }
    }
}