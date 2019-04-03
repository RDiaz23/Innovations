using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Innovations.Model.Schemas;

namespace Innovations.Model.Services.Interfaces
{
    public interface IDriverService
    {
        Task<IEnumerable<Driver>> GetDrivers();
        Task<Driver> GetDriverSelect(string id);
        Task<object> InsertDriver(Driver user);
    }
}
