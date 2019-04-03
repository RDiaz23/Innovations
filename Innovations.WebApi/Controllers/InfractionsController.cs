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
    [Authorize(Roles = "Empleado")]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class InfractionsController : ControllerBase
    {
        private readonly IInfractionService _infractionService;

        public InfractionsController(IInfractionService infractionService)
        {
            _infractionService = infractionService;
        }

        // GET: api/Infractions
        [HttpGet]
        public async Task<IActionResult> Get()
        {

            var infractions = await _infractionService.GetInfraction();

            return Ok(infractions);
        }

        // GET: api/Infractions/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var user = await _infractionService.GetInfractionSelect(id);
            return Ok(user);
        }

        // POST: api/Infractions
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Infraction value)
        {
            if (value == null)
                return BadRequest();

            var response = await _infractionService.InsertInfraction(value);

            if (response == null)
                return StatusCode(500, new { codeerror = 500, error = "algo salio mal" });

            return Ok(response);
        }

        // PUT: api/Infractions/5
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
