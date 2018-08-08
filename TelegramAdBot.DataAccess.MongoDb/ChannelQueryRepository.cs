using TelegramAdBot.Entities;

namespace TelegramAdBot.DataAccess.MongoDb
{
    public class ChannelQueryRepository : MongoRepository<ChannelQuery>, IChannelQueryRepository
    {
        public ChannelQueryRepository(Util<ChannelQuery> util, string connectionString) : base(util, connectionString)
        {
        }
    }
}