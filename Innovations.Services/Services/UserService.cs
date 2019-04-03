using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Innovations.Data;
using Innovations.Model.Schemas.Users;
using Innovations.Model.Services.Interfaces;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace Innovations.Services
{
    public class UserService : IUserService
    {
        private readonly InnovationsDataContext _context;
        private readonly IConfiguration _config;
        public UserService(InnovationsDataContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

         public async Task<IEnumerable<User>> GetUser()
        {
            var users = await _context.Users.ToArrayAsync();

            var response = users.Select(u => new User
            {
                iduser = u.iduser,
                name = u.name,
                lastName = u.lastName,
                idrole = u.idrole,
                phone = u.phone,
                email = u.email,
                role = u.role

            });

            return response;
        }

        public async Task<User> GetUserSelect(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(i => i.iduser == id);
            
            return user;
        }

        public async Task<object> InsertUser(User user)
        {
            var email = user.email.ToLower();
            if (await _context.Users.AnyAsync(u => u.email == email))
            {
                return new { code = 400, message = "Email registrado"};
            }
            CrearPasswordHash(user.password, out byte[] passwordHash, out byte[] passwordSalt);

            User newUser = new User
            {
                iduser = user.iduser,
                idrole = user.idrole,
                name = user.name,
                lastName = user.lastName,
                phone = user.phone,
                email = user.email.ToLower(),
                password_hash = passwordHash,
                password_salt = passwordSalt,
                active = true
            };
            try {
                await _context.Users.AddAsync(newUser);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex) {
                return new { code = 500, message = ex.Message };
            }

            var response = newUser;

            return response;
        }

        public async Task<object> Login(User user)
        {
            var email = user.email.ToLower();

            var usuario = await _context.Users.Where(u => u.active == true).Include(u => u.role).FirstOrDefaultAsync(u => u.email == email);

            if (usuario == null)
            {
                return  404  ;
            }

            if (!VerificarPasswordHash(user.password, usuario.password_hash, usuario.password_salt))
            {
                return 404;
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.iduser.ToString()),
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, usuario.role.name ),
                new Claim("idusuario", usuario.iduser.ToString() ),
                new Claim("rol", usuario.role.name ),
                new Claim("nombre", usuario.name )
            };

            return new { token = GenerarToken(claims) };
        }
        public void CrearPasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }

        }
        private bool VerificarPasswordHash(string password, byte[] passwordHashAlmacenado, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return new ReadOnlySpan<byte>(passwordHashAlmacenado).SequenceEqual(new ReadOnlySpan<byte>(passwordHash));
            }
        }

        private string GenerarToken(List<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
              _config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              expires: DateTime.Now.AddMinutes(15),
              signingCredentials: creds,
              claims: claims);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
