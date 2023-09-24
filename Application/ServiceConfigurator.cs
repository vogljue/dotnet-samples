// Infrastructure Service Builder class in C#.
using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NLog.Extensions.Logging;
using Carservice.Services;

namespace Carservice.Application
{
    public class ServiceCofigurator 
    {
        static public IServiceProvider GetServiceProvider() 
        {
            //setup our Service configuration
            IConfigurationBuilder configBuilder = new ConfigurationBuilder();
            configBuilder.AddXmlFile("carservice.config.xml");
            IConfiguration config = configBuilder.Build();

            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging(loggingBuilder => 
            {
                loggingBuilder.AddNLog(config);
            });
            serviceCollection.AddOptions();
            serviceCollection.Configure<CarServiceOptions>(config.GetSection("CarServiceOptions"));
            serviceCollection.Configure<FooServiceOptions>(config.GetSection("FooServiceOptions"));
            serviceCollection.AddSingleton<ICarService, CarService>();
            serviceCollection.AddTransient<IFooService, FooService>();
            IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
            
            //configure console logging
            //serviceProvider.GetService<ILoggerFactory>().AddConsole(LogLevel.Debug);
            //serviceProvider.GetService<ILoggerFactory>().AddNLog();
         
            return serviceProvider;
        }
    }
}