// Carport class in C# (Tutorial).
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Carservice.Entities
{
    /// <summary>
    /// Represents the carport class.
    /// </summary>
    [DataContract]
    public class Carport : IEntity
    {
        #region Properties
        [DataMember]          
        public int Id { get; set; }
        [DataMember]  
        public List<Car> Cars { get; set; }
        #endregion
        
        #region Constructors
        public Carport()
        {
            this.Cars = new List<Car>();
        }
        #endregion

        #region Methods
        public void Add(Car car)
        {
            this.Cars.Add(car);
        }
        
        public void AddRange(List<Car> cars)
        {
            this.Cars.AddRange(cars);
        }
        
        public void Remove(Car car)
        {
            this.Cars.Remove(car);
        }
        #endregion
    }
}