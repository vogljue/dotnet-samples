using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Data;
using Carservice.Services;

namespace Carservice.Application
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
            IServiceProvider serviceProvider = ServiceCofigurator.GetServiceProvider();

            var logger = serviceProvider.GetService<ILoggerFactory>().CreateLogger<Program>();
            logger.LogInformation("Starting application");

            //do the actual work here
            using (IFooService foo = serviceProvider.GetService<IFooService>())
            {
                logger.LogInformation("FooService first run");
                foo.DoFileService();
                foo.DoRegexService();
            }

            //do the actual work here
            using (IFooService foo = serviceProvider.GetService<IFooService>())
            {
                logger.LogInformation("FooService second run");
                foo.DoFileService();
                foo.DoRegexService();
            }

            //do the actual work here
            using (ICarService carService = serviceProvider.GetService<ICarService>())
            {
                carService.CreateCarport();
                carService.SellCar(1, "Mike Coley");

                DataSet ds = carService.ListCars();
                logger.LogInformation("DataSet {0}", ds.GetXml());
            }

            logger.LogInformation("Finishing application");
        }
    }
}
