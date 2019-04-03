using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Innovations.Model.Schemas;

namespace Innovations.Model.Services.Interfaces
{
    public interface ICarService
    {
        Task<IEnumerable<Car>> GetCar();

        Task<Car> GetCarSelect(string enrollment);
        Task<object> InsertCar(Car car);
    }
}
