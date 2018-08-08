using System.Threading.Tasks;
using MongoDB.Driver;
using TelegramAdBot.Entities;

namespace TelegramAdBot.DataAccess.MongoDb
{
    public class ChannelParameterRepository : MongoRepository<ChannelParameter>, IChannelParameterRepository
    {
        public ChannelParameterRepository(Util<ChannelParameter> util, string connectionString) : base(util, connectionString)
        {
        }

        public async Task<ChannelParameter> GetByPriorityAsync(int priority)
        {
            return await this.collection.Find(c => c.Priority == priority).SingleAsync();
        }
    }
}