using TelegramAdBot.Entities;

namespace TelegramAdBot.DataAccess
{
    public interface IChannelQueryRepository : IMongoDbRepository<ChannelQuery>
    {
    }
}