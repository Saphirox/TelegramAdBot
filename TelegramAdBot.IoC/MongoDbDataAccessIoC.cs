using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using TelegramAdBot.Configurations;
using TelegramAdBot.DataAccess;
using TelegramAdBot.DataAccess.MongoDb;
using TelegramAdBot.Entities;

namespace TelegramAdBot.IoC
{
    public static class MongoDbDataAccessIoC
    {
        public static IServiceCollection RegisterMongoDbDataAccess(this IServiceCollection services)
        {
            services.AddTransient(typeof(Util<>));
            services.AddSingleton<IUserRepository>(
                resolver => 
                    new UserRepository(resolver.GetService<Util<AppUser>>(), 
                        resolver.GetService<IOptions<MongoDbSettings>>()
                            .Value.ConnectionString));


            services.AddSingleton<IChannelParameterRepository>(resolver =>
                new ChannelParameterRepository(resolver.GetService<Util<ChannelParameter>>(), 
                    resolver.GetService<IOptions<MongoDbSettings>>().Value.ConnectionString));
            
            services.AddSingleton<IChannelQueryRepository>(resolver =>
                new ChannelQueryRepository(resolver.GetService<Util<ChannelQuery>>(), 
                    resolver.GetService<IOptions<MongoDbSettings>>().Value.ConnectionString));
            
            return services;
        }
    }
}