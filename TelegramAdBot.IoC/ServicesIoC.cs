using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Telegram.Bot;
using TelegramAdBot.Configurations;
using TelegramAdBot.Services;
using TelegramAdBot.Services.Impl;
using TelegramAdBot.Services.Impl.Commands;

namespace TelegramAdBot.IoC
{
    public static class ServicesIoC
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton(
                (r) => new TelegramBotClient(r.GetService<IOptions<BotConfiguration>>().Value.Token));
            
            services.AddSingleton<IBotService, BotService>();
            services.AddSingleton<IBotCommandsFactory, BotCommandsFactory>();
            services.AddTransient<IMessageService, MessageService>();
            services.AddTransient<IUserService, UserService>();
            
            return services;
        }
    }
}
