using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Innovations.Model.Schemas;
using Innovations.Model.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Innovations.WebApi.Controllers
{
    [Authorize(Roles = "Administrador,Empleado")]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class CarsController : ControllerBase
    {
        private readonly ICarService _carService;
        public CarsController(ICarService carService)
        {
            _carService = carService;
        }
        // GET: api/Cars
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var cars = await _carService.GetCar();

            return Ok(cars);
        }

        // GET: api/Cars/5
        [HttpGet("{enrollment}")]
        public async Task<IActionResult> Get(string enrollment)
        {
            var car = await _carService.GetCarSelect(enrollment);
            return Ok(car);
        }
        
        // POST: api/Cars
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Car value)
        {
            if (value == null)
                return BadRequest();

            var response = await _carService.InsertCar(value);

            if (response == null)
                return StatusCode(500, new { codeerror = 500, error = "algo salio mal" });

            return Ok(response);
        }
        
        // PUT: api/Cars/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
