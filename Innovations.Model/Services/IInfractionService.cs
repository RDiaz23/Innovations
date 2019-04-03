using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Innovations.Model.Schemas;

namespace Innovations.Model.Services.Interfaces
{
    public interface IInfractionService
    {
        Task<IEnumerable<Infraction>> GetInfraction();

        Task<Infraction> GetInfractionSelect(int id);
        Task<object> InsertInfraction(Infraction user);
    }
}
