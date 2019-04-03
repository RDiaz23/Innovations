using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Innovations.Model.Schemas.Users;
using Innovations.Model.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Innovations.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {

        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {            
            _userService = userService;
        }
        [Authorize(Roles = "Administrador")]
        // GET: api/Users
        [HttpGet]
        public async Task<IActionResult> Get()
        {

            var user = await _userService.GetUser();

            return Ok(user);
        }
        [Authorize(Roles = "Administrador")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var user = await _userService.GetUserSelect(id);
            return Ok(user);
        }
        [Authorize(Roles = "Administrador")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]User value)
        {
            if (value == null)
                return BadRequest();

           var response = await _userService.InsertUser(value);

            if (response == null)
                return StatusCode(500, new { codeerror = 500, error = "algo salio mal"});

            return Ok(response);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login([FromBody]User value)
        {
            var token = await _userService.Login(value);

            if (token.Equals(404))
                return NotFound();

            return Ok(token);
        }
    }
}
