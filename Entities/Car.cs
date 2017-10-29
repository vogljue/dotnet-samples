// Car class in C# (Tutorial).
using System.Runtime.Serialization;

namespace Carservice.Entities
{
    /// <summary>
    /// Represents the car class.
    /// </summary>
    [DataContract]
    public class Car : IEntity
    {
        #region Properties
        [DataMember]
        public int Id { get; set; }
        [DataMember]  
        public string Model { get; set; }
        [DataMember]  
        public string Vendor { get; set; }
        [DataMember]  
        public string Owner { get; set; }
        #endregion
        
        #region Constructors
        public Car()
        {
            this.Id = 0;
            this.Model = "unkown";
            this.Vendor = "unkown";
            this.Owner = "unkown";
        }
        
        public Car(int id, string model, string vendor) 
        {
            this.Id = id;
            this.Model = model;
            this.Vendor = vendor;
            this.Owner = "unkown";
        }
        #endregion
        
        #region Methods
        public void setOwner(string owner) 
        {
            this.Owner = owner;
        }
        
        public override string ToString() 
        {
            return string.Format($"[id={Id}],[model={Model}],[vendor={Vendor}],[owner={Owner}]");
        }
        #endregion
    }
}