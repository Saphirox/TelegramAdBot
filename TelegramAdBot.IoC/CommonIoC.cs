using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using TelegramAdBot.Configurations;

namespace TelegramAdBot.IoC
{
    public static class CommonIoC
    {
        public static IServiceCollection RegisterCommonDependencies(this IServiceCollection serviceCollection)
        {
            return serviceCollection;
        }

        public static IServiceCollection RegisterConfiguration(this IServiceCollection services, IConfiguration root)
        {
            services.Configure<BotConfiguration>(root.GetSection(nameof(BotConfiguration)));
            services.Configure<MongoDbSettings>(root.GetSection(nameof(MongoDbSettings)));
            return services;
        }

    public static IDependencyResolver RegisterDependencyResolver(this IServiceCollection serviceCollection)
    {
            var dependencyResolver =
                new DependencyResolver().RegisterServiceProvider(serviceCollection.BuildServiceProvider());

            serviceCollection.AddSingleton(typeof(IDependencyResolver), dependencyResolver);

            return dependencyResolver;
        }
    }
}