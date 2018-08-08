using System.Threading.Tasks;
using TelegramAdBot.Entities;

namespace TelegramAdBot.DataAccess
{
    public interface IChannelParameterRepository : IMongoDbRepository<ChannelParameter>
    {
        Task<ChannelParameter> GetByPriorityAsync(int firstPriority);
    }
}