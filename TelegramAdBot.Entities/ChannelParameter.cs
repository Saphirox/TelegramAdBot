using System.Collections.Generic;

namespace TelegramAdBot.Entities
{
    [CollectionName("ChannelParameters")]
    public class ChannelParameter : Entity
    {
        public string Name { get; set; }

        public int Priority { get; set; }

        public ICollection<ChannelTopic> ChannelTopics { get; set; }
    }
}