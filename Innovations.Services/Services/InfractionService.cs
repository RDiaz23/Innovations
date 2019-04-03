using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Innovations.Data;
using Innovations.Model.Schemas;
using Innovations.Model.Services.Interfaces;
using System.Threading.Tasks;

namespace Innovations.Services
{
    public class InfractionService : IInfractionService
    {
        private readonly InnovationsDataContext _context;
        public InfractionService(InnovationsDataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Infraction>> GetInfraction()
        {
            var infractions = await _context.Infractions.ToArrayAsync();

            var response = infractions.Select(i => new Infraction
            {
                id = i.id,
                date = DateTime.Now,
                description = i.description,
                dni = i.dni,
                enrollment = i.enrollment,
                points = i.points                
            });

            return response;
        }

        public async Task<Infraction> GetInfractionSelect(int id)
        {
            var infraction = await _context.Infractions.FirstOrDefaultAsync(i => i.id == id);

            return infraction;
        }

        public async Task<object> InsertInfraction(Infraction infraction)
        {
            var infractionId = _context.Infractions.MaxAsync();
            var newId = infractionId.Id + 1;
            Infraction newInfraction = new Infraction
            {
                id = newId,
                date = DateTime.Now,
                description = infraction.description,
                dni = infraction.dni,
                enrollment = infraction.enrollment,
                points = infraction.points
            };
            try
            {
                await _context.Infractions.AddAsync(newInfraction);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new { code = 500, message = ex.Message };
            }

            var response = newInfraction;

            return response;
        }
    }
}
