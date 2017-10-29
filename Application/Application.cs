using System;
using System.Data;
using Microsoft.Extensions.DependencyInjection;  
using Microsoft.Extensions.Logging;
using carservice.Services;

namespace carservice.Application
{
    /// <summary>
    /// Represents the application class.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Executes the application.
        /// </summary>        
        static public void Main(string[] args)
        {
            //setup our Service configuration
            IServiceProvider serviceProvider = AppServiceBuilder.BuildServiceProvider();

            var logger = serviceProvider.GetService<ILoggerFactory>().CreateLogger<Program>();
            logger.LogInformation("Starting application");

            //do the actual work here
            var foo = serviceProvider.GetService<IFooService>();
            foo.DoFileService();
            foo.DoRegexService();

            //do the actual work here
            var carService = serviceProvider.GetService<ICarService>();
            carService.CreateCarport();
            carService.SellCar(1, "Mike Coley");
            
            DataSet ds= carService.ListCars();
            logger.LogInformation("DataSet {0}", ds.GetXml());

            logger.LogInformation("Finishing application");
        }
    }
}
