// Car service class in C# (Tutorial).
using System.Data;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using carservice.Entities;
using carservice.DataAccess;

namespace carservice.Services
{
    /// <summary>
    /// Represents the service interface..
    /// </summary>
    public interface ICarService  
    {
        void CreateCarport();
        DataSet ListCars();
        void SellCar(int id, string owner);
    }    

    /// <summary>
    /// Represents the service configuration options.
    /// </summary>
    public class CarServiceOptions
    {
        public string Server { get; set; }
    }
    
    /// <summary>
    /// Represents the service class.
    /// </summary>
    public class CarService : ICarService  
    {
        private readonly ILogger<CarService> _logger;
        private readonly string _server;
        private readonly IRepository<Car> _repository;
 
        public CarService(ILoggerFactory loggerFactory, IOptions<CarServiceOptions> options)
        {
            _logger = loggerFactory.CreateLogger<CarService>();
            _server = options.Value.Server;
            _repository = new CarRepository();
        }
        
        public void CreateCarport()
        {
            _logger.LogInformation($"Creating carport test on server {_server}!");

            // Car objects
            Car car1 = new Car(1, "Golf", "VW");
            Car car2 = new Car(2, "Aygo", "Toyota");

            // Car Repository 
            _repository.Add(car1);
            _repository.Add(car2);
        }

        public void SellCar(int id, string owner)
        {
            _logger.LogInformation($"Selling car test on server {_server}!");

            Car carId = _repository.FindById(id);
            carId.setOwner(owner);
        
            _repository.Update(carId);
        }
        public DataSet ListCars()
        {
            _logger.LogInformation($"Listing carport test on server {_server}!");

            IEnumerable<Car> cars = _repository.findAll();
            DataSet ds = new DataSet();
            foreach (Car car in cars) {
                DataSet dscar = CarAssembler.CreateDto(car);
                ds.Merge(dscar);
            }
            return ds;
        }
    }
}