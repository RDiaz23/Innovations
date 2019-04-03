using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Innovations.Data;
using Innovations.Model.Schemas.Users;
using Microsoft.AspNetCore.Authorization;

namespace Innovations.WebApi.Controllers
{
    [Authorize(Roles = "Administrador")]
    [Route("api/[controller]")]
    public class RolesController : ControllerBase
    {
        private readonly InnovationsDataContext _context;
        public RolesController(InnovationsDataContext context)
        {
            _context = context;
        }
        // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var roles = await _context.Roles
                .ToArrayAsync();

            var response = roles.Select(u => new
            {
                u.idrole,
                u.name,
                u.description
            });

            return Ok(response);
        }
    }
}