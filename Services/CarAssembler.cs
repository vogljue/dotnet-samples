// DTO Assembler pattern in C#.
using System;
using System.Data;
using Carservice.Entities;

namespace Carservice.Services
{
    public static class CarAssembler 
    {
        public static DataSet CreateDto(Car car) 
        {
            DataSet ds = new DataSet();
            DataTable table = new DataTable("Car");

            DataColumn column;
            column = new DataColumn("Id", typeof(System.Int32));
            table.Columns.Add(column);
            column = new DataColumn("Model", typeof(System.String));
            table.Columns.Add(column);
            column = new DataColumn("Vendor", typeof(System.String));
            table.Columns.Add(column);
            column = new DataColumn("Owner", typeof(System.String));
            table.Columns.Add(column);
            
            DataRow row = table.NewRow();
            row["Id"] = car.Id;
            row["Model"] = car.Model;
            row["Vendor"] = car.Vendor;
            row["Owner"] = car.Owner;
            table.Rows.Add(row);
            
            ds.Tables.Add(table);

            return ds;
        }
        
        public static Car UpdateDomainObject(DataSet ds)
        {
            Car car = new Car();
            DataTable table = ds.Tables["Car"];
            foreach (DataRow row in table.Rows)
            {
                car.Id = Convert.ToInt32(row["Id"]);   
                car.Model = Convert.ToString(row["Model"]);   
                car.Vendor = Convert.ToString(row["Vendor"]);   
                car.Owner = Convert.ToString(row["Owner"]);   
            }
            return car;
        }
    }
}