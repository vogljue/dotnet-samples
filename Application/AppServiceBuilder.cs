// Infrastructure Service Builder class in C#.
using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;  
using NLog.Extensions.Logging;
using carservice.Services;

namespace carservice.Application
{
    public class AppServiceBuilder 
    {
        static public IServiceProvider BuildServiceProvider() 
        {
            //setup our Service configuration
            IConfigurationBuilder configBuilder = new ConfigurationBuilder();
            configBuilder.AddJsonFile("carservice.config.json");
            IConfiguration config = configBuilder.Build();

            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging();
            serviceCollection.AddOptions();
            serviceCollection.Configure<CarServiceOptions>(config.GetSection("CarServiceOptions"));
            serviceCollection.Configure<FooServiceOptions>(config.GetSection("FooServiceOptions"));
            serviceCollection.AddSingleton<ICarService, CarService>();
            serviceCollection.AddSingleton<IFooService, FooService>();
            IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
            
            //configure console logging
            //serviceProvider.GetService<ILoggerFactory>().AddConsole(LogLevel.Debug);
            serviceProvider.GetService<ILoggerFactory>().AddNLog();
         
            return serviceProvider;
        }
    }
}