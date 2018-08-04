using System;
using TelegramAdBot.Entities;

namespace TelegramAdBot.DataAccess
{
    public interface IMongoDbRepository<TEntity> : IRepositoryBase<TEntity, string> 
        where TEntity : class, IEntity<string>, new()
    {
    }
}
