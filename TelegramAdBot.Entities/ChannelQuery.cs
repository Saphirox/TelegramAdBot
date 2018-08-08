using System.Collections.Generic;

namespace TelegramAdBot.Entities
{
    [CollectionName("ChannelQueries")]
    public class ChannelQuery : Entity
    {
        public ChannelQuery()
        {
            
        }
        
        public ChannelQuery(string name)
        {
            this.Name = name;
        }
        
        public string Name { get; set; }
        
        public ICollection<ChannelParameter> ChannelParameters { get; set; }
    }
}