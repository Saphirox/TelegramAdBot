namespace TelegramAdBot.Entities
{
    [CollectionName("ChannelTopics")]
    public class ChannelTopic : Entity
    {
        public string Name { get; set; }

        public bool IsUserDefined { get; set; }

        public bool IsChecked { get; set; }
    }
}