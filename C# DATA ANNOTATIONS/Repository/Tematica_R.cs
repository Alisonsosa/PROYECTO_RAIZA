using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RAIZA.Interfaces;
using RAIZA.Models;
using RAIZA.Data;

namespace RAIZA.Repositories
{
    public class Tematica_R : ITematicaI
    {
        private readonly DatabaseService _context;

        public Tematica_R(DatabaseService context)
        {
            _context = context;
        }

        public async Task<List<Tematica>> GetTematicas() =>
            await _context.Tematica.ToListAsync();

        public async Task<Tematica?> GetTematicaById(int id) =>
            await _context.Tematica.FirstOrDefaultAsync(t => t.idtematica == id);

        public async Task<bool> CreateTematica(Tematica tematica)
        {
            await _context.Tematica.AddAsync(tematica);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateTematica(Tematica tematica)
        {
            _context.Tematica.Update(tematica);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteTematica(int id)
        {
            var tematica = await _context.Tematica.FirstOrDefaultAsync(t => t.idtematica == id);
            if (tematica == null) return false;

            _context.Tematica.Remove(tematica);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}