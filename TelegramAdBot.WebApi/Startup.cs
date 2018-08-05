using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TelegramAdBot.Configurations;
using TelegramAdBot.IoC;
using TelegramAdBot.Services;

namespace TelegramAdBot.WebApi
{
    public class Startup
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfigurationRoot _configuration;
        private IDependencyResolver _dependencyResolver;

        public Startup(IHostingEnvironment env)
        {
            this._env = env;

            _configuration = new ConfigurationBuilder()
                .SetBasePath(_env.ContentRootPath)
                .AddJsonFile("appsettings.json", reloadOnChange: true, optional: false)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", reloadOnChange: true, optional: true)
                .AddInMemoryCollection(new Dictionary<string, string> () {

                })
                .Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            _dependencyResolver =
                services
                    .RegisterMongoDbDataAccess()
                    .RegisterServices()
                    .RegisterConfiguration(_configuration)
                    .RegisterDependencyResolver();
            
            services.AddOptions();
            services.AddMvc();
            services.AddLogging();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IBotService bot)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            bot.SetWebhook();
            app.UseMvc();
        }
    }
}