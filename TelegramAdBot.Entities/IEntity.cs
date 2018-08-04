using System;
namespace TelegramAdBot.Entities
{
    public interface IEntity<T>
    {
        T Id { get; set; }
    }
}
