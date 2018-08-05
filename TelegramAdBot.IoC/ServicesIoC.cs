using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Telegram.Bot;
using TelegramAdBot.Configurations;
using TelegramAdBot.Services;
using TelegramAdBot.Services.Handlers;
using TelegramAdBot.Services.Impl;
using TelegramAdBot.Services.Impl.Commands;
using TelegramAdBot.Services.Impl.Handlers;

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

            services.AddTransient<ICommand, ChooseRoleCommand>();
            services.AddTransient<ICallbackQuery, ChooseRoleHandler>();

           // services.AddTransient<Lazy<ICollection<ICommand>>(typeof(),(r) => new Lazy<>(r.GetServices<ICommand>()));
            services.AddTransient((r) => new Lazy<IEnumerable<ICommand>>(r.GetServices<ICommand>()));
            services.AddTransient((r) => new Lazy<IEnumerable<ICallbackQuery>>(r.GetServices<ICallbackQuery>()));
            return services;
        }
    }
}
