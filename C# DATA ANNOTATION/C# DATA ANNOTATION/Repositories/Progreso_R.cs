using Microsoft.EntityFrameworkCore;
using RAIZA.Data;
using RAIZA.Interfaces;
using RAIZA.Models;
using RAIZA.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RAIZA.Repositories
{
    public class Progreso_R : Progreso_I
    {
        private readonly DatabaseService _context;

        public Progreso_R(DatabaseService context)
        {
            this._context = context;
        }

        public async Task<List<Progreso>> GetProgresos() =>
            await _context.Progreso.ToListAsync();

        public async Task<Progreso> GetProgresoById(int id) =>
            await _context.Progreso.FirstOrDefaultAsync(p => p.idprogreso == id);

        public async Task<bool> CreateProgreso(Progreso progreso)
        {
            await _context.Progreso.AddAsync(progreso);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateProgreso(Progreso progreso)
        {
            _context.Progreso.Update(progreso);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteProgreso(int id)
        {
            var progreso = await _context.Progreso.FirstOrDefaultAsync(p => p.idprogreso == id);
            if (progreso == null) return false;

            _context.Progreso.Remove(progreso);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}