// Car Repository in C#.
using System.Collections.Generic;
using carservice.Entities;

namespace carservice.DataAccess
{
    public class CarRepository : IRepository<Car>
    {
        private Carport _carport;
        
        public CarRepository()
        {
            _carport = new Carport();
        }

        public IEnumerable<Car> findAll() 
        {
            return _carport.Cars;    
        }
        
        public void Add(Car entity)
        {
            _carport.Add(entity);
        }
        
        
        public void Delete(Car entity)
        {
            _carport.Remove(entity);
        }
            
        public void Update(Car entity)
        {
            _carport.Cars.Contains(entity);
        }
        
        public Car FindById(int Id)
        {
            return _carport.Cars.Find(x => x.Id == Id);
        }
    }
}