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
    public class DriversController : ControllerBase
    {
        private readonly IDriverService _driverService;
        public DriversController(IDriverService driverService)
        {
            _driverService = driverService;
        }
        // GET: api/Drivers
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var drivers = await _driverService.GetDrivers();

            return Ok(drivers);
        }

        // GET: api/Drivers/5
        [HttpGet("{dni}")]
        public async Task<IActionResult> Get(string dni)
        {
            var driver = await _driverService.GetDriverSelect(dni);
            return Ok(driver);
        }
        
        // POST: api/Drivers
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Driver value)
        {
            if (value == null)
                return BadRequest();

            var response = await _driverService.InsertDriver(value);

            if (response == null)
                return StatusCode(500, new { codeerror = 500, error = "algo salio mal" });

            return Ok(response);
        }
        
        // PUT: api/Drivers/5
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
