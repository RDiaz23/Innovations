using System;
using System.Collections.Generic;
using System.Text;
using Innovations.Model.Schemas;
using Innovations.Model.Services.Interfaces;
using System.Threading.Tasks;
using Innovations.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Innovations.Services
{
    public class CarService : ICarService
    {
        private readonly InnovationsDataContext _context;
        public CarService(InnovationsDataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Car>> GetCar()
        {
            var cars = await _context.Cars.ToArrayAsync();

            var response = cars.Select(c => new Car
            {
                enrollment = c.enrollment,
                brand = c.brand,
                model = c.model
            });

            return response;
        }

        public async Task<Car> GetCarSelect(string enrollment)
        {
            var car = await _context.Cars.FirstOrDefaultAsync(i => i.enrollment == enrollment);

            return car;
        }

        public async Task<object> InsertCar(Car car)
        {
            var enrollment = car.enrollment.ToLower();
            if (await _context.Cars.AnyAsync(c => c.enrollment == enrollment))
            {
                return new { code = 400, message = "Auto registrado" };
            }

            Car newCar = new Car
            {
                enrollment = car.enrollment,
                brand = car.brand,
                model = car.model,
                active = true
            };
            try
            {
                await _context.Cars.AddAsync(newCar);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new { code = 500, message = ex.Message} ;
            }

            var response = newCar;

            return response;
        }
    }
}
