using TelegramAdBot.Entities;

namespace TelegramAdBot.DataAccess
{
    public interface IChannelTopicRepository : IMongoDbRepository<ChannelTopic>
    {
    }
}