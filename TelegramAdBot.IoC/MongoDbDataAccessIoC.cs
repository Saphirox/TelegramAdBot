using System;
using Microsoft.Extensions.DependencyInjection;
using TelegramAdBot.DataAccess;
using TelegramAdBot.DataAccess.MongoDb;

namespace TelegramAdBot.IoC
{
    public static class MongoDbDataAccessIoC
    {
        public static IServiceCollection RegisterMongoDbDataAccess(this IServiceCollection services)
        {
            services.AddTransient<IUserRepository, UserRepository>();
            
            return services;
        }
    }
}