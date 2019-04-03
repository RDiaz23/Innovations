using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Innovations.Data;
using Innovations.Model.Schemas;
using Innovations.Model.Schemas.Users;
using Innovations.Services;
using Microsoft.AspNetCore.Authorization;

namespace Innovations.WebApi.Controllers
{
    
    [Route("api/[controller]")]
    public class ValuesController : ControllerBase
    {
        private readonly InnovationsDataContext _context;

        private readonly UserService _userService;
        public ValuesController(InnovationsDataContext context, UserService userService)
        {
            _context = context;
            _userService = userService;
            var a = new List<User>();
            
            if (!_context.Users.Any())
            {
                for (int i = 0; i < 3; i++)
                {
                    userService.CrearPasswordHash("admin", out byte[] passwordHash, out byte[] passwordSalt);
                    var b = new User();
                    b.iduser = i + 1;
                    b.email = "admin" + (i + 1) + "@innovation.com";
                    b.active = true;
                    b.password_hash = passwordHash;
                    b.password_salt = passwordSalt;
                    b.idrole = 1;
                    b.name = "Luke" + i;
                    b.lastName = "Skywalker" + i;
                    a.Add(b);


                }
                context.Users.AddRange(a);
                context.SaveChanges();
            }

            var c = new List<Role>();

            if (!_context.Roles.Any())
            {
                for (int i = 0; i < 2; i++)
                {
                    var d = new Role();
                    d.idrole = i + 1;
                    d.name = i == 0 ? "Administrador" : "Empleado";
                    d.description = "roles Innovations";
                    d.active = true;
                    c.Add(d);
                }
                context.Roles.AddRange(c);
                context.SaveChanges();
            }

            var drivers = new List<Driver>();

            if (!_context.Drivers.Any())
            {
                for (int i = 0; i < 500; i++)
                {
                    var driver = new Driver();
                    driver.dni = "12345678Z" + i;
                    driver.lastName = "perez" + i;
                    driver.name = "juan" + i;
                    driver.active = true;
                    driver.email = "abc" + i + "@innovation.com";
                    driver.points = 1000;
                    drivers.Add(driver);
                }
                context.Drivers.AddRange(drivers);
                context.SaveChanges();
            }

            var cars = new List<Car>();

            if (!_context.Cars.Any())
            {
                for (int i = 0; i < 500; i++)
                {
                    var car = new Car();
                    car.enrollment = "12345678Z" + i;
                    car.brand = i % 2 ==  0 ? "Ford" : "KIA";
                    car.model = "2019";
                    car.active = true;
                    cars.Add(car);
                }
                context.Cars.AddRange(cars);
                context.SaveChanges();
            }

            var infractions = new List<Infraction>();

            if (!_context.Infractions.Any())
            {
                for (int i = 0; i < 50; i++)
                {
                    var infraction = new Infraction();
                    infraction.dni = "12345678Z" + i;
                    infraction.description = "multa 500" + i;
                    infraction.enrollment = "12345678Z" + i;
                    infraction.date = DateTime.Now;
                    infractions.Add(infraction);
                }
                context.Infractions.AddRange(infractions);
                context.SaveChanges();
            }
        }
        // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = await _context.Users
                .ToArrayAsync();

            var response = users.Select(u => new
            {
                u.iduser,
                u.name,
                u.lastName
            });

            return Ok(response);
        }
        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var user = _context.Users.FirstOrDefault(i => i.iduser == id);

            return Ok(user);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]User value)
        {
            if (value == null)
                return BadRequest();

            _context.Users.Add(value);
            _context.SaveChanges();

            return Ok(new { id = value.iduser });
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
