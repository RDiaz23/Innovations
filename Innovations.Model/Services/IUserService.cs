using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Innovations.Model.Schemas.Users;

namespace Innovations.Model.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetUser();

        Task<User> GetUserSelect(int id);
        Task<object> InsertUser(User user);

        Task<object> Login(User user);
    }
}
