using System;
using System.Collections.Generic;
using System.Text;
using Innovations.Data;
using System.Threading.Tasks;
using Innovations.Model.Schemas;
using Innovations.Model.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Innovations.Services
{

    public class DriverService : IDriverService
    {
        private readonly InnovationsDataContext _context;
        public DriverService(InnovationsDataContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Driver>> GetDrivers()
        {
            var drivers = await _context.Drivers.ToArrayAsync();

            var response = drivers.Select(u => new Driver
            {
                dni = u.dni,
                name = u.name,
                lastName = u.lastName
            });

            return response;
        }

        public async Task<Driver> GetDriverSelect(string id)
        {
            var user = await _context.Drivers.FirstOrDefaultAsync(i => i.dni == id);

            return user;
        }

        public async Task<object> InsertDriver(Driver driver)
        {
            var dni = driver.dni;
            if (await _context.Drivers.AnyAsync(u => u.dni == dni))
            {
                return new { code = 400, message = "el dni se encuentra registrado validar datos" };
            }

            Driver newDriver = new Driver
            {
                dni = driver.dni,
                points = driver.points,
                name = driver.name,
                lastName = driver.lastName,
                email = driver.email.ToLower(),
                active = true
            };
            try
            {
                await _context.Drivers.AddAsync(newDriver);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new { code = 500, message = "Transaccion Erronea" }; ;
            }

            var response = newDriver;

            return response;
        }
    }
}
